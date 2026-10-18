-- CleanDB.sql
-- Xóa toàn bộ dữ liệu trong Database để reset
-- Thứ tự xóa: Bảng con (Detail) -> Bảng cha (Master) -> Bảng danh mục -> Bảng hệ thống

USE [HospitalManagement]
GO

SET NOCOUNT ON;

PRINT '🔥 Starting Clean Database Process...'

-- 1. Xóa chi tiết giao dịch (Transation Details)
DELETE FROM [dbo].[MedicineSaleDetails];
DELETE FROM [dbo].[PrescriptionDetails];
PRINT '✅ Transaction Details Deleted'

-- 2. Xóa giao dịch chính (Transactions)
DELETE FROM [dbo].[Invoices];
DELETE FROM [dbo].[MedicineSales];
DELETE FROM [dbo].[Prescriptions];
PRINT '✅ Transactions Deleted'

-- 3. Xóa hoạt động khám chữa bệnh (Medical Activities)
DELETE FROM [dbo].[Appointments];
DELETE FROM [dbo].[Visits];
PRINT '✅ Medical Activities Deleted'

-- 4. Xóa nhân sự (HR) - Cần xóa trước khi xóa Users
-- Lưu ý: Employee và Doctor có thể liên kết với User
DELETE FROM [dbo].[Doctors];
DELETE FROM [dbo].[Employees];
PRINT '✅ HR Data Deleted'

-- 5. Xóa danh mục (Catalogs)
DELETE FROM [dbo].[Patients]; -- Bệnh nhân được tham chiếu bởi Visit, Appointment...
DELETE FROM [dbo].[Medicines];
DELETE FROM [dbo].[Diseases];
PRINT '✅ Catalogs Deleted'

-- 6. Xóa dữ liệu hệ thống (System Data)
DELETE FROM [dbo].[ClinicSettings];
DELETE FROM [dbo].[Users]; -- Xóa cuối cùng vì được tham chiếu bởi Doctor, Employee
PRINT '✅ System Data Deleted'

-- 7. Reset Identity (Tùy chọn: Reset ID về 1)
DBCC CHECKIDENT ('[dbo].[MedicineSaleDetails]', RESEED, 0);
-- PrescriptionDetails has Composite Key, no IDENTITY
DBCC CHECKIDENT ('[dbo].[Invoices]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[MedicineSales]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Prescriptions]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Appointments]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Visits]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Doctors]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Employees]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Patients]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Medicines]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Diseases]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[ClinicSettings]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Users]', RESEED, 0);
PRINT '✅ IDs Reset'

PRINT '🎉 Clean Database Completed Successfully!'
GO
