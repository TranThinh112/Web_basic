using System;
using HospitalManagement.DAL;
using HospitalManagement.DTO;

namespace HospitalManagement.BLL
{
    /// <summary>
    /// Business Logic Layer for Clinic Settings
    /// </summary>
    public class ClinicSettingsBLL
    {
        private readonly ClinicSettingsDAL _dal;

        public ClinicSettingsBLL()
        {
            _dal = new ClinicSettingsDAL();
        }

        /// <summary>
        /// Get current clinic operating hours
        /// </summary>
        public ClinicSettingsDTO GetClinicHours()
        {
            return _dal.GetSettings();
        }

        /// <summary>
        /// Save clinic operating hours with validation
        /// </summary>
        public bool SaveClinicSettings(TimeSpan openingTime, TimeSpan closingTime, decimal examinationFee)
        {
            // Validation
            if (openingTime >= closingTime)
                throw new ArgumentException("Giờ mở cửa phải nhỏ hơn giờ đóng cửa!");

            if (openingTime.TotalHours < 0 || closingTime.TotalHours > 24)
                throw new ArgumentException("Giờ không hợp lệ!");

            if (examinationFee < 0)
                throw new ArgumentException("Phí khám không được âm!");

            var settings = new ClinicSettingsDTO
            {
                OpeningTime = openingTime,
                ClosingTime = closingTime,
                IsActive = true,
                ExaminationFee = examinationFee
            };

            return _dal.SaveSettings(settings);
        }

        /// <summary>
        /// Check if given time is within operating hours
        /// </summary>
        public bool IsWithinOperatingHours(TimeSpan time)
        {
            var settings = _dal.GetSettings();
            return settings.IsWithinHours(time);
        }

        /// <summary>
        /// Get opening time
        /// </summary>
        public TimeSpan GetOpeningTime()
        {
            return _dal.GetSettings().OpeningTime;
        }

        /// <summary>
        /// Get closing time
        /// </summary>
        public TimeSpan GetClosingTime()
        {
            return _dal.GetSettings().ClosingTime;
        }
    }
}
