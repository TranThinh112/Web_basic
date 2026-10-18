using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    /// <summary>
    /// Form xem lịch sử hóa đơn thuốc
    /// </summary>
    public partial class InvoiceHistoryForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly MedicineSaleBLL _saleBLL;
        private List<MedicineSaleDTO> _allSales;

        public InvoiceHistoryForm()
        {
            InitializeComponent();
            _saleBLL = new MedicineSaleBLL();
            _allSales = new List<MedicineSaleDTO>();

            // Form Style
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            UIHelper.ConfigureModernForm(this);
            UIHelper.ConfigureDevExpressGridView(gridViewInvoices);
            
            dtpFrom.EditValue = DateTime.Today.AddDays(-30);
            dtpTo.EditValue = DateTime.Today;
            
            gridViewInvoices.DoubleClick += DgvInvoices_DoubleClick;
            
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DateTime fromDate = dtpFrom.DateTime.Date;
                DateTime toDate = dtpTo.DateTime.Date.AddDays(1);

                // Get sales in date range
                _allSales = _saleBLL.GetSalesByDateRange(fromDate, toDate);

                // Filter by status
                string statusFilter = cboStatus.Text; 
                if (statusFilter != "Tất cả")
                {
                    string status = "";
                    switch (statusFilter)
                    {
                        case "Chờ TT": status = "Pending"; break;
                        case "Đã TT": status = "Paid"; break;
                        case "Đã phát": status = "Dispensed"; break;
                        case "Đã hủy": status = "Cancelled"; break;
                    }
                    if (!string.IsNullOrEmpty(status))
                        _allSales = _allSales.Where(s => s.Status == status).ToList();
                }

                // Filter by search
                string keyword = txtSearch.Text.Trim().ToLower();
                if (!string.IsNullOrEmpty(keyword) && keyword != "mã đơn, tên kh...")
                {
                    _allSales = _allSales.Where(s =>
                        (s.SaleCode ?? "").ToLower().Contains(keyword) ||
                        (s.PatientName ?? "").ToLower().Contains(keyword) ||
                        (s.WalkInCustomerName ?? "").ToLower().Contains(keyword)
                    ).ToList();
                }

                BindData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void BindData()
        {
            gridInvoices.DataSource = null;
            
            if (_allSales == null || _allSales.Count == 0)
                return;

            gridInvoices.DataSource = _allSales.Select(s => {
                string customerName = s.IsWalkIn ? (s.WalkInCustomerName ?? "Khách vãng lai") : s.PatientName;
                if (string.IsNullOrWhiteSpace(customerName)) customerName = "Khách vãng lai";
                return new
                {
                    s.SaleId,
                    SaleCode = s.SaleCode ?? $"MS-{s.SaleId}",
                    CustomerName = customerName,
                    SaleDate = s.SaleDate,
                    TotalAmount = s.TotalAmount,
                    StatusDisplay = s.StatusDisplay ?? GetStatusDisplay(s.Status)
                };
            }).ToList();
        }

        private string GetStatusDisplay(string status)
        {
            switch (status)
            {
                case "Pending": return "⏳ Chờ TT";
                case "Paid": return "💰 Đã TT";
                case "Dispensed": return "✅ Đã phát";
                case "Cancelled": return "❌ Đã hủy";
                default: return status ?? "";
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 0;
            txtSearch.Text = "";
            dtpFrom.EditValue = DateTime.Today.AddDays(-30);
            dtpTo.EditValue = DateTime.Today;
            LoadData();
        }

        private void DgvInvoices_DoubleClick(object sender, EventArgs e)
        {
            BtnView_Click(sender, e);
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            if (gridViewInvoices.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Vui lòng chọn hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                object row = gridViewInvoices.GetRow(gridViewInvoices.FocusedRowHandle);
                int saleId = (int)row.GetType().GetProperty("SaleId").GetValue(row, null);
                new MedicineSaleInvoiceForm(saleId).ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (gridViewInvoices.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Vui lòng chọn hóa đơn để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                object row = gridViewInvoices.GetRow(gridViewInvoices.FocusedRowHandle);
                int saleId = (int)row.GetType().GetProperty("SaleId").GetValue(row, null);
                
                // Invoke Print Preview directly
                var form = new MedicineSaleInvoiceForm(saleId);
                form.ShowPrintPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
