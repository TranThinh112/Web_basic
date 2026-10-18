using System;
using System.Collections.Generic;
using HospitalManagement.DAL;
using HospitalManagement.DTO;

namespace HospitalManagement.BLL
{
    public class AppointmentBLL
    {
        private readonly AppointmentDAL _appointmentDAL;
        private readonly PatientDAL _patientDAL;
        private readonly DoctorDAL _doctorDAL;

        public AppointmentBLL()
        {
            _appointmentDAL = new AppointmentDAL();
            _patientDAL = new PatientDAL();
            _doctorDAL = new DoctorDAL();
        }

        /// <summary>
        /// Tạo lịch hẹn mới
        /// </summary>
        public int CreateAppointment(AppointmentDTO appointment)
        {
            ValidateAppointment(appointment);

            // Kiểm tra trùng lịch
            if (CheckConflict(appointment.DoctorId, appointment.AppointmentDate))
                throw new Exception("Bác sĩ đã có lịch hẹn vào thời điểm này");

            appointment.Status = "Pending";
            return _appointmentDAL.Insert(appointment);
        }

        /// <summary>
        /// Lấy lịch hẹn theo Id
        /// </summary>
        public AppointmentDTO GetAppointmentById(int appointmentId)
        {
            return _appointmentDAL.GetById(appointmentId);
        }

        /// <summary>
        /// Lấy lịch hẹn theo bác sĩ
        /// </summary>
        public List<AppointmentDTO> GetAppointmentsByDoctor(int doctorId)
        {
            return _appointmentDAL.GetByDoctor(doctorId);
        }

        /// <summary>
        /// Lấy lịch hẹn theo bác sĩ trong ngày
        /// </summary>
        public List<AppointmentDTO> GetAppointmentsByDoctor(int doctorId, DateTime date)
        {
            var allAppointments = _appointmentDAL.GetByDoctor(doctorId);
            List<AppointmentDTO> filtered = new List<AppointmentDTO>();

            foreach (var apt in allAppointments)
            {
                if (apt.AppointmentDate.Date == date.Date)
                    filtered.Add(apt);
            }
            return filtered;
        }

        /// <summary>
        /// Lấy lịch hẹn theo bệnh nhân
        /// </summary>
        public List<AppointmentDTO> GetAppointmentsByPatient(int patientId)
        {
            return _appointmentDAL.GetByPatient(patientId);
        }

        /// <summary>
        /// Lấy lịch hẹn trong ngày
        /// </summary>
        public List<AppointmentDTO> GetTodayAppointments()
        {
            return _appointmentDAL.GetByDate(DateTime.Today);
        }

        /// <summary>
        /// Lấy lịch hẹn còn lại trong ngày (Pending/Confirmed)
        /// </summary>
        public List<AppointmentDTO> GetPendingTodayAppointments()
        {
            var all = _appointmentDAL.GetByDate(DateTime.Today);
            var pending = new List<AppointmentDTO>();
            foreach (var apt in all)
            {
                if (apt.Status == "Pending" || apt.Status == "Confirmed")
                    pending.Add(apt);
            }
            return pending;
        }

        /// <summary>
        /// Lấy lịch hẹn theo ngày
        /// </summary>
        public List<AppointmentDTO> GetAppointmentsByDate(DateTime date)
        {
            return _appointmentDAL.GetByDate(date);
        }

        /// <summary>
        /// Lấy lịch hẹn có phân trang và sắp xếp
        /// </summary>
        public List<AppointmentDTO> GetAppointmentsPaged(string keyword, int page, int pageSize, string sortColumn = null, bool sortAsc = true)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _appointmentDAL.GetPaged(page, pageSize, sortColumn, sortAsc);
            return _appointmentDAL.SearchPaged(keyword, page, pageSize, sortColumn, sortAsc);
        }

        /// <summary>
        /// Đếm tổng số lịch hẹn
        /// </summary>
        public int GetAppointmentCount(string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _appointmentDAL.GetCount();
            return _appointmentDAL.SearchCount(keyword);
        }

        /// <summary>
        /// Cập nhật lịch hẹn
        /// </summary>
        public bool UpdateAppointment(AppointmentDTO appointment)
        {
            ValidateAppointment(appointment);
            return _appointmentDAL.Update(appointment);
        }

        /// <summary>
        /// Cập nhật trạng thái lịch hẹn
        /// </summary>
        public bool UpdateAppointmentStatus(int appointmentId, string status)
        {
            string[] validStatuses = { "Pending", "Confirmed", "Completed", "Cancelled" };
            bool isValid = false;

            foreach (var s in validStatuses)
            {
                if (s.Equals(status, StringComparison.OrdinalIgnoreCase))
                {
                    isValid = true;
                    status = s;
                    break;
                }
            }

            if (!isValid)
                throw new ArgumentException("Trạng thái không hợp lệ");

            return _appointmentDAL.UpdateStatus(appointmentId, status);
        }

        /// <summary>
        /// Đánh dấu bệnh nhân đã đến
        /// </summary>
        public bool MarkAsArrived(int appointmentId)
        {
            return UpdateAppointmentStatus(appointmentId, "Confirmed");
        }

        /// <summary>
        /// Đánh dấu đã hoàn thành
        /// </summary>
        public bool MarkAsCompleted(int appointmentId)
        {
            return UpdateAppointmentStatus(appointmentId, "Completed");
        }

        /// <summary>
        /// Hủy lịch hẹn
        /// </summary>
        public bool CancelAppointment(int appointmentId)
        {
            return UpdateAppointmentStatus(appointmentId, "Cancelled");
        }

        /// <summary>
        /// Xóa lịch hẹn
        /// </summary>
        public bool DeleteAppointment(int appointmentId)
        {
            return _appointmentDAL.Delete(appointmentId);
        }

        /// <summary>
        /// Kiểm tra trùng lịch hẹn
        /// Chỉ kiểm tra các lịch Pending hoặc Confirmed, bỏ qua Cancelled và Completed
        /// Khoảng cách tối thiểu giữa 2 lịch: 15 phút
        /// </summary>
        public bool CheckConflict(int doctorId, DateTime appointmentDate, int? excludeAppointmentId = null)
        {
            var appointments = _appointmentDAL.GetByDoctor(doctorId);
            const int MIN_GAP_MINUTES = 15; // Khoảng cách tối thiểu 15 phút

            foreach (var apt in appointments)
            {
                // Bỏ qua lịch hẹn đang được chỉnh sửa
                if (excludeAppointmentId.HasValue && apt.AppointmentId == excludeAppointmentId.Value)
                    continue;

                // Bỏ qua lịch đã hủy hoặc đã hoàn thành
                if (apt.Status == "Cancelled" || apt.Status == "Completed")
                    continue;

                // Chỉ check lịch trong cùng ngày
                if (apt.AppointmentDate.Date != appointmentDate.Date)
                    continue;

                // Kiểm tra xem có trùng khung giờ không
                TimeSpan diff = apt.AppointmentDate - appointmentDate;
                if (Math.Abs(diff.TotalMinutes) < MIN_GAP_MINUTES)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Validate thông tin lịch hẹn
        /// </summary>
        private void ValidateAppointment(AppointmentDTO appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));

            if (appointment.PatientId <= 0)
                throw new ArgumentException("Phải chọn bệnh nhân");

            if (appointment.DoctorId <= 0)
                throw new ArgumentException("Phải chọn bác sĩ");

            if (appointment.AppointmentDate < DateTime.Now.AddMinutes(-5))
                throw new ArgumentException("Thời gian hẹn phải trong tương lai");

            // Kiểm tra bệnh nhân tồn tại
            if (_patientDAL.GetById(appointment.PatientId) == null)
                throw new Exception("Bệnh nhân không tồn tại");

            // Kiểm tra bác sĩ tồn tại
            if (_doctorDAL.GetById(appointment.DoctorId) == null)
                throw new Exception("Bác sĩ không tồn tại");
        }
    }
}
