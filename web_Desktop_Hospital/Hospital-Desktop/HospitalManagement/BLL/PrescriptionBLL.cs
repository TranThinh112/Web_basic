using System;
using System.Collections.Generic;
using HospitalManagement.DAL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.BLL
{
    public class PrescriptionBLL
    {
        private readonly PrescriptionDAL _prescriptionDAL;
        private readonly PrescriptionDetailDAL _prescriptionDetailDAL;
        private readonly MedicineDAL _medicineDAL;
        private readonly VisitDAL _visitDAL;

        public PrescriptionBLL()
        {
            _prescriptionDAL = new PrescriptionDAL();
            _prescriptionDetailDAL = new PrescriptionDetailDAL();
            _medicineDAL = new MedicineDAL();
            _visitDAL = new VisitDAL();
        }

        /// <summary>
        /// Tạo đơn thuốc với chi tiết
        /// </summary>
        public int CreatePrescription(int visitId, List<PrescriptionDetailDTO> details)
        {
            // Kiểm tra lượt khám tồn tại
            if (_visitDAL.GetById(visitId) == null)
                throw new Exception("Lượt khám không tồn tại");

            // Kiểm tra đã có đơn thuốc chưa
            if (_prescriptionDAL.GetByVisit(visitId) != null)
                throw new Exception("Lượt khám này đã có đơn thuốc");

            // Validate chi tiết đơn thuốc
            if (details == null || details.Count == 0)
                throw new ArgumentException("Đơn thuốc phải có ít nhất một loại thuốc");

            foreach (var detail in details)
            {
                ValidateDetail(detail);
            }

            // Tạo đơn thuốc
            PrescriptionDTO prescription = new PrescriptionDTO
            {
                VisitId = visitId,
                CreatedDate = DateTime.Now
            };

            int prescriptionId = _prescriptionDAL.Insert(prescription);

            // Thêm chi tiết
            foreach (var detail in details)
            {
                detail.PrescriptionId = prescriptionId;
                _prescriptionDetailDAL.Insert(detail);
            }

            return prescriptionId;
        }

        /// <summary>
        /// Lấy đơn thuốc theo Id
        /// </summary>
        public PrescriptionDTO GetPrescriptionById(int prescriptionId)
        {
            return _prescriptionDAL.GetById(prescriptionId);
        }

        /// <summary>
        /// Lấy đơn thuốc theo lượt khám
        /// </summary>
        public PrescriptionDTO GetPrescriptionByVisit(int visitId)
        {
            return _prescriptionDAL.GetByVisit(visitId);
        }

        /// <summary>
        /// Lấy đơn thuốc theo ngày
        /// </summary>
        public List<PrescriptionDTO> GetPrescriptionsByDate(DateTime date)
        {
            return _prescriptionDAL.GetByDate(date);
        }

        /// <summary>
        /// Lấy chi tiết đơn thuốc
        /// </summary>
        public List<PrescriptionDetailDTO> GetPrescriptionDetails(int prescriptionId)
        {
            return _prescriptionDetailDAL.GetByPrescription(prescriptionId);
        }

        /// <summary>
        /// Thêm thuốc vào đơn
        /// </summary>
        public bool AddMedicineToPrescription(PrescriptionDetailDTO detail)
        {
            ValidateDetail(detail);
            return _prescriptionDetailDAL.Insert(detail);
        }

        /// <summary>
        /// Cập nhật chi tiết đơn thuốc
        /// </summary>
        public bool UpdatePrescriptionDetail(PrescriptionDetailDTO detail)
        {
            ValidateDetail(detail);
            return _prescriptionDetailDAL.Update(detail);
        }

        /// <summary>
        /// Xóa thuốc khỏi đơn
        /// </summary>
        public bool RemoveMedicineFromPrescription(int prescriptionId, int medicineId)
        {
            return _prescriptionDetailDAL.Delete(prescriptionId, medicineId);
        }

        /// <summary>
        /// Tính tổng tiền thuốc
        /// </summary>
        public decimal CalculateTotalCost(int prescriptionId)
        {
            var details = _prescriptionDetailDAL.GetByPrescription(prescriptionId);
            decimal total = 0;

            foreach (var detail in details)
            {
                total += detail.LineTotal;
            }

            return total;
        }

        /// <summary>
        /// Tạo hoặc cập nhật đơn thuốc
        /// </summary>
        public int CreateOrUpdatePrescription(int visitId, List<PrescriptionDetailDTO> details)
        {
            // Kiểm tra lượt khám tồn tại
            if (_visitDAL.GetById(visitId) == null)
                throw new Exception("Lượt khám không tồn tại");

            // Validate chi tiết đơn thuốc
            if (details == null || details.Count == 0)
                throw new ArgumentException("Đơn thuốc phải có ít nhất một loại thuốc");

            foreach (var detail in details)
            {
                ValidateDetail(detail);
            }

            // Kiểm tra đã có đơn thuốc chưa
            var existingPrescription = _prescriptionDAL.GetByVisit(visitId);
            int prescriptionId;

            if (existingPrescription != null)
            {
                // Xóa chi tiết cũ
                _prescriptionDetailDAL.DeleteByPrescription(existingPrescription.PrescriptionId);
                prescriptionId = existingPrescription.PrescriptionId;
            }
            else
            {
                // Tạo đơn thuốc mới
                PrescriptionDTO prescription = new PrescriptionDTO
                {
                    VisitId = visitId,
                    CreatedDate = DateTime.Now
                };
                prescriptionId = _prescriptionDAL.Insert(prescription);
            }

            // Thêm chi tiết mới
            foreach (var detail in details)
            {
                detail.PrescriptionId = prescriptionId;
                _prescriptionDetailDAL.Insert(detail);
            }

            return prescriptionId;
        }

        /// <summary>
        /// Xóa đơn thuốc
        /// </summary>
        public bool DeletePrescription(int prescriptionId)
        {
            // Xóa chi tiết trước
            _prescriptionDetailDAL.DeleteByPrescription(prescriptionId);
            // Sau đó xóa đơn thuốc
            return _prescriptionDAL.Delete(prescriptionId);
        }

        /// <summary>
        /// Validate chi tiết đơn thuốc
        /// </summary>
        private void ValidateDetail(PrescriptionDetailDTO detail)
        {
            if (detail == null)
                throw new ArgumentNullException(nameof(detail));

            if (detail.MedicineId <= 0)
                throw new ArgumentException("Phải chọn thuốc");

            if (!ValidationHelper.ValidateQuantity(detail.Quantity))
                throw new ArgumentException("Số lượng phải lớn hơn 0");

            if (_medicineDAL.GetById(detail.MedicineId) == null)
                throw new Exception("Thuốc không tồn tại trong danh mục");
        }
    }
}
