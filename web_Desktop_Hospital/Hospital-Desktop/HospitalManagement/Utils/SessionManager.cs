using HospitalManagement.DTO;

namespace HospitalManagement.Utils
{
    public static class SessionManager
    {
        public static UserDTO CurrentUser { get; private set; }
        public static DoctorDTO CurrentDoctor { get; private set; }
        public static EmployeeDTO CurrentEmployee { get; private set; }

        public static bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        public static string CurrentRole
        {
            get { return CurrentUser != null ? CurrentUser.Role : string.Empty; }
        }

        public static void Login(UserDTO user)
        {
            CurrentUser = user;
        }

        public static void Login(UserDTO user, DoctorDTO doctor)
        {
            CurrentUser = user;
            CurrentDoctor = doctor;
        }

        public static void Login(UserDTO user, EmployeeDTO employee)
        {
            CurrentUser = user;
            CurrentEmployee = employee;
        }

        public static void Logout()
        {
            CurrentUser = null;
            CurrentDoctor = null;
            CurrentEmployee = null;
        }

        public static bool IsAdmin()
        {
            return IsLoggedIn && CurrentUser.Role == "Admin";
        }

        public static bool IsDoctor()
        {
            return IsLoggedIn && CurrentUser.Role == "Doctor";
        }

        public static bool IsReceptionist()
        {
            return IsLoggedIn && CurrentUser.Role == "Receptionist";
        }
    }
}
