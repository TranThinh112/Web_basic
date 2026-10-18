# 📕 Data Access Layer (DAL)

Lớp **Data Access Layer (DAL)** chịu trách nhiệm giao tiếp trực tiếp với Cơ sở dữ liệu (SQL Server). Đây là nơi duy nhất chứa các câu lệnh truy vấn dữ liệu (LINQ, SQL).

## 🎯 Nhiệm vụ chính

1.  **CRUD**: Create, Read, Update, Delete dữ liệu.
2.  **Kết nối Database**: Quản lý `DbContext` (Entity Framework).
3.  **Query Optimization**: Viết các query tối ưu (ví dụ: dùng `Join` thay vì N+1 query).

## 🏗 Cấu trúc Code

Sử dụng **Entity Framework 6** làm ORM chính.
Mỗi DAL class thường implement các phương thức chuẩn: `GetById`, `GetAll`, `Insert`, `Update`, `Delete`.

### Ví dụ: `AppointmentDAL.cs`

```csharp
public class AppointmentDAL
{
    // Lấy dữ liệu sử dụng EF
    public List<AppointmentDTO> GetAll()
    {
        using (var db = new ClinicDbContext()) // Luôn dispose DbContext
        {
            // Sử dụng LINQ để truy vấn
            return db.Appointments
                     .Where(a => a.IsActive)
                     .ToList()
                     .Select(MapToDTO) // Map Entity sang DTO
                     .ToList();
        }
    }
}
```

## 📋 Danh sách các DAL Classes

*   `ClinicDbContext`: Lớp Context chính quản lý các `DbSet`.
*   `AppointmentDAL`
*   `PatientDAL`
*   `UserDAL`
*   `MedicineDAL`
*   `InvoiceDAL`
*   `EmployeeDAL`
*   ... (và các DAL khác tương ứng với các bảng trong DB).

## ⚙️ Kỹ thuật quan trọng

*   **Using Block**: Luôn bọc `DbContext` trong `using (...)` để đảm bảo kêt nối được đóng ngay sau khi query xong.
*   **DTO Mapping**: DAL trả về `DTO` (Data Transfer Object), không trả về Entity của EF để tránh lỗi `Circular Reference` hoặc `Lazy Loading` khi lên UI.
*   **LINQ**: Sử dụng LINQ to Entities để viết query an toàn và dễ đọc.

## ⚠️ Lưu ý cho Dev

*   Tuyệt đối **không viết logic nghiệp vụ (Validate)** tại đây.
*   Hạn chế sửa trực tiếp file `ClinicDbContext` nếu đang dùng Database First (sẽ bị ghi đè khi update model). Nếu dùng Code First, hãy dùng Migrations.
