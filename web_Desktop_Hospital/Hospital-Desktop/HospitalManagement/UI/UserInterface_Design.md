# 📗 User Interface (UI)

Lớp **User Interface (UI)** là bộ mặt của ứng dụng, nơi người dùng tương tác. Dự án sử dụng **Windows Forms** kết hợp với thư viện **DevExpress** để mang lại trải nghiệm người dùng hiện đại, đẹp mắt và chuyên nghiệp.

## 🎯 Đặc điểm thiết kế

1.  **Modern UI**: Sử dụng DevExpress Skins (WXI, Office 2019) và các control hiện đại (TileControl, AccordionControl, FluentDesignForm).
2.  **Consistency**: Các form đều tuân theo layout chuẩn với Header trắng và Action Bar ở dưới cùng.
3.  **Role-Based Access**: Giao diện tự động thích ứng theo vai trò người dùng (Admin, Doctor, etc.).

## 🏗 Cấu trúc Thư mục

*   `MainDashboard.cs`: Màn hình chính, chứa Menu (Accordion) và Dashboard Tiles.
*   `Login.cs`: Form đăng nhập.
*   `BaseForm` (nếu có): Form cha chứa logic chung.
*   **Management Forms**: Các form danh sách/quản lý (CRUD).
    *   `PatientForm.cs`, `UserForm.cs`, `MedicineForm.cs`, ...
    *   Đặc điểm: Chứa GridView hiển thị dữ liệu, thanh tìm kiếm, nút Thêm/Sửa/Xóa.
*   **Detail Forms**: Các form nhập liệu chi tiết (Dialog).
    *   `PatientDetailForm.cs`, `AppointmentDetailForm.cs`...
    *   Đặc điểm: Các ô nhập liệu (TextEdit, ComboBoxEdit), nút Lưu/Hủy (nằm ở Action Bar).

## 🛠 Công nghệ & Kỹ thuật

*   **DevExpress GridControl**: Dùng để hiển thị dữ liệu dạng bảng. Hỗ trợ sort, filter, group mạnh mẽ.
*   **UIHelper**: Một class tiện ích (`Utils/UIHelper.cs`) giúp chuẩn hóa giao diện (màu sắc, font chữ, tạo button động).
    *   `UIHelper.PrimaryColor`: Màu chủ đạo.
    *   `UIHelper.CreateActionPanel()`: Tạo thanh hành động dưới đáy form.
*   **Auto-Refresh**: Dashboard sử dụng `Timer` để tự động cập nhật dữ liệu (real-time feeling).

## 🧩 Cách thêm một Form mới

1.  **Tạo Form**: Add New Windows Form.
2.  **Kế thừa/Setup**:
    *   Đặt `StartPosition = CenterScreen`.
    *   Xóa Border (nếu muốn custom header).
3.  **Layout**:
    *   Thêm `Panel` Header (Top).
    *   Thêm `FlowLayoutPanel` Action Bar (Bottom).
    *   Thêm Content ở giữa.
4.  **Wiring**:
    *   Trong Constructor, gọi `UIHelper.StyleButton()` cho các nút.
    *   Gọi BLL để lấy dữ liệu.

## ⚠️ Lưu ý cho Dev

*   **Không viết query SQL** trực tiếp trong UI. Hãy gọi BLL.
*   Xử lý **Try-Catch** khi gọi BLL để hiển thị thông báo lỗi thân thiện cho user.
*   Luôn dispose các resource nặng nếu có (mặc dù WinForms tự quản lý khá tốt).
