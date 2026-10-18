-- Patch: Add Account Lockout Columns to Users table
USE [HospitalManagement]
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = 'FailedLoginAttempts')
BEGIN
    PRINT '⚠️ "FailedLoginAttempts" column missing. Adding...'
    ALTER TABLE [dbo].[Users] ADD [FailedLoginAttempts] INT NOT NULL DEFAULT 0;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = 'LockoutEndTime')
BEGIN
    PRINT '⚠️ "LockoutEndTime" column missing. Adding...'
    ALTER TABLE [dbo].[Users] ADD [LockoutEndTime] DATETIME NULL;
END

PRINT '✅ Lockout columns checked/added.'
GO
