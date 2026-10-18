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
    /// Form xem lịch sử hóa đơn khám bệnh
    /// </summary>
    public partial class ExaminationInvoiceHistoryForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly InvoiceBLL _invoiceBLL;
        private List<InvoiceDTO> _allInvoices;

        public ExaminationInvoiceHistoryForm()
        {
            InitializeComponent();
            _invoiceBLL = new InvoiceBLL();
            _allInvoices = new List<InvoiceDTO>();

            // Form Style
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            UIHelper.ConfigureModernForm(this);
            UIHelper.ConfigureDevExpressGridView(gridViewInvoices);

            dtpFrom.EditValue = DateTime.Today.AddDays(-30);
            dtpTo.EditValue = DateTime.Today;
            
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DateTime fromDate = dtpFrom.DateTime.Date;
                DateTime toDate = dtpTo.DateTime.Date.AddDays(1);

                // Get invoices in date range
                _allInvoices = _invoiceBLL.GetInvoicesByDateRange(fromDate, toDate);

                // Filter by status
                string statusFilter = cboStatus.Text; // or EditValue if used ComboBox with objects
                if (statusFilter != "Tất cả")
                {
                    string status = "";
                    switch (statusFilter)
                    {
                        case "Chờ TT": status = "Pending"; break;
                        case "Đã TT": status = "Paid"; break;
                    }
                    if (!string.IsNullOrEmpty(status))
                        _allInvoices = _allInvoices.Where(i => i.Status == status).ToList();
                }

                // Filter by search
                string keyword = txtSearch.Text.Trim().ToLower();
                if (!string.IsNullOrEmpty(keyword) && keyword != "mã hđ, tên bn...")
                {
                    _allInvoices = _allInvoices.Where(i =>
                        (i.InvoiceCode ?? "").ToLower().Contains(keyword) ||
                        i.InvoiceId.ToString().Contains(keyword) ||
                        (i.PatientName ?? "").ToLower().Contains(keyword)
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
            
            if (_allInvoices == null || _allInvoices.Count == 0)
                return;

            gridInvoices.DataSource = _allInvoices.Select(i => new
            {
                i.InvoiceId,
                InvoiceCode = i.InvoiceCode ?? $"HD-{i.InvoiceId}",
                PatientName = i.PatientName ?? "---",
                InvoiceDate = i.PaymentDate ?? i.InvoiceDate,
                TotalAmount = i.TotalAmount,
                StatusDisplay = GetStatusDisplay(i.Status)
            }).ToList();
        }

        private string GetStatusDisplay(string status)
        {
            switch (status)
            {
                case "Pending": return "⏳ Chờ TT";
                case "Paid": return "✅ Đã TT";
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
                // dynamic or reflect if anonymous type
                // But DevExpress GridView usually requires real type or it binds to properties.
                // We used anonymous type in Select(). 
                // GridView wraps it in ReadonlyThreadSafeProxyForObjectFromGenericList or similar.
                // Property access works.
                
                // Better way: use concrete class or dynamic. 
                // Since I used anonymous type, I can access properties via reflection or dynamic.
                // OR I can use InvoiceDTO directly and add computed properties there? 
                // But I wanted to format display. 
                // Let's use InvoiceDTO and handle column binding in designer manually set FieldNames which I did.
                // status display needs custom column or CustomColumnDisplayText event.
                // BUT previous BindData uses anonymous type: Select(i => new { ... })
                // So gridViewInvoices.GetRow returns that anonymous object.
                
                int invoiceId = (int)row.GetType().GetProperty("InvoiceId").GetValue(row, null);
                new InvoicePrintForm(invoiceId).ShowDialog();
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
                int invoiceId = (int)row.GetType().GetProperty("InvoiceId").GetValue(row, null);
                
                // Invoke Print Preview directly
                var form = new InvoicePrintForm(invoiceId);
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
