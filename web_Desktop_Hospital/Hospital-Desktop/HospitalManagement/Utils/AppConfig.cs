using System.Configuration;

namespace HospitalManagement.Utils
{
    public static class AppConfig
    {
        public static string ConnectionString
        {
            get
            {
                var connStr = ConfigurationManager.ConnectionStrings["HospitalDB"];
                return connStr != null ? connStr.ConnectionString : @"Data Source=.\SQLEXPRESS;Initial Catalog=HospitalManagement;Integrated Security=True";
            }
        }

        public static string ExaminationFee
        {
            get { return ConfigurationManager.AppSettings["ExaminationFee"] ?? "100000"; }
        }

        public static string ClinicName
        {
            get { return ConfigurationManager.AppSettings["ClinicName"] ?? "Phòng khám Tư nhân"; }
        }
    }
}
