# 📙 Data Transfer Objects (DTO)

Lớp **Simple Data Transfer Objects (DTO)** chứa các class C# thuần (POCO - Plain Old CLR Objects). Mục đích chính là đóng gói dữ liệu để truyền tải giữa các lớp (DAL <-> BLL <-> UI) mà không mang theo các ràng buộc của Entity Framework hay logic nghiệp vụ.

## 🎯 Tại sao dùng DTO?

1.  **Decoupling**: Ngắt sự phụ thuộc giữa UI và Database Entity. Nếu DB thay đổi, chỉ cần sửa DAL mapping, UI không bị ảnh hưởng trực tiếp.
2.  **Security**: Ẩn các trường nhạy cảm của bảng DB (nếu cần).
3.  **Serialization**: DTO dễ dàng serialize thành JSON/XML nếu sau này phát triển API.
4.  **Performance**: Chỉ lấy các trường cần thiết.

## 🏗 Cấu trúc

Các class DTO thường chỉ chứa Properties (`get; set;`), không có method logic.

### Ví dụ: `PatientDTO.cs`

```csharp
public class PatientDTO
{
    public int PatientId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public string MedicalHistory { get; set; }
    
    // Có thể thêm property tính toán nhẹ
    public int Age => DateTime.Now.Year - DateOfBirth.Year;
}
```

## 📋 Hướng dẫn sử dụng

*   Khi thêm bảng mới trong DB, hãy tạo một DTO tương ứng với hậu tố `DTO`.
*   Tên Property nên đặt giống tên cột trong DB để dễ mapping (hoặc dùng AutoMapper sau này).
*   Có thể tạo các **Composite DTO** (DTO tổng hợp) để chứa dữ liệu từ nhiều bảng (ví dụ: `AppointmentDetailDTO` chứa thông tin Lịch hẹn + Tên Bác sĩ + Tên Bệnh nhân).

## ⚠️ Lưu ý

*   Không đặt logic tính toán phức tạp vào DTO.
*   Không tham chiếu trực tiếp đến `DbContext` hay thư viện UI trong DTO.
