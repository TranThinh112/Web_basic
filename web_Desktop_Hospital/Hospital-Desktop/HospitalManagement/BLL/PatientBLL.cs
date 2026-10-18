using System;
using System.Collections.Generic;
using HospitalManagement.DAL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.BLL
{
    public class PatientBLL
    {
        private readonly PatientDAL _patientDAL;
        private readonly VisitDAL _visitDAL;

        public PatientBLL()
        {
            _patientDAL = new PatientDAL();
            _visitDAL = new VisitDAL();
        }

        /// <summary>
        /// Đăng ký bệnh nhân mới
        /// </summary>
        public int RegisterPatient(PatientDTO patient)
        {
            ValidatePatient(patient);

            if (!string.IsNullOrWhiteSpace(patient.CCCD) && _patientDAL.CCCDExists(patient.CCCD))
                throw new Exception("Số CCCD đã tồn tại trong hệ thống");

            if (!string.IsNullOrWhiteSpace(patient.Phone) && _patientDAL.PhoneExists(patient.Phone))
                throw new Exception("Số điện thoại đã được sử dụng bởi bệnh nhân khác");

            // Tạo mã bệnh nhân tự động
            int patientId = _patientDAL.Insert(patient);
            
            // Cập nhật PatientCode sau khi insert
            patient.PatientId = patientId;
            patient.PatientCode = "BN" + patientId.ToString("D4"); // BN0001, BN0002, ...
            _patientDAL.Update(patient);

            return patientId;
        }

        /// <summary>
        /// Lấy thông tin bệnh nhân theo Id
        /// </summary>
        public PatientDTO GetPatientById(int patientId)
        {
            return _patientDAL.GetById(patientId);
        }

        /// <summary>
        /// Lấy thông tin bệnh nhân theo CCCD
        /// </summary>
        public PatientDTO GetPatientByCCCD(string cccd)
        {
            if (string.IsNullOrWhiteSpace(cccd))
                return null;
            return _patientDAL.GetByCCCD(cccd);
        }

        /// <summary>
        /// Lấy tất cả bệnh nhân
        /// </summary>
        public List<PatientDTO> GetAllPatients()
        {
            return _patientDAL.GetAll();
        }

        /// <summary>
        /// Tìm kiếm bệnh nhân theo từ khóa (tên, CCCD, SĐT)
        /// </summary>
        public List<PatientDTO> SearchPatients(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _patientDAL.GetAll();
            return _patientDAL.Search(keyword);
        }

        /// <summary>
        /// Lấy bệnh nhân có phân trang và sắp xếp
        /// </summary>
        public List<PatientDTO> GetPatientsPaged(string keyword, int page, int pageSize, string sortColumn = null, bool sortAsc = true)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _patientDAL.GetPaged(page, pageSize, sortColumn, sortAsc);
            return _patientDAL.SearchPaged(keyword, page, pageSize, sortColumn, sortAsc);
        }

        /// <summary>
        /// Đếm tổng số bệnh nhân (theo keyword nếu có)
        /// </summary>
        public int GetPatientCount(string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _patientDAL.GetCount();
            return _patientDAL.SearchCount(keyword);
        }

        /// <summary>
        /// Cập nhật thông tin bệnh nhân
        /// </summary>
        public bool UpdatePatient(PatientDTO patient)
        {
            ValidatePatient(patient);

            if (!string.IsNullOrWhiteSpace(patient.CCCD) && _patientDAL.CCCDExists(patient.CCCD, patient.PatientId))
                throw new Exception("Số CCCD đã được sử dụng bởi bệnh nhân khác");

            if (!string.IsNullOrWhiteSpace(patient.Phone) && _patientDAL.PhoneExists(patient.Phone, patient.PatientId))
                throw new Exception("Số điện thoại đã được sử dụng bởi bệnh nhân khác");

            return _patientDAL.Update(patient);
        }

        /// <summary>
        /// Xóa bệnh nhân
        /// </summary>
        public bool DeletePatient(int patientId)
        {
            // Kiểm tra xem bệnh nhân có lịch sử khám không
            var visits = _visitDAL.GetByPatient(patientId);
            if (visits != null && visits.Count > 0)
                throw new Exception("Không thể xóa bệnh nhân đã có lịch sử khám bệnh");

            return _patientDAL.Delete(patientId);
        }

        /// <summary>
        /// Lấy lịch sử khám bệnh của bệnh nhân
        /// </summary>
        public List<VisitDTO> GetPatientHistory(int patientId)
        {
            return _visitDAL.GetByPatient(patientId);
        }

        /// <summary>
        /// Kiểm tra CCCD đã tồn tại chưa
        /// </summary>
        public bool CheckDuplicateCCCD(string cccd, int? excludePatientId = null)
        {
            if (string.IsNullOrWhiteSpace(cccd))
                return false;
            return _patientDAL.CCCDExists(cccd, excludePatientId);
        }

        /// <summary>
        /// Cập nhật tiền sử bệnh
        /// </summary>
        public bool UpdateMedicalHistory(int patientId, string medicalHistory)
        {
            PatientDTO patient = _patientDAL.GetById(patientId);
            if (patient == null)
                throw new Exception("Không tìm thấy bệnh nhân");

            patient.MedicalHistory = medicalHistory;
            return _patientDAL.Update(patient);
        }

        /// <summary>
        /// Validate thông tin bệnh nhân
        /// </summary>
        private void ValidatePatient(PatientDTO patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            if (!ValidationHelper.ValidateFullName(patient.FullName))
                throw new ArgumentException("Họ tên bệnh nhân không hợp lệ");

            if (!ValidationHelper.ValidateDateOfBirth(patient.DateOfBirth))
                throw new ArgumentException("Ngày sinh không hợp lệ");

            if (!string.IsNullOrWhiteSpace(patient.Phone) && !ValidationHelper.ValidatePhone(patient.Phone))
                throw new ArgumentException("Số điện thoại không hợp lệ (10-11 số, bắt đầu bằng 0)");

            if (!string.IsNullOrWhiteSpace(patient.CCCD) && !ValidationHelper.ValidateIdentityCard(patient.CCCD))
                throw new ArgumentException("Số CCCD/CMND không hợp lệ");

            if (!string.IsNullOrWhiteSpace(patient.Gender) && !ValidationHelper.ValidateGender(patient.Gender))
                throw new ArgumentException("Giới tính không hợp lệ");
        }
    }
}
