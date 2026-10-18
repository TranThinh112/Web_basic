using System;
using System.Collections.Generic;
using HospitalManagement.DAL;
using HospitalManagement.DTO;

namespace HospitalManagement.BLL
{
    /// <summary>
    /// Business Logic cho bán thuốc tại quầy dược
    /// </summary>
    public class MedicineSaleBLL
    {
        private readonly MedicineSaleDAL _saleDAL;
        private readonly MedicineSaleDetailDAL _detailDAL;
        private readonly MedicineDAL _medicineDAL;
        private readonly PrescriptionDAL _prescriptionDAL;
        private readonly PrescriptionDetailDAL _prescriptionDetailDAL;

        public MedicineSaleBLL()
        {
            _saleDAL = new MedicineSaleDAL();
            _detailDAL = new MedicineSaleDetailDAL();
            _medicineDAL = new MedicineDAL();
            _prescriptionDAL = new PrescriptionDAL();
            _prescriptionDetailDAL = new PrescriptionDetailDAL();
        }

        #region Sale Operations

        /// <summary>
        /// Tạo đơn bán thuốc mới (Dược sĩ)
        /// </summary>
        public int CreateSale(int? patientId, int? visitId, int? prescriptionId, int pharmacistId, string notes = null)
        {
            var sale = new MedicineSaleDTO
            {
                PatientId = patientId,
                VisitId = visitId,
                PrescriptionId = prescriptionId,
                PharmacistId = pharmacistId,
                SaleDate = DateTime.Now,
                Status = "Pending",
                Notes = notes
            };

            return _saleDAL.Insert(sale);
        }

        /// <summary>
        /// Tạo đơn bán từ đơn thuốc BS
        /// </summary>
        public int CreateSaleFromPrescription(int prescriptionId, int pharmacistId)
        {
            var prescription = _prescriptionDAL.GetByPrescriptionId(prescriptionId);
            if (prescription == null)
                throw new Exception("Đơn thuốc không tồn tại");

            // Lấy thông tin visit để có patientId
            var visit = new VisitDAL().GetById(prescription.VisitId);
            
            // Tạo sale
            int saleId = CreateSale(visit?.PatientId, prescription.VisitId, prescriptionId, pharmacistId);

            // Copy chi tiết từ đơn thuốc
            var prescriptionDetails = _prescriptionDetailDAL.GetByPrescription(prescriptionId);
            decimal total = 0;

            foreach (var pd in prescriptionDetails)
            {
                var medicine = _medicineDAL.GetById(pd.MedicineId);
                if (medicine == null) continue;

                // Kiểm tra tồn kho
                if (medicine.StockQuantity < pd.Quantity)
                    throw new Exception($"Thuốc '{medicine.MedicineName}' không đủ tồn kho (cần {pd.Quantity}, còn {medicine.StockQuantity})");

                var detail = new MedicineSaleDetailDTO
                {
                    SaleId = saleId,
                    MedicineId = pd.MedicineId,
                    Quantity = pd.Quantity,
                    UnitPrice = medicine.Price
                };
                _detailDAL.Insert(detail);
                total += detail.LineTotal;
            }

            // Cập nhật tổng tiền
            var sale = _saleDAL.GetById(saleId);
            sale.TotalAmount = total;
            _saleDAL.Update(sale);

            return saleId;
        }

        /// <summary>
        /// Lấy đơn bán theo ID
        /// </summary>
        public MedicineSaleDTO GetSaleById(int saleId)
        {
            return _saleDAL.GetById(saleId);
        }

        /// <summary>
        /// Lấy danh sách đơn chờ thanh toán
        /// </summary>
        public List<MedicineSaleDTO> GetPendingPayments()
        {
            return _saleDAL.GetPendingPayments();
        }

        /// <summary>
        /// Lấy danh sách đơn đã thanh toán chờ phát thuốc
        /// </summary>
        public List<MedicineSaleDTO> GetPaidNotDispensed()
        {
            return _saleDAL.GetPaidNotDispensed();
        }

        /// <summary>
        /// Lấy đơn bán theo ngày
        /// </summary>
        public List<MedicineSaleDTO> GetSalesByDate(DateTime date)
        {
            return _saleDAL.GetByDate(date);
        }

        /// <summary>
        /// Lấy đơn bán theo khoảng thời gian
        /// </summary>
        public List<MedicineSaleDTO> GetSalesByDateRange(DateTime fromDate, DateTime toDate)
        {
            return _saleDAL.GetByDateRange(fromDate, toDate);
        }

        #endregion

        #region Detail Operations

        /// <summary>
        /// Thêm thuốc vào đơn bán
        /// </summary>
        public void AddItem(int saleId, int medicineId, int quantity)
        {
            var medicine = _medicineDAL.GetById(medicineId);
            if (medicine == null)
                throw new Exception("Thuốc không tồn tại");

            if (medicine.StockQuantity < quantity)
                throw new Exception($"Thuốc '{medicine.MedicineName}' không đủ tồn kho");

            var detail = new MedicineSaleDetailDTO
            {
                SaleId = saleId,
                MedicineId = medicineId,
                Quantity = quantity,
                UnitPrice = medicine.Price
            };
            _detailDAL.Insert(detail);

            // Cập nhật tổng tiền
            RecalculateTotal(saleId);
        }

        /// <summary>
        /// Xóa thuốc khỏi đơn bán
        /// </summary>
        public void RemoveItem(int detailId, int saleId)
        {
            _detailDAL.Delete(detailId);
            RecalculateTotal(saleId);
        }

        /// <summary>
        /// Lấy chi tiết đơn bán
        /// </summary>
        public List<MedicineSaleDetailDTO> GetSaleDetails(int saleId)
        {
            return _detailDAL.GetBySale(saleId);
        }

        /// <summary>
        /// Tính lại tổng tiền
        /// </summary>
        private void RecalculateTotal(int saleId)
        {
            var details = _detailDAL.GetBySale(saleId);
            decimal total = 0;
            foreach (var d in details) total += d.LineTotal;

            var sale = _saleDAL.GetById(saleId);
            sale.TotalAmount = total;
            _saleDAL.Update(sale);
        }

        #endregion

        #region Payment & Dispensing

        /// <summary>
        /// Thu ngân xác nhận thanh toán
        /// </summary>
        public bool ProcessPayment(int saleId, int cashierId, string paymentMethod = "Cash")
        {
            var sale = _saleDAL.GetById(saleId);
            if (sale == null)
                throw new Exception("Đơn bán không tồn tại");

            if (sale.Status != "Pending")
                throw new Exception("Đơn này không ở trạng thái chờ thanh toán");

            sale.CashierId = cashierId;
            sale.PaymentMethod = paymentMethod;
            sale.PaymentDate = DateTime.Now;
            sale.Status = "Paid";

            return _saleDAL.Update(sale);
        }

        /// <summary>
        /// Dược sĩ xác nhận đã phát thuốc
        /// </summary>
        public bool DispenseMedicine(int saleId)
        {
            var sale = _saleDAL.GetById(saleId);
            if (sale == null)
                throw new Exception("Đơn bán không tồn tại");

            if (sale.Status != "Paid")
                throw new Exception("Đơn chưa được thanh toán");

            // Trừ tồn kho
            var details = _detailDAL.GetBySale(saleId);
            foreach (var d in details)
            {
                _medicineDAL.UpdateStock(d.MedicineId, -d.Quantity);
            }

            sale.Status = "Dispensed";
            return _saleDAL.Update(sale);
        }

        /// <summary>
        /// Hủy đơn bán
        /// </summary>
        public bool CancelSale(int saleId)
        {
            var sale = _saleDAL.GetById(saleId);
            if (sale == null)
                throw new Exception("Đơn bán không tồn tại");

            if (sale.Status == "Dispensed")
                throw new Exception("Không thể hủy đơn đã phát thuốc");

            sale.Status = "Cancelled";
            return _saleDAL.Update(sale);
        }

        #endregion
    }
}
