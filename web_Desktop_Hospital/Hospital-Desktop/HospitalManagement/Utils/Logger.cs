using System;
using System.IO;

namespace HospitalManagement.Utils
{
    public static class Logger
    {
        private static readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly object LockObj = new object();

        public static void Log(string message)
        {
            Log("INFO", message);
        }

        public static void LogError(string message)
        {
            Log("ERROR", message);
        }

        public static void LogError(Exception ex)
        {
            Log("ERROR", $"{ex.Message}\n{ex.StackTrace}");
        }

        public static void LogError(string context, Exception ex)
        {
            Log("ERROR", $"[{context}] {ex.Message}\n{ex.StackTrace}");
        }

        public static void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        public static void LogAudit(string action, string user, string details)
        {
            Log("AUDIT", $"User: {user} | Action: {action} | {details}");
        }

        private static void Log(string level, string message)
        {
            try
            {
                lock (LockObj)
                {
                    if (!Directory.Exists(LogPath))
                        Directory.CreateDirectory(LogPath);

                    string fileName = $"log_{DateTime.Now:yyyyMMdd}.txt";
                    string filePath = Path.Combine(LogPath, fileName);
                    string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}{Environment.NewLine}";


                    File.AppendAllText(filePath, logEntry);
                }
            }
            catch { }
        }
    }
}
