-- =============================================
-- BACKUP DATABASE SCRIPT
-- Tạo Database dự phòng: HospitalManagement_Backup
-- Sử dụng trước khi upgrade lên Entity Framework
-- =============================================

USE master;
GO

-- =============================================
-- PHẦN 1: TẠO DATABASE MỚI
-- =============================================
PRINT N'=== ĐANG TẠO DATABASE DỰ PHÒNG ===';

-- Tạo database backup mới (không xóa database gốc)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'HospitalManagement_Backup')
BEGIN
    CREATE DATABASE HospitalManagement_Backup;
    PRINT N'Đã tạo database HospitalManagement_Backup';
END
ELSE
BEGIN
    PRINT N'Database HospitalManagement_Backup đã tồn tại, đang xóa và tạo lại...';
    ALTER DATABASE HospitalManagement_Backup SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE HospitalManagement_Backup;
    CREATE DATABASE HospitalManagement_Backup;
END
GO

USE HospitalManagement_Backup;
GO

-- =============================================
-- PHẦN 2: TẠO CÁC BẢNG
-- =============================================
PRINT N'=== ĐANG TẠO CÁC BẢNG ===';

-- Bảng Users (Người dùng hệ thống)
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Doctor', 'Receptionist')),
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- Bảng Patients (Bệnh nhân)
CREATE TABLE Patients (
    PatientId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(10),
    Phone NVARCHAR(20),
    Address NVARCHAR(255),
    CCCD NVARCHAR(20) UNIQUE,
    MedicalHistory NVARCHAR(MAX)
);
GO

-- Bảng Doctors (Bác sĩ)
CREATE TABLE Doctors (
    DoctorId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Specialty NVARCHAR(100),
    UserId INT NOT NULL UNIQUE,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
GO

-- Bảng Diseases (Danh mục bệnh)
CREATE TABLE Diseases (
    DiseaseId INT IDENTITY(1,1) PRIMARY KEY,
    DiseaseName NVARCHAR(100) NOT NULL,
    Category NVARCHAR(50),
    Description NVARCHAR(MAX)
);
GO

-- Bảng Visits (Lượt khám)
CREATE TABLE Visits (
    VisitId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    VisitDate DATETIME NOT NULL DEFAULT GETDATE(),
    Symptoms NVARCHAR(MAX),
    Diagnosis NVARCHAR(MAX),
    DiseaseId INT NULL,
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId),
    FOREIGN KEY (DiseaseId) REFERENCES Diseases(DiseaseId)
);
GO

-- Bảng Medicines (Thuốc)
CREATE TABLE Medicines (
    MedicineId INT IDENTITY(1,1) PRIMARY KEY,
    MedicineName NVARCHAR(100) NOT NULL,
    Unit NVARCHAR(20),
    Price DECIMAL(18,2) NOT NULL DEFAULT 0
);
GO

-- Bảng Prescriptions (Đơn thuốc)
CREATE TABLE Prescriptions (
    PrescriptionId INT IDENTITY(1,1) PRIMARY KEY,
    VisitId INT NOT NULL UNIQUE,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (VisitId) REFERENCES Visits(VisitId)
);
GO

-- Bảng PrescriptionDetails (Chi tiết đơn thuốc)
CREATE TABLE PrescriptionDetails (
    PrescriptionId INT NOT NULL,
    MedicineId INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    Usage NVARCHAR(255),
    PRIMARY KEY (PrescriptionId, MedicineId),
    FOREIGN KEY (PrescriptionId) REFERENCES Prescriptions(PrescriptionId),
    FOREIGN KEY (MedicineId) REFERENCES Medicines(MedicineId)
);
GO

-- Bảng Appointments (Lịch hẹn)
CREATE TABLE Appointments (
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Reason NVARCHAR(255),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Confirmed', 'Completed', 'Cancelled')),
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId)
);
GO

-- Bảng Invoices (Hóa đơn)
CREATE TABLE Invoices (
    InvoiceId INT IDENTITY(1,1) PRIMARY KEY,
    VisitId INT NOT NULL UNIQUE,
    PatientId INT NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    PaymentDate DATETIME NOT NULL DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50) DEFAULT 'Cash' CHECK (PaymentMethod IN ('Cash', 'Card', 'Transfer')),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Paid', 'Cancelled')),
    FOREIGN KEY (VisitId) REFERENCES Visits(VisitId),
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId)
);
GO

-- Bảng Employees (Nhân viên) - MỚI THÊM
CREATE TABLE Employees (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NULL,
    Gender NVARCHAR(10) NULL,
    Phone NVARCHAR(15) NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(200) NULL,
    Position NVARCHAR(50) NULL,
    Department NVARCHAR(50) NULL,
    HireDate DATE NOT NULL DEFAULT GETDATE(),
    UserId INT NULL,
    CONSTRAINT FK_Employees_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
GO

PRINT N'Đã tạo tất cả các bảng thành công!';
GO

-- =============================================
-- PHẦN 3: SAO CHÉP DỮ LIỆU TỪ DATABASE GỐC
-- =============================================
PRINT N'=== ĐANG SAO CHÉP DỮ LIỆU ===';

-- Kiểm tra database gốc có tồn tại không
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'HospitalManagement')
BEGIN
    PRINT N'Đang sao chép dữ liệu từ HospitalManagement...';
    
    -- Tắt IDENTITY_INSERT để copy dữ liệu
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Users ON;
    INSERT INTO HospitalManagement_Backup.dbo.Users (UserId, Username, PasswordHash, Role, IsActive)
    SELECT UserId, Username, PasswordHash, Role, IsActive FROM HospitalManagement.dbo.Users;
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Users OFF;
    PRINT N'  - Đã copy bảng Users';
    
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Patients ON;
    INSERT INTO HospitalManagement_Backup.dbo.Patients (PatientId, FullName, DateOfBirth, Gender, Phone, Address, CCCD, MedicalHistory)
    SELECT PatientId, FullName, DateOfBirth, Gender, Phone, Address, CCCD, MedicalHistory FROM HospitalManagement.dbo.Patients;
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Patients OFF;
    PRINT N'  - Đã copy bảng Patients';
    
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Doctors ON;
    INSERT INTO HospitalManagement_Backup.dbo.Doctors (DoctorId, FullName, Specialty, UserId)
    SELECT DoctorId, FullName, Specialty, UserId FROM HospitalManagement.dbo.Doctors;
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Doctors OFF;
    PRINT N'  - Đã copy bảng Doctors';
    
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Diseases ON;
    INSERT INTO HospitalManagement_Backup.dbo.Diseases (DiseaseId, DiseaseName, Category, Description)
    SELECT DiseaseId, DiseaseName, Category, Description FROM HospitalManagement.dbo.Diseases;
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Diseases OFF;
    PRINT N'  - Đã copy bảng Diseases';
    
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Medicines ON;
    INSERT INTO HospitalManagement_Backup.dbo.Medicines (MedicineId, MedicineName, Unit, Price)
    SELECT MedicineId, MedicineName, Unit, Price FROM HospitalManagement.dbo.Medicines;
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Medicines OFF;
    PRINT N'  - Đã copy bảng Medicines';
    
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Visits ON;
    INSERT INTO HospitalManagement_Backup.dbo.Visits (VisitId, PatientId, DoctorId, VisitDate, Symptoms, Diagnosis, DiseaseId)
    SELECT VisitId, PatientId, DoctorId, VisitDate, Symptoms, Diagnosis, DiseaseId FROM HospitalManagement.dbo.Visits;
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Visits OFF;
    PRINT N'  - Đã copy bảng Visits';
    
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Prescriptions ON;
    INSERT INTO HospitalManagement_Backup.dbo.Prescriptions (PrescriptionId, VisitId, CreatedDate)
    SELECT PrescriptionId, VisitId, CreatedDate FROM HospitalManagement.dbo.Prescriptions;
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Prescriptions OFF;
    PRINT N'  - Đã copy bảng Prescriptions';
    
    INSERT INTO HospitalManagement_Backup.dbo.PrescriptionDetails (PrescriptionId, MedicineId, Quantity, Usage)
    SELECT PrescriptionId, MedicineId, Quantity, Usage FROM HospitalManagement.dbo.PrescriptionDetails;
    PRINT N'  - Đã copy bảng PrescriptionDetails';
    
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Appointments ON;
    INSERT INTO HospitalManagement_Backup.dbo.Appointments (AppointmentId, PatientId, DoctorId, AppointmentDate, Reason, Status)
    SELECT AppointmentId, PatientId, DoctorId, AppointmentDate, Reason, Status FROM HospitalManagement.dbo.Appointments;
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Appointments OFF;
    PRINT N'  - Đã copy bảng Appointments';
    
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Invoices ON;
    INSERT INTO HospitalManagement_Backup.dbo.Invoices (InvoiceId, VisitId, PatientId, TotalAmount, PaymentDate, PaymentMethod, Status)
    SELECT InvoiceId, VisitId, PatientId, TotalAmount, PaymentDate, PaymentMethod, Status FROM HospitalManagement.dbo.Invoices;
    SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Invoices OFF;
    PRINT N'  - Đã copy bảng Invoices';
    
    -- Copy bảng Employees nếu tồn tại
    IF EXISTS (SELECT * FROM HospitalManagement.INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Employees')
    BEGIN
        SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Employees ON;
        INSERT INTO HospitalManagement_Backup.dbo.Employees (EmployeeId, FullName, DateOfBirth, Gender, Phone, Email, Address, Position, Department, HireDate, UserId)
        SELECT EmployeeId, FullName, DateOfBirth, Gender, Phone, Email, Address, Position, Department, HireDate, UserId FROM HospitalManagement.dbo.Employees;
        SET IDENTITY_INSERT HospitalManagement_Backup.dbo.Employees OFF;
        PRINT N'  - Đã copy bảng Employees';
    END
    
    PRINT N'=== HOÀN TẤT SAO CHÉP DỮ LIỆU ===';
END
ELSE
BEGIN
    PRINT N'Không tìm thấy database gốc HospitalManagement. Tạo dữ liệu mẫu...';
    
    -- Tạo tài khoản Admin mặc định (password: admin123)
    INSERT INTO Users (Username, PasswordHash, Role, IsActive)
    VALUES ('admin', '240be518fabd2724ddb6f04eeb9d56f5b00ee8fa1c3c18b9b7456c7c1b2d8e0d', 'Admin', 1);
    
    -- Tạo một số bệnh thường gặp
    INSERT INTO Diseases (DiseaseName, Category, Description) VALUES
    (N'Cảm cúm', N'Seasonal', N'Bệnh cúm mùa do virus influenza'),
    (N'Viêm họng', N'Common', N'Viêm niêm mạc họng'),
    (N'Sốt virus', N'Seasonal', N'Sốt do nhiễm virus'),
    (N'Viêm phế quản', N'Common', N'Viêm đường hô hấp dưới');
    
    -- Tạo một số thuốc mẫu
    INSERT INTO Medicines (MedicineName, Unit, Price) VALUES
    (N'Paracetamol 500mg', N'Viên', 500),
    (N'Amoxicillin 500mg', N'Viên', 2000),
    (N'Vitamin C 1000mg', N'Viên', 1000);
    
    PRINT N'Đã tạo dữ liệu mẫu!';
END
GO

-- =============================================
-- PHẦN 4: THỐNG KÊ KẾT QUẢ
-- =============================================
PRINT N'';
PRINT N'=== THỐNG KÊ DỮ LIỆU TRONG BACKUP ===';
SELECT 'Users' AS [Bảng], COUNT(*) AS [Số bản ghi] FROM Users
UNION ALL SELECT 'Patients', COUNT(*) FROM Patients
UNION ALL SELECT 'Doctors', COUNT(*) FROM Doctors
UNION ALL SELECT 'Diseases', COUNT(*) FROM Diseases
UNION ALL SELECT 'Medicines', COUNT(*) FROM Medicines
UNION ALL SELECT 'Visits', COUNT(*) FROM Visits
UNION ALL SELECT 'Prescriptions', COUNT(*) FROM Prescriptions
UNION ALL SELECT 'Appointments', COUNT(*) FROM Appointments
UNION ALL SELECT 'Invoices', COUNT(*) FROM Invoices
UNION ALL SELECT 'Employees', COUNT(*) FROM Employees;
GO

PRINT N'';
PRINT N'=============================================';
PRINT N'  BACKUP HOÀN TẤT!';
PRINT N'  Database: HospitalManagement_Backup';
PRINT N'  Thời gian: ' + CONVERT(NVARCHAR, GETDATE(), 120);
PRINT N'=============================================';
GO
