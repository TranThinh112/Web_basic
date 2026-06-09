using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_buoi_7.Data;
using Web_buoi_7.Models;

namespace Web_buoi_7.Controllers
{
    [Authorize(Roles = "Student")]
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EnrollmentController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Đăng ký học phần
        public async Task<IActionResult> Register(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);

            var existed = await _context.Enrollments
                .AnyAsync(e => e.UserId == user.Id &&
                               e.CourseId == courseId);

            if (!existed)
            {
                var enrollment = new Enrollment
                {
                    UserId = user.Id,
                    CourseId = courseId,
                    EnrollDate = DateTime.Now
                };

                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MyCourses));
        }

        // Danh sách học phần đã đăng ký
        public async Task<IActionResult> MyCourses()
        {
            var user = await _userManager.GetUserAsync(User);

            var enrollments = await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.UserId == user.Id)
                .ToListAsync();

            return View(enrollments);
        }

        // Hủy đăng ký học phần
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _context.Enrollments
                .FindAsync(id);

            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MyCourses));
        }
    }
}