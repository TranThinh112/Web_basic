using System;
using System.Collections.Generic;
using HospitalManagement.DAL;
using HospitalManagement.DTO;

namespace HospitalManagement.BLL
{
    public class InvoiceBLL
    {
        private readonly InvoiceDAL _invoiceDAL;
        private readonly VisitDAL _visitDAL;
        private readonly PrescriptionDAL _prescriptionDAL;
        private readonly PrescriptionDetailDAL _prescriptionDetailDAL;

        private readonly ClinicSettingsBLL _settingsBLL;

        public InvoiceBLL()
        {
            _invoiceDAL = new InvoiceDAL();
            _visitDAL = new VisitDAL();
            _prescriptionDAL = new PrescriptionDAL();
            _prescriptionDetailDAL = new PrescriptionDetailDAL();
            _settingsBLL = new ClinicSettingsBLL();
        }

        /// <summary>
        /// Tạo hóa đơn cho lượt khám
        /// </summary>
        public int CreateInvoice(int visitId, string paymentMethod = "Cash")
        {
            VisitDTO visit = _visitDAL.GetById(visitId);
            if (visit == null)
                throw new Exception("Lượt khám không tồn tại");

            // Kiểm tra đã có hóa đơn chưa
            if (_invoiceDAL.GetByVisit(visitId) != null)
                throw new Exception("Lượt khám này đã có hóa đơn");

            decimal totalAmount = CalculateTotal(visitId);

            InvoiceDTO invoice = new InvoiceDTO
            {
                VisitId = visitId,
                PatientId = visit.PatientId,
                TotalAmount = totalAmount,
                PaymentDate = DateTime.Now,
                PaymentMethod = paymentMethod,
                Status = "Pending"
            };

            return _invoiceDAL.Insert(invoice);
        }

        /// <summary>
        /// Lấy hóa đơn theo Id
        /// </summary>
        public InvoiceDTO GetInvoiceById(int invoiceId)
        {
            return _invoiceDAL.GetById(invoiceId);
        }

        /// <summary>
        /// Lấy hóa đơn theo lượt khám
        /// </summary>
        public InvoiceDTO GetInvoiceByVisit(int visitId)
        {
            return _invoiceDAL.GetByVisit(visitId);
        }

        /// <summary>
        /// Lấy hóa đơn theo bệnh nhân
        /// </summary>
        public List<InvoiceDTO> GetInvoicesByPatient(int patientId)
        {
            return _invoiceDAL.GetByPatient(patientId);
        }

        /// <summary>
        /// Lấy hóa đơn theo khoảng thời gian
        /// </summary>
        public List<InvoiceDTO> GetInvoicesByDateRange(DateTime fromDate, DateTime toDate)
        {
            return _invoiceDAL.GetByDateRange(fromDate, toDate);
        }

        /// <summary>
        /// Tính tổng tiền (khám + thuốc)
        /// </summary>
        public decimal CalculateTotal(int visitId)
        {
            decimal total = _settingsBLL.GetClinicHours().ExaminationFee;

            // Cộng thêm tiền thuốc nếu có
            PrescriptionDTO prescription = _prescriptionDAL.GetByVisit(visitId);
            if (prescription != null)
            {
                var details = _prescriptionDetailDAL.GetByPrescription(prescription.PrescriptionId);
                foreach (var detail in details)
                {
                    total += detail.LineTotal;
                }
            }

            return total;
        }

        /// <summary>
        /// Lấy chi tiết hóa đơn (phí khám + thuốc)
        /// </summary>
        public InvoiceBreakdown GetInvoiceBreakdown(int visitId)
        {
            decimal fee = _settingsBLL.GetClinicHours().ExaminationFee;
            InvoiceBreakdown breakdown = new InvoiceBreakdown
            {
                ExaminationFee = fee,
                MedicineTotal = 0,
                MedicineDetails = new List<PrescriptionDetailDTO>()
            };

            PrescriptionDTO prescription = _prescriptionDAL.GetByVisit(visitId);
            if (prescription != null)
            {
                breakdown.MedicineDetails = _prescriptionDetailDAL.GetByPrescription(prescription.PrescriptionId);
                foreach (var detail in breakdown.MedicineDetails)
                {
                    breakdown.MedicineTotal += detail.LineTotal;
                }
            }

            breakdown.GrandTotal = breakdown.ExaminationFee + breakdown.MedicineTotal;
            return breakdown;
        }

        /// <summary>
        /// Cập nhật hóa đơn
        /// </summary>
        public bool UpdateInvoice(InvoiceDTO invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));
            return _invoiceDAL.Update(invoice);
        }

        /// <summary>
        /// Đánh dấu đã thanh toán
        /// </summary>
        public bool MarkAsPaid(int invoiceId)
        {
            return _invoiceDAL.MarkAsPaid(invoiceId);
        }

        /// <summary>
        /// Thanh toán hóa đơn
        /// </summary>
        public bool ProcessPayment(int invoiceId, string paymentMethod)
        {
            InvoiceDTO invoice = _invoiceDAL.GetById(invoiceId);
            if (invoice == null)
                throw new Exception("Hóa đơn không tồn tại");

            if (invoice.Status == "Paid")
                throw new Exception("Hóa đơn đã được thanh toán");

            invoice.PaymentMethod = paymentMethod;
            invoice.PaymentDate = DateTime.Now;
            invoice.Status = "Paid";

            return _invoiceDAL.Update(invoice);
        }

        /// <summary>
        /// Hủy hóa đơn
        /// </summary>
        public bool CancelInvoice(int invoiceId)
        {
            InvoiceDTO invoice = _invoiceDAL.GetById(invoiceId);
            if (invoice == null)
                throw new Exception("Hóa đơn không tồn tại");

            if (invoice.Status == "Paid")
                throw new Exception("Không thể hủy hóa đơn đã thanh toán");

            invoice.Status = "Cancelled";
            return _invoiceDAL.Update(invoice);
        }

        /// <summary>
        /// Xóa hóa đơn
        /// </summary>
        public bool DeleteInvoice(int invoiceId)
        {
            return _invoiceDAL.Delete(invoiceId);
        }

        /// <summary>
        /// Thống kê doanh thu theo khoảng thời gian
        /// </summary>
        public decimal GetTotalRevenue(DateTime fromDate, DateTime toDate)
        {
            var invoices = _invoiceDAL.GetByDateRange(fromDate, toDate);
            decimal total = 0;

            foreach (var invoice in invoices)
            {
                if (invoice.Status == "Paid")
                    total += invoice.TotalAmount;
            }

            return total;
        }
    }

    /// <summary>
    /// Chi tiết hóa đơn
    /// </summary>
    public class InvoiceBreakdown
    {
        public decimal ExaminationFee { get; set; }
        public decimal MedicineTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public List<PrescriptionDetailDTO> MedicineDetails { get; set; }
    }
}
