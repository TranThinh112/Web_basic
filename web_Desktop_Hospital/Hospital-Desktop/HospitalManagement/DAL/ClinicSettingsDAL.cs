using System.Linq;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Data Access Layer for Clinic Settings
    /// Uses singleton pattern - only one settings row in database
    /// </summary>
    public class ClinicSettingsDAL
    {
        /// <summary>
        /// Get current clinic settings (creates default if not exists)
        /// </summary>
        public ClinicSettingsDTO GetSettings()
        {
            using (var db = new ClinicDbContext())
            {
                var settings = db.ClinicSettings.FirstOrDefault();
                if (settings == null)
                {
                    // Create default settings
                    settings = new ClinicSettingsDTO();
                    db.ClinicSettings.Add(settings);
                    db.SaveChanges();
                }
                return settings;
            }
        }

        /// <summary>
        /// Save clinic settings (update existing or insert new)
        /// </summary>
        public bool SaveSettings(ClinicSettingsDTO settings)
        {
            using (var db = new ClinicDbContext())
            {
                var existing = db.ClinicSettings.FirstOrDefault();
                if (existing != null)
                {
                    existing.OpeningTime = settings.OpeningTime;
                    existing.ClosingTime = settings.ClosingTime;
                    existing.IsActive = settings.IsActive;
                    existing.ExaminationFee = settings.ExaminationFee;
                }
                else
                {
                    db.ClinicSettings.Add(settings);
                }
                return db.SaveChanges() > 0;
            }
        }
    }
}
