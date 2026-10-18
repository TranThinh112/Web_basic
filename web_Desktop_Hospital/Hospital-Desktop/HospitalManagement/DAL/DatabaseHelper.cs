using System;
using System.Configuration;
using System.Data.SqlClient;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Helper class để quản lý kết nối database
    /// </summary>
    public static class DatabaseHelper
    {
        private static string _connectionString;

        /// <summary>
        /// Lấy connection string từ App.config hoặc sử dụng giá trị mặc định
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    // Thử lấy từ App.config
                    var connStr = ConfigurationManager.ConnectionStrings["HospitalDB"];
                    if (connStr != null)
                    {
                        _connectionString = connStr.ConnectionString;
                    }
                    else
                    {
                        // Connection string mặc định cho SQL Server Express
                        _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=HospitalManagement;Integrated Security=True";
                    }
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// Tạo và trả về một SqlConnection mới
        /// </summary>
        /// <returns>SqlConnection object</returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Kiểm tra kết nối đến database
        /// </summary>
        /// <returns>True nếu kết nối thành công</returns>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra kết nối và trả về thông báo lỗi nếu có
        /// </summary>
        /// <param name="errorMessage">Thông báo lỗi nếu kết nối thất bại</param>
        /// <returns>True nếu kết nối thành công</returns>
        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Thực thi một câu lệnh SQL không trả về dữ liệu (INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="query">Câu lệnh SQL</param>
        /// <param name="parameters">Tham số</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Thực thi câu lệnh SQL và trả về giá trị đầu tiên
        /// </summary>
        /// <param name="query">Câu lệnh SQL</param>
        /// <param name="parameters">Tham số</param>
        /// <returns>Giá trị đầu tiên trong kết quả</returns>
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
