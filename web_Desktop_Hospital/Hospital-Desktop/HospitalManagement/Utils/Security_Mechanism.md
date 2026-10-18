# Cơ chế Bảo mật Mật khẩu (Password Security Mechanism)

Tài liệu này mô tả chi tiết cơ chế bảo mật mật khẩu được áp dụng trong dự án Hospital Management, tập trung vào kỹ thuật "Salting" và quy trình chuyển đổi dữ liệu (Migration).

## 1. Tổng quan vấn đề
Khi lưu trữ mật khẩu người dùng, việc lưu trữ dạng văn bản thuần (plaintext) là tối kỵ. Phương pháp tiêu chuẩn là lưu mã băm (Hash). Tuy nhiên, nếu chỉ sử dụng Hash đơn thuần (ví dụ `SHA256(password)`), hệ thống sẽ gặp phải các rủi ro:
*   **Rainbow Table Attack:** Hacker sử dụng bảng tra cứu các mã băm đã tính sẵn của hàng triệu mật khẩu phổ biến để dò ngược lại password.
*   **Trùng lặp Hash:** Hai người dùng có cùng mật khẩu sẽ có hash giống hệt nhau, giúp hacker dễ dàng nhận diện và tấn công hàng loạt.

## 2. Giải pháp: Password Salting

### Nguyên lý hoạt động
Chúng tôi áp dụng kỹ thuật **Salting** bằng cách thêm một chuỗi ngẫu nhiên (Salt) vào mật khẩu trước khi băm.

```math
Hash = SHA256(Password + Salt)
```

*   **Salt:** Một chuỗi ngẫu nhiên duy nhất (32-byte Base64), được sinh ra riêng biệt cho **từng người dùng**.
*   **Lưu trữ:** Cả `Salt` và `Hash` đều được lưu trong cơ sở dữ liệu (`UserDTO`).

### Tại sao an toàn hơn?
Cơ chế này dựa trên nguyên lý đánh đổi **Không gian - Thời gian (Space-Time Tradeoff)**:
1.  **Vô hiệu hóa Rainbow Table:** Vì mỗi người dùng có một Salt riêng, bản `Hash` cuối cùng là duy nhất ngay cả khi hai người dùng chọn cùng mật khẩu `123456`. Bảng tính sẵn của hacker trở nên vô dụng.
2.  **Tăng chi phí tấn công:** Để phá khóa, hacker buộc phải thực hiện tấn công vét cạn (Brute-force) cho **từng tài khoản riêng biệt**, thay vì tấn công một lần cho toàn bộ database.

## 3. Quy trình Auto-Migration (Tự động chuyển đổi)

Để đảm bảo trải nghiệm người dùng không bị gián đoạn (không yêu cầu người dùng cũ phải reset mật khẩu), chúng tôi áp dụng cơ chế **Lazy Migration**:

### Luồng xử lý khi đăng nhập (`UserBLL.Login`):

1.  **Kiểm tra Salt:** Hệ thống kiểm tra xem tài khoản có `Salt` trong database hay không.
2.  **Trường hợp tài khoản CŨ (Chưa có Salt):**
    *   Hệ thống thử xác thực bằng mật khẩu của người dùng với cơ chế cũ (SHA256 không Salt).
    *   Nếu đúng:
        *   Sinh `Salt` mới ngẫu nhiên.
        *   Tính toán lại `Hash` mới (`Pass + Salt`).
        *   Cập nhật `Salt` và `Hash` vào database ngay lập tức.
        *   Cho phép đăng nhập.
3.  **Trường hợp tài khoản MỚI (Đã có Salt):**
    *   Hệ thống lấy `Salt` từ database.
    *   Tính toán `Hash` từ mật khẩu nhập vào (`Pass + Salt`).
    *   So sánh với `Hash` trong database.

### Lợi ích
*   **Không gián đoạn:** Người dùng cũ đăng nhập bình thường.
*   **Bảo mật tăng dần:** Hệ thống tự động và âm thầm nâng cấp bảo mật cho từng tài khoản ngay khi họ quay lại sử dụng.

## 4. Các biện pháp bổ sung

*   **Chống dò tài khoản (Anti-Enumeration):** Hệ thống luôn trả về thông báo lỗi chung *"Tên đăng nhập hoặc mật khẩu không chính xác"* cho mọi trường hợp sai (sai username, sai password, tài khoản bị khóa), ngăn chặn hacker xác định tài khoản nào tồn tại trong hệ thống.
*   **Logging:** Mọi hành động đăng nhập thành công, thất bại, hoặc nâng cấp tài khoản đều được ghi log để kiểm toán (`Utils/Logger.cs`).
