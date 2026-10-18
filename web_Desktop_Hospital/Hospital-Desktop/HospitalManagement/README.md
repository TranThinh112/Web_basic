# 🏥 Hospital Management System (HMS)

Một hệ thống quản lý bệnh viện toàn diện được xây dựng bằng **C# WinForms** và **Entity Framework**. Hệ thống được thiết kế theo mô hình **3-Tier Architecture** (3 lớp) để đảm bảo tính dễ bảo trì, mở rộng và kiểm thử.

## 🚀 Tính năng chính

*   **Quản lý Bệnh nhân (Patients)**: Thêm, sửa, xóa, tìm kiếm hồ sơ bệnh nhân.
*   **Lịch hẹn (Appointments)**: Đặt lịch, quản lý trạng thái lịch hẹn, kiểm tra trùng lịch.
*   **Khám bệnh (Visits)**: Ghi nhận triệu chứng, chẩn đoán, kê đơn thuốc.
*   **Dược (Pharmacy)**: Quản lý kho thuốc, bán thuốc, phát thuốc theo đơn.
*   **Tài chính (Finance)**: Thu phí khám, thanh toán tiền thuốc, báo cáo doanh thu.
*   **Quản lý Nhân sự (HR)**: Quản lý bác sĩ, nhân viên, phân quyền người dùng (Role-based).
*   **Dashboard Thông minh**: Hiển thị thông tin theo vai trò (Admin, Doctor, Receptionist, Cashier, Pharmacist) với tính năng tự động cập nhật (Auto-refresh).

---

## 🏗 Kiến trúc Hệ thống

Dự án tuân thủ nghiêm ngặt mô hình 3 lớp:

1.  **Presentation Layer (UI)**:
    *   Sử dụng **Windows Forms** với **DevExpress** controls để tạo giao diện hiện đại (Modern UI).
    *   Tương tác với người dùng và gọi xuống BLL.
    *   Nằm trong thư mục `UI/`.

2.  **Business Logic Layer (BLL)**:
    *   Chứa các quy tắc nghiệp vụ (validations, calculations, logical flows).
    *   Không truy cập trực tiếp database, chỉ gọi qua DAL.
    *   Nằm trong thư mục `BLL/`.

3.  **Data Access Layer (DAL)**:
    *   Chịu trách nhiệm tương tác trực tiếp với Database (CRUD operations).
    *   Sử dụng **Entity Framework** (Code First hoặc Database First) hoặc ADO.NET wrapper.
    *   Nằm trong thư mục `DAL/`.

4.  **Data Transfer Objects (DTO)**:
    *   Các class POCO đơn giản dùng để truyền dữ liệu giữa các lớp.
    *   Nằm trong thư mục `DTO/`.

5.  **Utilities (Utils)**:
    *   Các hàm hỗ trợ chung (Session, Helper, Constants).
    *   Nằm trong thư mục `Utils/`.

---

## 🛠 Yêu cầu Kỹ thuật

*   **IDE**: Visual Studio 2019/2022.
*   **Framework**: .NET Framework 4.7.2+ (hoặc .NET Core/5/6 tùy cấu hình).
*   **Database**: SQL Server.
*   **Dependencies**:
    *   DevExpress (các gói WinForms).
    *   Entity Framework 6.

## 🏁 Cài đặt & Chạy ứng dụng

1.  **Cấu hình Database**:
    *   Mở file `App.config`, chỉnh sửa `connectionString` (`ClinicContext`) để trỏ về SQL Server của bạn.
    *   Chạy script SQL trong thư mục `Database/` (nếu có) hoặc để EF tự tạo database (nếu dùng Code First).

2.  **Restore Nuget Packages**:
    *   Chạy lệnh `dotnet restore` hoặc chuột phải Solution -> Restore NuGet Packages.

3.  **Build & Run**:
    *   Build Solution (`Ctrl+Shift+B`).
    *   Chạy project (`F5`).

4.  **Tài khoản Mặc định** (nếu có seed data):
    *   Admin: `admin` / `123456`
    *   Doctor: `doctor` / `123456`
    *   Cashier: `cashier` / `123456`

---

## 📂 Hướng dẫn Chi tiết từng Layer

Vui lòng đọc file tài liệu kỹ thuật trong từng thư mục con để hiểu sâu hơn về kỹ thuật:

*   [📘 Business Logic Layer (BLL)](BLL/BusinessLogic_Guide.md)
*   [📕 Data Access Layer (DAL)](DAL/DataAccess_Guide.md)
*   [📗 User Interface (UI)](UI/UserInterface_Design.md)
*   [📙 Data Transfer Objects (DTO)](DTO/DataTransferObjects_Spec.md)
*   [📓 Utilities (Utils)](Utils/Utilities_Helper.md)
*   [🗄️ Database Setup](Database/Database_Setup.md)
