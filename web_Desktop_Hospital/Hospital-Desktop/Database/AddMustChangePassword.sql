-- =============================================
-- Secure Password Reset: Add MustChangePassword column
-- Run this script on your database
-- =============================================

USE [HospitalManagement]
GO

-- Check if column exists before adding
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'MustChangePassword'
)
BEGIN
    ALTER TABLE Users ADD MustChangePassword BIT NOT NULL DEFAULT 0;
    PRINT '✅ Column MustChangePassword added successfully.';
END
ELSE
BEGIN
    PRINT '✅ Column MustChangePassword already exists.';
END
GO

