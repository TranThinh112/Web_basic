using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalManagement.DAL;
using HospitalManagement.DTO;

namespace HospitalManagement.BLL
{
    public class VisitBLL
    {
        private readonly VisitDAL _visitDAL;
        private readonly PatientDAL _patientDAL;
        private readonly DoctorDAL _doctorDAL;
        private readonly AppointmentDAL _appointmentDAL;

        public VisitBLL()
        {
            _visitDAL = new VisitDAL();
            _patientDAL = new PatientDAL();
            _doctorDAL = new DoctorDAL();
            _appointmentDAL = new AppointmentDAL();
        }

        /// <summary>
        /// Tạo lượt khám mới
        /// </summary>
        public int CreateVisit(VisitDTO visit)
        {
            ValidateVisit(visit);
            visit.VisitDate = DateTime.Now;
            return _visitDAL.Insert(visit);
        }

        /// <summary>
        /// Tạo lượt khám từ lịch hẹn
        /// Visit được tạo với status "PendingPayment", chỉ hiện cho bác sĩ sau khi thanh toán
        /// </summary>
        public int CreateVisitFromAppointment(int appointmentId)
        {
            AppointmentDTO appointment = _appointmentDAL.GetById(appointmentId);
            if (appointment == null)
                throw new Exception("Lịch hẹn không tồn tại");

            // Kiểm tra trạng thái lịch hẹn
            if (appointment.Status == "Cancelled")
                throw new Exception("Lịch hẹn đã bị hủy - Bệnh nhân không đến!");

            if (appointment.Status == "Completed")
                throw new Exception("Lịch hẹn này đã được khám rồi!");

            if (appointment.Status == "NoShow")
                throw new Exception("Bệnh nhân không đến theo lịch hẹn này!");

            // Chỉ cho phép tạo lượt khám nếu trạng thái là Pending hoặc Confirmed
            if (appointment.Status != "Pending" && appointment.Status != "Confirmed")
                throw new Exception($"Không thể tạo lượt khám với trạng thái: {appointment.Status}");

            VisitDTO visit = new VisitDTO
            {
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                VisitDate = DateTime.Now,
                Status = "PendingPayment"  // Chờ thanh toán, chưa hiện cho bác sĩ
            };

            int visitId = _visitDAL.Insert(visit);

            // Đánh dấu lịch hẹn đã hoàn thành
            _appointmentDAL.UpdateStatus(appointmentId, "Completed");

            return visitId;
        }

        /// <summary>
        /// Đánh dấu bệnh nhân đã đến
        /// </summary>
        public bool MarkPatientArrived(int appointmentId)
        {
            var appointment = _appointmentDAL.GetById(appointmentId);
            if (appointment == null)
                throw new Exception("Lịch hẹn không tồn tại");

            if (appointment.Status == "Cancelled")
                throw new Exception("Lịch hẹn đã bị hủy!");

            return _appointmentDAL.UpdateStatus(appointmentId, "Confirmed");
        }

        /// <summary>
        /// Đánh dấu bệnh nhân không đến
        /// </summary>
        public bool MarkPatientNoShow(int appointmentId)
        {
            var appointment = _appointmentDAL.GetById(appointmentId);
            if (appointment == null)
                throw new Exception("Lịch hẹn không tồn tại");

            if (appointment.Status == "Completed")
                throw new Exception("Lịch hẹn này đã được khám rồi!");

            return _appointmentDAL.UpdateStatus(appointmentId, "NoShow");
        }

        /// <summary>
        /// Lấy thông tin lượt khám theo Id
        /// </summary>
        public VisitDTO GetVisitById(int visitId)
        {
            return _visitDAL.GetById(visitId);
        }

        /// <summary>
        /// Lấy lịch sử khám của bệnh nhân
        /// </summary>
        public List<VisitDTO> GetVisitsByPatient(int patientId)
        {
            return _visitDAL.GetByPatient(patientId);
        }

        /// <summary>
        /// Lấy danh sách bệnh nhân khám theo bác sĩ trong ngày
        /// </summary>
        public List<VisitDTO> GetVisitsByDoctor(int doctorId, DateTime date)
        {
            return _visitDAL.GetByDoctorAndDate(doctorId, date);
        }

        /// <summary>
        /// Lấy tất cả lượt khám theo bác sĩ
        /// </summary>
        public List<VisitDTO> GetVisitsByDoctor(int doctorId)
        {
            return _visitDAL.GetByDoctor(doctorId);
        }

        /// <summary>
        /// Lấy lượt khám trong ngày
        /// </summary>
        public List<VisitDTO> GetTodayVisits()
        {
            return _visitDAL.GetByDate(DateTime.Today);
        }

        /// <summary>
        /// Lấy lượt khám theo ngày
        /// </summary>
        public List<VisitDTO> GetVisitsByDate(DateTime date)
        {
            return _visitDAL.GetByDate(date);
        }

        /// <summary>
        /// Lấy lượt khám có phân trang và sắp xếp
        /// </summary>
        public List<VisitDTO> GetVisitsPaged(string keyword, int page, int pageSize, string sortColumn = null, bool sortAsc = true)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _visitDAL.GetPaged(page, pageSize, sortColumn, sortAsc);
            return _visitDAL.SearchPaged(keyword, page, pageSize, sortColumn, sortAsc);
        }

        /// <summary>
        /// Đếm tổng số lượt khám
        /// </summary>
        public int GetVisitCount(string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _visitDAL.GetCount();
            return _visitDAL.SearchCount(keyword);
        }

        #region Safe Async Methods

        /// <summary>
        /// Safe Async: Lấy lượt khám theo bác sĩ và ngày - chạy trên background thread
        /// </summary>
        public System.Threading.Tasks.Task<List<VisitDTO>> GetVisitsByDoctorSafeAsync(int doctorId, DateTime date)
        {
            return _visitDAL.GetByDoctorAndDateSafeAsync(doctorId, date);
        }

        /// <summary>
        /// Safe Async: Lấy lượt khám theo ngày - chạy trên background thread
        /// </summary>
        public System.Threading.Tasks.Task<List<VisitDTO>> GetVisitsByDateSafeAsync(DateTime date)
        {
            return _visitDAL.GetByDateSafeAsync(date);
        }

        #endregion

        /// <summary>
        /// Cập nhật thông tin lượt khám
        /// </summary>
        public bool UpdateVisit(VisitDTO visit)
        {
            if (visit == null)
                throw new ArgumentNullException(nameof(visit));
            return _visitDAL.Update(visit);
        }

        /// <summary>
        /// Cập nhật trạng thái lượt khám
        /// </summary>
        public bool UpdateVisitStatus(int visitId, string status)
        {
            var visit = _visitDAL.GetById(visitId);
            if (visit == null)
                throw new Exception("Lượt khám không tồn tại");

            string[] validStatuses = { "Waiting", "InProgress", "Completed" };
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

            visit.Status = status;
            return _visitDAL.Update(visit);
        }

        /// <summary>
        /// Hoàn thành lượt khám - Bác sĩ xác nhận đã khám xong
        /// </summary>
        public bool CompleteVisit(int visitId)
        {
            return UpdateVisitStatus(visitId, "Completed");
        }

        /// <summary>
        /// Cập nhật chẩn đoán
        /// </summary>
        public bool UpdateDiagnosis(int visitId, string symptoms, string diagnosis, int? diseaseId)
        {
            return _visitDAL.UpdateDiagnosis(visitId, symptoms, diagnosis, diseaseId);
        }

        /// <summary>
        /// Lấy danh sách bệnh nhân chờ khám của bác sĩ
        /// </summary>
        public List<AppointmentDTO> GetWaitingList(int doctorId)
        {
            var appointments = _appointmentDAL.GetByDoctor(doctorId);
            List<AppointmentDTO> waitingList = new List<AppointmentDTO>();

            foreach (var apt in appointments)
            {
                if (apt.AppointmentDate.Date == DateTime.Today &&
                    (apt.Status == "Pending" || apt.Status == "Confirmed"))
                {
                    waitingList.Add(apt);
                }
            }

            return waitingList;
        }

        /// <summary>
        /// Xóa lượt khám
        /// </summary>
        public bool DeleteVisit(int visitId)
        {
            return _visitDAL.Delete(visitId);
        }

        /// <summary>
        /// Validate thông tin lượt khám
        /// </summary>
        private void ValidateVisit(VisitDTO visit)
        {
            if (visit == null)
                throw new ArgumentNullException(nameof(visit));

            if (visit.PatientId <= 0)
                throw new ArgumentException("Phải chọn bệnh nhân");

            if (visit.DoctorId <= 0)
                throw new ArgumentException("Phải chọn bác sĩ");

            if (_patientDAL.GetById(visit.PatientId) == null)
                throw new Exception("Bệnh nhân không tồn tại");

            if (_doctorDAL.GetById(visit.DoctorId) == null)
                throw new Exception("Bác sĩ không tồn tại");
        }
    }
}
