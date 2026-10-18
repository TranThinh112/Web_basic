# 📘 Business Logic Layer (BLL)

Lớp **Business Logic Layer (BLL)** đóng vai trò trung gian "bộ não" của ứng dụng, xử lý mọi logic nghiệp vụ trước khi dữ liệu được lưu xuống database hoặc hiển thị lên UI.

## 🎯 Nhiệm vụ chính

1.  **Validation (Kiểm tra dữ liệu)**:
    *   Đảm bảo dữ liệu đầu vào từ UI hợp lệ (ví dụ: ngày hẹn phải ở tương lai, số lượng thuốc > 0).
    *   Ném ra các `Exception` rõ ràng nếu dữ liệu không hợp lệ.

2.  **Logic Nghiệp vụ**:
    *   Tính toán (ví dụ: Tính tổng tiền hóa đơn, tính doanh thu).
    *   Điều phối quy trình (ví dụ: Khi tạo hóa đơn thuốc -> trừ kho thuốc trong `MedicineSaleBLL`).

3.  **Mapping/Transformation**:
    *   Lấy dữ liệu từ DAL (dạng Entity/DTO) và có thể sơ chế thêm trước khi trả về UI.

## 🏗 Cấu trúc Code

Mỗi thực thể (Entity) thường có một BLL class tương ứng. Ví dụ: `AppointmentDTO` được quản lý bởi `AppointmentBLL`.

### Ví dụ: `AppointmentBLL.cs`

```csharp
public class AppointmentBLL
{
    private readonly AppointmentDAL _appointmentDAL;

    // Validate và tạo lịch hẹn
    public int CreateAppointment(AppointmentDTO appt)
    {
        // 1. Validate
        if (appt.AppointmentDate < DateTime.Now)
            throw new Exception("Ngày hẹn không hợp lệ");

        // 2. Business Check: Kiểm tra trùng lịch bác sĩ
        if (CheckConflict(appt.DoctorId, appt.AppointmentDate))
            throw new Exception("Bác sĩ đã kín lịch vào giờ này");

        // 3. Call DAL
        return _appointmentDAL.Insert(appt);
    }
}
```

## 📋 Danh sách các BLL Classes

*   `AppointmentBLL`: Quản lý đặt lịch, hủy lịch, check trùng lịch.
*   `PatientBLL`: Quản lý thông tin hồ sơ bệnh nhân check trùng CMD.
*   `UserBLL`: Đăng nhập, phân quyền, đổi mật khẩu.
*   `MedicineBLL`: Quản lý danh mục thuốc, cảnh báo hết hàng.
*   `MedicineSaleBLL`: Xử lý bán thuốc, trừ tồn kho, thống kê đơn thuốc.
*   `InvoiceBLL`: Quản lý hóa đơn khám bệnh, tính toán tổng tiền.
*   `ClinicSettingsBLL`: Quản lý cấu hình phòng khám (giờ mở cửa, phí khám).

## ⚠️ Lưu ý cho Dev

*   **Không gọi UI Controls trong BLL**. BLL phải độc lập hoàn toàn với Giao diện.
*   **Dependency Injection**: Hiện tại đang khởi tạo trực tiếp DAL (`new AppointmentDAL()`). Trong tương lai nên refactor sang Constructor Injection để dễ Unit Test.
*   **Exception Handling**: Ném Exception để UI bắt (try-catch) và hiển thị messagebox, không được "nuốt" lỗi âm thầm.
