-- =============================================
-- Secure Password Migration Script
-- Run this AFTER AddMustChangePassword.sql
-- =============================================
-- Purpose: Force all existing users to change their password
-- This ensures all users update to the new security standard
-- =============================================

USE [HospitalManagement]
GO

-- Step 1: Add MustChangePassword column if not exists
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

-- Step 2: Set MustChangePassword = 1 for ALL existing active users
-- This forces everyone to change their password on next login
UPDATE Users 
SET MustChangePassword = 1 
WHERE IsActive = 1;

PRINT '✅ All active users flagged to change password on next login.';
PRINT CONCAT('   Updated users: ', @@ROWCOUNT);
GO

-- Step 3: Optional - Show affected users
SELECT 
    UserId, 
    Username, 
    Role, 
    IsActive,
    MustChangePassword,
    'Must change password on next login' AS [Status]
FROM Users 
WHERE MustChangePassword = 1
ORDER BY Role, Username;
GO

-- =============================================
-- ROLLBACK SCRIPT (if needed)
-- =============================================
-- To undo this migration, run:
-- UPDATE Users SET MustChangePassword = 0 WHERE IsActive = 1;
-- =============================================
