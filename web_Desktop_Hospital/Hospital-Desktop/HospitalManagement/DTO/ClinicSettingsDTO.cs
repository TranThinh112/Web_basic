using System;

namespace HospitalManagement.DTO
{
    /// <summary>
    /// DTO for Clinic Settings - stores operating hours
    /// </summary>
    public class ClinicSettingsDTO
    {
        public int SettingId { get; set; }
        
        /// <summary>
        /// Clinic opening time (e.g., 15:00)
        /// </summary>
        public TimeSpan OpeningTime { get; set; }
        
        /// <summary>
        /// Clinic closing time (e.g., 21:00)
        /// </summary>
        public TimeSpan ClosingTime { get; set; }
        
        /// <summary>
        /// Whether clinic is currently active
        /// </summary>
        public bool IsActive { get; set; }

        public ClinicSettingsDTO()
        {
            OpeningTime = new TimeSpan(8, 0, 0); // 08:00 default
            ClosingTime = new TimeSpan(17, 0, 0); // 17:00 default
            IsActive = true;
            ExaminationFee = 100000; // Default fee
        }

        public decimal ExaminationFee { get; set; }

        /// <summary>
        /// Display opening time as string HH:mm
        /// </summary>
        public string OpeningTimeDisplay => OpeningTime.ToString(@"hh\:mm");

        /// <summary>
        /// Display closing time as string HH:mm
        /// </summary>
        public string ClosingTimeDisplay => ClosingTime.ToString(@"hh\:mm");

        /// <summary>
        /// Check if given time is within operating hours
        /// </summary>
        public bool IsWithinHours(TimeSpan time)
        {
            return time >= OpeningTime && time <= ClosingTime;
        }
    }
}
