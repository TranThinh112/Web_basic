-- SeedData.sql
-- Khởi tạo dữ liệu mẫu thiết yếu cho hệ thống
-- Lưu ý: Chạy CleanDB.sql trước khi chạy script này để tránh lỗi trùng lặp

USE [HospitalManagement]
GO

SET NOCOUNT ON;
PRINT '🌱 Starting Seed Data Process...'

-- 1. Users (Tài khoản)
-- Password mặc định: "123456" 
-- HashVersion = 2 (BCrypt)
-- BCrypt hash được tạo với workFactor=12
-- MustChangePassword = 0 cho seed data (admin không cần đổi)

-- Đảm bảo các cột mới tồn tại
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = 'Salt')
BEGIN
   ALTER TABLE [dbo].[Users] ADD [Salt] NVARCHAR(256) NULL;
END
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = 'MustChangePassword')
BEGIN
   ALTER TABLE [dbo].[Users] ADD [MustChangePassword] BIT NOT NULL DEFAULT 0;
END
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = 'HashVersion')
BEGIN
   ALTER TABLE [dbo].[Users] ADD [HashVersion] INT NOT NULL DEFAULT 1;
END


INSERT INTO [dbo].[Users] ([Username], [PasswordHash], [Salt], [Role], [IsActive], [MustChangePassword], [HashVersion]) VALUES 
('admin', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'Admin', 1, 0, 1),
('doctor1', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'Doctor', 1, 0, 1),
('doctor2', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'Doctor', 1, 0, 1),
('nurse1', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'Nurse', 1, 0, 1),
('cashier1', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'Cashier', 1, 0, 1),
('pharma1', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'Pharmacist', 1, 0, 1),
('recep1', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', NULL, 'Receptionist', 1, 0, 1);

PRINT '✅ Users Seeded (HashVersion=1, will auto-migrate to BCrypt on login)'

-- 2. Clinic Settings (Cấu hình)
INSERT INTO [dbo].[ClinicSettings] ([ClinicName], [Address], [Phone], [ExaminationFee], [OpeningTime], [ClosingTime]) VALUES 
(N'Phòng Khám Đa Khoa Suzu', N'123 Đường Số 1, Quận 1, TP.HCM', '0901234567', 150000, '07:30', '17:00');

PRINT '✅ Clinic Settings Seeded'

-- 3. Doctors (Bác sĩ) - Liên kết với User
-- Lấy UserId tương ứng từ bảng Users
DECLARE @DocUserId1 INT = (SELECT UserId FROM Users WHERE Username = 'doctor1');
DECLARE @DocUserId2 INT = (SELECT UserId FROM Users WHERE Username = 'doctor2');

INSERT INTO [dbo].[Doctors] ([FullName], [Specialty], [UserId]) VALUES 
(N'Nguyễn Văn A', N'Nội khoa', @DocUserId1),
(N'Trần Thị B', N'Nhi khoa', @DocUserId2);

PRINT '✅ Doctors Seeded'

-- 4. Employees (Nhân viên) - Liên kết với User
DECLARE @AdminId INT = (SELECT UserId FROM Users WHERE Username = 'admin');
DECLARE @CashierId INT = (SELECT UserId FROM Users WHERE Username = 'cashier1');
DECLARE @PharmaId INT = (SELECT UserId FROM Users WHERE Username = 'pharma1');
DECLARE @RecepId INT = (SELECT UserId FROM Users WHERE Username = 'recep1');

INSERT INTO [dbo].[Employees] ([FullName], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Position], [Department], [HireDate], [UserId]) VALUES 
(N'Quản Trị Viên', '1990-01-01', N'Nam', '0909000000', 'admin@suzu.com', N'TP.HCM', N'Quản lý', N'Ban Giám Đốc', GETDATE(), @AdminId),
(N'Lê Thu Ngân', '1995-05-05', N'Nữ', '0909000001', 'cashier@suzu.com', N'TP.HCM', N'Thu ngân', N'Tài chính', GETDATE(), @CashierId),
(N'Phạm Dược Sĩ', '1994-04-04', N'Nam', '0909000002', 'pharma@suzu.com', N'TP.HCM', N'Dược sĩ', N'Kho Dược', GETDATE(), @PharmaId),
(N'Hoàng Lễ Tân', '1998-08-08', N'Nữ', '0909000003', 'welcome@suzu.com', N'TP.HCM', N'Lễ tân', N'Sảnh Chờ', GETDATE(), @RecepId);

PRINT '✅ Employees Seeded'

-- 5. Medicines (Thuốc)
INSERT INTO [dbo].[Medicines] ([MedicineName], [Unit], [Price], [StockQuantity], [ExpiryDate], [Description]) VALUES 
(N'Paracetamol 500mg', N'Viên', 1000, 1000, DATEADD(YEAR, 2, GETDATE()), N'Thuốc giảm đau, hạ sốt'),
(N'Amoxicillin 500mg', N'Viên', 2500, 500, DATEADD(YEAR, 2, GETDATE()), N'Kháng sinh nhóm Penicillin'),
(N'Vitamin C 500mg', N'Vỉ', 15000, 200, DATEADD(YEAR, 1, GETDATE()), N'Tăng cường sức đề kháng'),
(N'Loratadin 10mg', N'Viên', 1200, 800, DATEADD(YEAR, 2, GETDATE()), N'Thuốc chống dị ứng'),
(N'Omeprazol 20mg', N'Viên', 3000, 600, DATEADD(YEAR, 2, GETDATE()), N'Thuốc điều trị dạ dày');

PRINT '✅ Medicines Seeded'

-- 6. Diseases (Danh mục bệnh)
INSERT INTO [dbo].[Diseases] ([DiseaseName], [Category], [Description]) VALUES 
(N'Cảm cúm', N'Common', N'Bệnh truyền nhiễm do virus cúm. Triệu chứng: Sốt, ho, đau họng, chảy nước mũi'),
(N'Viêm họng cấp', N'Common', N'Viêm niêm mạc họng. Triệu chứng: Đau họng, rát cổ, khó nuốt'),
(N'Rối loạn tiêu hóa', N'Common', N'Hệ tiêu hóa hoạt động bất thường. Triệu chứng: Đau bụng, buồn nôn, tiêu chảy'),
(N'Dị ứng thời tiết', N'Seasonal', N'Phản ứng quá mẫn của cơ thể. Triệu chứng: Hắt hơi, ngứa mũi, nổi mẩn đỏ');

PRINT '✅ Diseases Seeded'

-- 7. Patients (Bệnh nhân mẫu)
INSERT INTO [dbo].[Patients] ([PatientCode], [FullName], [DateOfBirth], [Gender], [Phone], [Address], [CCCD], [MedicalHistory]) VALUES 
(N'BN001', N'Nguyễn Văn Bệnh Nhân', '1985-12-12', N'Nam', '0901111222', N'Quận 3, TP.HCM', '079085012345', N'Không có tiền sử bệnh lý đặc biệt'),
(N'BN002', N'Trần Thị Khách', '2000-10-10', N'Nữ', '0903333444', N'Quận 5, TP.HCM', '079000054321', N'Dị ứng hải sản');

PRINT '✅ Patients Seeded'

-- 8. Appointments (Lịch hẹn mẫu - Hôm nay)
DECLARE @PatId1 INT = (SELECT TOP 1 PatientId FROM Patients WHERE FullName LIKE N'%Bệnh Nhân%');
DECLARE @DocId1 INT = (SELECT TOP 1 DoctorId FROM Doctors WHERE FullName LIKE N'%Văn A%');

-- Lịch hẹn Pending hôm nay
INSERT INTO [dbo].[Appointments] ([PatientId], [DoctorId], [AppointmentDate], [Reason], [Status]) VALUES 
(@PatId1, @DocId1, DATEADD(HOUR, 2, GETDATE()), N'Đau đầu, chóng mặt', 'Pending');

PRINT '✅ Appointments Seeded'

PRINT '🎉 Seed Data Completed Successfully!'
GO


SET NOCOUNT ON;
PRINT N'🔧 Bắt đầu cập nhật CHECK constraint cho bảng Visits...'
-- 1. Tìm và xóa CHECK constraint cũ
DECLARE @constraintName NVARCHAR(128)
SELECT @constraintName = c.name 
FROM sys.check_constraints c
INNER JOIN sys.columns col ON c.parent_column_id = col.column_id AND c.parent_object_id = col.object_id
WHERE c.parent_object_id = OBJECT_ID('dbo.Visits') 
  AND col.name = 'Status'
IF @constraintName IS NOT NULL
BEGIN
    PRINT N'  → Xóa constraint cũ: ' + @constraintName
    EXEC('ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [' + @constraintName + ']')
    PRINT N'  ✓ Đã xóa constraint cũ'
END
ELSE
BEGIN
    PRINT N'  ⚠ Không tìm thấy CHECK constraint cho cột Status'
END
-- 2. Thêm CHECK constraint mới với PendingPayment
PRINT N'  → Thêm CHECK constraint mới...'
ALTER TABLE [dbo].[Visits] 
ADD CONSTRAINT CK_Visits_Status 
CHECK ([Status] IN ('PendingPayment', 'Waiting', 'InProgress', 'Completed'))
PRINT N'  ✓ Đã thêm CHECK constraint mới'