using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.LookAndFeel;
using HospitalManagement.DAL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    public partial class RevenueStatisticsForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly InvoiceDAL _invoiceDAL;
        private readonly MedicineSaleDAL _medicineSaleDAL;

        public RevenueStatisticsForm()
        {
            _invoiceDAL = new InvoiceDAL();
            _medicineSaleDAL = new MedicineSaleDAL();
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            
            InitializeDateDefaults();
            SetupSummaryGrid();
            SetupDetailGrid();
            LoadData();
        }

        private void InitializeDateDefaults()
        {
            deFromDate.DateTime = DateTime.Today.AddMonths(-1);
            deToDate.DateTime = DateTime.Today;
        }

        private void SetupSummaryGrid()
        {
            gridViewSummary.OptionsView.ShowGroupPanel = false;
            gridViewSummary.OptionsView.ColumnAutoWidth = true;
            gridViewSummary.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewSummary.OptionsBehavior.Editable = false;

            gridViewSummary.Columns.Clear();

            GridColumn colDate = new GridColumn();
            colDate.FieldName = "Date";
            colDate.Caption = "Ngày";
            colDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colDate.Width = 120;
            colDate.Visible = true;
            gridViewSummary.Columns.Add(colDate);

            GridColumn colExam = new GridColumn();
            colExam.FieldName = "ExaminationRevenue";
            colExam.Caption = "Phí khám";
            colExam.DisplayFormat.FormatString = "#,##0 đ";
            colExam.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colExam.Width = 150;
            colExam.Visible = true;
            gridViewSummary.Columns.Add(colExam);

            GridColumn colMed = new GridColumn();
            colMed.FieldName = "MedicineRevenue";
            colMed.Caption = "Bán thuốc";
            colMed.DisplayFormat.FormatString = "#,##0 đ";
            colMed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colMed.Width = 150;
            colMed.Visible = true;
            gridViewSummary.Columns.Add(colMed);

            GridColumn colTotal = new GridColumn();
            colTotal.FieldName = "TotalRevenue";
            colTotal.Caption = "Tổng cộng";
            colTotal.DisplayFormat.FormatString = "#,##0 đ";
            colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colTotal.Width = 150;
            colTotal.Visible = true;
            gridViewSummary.Columns.Add(colTotal);

            GridColumn colCount = new GridColumn();
            colCount.FieldName = "TransactionCount";
            colCount.Caption = "Số GD";
            colCount.Width = 80;
            colCount.Visible = true;
            gridViewSummary.Columns.Add(colCount);
        }

        private void SetupDetailGrid()
        {
            gridViewDetails.OptionsView.ShowGroupPanel = false;
            gridViewDetails.OptionsView.ColumnAutoWidth = true;
            gridViewDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewDetails.OptionsBehavior.Editable = false;

            gridViewDetails.Columns.Clear();

            GridColumn colDate = new GridColumn();
            colDate.FieldName = "TransactionDate";
            colDate.Caption = "Ngày";
            colDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colDate.Width = 130;
            colDate.Visible = true;
            gridViewDetails.Columns.Add(colDate);

            GridColumn colType = new GridColumn();
            colType.FieldName = "TransactionType";
            colType.Caption = "Loại";
            colType.Width = 100;
            colType.Visible = true;
            gridViewDetails.Columns.Add(colType);

            GridColumn colCode = new GridColumn();
            colCode.FieldName = "Code";
            colCode.Caption = "Mã giao dịch";
            colCode.Width = 150;
            colCode.Visible = true;
            gridViewDetails.Columns.Add(colCode);

            GridColumn colCustomer = new GridColumn();
            colCustomer.FieldName = "CustomerName";
            colCustomer.Caption = "Khách hàng";
            colCustomer.Width = 180;
            colCustomer.Visible = true;
            gridViewDetails.Columns.Add(colCustomer);

            GridColumn colAmount = new GridColumn();
            colAmount.FieldName = "Amount";
            colAmount.Caption = "Số tiền";
            colAmount.DisplayFormat.FormatString = "#,##0 đ";
            colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colAmount.Width = 120;
            colAmount.Visible = true;
            gridViewDetails.Columns.Add(colAmount);

            GridColumn colPaymentMethod = new GridColumn();
            colPaymentMethod.FieldName = "PaymentMethod";
            colPaymentMethod.Caption = "Thanh toán";
            colPaymentMethod.Width = 110;
            colPaymentMethod.Visible = true;
            gridViewDetails.Columns.Add(colPaymentMethod);

            GridColumn colStatus = new GridColumn();
            colStatus.FieldName = "Status";
            colStatus.Caption = "Trạng thái";
            colStatus.Width = 120;
            colStatus.Visible = true;
            gridViewDetails.Columns.Add(colStatus);
        }

        private void LoadData()
        {
            try
            {
                DateTime fromDate = deFromDate.DateTime.Date;
                DateTime toDate = deToDate.DateTime.Date.AddDays(1).AddSeconds(-1);

                // Get invoices (examination fee)
                var invoices = _invoiceDAL.GetByDateRange(fromDate, toDate)
                    .Where(i => i.Status == "Paid")
                    .ToList();

                // Get medicine sales
                var medicineSales = _medicineSaleDAL.GetByDateRange(fromDate, toDate)
                    .Where(s => s.Status == "Paid" || s.Status == "Dispensed")
                    .ToList();

                // Calculate totals
                decimal totalExamination = invoices.Sum(i => i.TotalAmount);
                decimal totalMedicine = medicineSales.Sum(s => s.TotalAmount);
                decimal grandTotal = totalExamination + totalMedicine;
                int transactionCount = invoices.Count + medicineSales.Count;

                // Update summary labels
                lblTotalExamination.Text = totalExamination.ToString("#,##0") + " đ";
                lblTotalMedicine.Text = totalMedicine.ToString("#,##0") + " đ";
                lblGrandTotal.Text = grandTotal.ToString("#,##0") + " đ";
                lblTransactionCount.Text = transactionCount.ToString();

                // Update summary by day grid
                UpdateSummaryGrid(invoices, medicineSales, fromDate, toDate);

                // Update detail grid
                UpdateDetailGrid(invoices, medicineSales);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummaryGrid(List<InvoiceDTO> invoices, List<MedicineSaleDTO> medicineSales, 
            DateTime fromDate, DateTime toDate)
        {
            var summaryList = new List<DailySummary>();

            // Group invoices by day
            var examByDay = invoices
                .GroupBy(i => (i.PaymentDate ?? i.InvoiceDate).Date)
                .ToDictionary(g => g.Key, g => new { Total = g.Sum(i => i.TotalAmount), Count = g.Count() });

            // Group medicine sales by day
            var medByDay = medicineSales
                .GroupBy(s => (s.PaymentDate ?? s.SaleDate).Date)
                .ToDictionary(g => g.Key, g => new { Total = g.Sum(s => s.TotalAmount), Count = g.Count() });

            // Combine all dates
            var allDates = examByDay.Keys.Union(medByDay.Keys).OrderByDescending(d => d).ToList();

            foreach (var date in allDates)
            {
                decimal examRevenue = examByDay.ContainsKey(date) ? examByDay[date].Total : 0;
                decimal medRevenue = medByDay.ContainsKey(date) ? medByDay[date].Total : 0;
                int examCount = examByDay.ContainsKey(date) ? examByDay[date].Count : 0;
                int medCount = medByDay.ContainsKey(date) ? medByDay[date].Count : 0;

                summaryList.Add(new DailySummary
                {
                    Date = date,
                    ExaminationRevenue = examRevenue,
                    MedicineRevenue = medRevenue,
                    TotalRevenue = examRevenue + medRevenue,
                    TransactionCount = examCount + medCount
                });
            }

            gridSummary.DataSource = summaryList;
            gridViewSummary.RefreshData();
        }

        private void UpdateDetailGrid(List<InvoiceDTO> invoices, List<MedicineSaleDTO> medicineSales)
        {
            var transactions = new List<TransactionItem>();

            // Add invoices
            foreach (var inv in invoices)
            {
                transactions.Add(new TransactionItem
                {
                    TransactionDate = inv.PaymentDate ?? inv.InvoiceDate,
                    TransactionType = "Phí khám",
                    Code = inv.InvoiceCode ?? $"INV-{inv.InvoiceId}",
                    CustomerName = inv.PatientName ?? "N/A",
                    Amount = inv.TotalAmount,
                    PaymentMethod = GetPaymentMethodDisplay(inv.PaymentMethod),
                    Status = GetStatusDisplay(inv.Status)
                });
            }

            // Add medicine sales
            foreach (var sale in medicineSales)
            {
                transactions.Add(new TransactionItem
                {
                    TransactionDate = sale.PaymentDate ?? sale.SaleDate,
                    TransactionType = "Bán thuốc",
                    Code = sale.SaleCode ?? $"MS-{sale.SaleId}",
                    CustomerName = sale.PatientName ?? (sale.IsWalkIn ? sale.WalkInCustomerName ?? "Khách vãng lai" : "N/A"),
                    Amount = sale.TotalAmount,
                    PaymentMethod = GetPaymentMethodDisplay(sale.PaymentMethod),
                    Status = GetStatusDisplay(sale.Status)
                });
            }

            // Sort by date descending
            transactions = transactions.OrderByDescending(t => t.TransactionDate).ToList();

            gridDetails.DataSource = transactions;
            gridViewDetails.RefreshData();
        }

        private string GetPaymentMethodDisplay(string method)
        {
            switch (method)
            {
                case "Cash": return "Tiền mặt";
                case "Card": return "Thẻ";
                case "Transfer": return "Chuyển khoản";
                default: return method ?? "N/A";
            }
        }

        private string GetStatusDisplay(string status)
        {
            switch (status)
            {
                case "Paid": return "Đã thanh toán";
                case "Dispensed": return "Đã phát thuốc";
                case "Pending": return "Chờ thanh toán";
                case "Cancelled": return "Đã hủy";
                default: return status ?? "N/A";
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            if (deFromDate.DateTime > deToDate.DateTime)
            {
                XtraMessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadData();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.FileName = $"DoanhThu_{deFromDate.DateTime:yyyyMMdd}_{deToDate.DateTime:yyyyMMdd}.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    gridDetails.ExportToXlsx(sfd.FileName);
                    XtraMessageBox.Show("Xuất file Excel thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Lỗi xuất file: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// DTO for daily summary display
    /// </summary>
    public class DailySummary
    {
        public DateTime Date { get; set; }
        public decimal ExaminationRevenue { get; set; }
        public decimal MedicineRevenue { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TransactionCount { get; set; }
    }

    /// <summary>
    /// DTO for transaction display in grid
    /// </summary>
    public class TransactionItem
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
