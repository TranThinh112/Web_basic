using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using BunnyChat.Service;
using BunnyChat.DTOs;
using BunnyChat.Models;
using BunnyChat.Helper;
using BCrypt.Net;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BunnyChat.Controllers

{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<Session> _sessionCollection;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;


        public AuthController(MongoDbService mongoDbService, ITokenService tokenService, IConfiguration configuration)
        {
            _usersCollection = mongoDbService.Database.GetCollection<User>("users");
            _sessionCollection = mongoDbService.Database.GetCollection<Session>("sessions");
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(SignUpDTORequest request)
        {
            try
            {
                //kiểm tra có thiếu dữ liệu ko
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.PassWord) || string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName) || string.IsNullOrWhiteSpace(request.UserName))
                {
                    return BadRequest(ApiResponse.Fail(
                        message: "Không thể thiếu Eamil, Password, Firstname, Lastname và Username"
                    ));
                }

                //kiểm tra username
                var duplicateUser = await _usersCollection.Find(u => u.Username == request.UserName || u.Email == request.Email).FirstOrDefaultAsync();

                if (duplicateUser != null)
                {

                    return StatusCode(409, ApiResponse.Fail(
                      message: "Username hoặc Email đã tồn tại"
                    ));
                }

                //mã hóa pâssword
                var HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.PassWord, 10);

                var displayName = $"{request.FirstName} {request.LastName}";
                //data gửi lên server
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Username = request.UserName,
                    Email = request.Email,
                    SearchName = StringHelper
                        .RemoveVietnameseDiacritics(displayName) //Bỏ dấu tiếng Việt
                        .ToLower() //chuyển thành chữ thường
                        .Trim(), //xóa khoảng trắng đầu và cuối
                    HashPassword = HashedPassword,
                    CreatedAt = DateTime.UtcNow,
                };

                //Insert user mới vào bảng
                await _usersCollection.InsertOneAsync(user);

                //trả về Response
                return Ok(ApiResponse.Success(
                    message: "Tạo tài khoản thành công",
                    new
                    {
                        id = user.Id,
                        username = user.Username,
                        displayname = user.DisplayName
                    }
                ));
            }

            //trả về lỗi
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi Signup");

                return StatusCode(500, ApiResponse.Fail(
                    message: $"{ex.Message}"
               ));
            }
        }

        //Api Login
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTORequest request)
        {
            try
            {
                //Lấy input & kiểm tra có bị thiếu field ko 
                if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.PassWord))
                {
                    return BadRequest(ApiResponse.Fail(
                        message: "Thiếu Usernanme hoặc Password"
                    ));
                }

                //Kiểm tra username có tồn tại hay ko 
                var user = await _usersCollection.Find(u => u.Username == request.UserName).FirstOrDefaultAsync();
                if (user == null)
                {
                    return Unauthorized(ApiResponse.Fail(
                        message: "Username hoặc Password không chính xác"
                    ));
                }

                //kiểm tra password có đúng hjay ko 
                var passwordCorrect = BCrypt.Net.BCrypt.Verify(request.PassWord, user.HashPassword);
                if (!passwordCorrect)
                {
                    return Unauthorized(ApiResponse.Fail(
                        message: "Username hoặc Password không chính xác"
                    ));
                }

                //nếu khớp, tạo accesToken với JWT
                var accessToken = _tokenService.CreateAccessToken(user);

                //tạo refreshToken
                var refreshToken = _tokenService.CreateRefreshToken();

                //thời gian hết hạn của Refresh Token
                var refreshExpiry = DateTime.UtcNow.AddDays(
                             Convert.ToDouble(_configuration["JwtSettings:RefreshTokenExpirationDays"])
                            );

                //import vao collectiuon sessiuon, nếu đã có trong collection, update RefreshToken

                    //tìm userID trong bảng session, nếu có thì delete
                await _sessionCollection.DeleteOneAsync(s => s.UserId == user.Id);


                await _sessionCollection.InsertOneAsync(new Session
                {
                    UserId = user.Id!,
                    Username = user.Username!,
                    RefreshToken = refreshToken,
                    ExpiresAt = refreshExpiry
                });

                // menthod Gắn refreshToken vào cookie
                Response.Cookies.Append(
                    "refreshToken",
                    refreshToken,
                    new CookieOptions
                    {
                        //Chống XSS, Chặn JavaScript đọc cookie.
                        HttpOnly = true,

                        //Gwuir cookie qua HTTPS, ko gửi qua HTTP
                        Secure = true,

                        // Chống CSRF
                        SameSite = SameSiteMode.Strict,

                        //  hết hạn sau 7 ngày.
                        Expires = refreshExpiry
                    }
                );

                //trả về accesToken
                return Ok(ApiResponse.Success(
                    message: $"{request.UserName} đăng nhập thành công",
                    new
                    {
                        accessToken,
                    }
                ));
            }
            // trả về lỗi
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gọi Login");

                return StatusCode(500, ApiResponse.Fail(
                    message: $"{ex.Message}"
                ));
            }
        }

        //Api SignOut
        [HttpPost("signout")]
        [HttpPost("logout")]
        public async Task<ActionResult> LogOut()
        {
            try
            {
                // lấy refresh token từ cookie
                var token = Request.Cookies["refreshToken"];

                //Check token tồn tại trong session
                if (!string.IsNullOrEmpty(token))
                {
                    //xóa refresh token trong sesion
                    await _sessionCollection.DeleteManyAsync(
                       s => s.RefreshToken == token
                    );
                }
                // xóa refreshtoken trong cookie
                Response.Cookies.Delete("refreshToken");

                //Trả kết quả
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gọi Signout");
                return StatusCode(500, ApiResponse.Fail(
                    message: $"{ex.Message}"
                ));
            }
        }

//tạo AccesToken mới từ RefreshToken
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                //lấy refreshToken từ cookie
                var refreshToken = Request.Cookies["refreshToken"];
                    //kiểm tra refreshToken có tồn tại ko
                if (string.IsNullOrWhiteSpace(refreshToken))
                {
                    return NotFound(ApiResponse.Fail(
                        message: "Không tìm thấy refreshToken"
                    ));
                }

                //so sánh với refreshToken trong dtb
                var session = await _sessionCollection
                .Find(x => x.RefreshToken == refreshToken)
                .FirstOrDefaultAsync();
                if (session == null)
                {
                    return Unauthorized(ApiResponse.Fail(
                        message: "Token không hợp lệ hoặc hết hạn"
                    ));
                }

                //kiểm tra hết hạn chưa 
                if(session.ExpiresAt < DateTime.UtcNow)
                {
                    return Unauthorized(ApiResponse.Fail(
                        message: "Token đã hết hạn"
                    ));
                }
                
                //tạo accesToken mới
                    //tìm userId ứng với RefreshToken trong session
                var user = await _usersCollection
                    .Find(x => x.Id == session.UserId)
                    .FirstOrDefaultAsync();


                var newAccesToken = _tokenService.CreateAccessToken(user);

                //trả về accessToken mới
                return Ok(ApiResponse.Success(
                    message: "Gia hạn token thành công",
                    new
                    {
                        accessToken = newAccesToken
                    }
                ));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gọi RefreshToken");
                return StatusCode(500, ApiResponse.Fail(
                    message: $"{ex.Message}; Lỗi hệ thống."
                ));
            }
        }

        // B1: Xác thực tài khoản bằng Username / Email / Số điện thoại
        [HttpPost("forgot/verify")]
        public async Task<IActionResult> VerifyForgotAccount([FromBody] VerifyForgotDTORequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request?.Identifier))
                {
                    return BadRequest(ApiResponse.Fail("Vui lòng nhập Username, Email hoặc Số điện thoại"));
                }

                var key = request.Identifier.Trim();

                // Tìm user theo username, email hoặc phone
                var user = await _usersCollection.Find(u =>
                    u.Username == key ||
                    u.Email == key ||
                    u.Phone == key
                ).FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound(ApiResponse.Fail("Không tìm thấy tài khoản phù hợp với thông tin đã nhập"));
                }

                // Trả về thông tin cơ bản để client xác nhận
                return Ok(ApiResponse.Success("Tìm thấy tài khoản", new
                {
                    username = user.Username,
                    displayName = user.DisplayName,
                    email = string.IsNullOrEmpty(user.Email) ? "" : MaskEmail(user.Email)
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail($"Lỗi hệ thống: {ex.Message}"));
            }
        }

        // B2: Đặt lại mật khẩu mới cho tài khoản
        [HttpPost("forgot/reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTORequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request?.Identifier))
                {
                    return BadRequest(ApiResponse.Fail("Thiếu thông tin xác thực tài khoản"));
                }

                if (string.IsNullOrWhiteSpace(request.NewPassword) || request.NewPassword.Length < 6)
                {
                    return BadRequest(ApiResponse.Fail("Mật khẩu mới phải có ít nhất 6 ký tự"));
                }

                var key = request.Identifier.Trim();

                var user = await _usersCollection.Find(u =>
                    u.Username == key ||
                    u.Email == key ||
                    u.Phone == key
                ).FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound(ApiResponse.Fail("Tài khoản không tồn tại"));
                }

                // Hash mật khẩu mới bằng BCrypt
                var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword, 10);

                var update = Builders<User>.Update
                    .Set(u => u.HashPassword, newHashedPassword)
                    .Set(u => u.UpdatedAt, DateTime.UtcNow);

                await _usersCollection.UpdateOneAsync(u => u.Id == user.Id, update);

                // Thu hồi/xóa toàn bộ refresh token sessions cũ của tài khoản này để bảo mật
                await _sessionCollection.DeleteManyAsync(s => s.UserId == user.Id);

                return Ok(ApiResponse.Success("Đặt lại mật khẩu thành công! Bạn có thể đăng nhập ngay bây giờ.", new
                {
                    username = user.Username
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Fail($"Lỗi hệ thống: {ex.Message}"));
            }
        }

        // Helper che giấu bớt ký tự email
        private static string MaskEmail(string email)
        {
            var parts = email.Split('@');
            if (parts.Length != 2) return email;
            var name = parts[0];
            var domain = parts[1];
            if (name.Length <= 2) return $"{name[0]}*@{domain}";
            return $"{name.Substring(0, 2)}***@{domain}";
        }


    }
}
