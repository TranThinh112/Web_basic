# 📓 Utilities (Utils)

Thư mục **Utils** chứa các class, method tiện ích, hằng số dùng chung cho toàn bộ dự án. Đây là nơi chứa các đoạn code "helper" giúp giảm sự lặp lại (DRY - Don't Repeat Yourself).

## 🛠 Các thành phần chính

### 1. `SessionManager.cs`
Quản lý phiên làm việc của người dùng hiện tại.
*   `CurrentUser`: Lưu thông tin User đang login.
*   `CurrentRole`: Trả về vai trò (Admin, Doctor...).
*   `Login(user)`: Thiết lập session.
*   `Logout()`: Xóa session.
*   `IsAdmin()`, `IsDoctor()`: Helper check quyền nhanh.

### 2. `UIHelper.cs`
Chuẩn hóa giao diện và giảm code lặp lại cho tầng UI.
*   **Colors**: Định nghĩa bộ màu chuẩn (`PrimaryColor`, `SuccessColor`, `WarningColor`...).
*   **Fonts**: Định nghĩa font tiêu đề, font nội dung chuẩn.
*   **Methods**:
    *   `CreateActionPanel()`: Tạo panel chứa nút bấm (Save/Cancel) chuẩn.
    *   `StyleButton(SimpleButton btn)`: Áp dụng style (màu, font, hover effect) cho nút.
    *   `ShowError()`, `ShowInfo()`: Hiển thị MessageBox chuẩn.

### 3. `ValidationHelper.cs` (Nếu có)
Chứa các hàm validate dữ liệu chung.
*   `IsValidEmail(string email)`
*   `IsValidPhone(string phone)`
*   `IsNumber(string str)`

### 4. `SecurityHelper.cs` (Khuyến nghị)
*   Hash password (MD5, SHA256, BCrypt).
*   Mã hóa dữ liệu nhạy cảm.

## 📋 Hướng dẫn đóng góp

*   Nếu bạn thấy một đoạn code được copy-paste sử dụng ở 2 nơi trở lên -> Hãy cân nhắc tách nó ra thành Helper và đặt vào `Utils`.
*   Đặt tên class Helper rõ ràng theo chức năng (`StringHelper`, `DateHelper`, `FileHelper`...).
*   Các method trong Utils thường nên là `public static` (Stateless).
