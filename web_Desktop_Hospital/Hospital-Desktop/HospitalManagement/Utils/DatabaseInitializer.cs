using System;
using System.Data.Entity;
using HospitalManagement.DAL;

namespace HospitalManagement.Utils
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            try
            {
                using (var db = new ClinicDbContext())
                {
                    // Check connection
                    db.Database.Connection.Open();
                    
                    // Check if ExaminationFee column exists in ClinicSettings
                    var checkCmd = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ClinicSettings' AND COLUMN_NAME = 'ExaminationFee'";
                    var count = db.Database.SqlQuery<int>(checkCmd).SingleAsync().Result;
                    
                    if (count == 0)
                    {
                        // Add column
                        var alterCmd = "ALTER TABLE ClinicSettings ADD ExaminationFee DECIMAL(18,2) NOT NULL DEFAULT 100000";
                        db.Database.ExecuteSqlCommand(alterCmd);
                    }
                    
                    // Also ensure StockQuantity and MinStockLevel for Medicines exist (from previous task)
                    var checkMed = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Medicines' AND COLUMN_NAME = 'MinStockLevel'";
                    var countMed = db.Database.SqlQuery<int>(checkMed).SingleAsync().Result;
                    
                    if (countMed == 0)
                    {
                         db.Database.ExecuteSqlCommand("ALTER TABLE Medicines ADD StockQuantity INT NOT NULL DEFAULT 0");
                         db.Database.ExecuteSqlCommand("ALTER TABLE Medicines ADD MinStockLevel INT NOT NULL DEFAULT 10");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or ignore if DB not ready (will be handled by main app)
                Console.WriteLine("DB Init Error: " + ex.Message);
            }
        }
    }
}
