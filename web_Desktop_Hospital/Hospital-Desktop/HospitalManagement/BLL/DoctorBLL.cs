using System;
using System.Collections.Generic;
using HospitalManagement.DAL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.BLL
{
    public class DoctorBLL
    {
        private readonly DoctorDAL _doctorDAL;
        private readonly UserDAL _userDAL;
        private readonly AppointmentDAL _appointmentDAL;

        public DoctorBLL()
        {
            _doctorDAL = new DoctorDAL();
            _userDAL = new UserDAL();
            _appointmentDAL = new AppointmentDAL();
        }

        /// <summary>
        /// Lấy tất cả bác sĩ
        /// </summary>
        public List<DoctorDTO> GetAllDoctors()
        {
            return _doctorDAL.GetAll();
        }

        /// <summary>
        /// Lấy thông tin bác sĩ theo Id
        /// </summary>
        public DoctorDTO GetDoctorById(int doctorId)
        {
            return _doctorDAL.GetById(doctorId);
        }

        /// <summary>
        /// Lấy thông tin bác sĩ theo UserId (tài khoản đăng nhập)
        /// </summary>
        public DoctorDTO GetDoctorByUserId(int userId)
        {
            return _doctorDAL.GetByUserId(userId);
        }

        /// <summary>
        /// Lấy bác sĩ theo chuyên khoa
        /// </summary>
        public List<DoctorDTO> GetDoctorsBySpecialty(string specialty)
        {
            return _doctorDAL.GetBySpecialty(specialty);
        }

        /// <summary>
        /// Thêm bác sĩ mới
        /// </summary>
        public int AddDoctor(DoctorDTO doctor)
        {
            ValidateDoctor(doctor);

            // Kiểm tra UserId có tồn tại và có role Doctor không
            UserDTO user = _userDAL.GetById(doctor.UserId);
            if (user == null)
                throw new Exception("Tài khoản người dùng không tồn tại");

            if (!user.Role.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Tài khoản phải có vai trò Bác sĩ");

            // Kiểm tra UserId đã được gán cho bác sĩ khác chưa
            DoctorDTO existingDoctor = _doctorDAL.GetByUserId(doctor.UserId);
            if (existingDoctor != null)
                throw new Exception("Tài khoản đã được gán cho bác sĩ khác");

            return _doctorDAL.Insert(doctor);
        }

        /// <summary>
        /// Cập nhật thông tin bác sĩ
        /// </summary>
        public bool UpdateDoctor(DoctorDTO doctor)
        {
            ValidateDoctor(doctor);
            return _doctorDAL.Update(doctor);
        }

        /// <summary>
        /// Xóa bác sĩ
        /// </summary>
        public bool DeleteDoctor(int doctorId)
        {
            // Kiểm tra xem bác sĩ có lịch hẹn không
            var appointments = _appointmentDAL.GetByDoctor(doctorId);
            if (appointments != null && appointments.Count > 0)
                throw new Exception("Không thể xóa bác sĩ đã có lịch hẹn");

            return _doctorDAL.Delete(doctorId);
        }

        /// <summary>
        /// Lấy lịch làm việc của bác sĩ trong ngày
        /// </summary>
        public List<AppointmentDTO> GetDoctorSchedule(int doctorId, DateTime date)
        {
            var allAppointments = _appointmentDAL.GetByDoctor(doctorId);
            List<AppointmentDTO> schedule = new List<AppointmentDTO>();

            foreach (var apt in allAppointments)
            {
                if (apt.AppointmentDate.Date == date.Date)
                    schedule.Add(apt);
            }

            return schedule;
        }

        /// <summary>
        /// Validate thông tin bác sĩ
        /// </summary>
        private void ValidateDoctor(DoctorDTO doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor));

            if (!ValidationHelper.ValidateFullName(doctor.FullName))
                throw new ArgumentException("Họ tên bác sĩ không hợp lệ");

            if (doctor.UserId <= 0)
                throw new ArgumentException("Phải chọn tài khoản cho bác sĩ");
        }
    }
}
