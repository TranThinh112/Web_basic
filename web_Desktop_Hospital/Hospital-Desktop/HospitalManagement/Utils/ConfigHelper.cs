using System;
using System.Configuration;

namespace HospitalManagement.Utils
{
    public static class ConfigHelper
    {
        /// <summary>
        /// Mã hóa ConnectionString trong App.config để bảo mật
        /// </summary>
        public static void EncryptConnectionString()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConfigurationSection section = config.GetSection("connectionStrings");

                if (section != null && !section.SectionInformation.IsProtected)
                {
                    // Sử dụng DataProtectionConfigurationProvider (DPAPI) -> Key gắn liền với máy tính này
                    section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                    section.SectionInformation.ForceSave = true;
                    config.Save(ConfigurationSaveMode.Modified);
                    
                    // Refresh section để ứng dụng đọc được giá trị mới ngay lập tức
                    ConfigurationManager.RefreshSection("connectionStrings");
                    Logger.LogAudit("SYSTEM", "System", "Encrypted Connection String in App.config");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("ConfigHelper.EncryptConnectionString", ex);
                // Không throw exception để tránh crash app, chỉ log lỗi
            }
        }

        // Lưu ý: Không cần hàm Decrypt thủ công. 
        // .NET Framework tự động giải mã khi gọi ConfigurationManager.ConnectionStrings["..."]
        // nếu section đó được bảo vệ bởi provider tương ứng.
    }
}
