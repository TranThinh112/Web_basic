-- =============================================
-- BCrypt Upgrade: Add HashVersion column
-- Run this script on your database
-- =============================================

USE [HospitalManagement]
GO

-- Add HashVersion column (1=SHA256 legacy, 2=BCrypt)
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'HashVersion'
)
BEGIN
    ALTER TABLE Users ADD HashVersion INT NOT NULL DEFAULT 1;
    PRINT '✅ Column HashVersion added successfully.';
END
ELSE
BEGIN
    PRINT '✅ Column HashVersion already exists.';
END
GO
