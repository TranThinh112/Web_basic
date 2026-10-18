# 🗄️ Database

Thư mục này chứa các tài nguyên liên quan đến Cơ sở dữ liệu.

## 📄 Scripts

*   Chứa các file `.sql` để tạo bảng, view, stored procedures (nếu không dùng Code First Migrations).
*   **`SeedData.sql`**: Script thêm dữ liệu mẫu (Admin default, danh mục thuốc mẫu...).

## 🔌 Connection String

Chuỗi kết nối được cấu hình trong file `App.config` (tại thư mục gốc của project), không nằm hard-code trong code C#.

Ví dụ:
```xml
<connectionStrings>
    <add name="ClinicContext" 
         connectionString="Data Source=.;Initial Catalog=HospitalManagement;Integrated Security=True" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

## 🛠 Entity Framework

Dự án sử dụng **Entity Framework 6**.
*   Nếu dùng **Code First**: Các thay đổi DB sẽ được quản lý qua Migrations (Enable-Migrations, Add-Migration, Update-Database).
*   Nếu dùng **Database First**: File `.edmx` (nếu có) sẽ nằm trong thư mục `DAL` hoặc `Database/Models`.

## ⚠️ Lưu ý

*   Luôn backup database trước khi chạy các script update cấu trúc.
*   Tránh commit file chứa password database thực tế lên Source Control (Git).
