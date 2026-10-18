-- Script tạo bảng ClinicSettings cho Hospital Management System
-- Chạy script này trong SQL Server Management Studio

-- Tạo bảng ClinicSettings
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClinicSettings')
BEGIN
    CREATE TABLE ClinicSettings (
        SettingId INT PRIMARY KEY IDENTITY(1,1),
        OpeningTime TIME NOT NULL DEFAULT '15:00:00',
        ClosingTime TIME NOT NULL DEFAULT '21:00:00',
        IsActive BIT NOT NULL DEFAULT 1
    );
    
    -- Insert default settings (15:00 - 21:00)
    INSERT INTO ClinicSettings (OpeningTime, ClosingTime, IsActive) 
    VALUES ('15:00:00', '21:00:00', 1);
    
    PRINT N'Đã tạo bảng ClinicSettings và thêm dữ liệu mặc định (15:00 - 21:00)';
END
ELSE
BEGIN
    PRINT N'Bảng ClinicSettings đã tồn tại';
END
GO
