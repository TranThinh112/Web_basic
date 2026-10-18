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
    /// Form thu phí khám - Thu ngân sử dụng
    /// </summary>
    public partial class ExaminationFeeForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly VisitBLL _visitBLL;
        private readonly InvoiceBLL _invoiceBLL;
        private readonly AppointmentBLL _appointmentBLL;
        private readonly ClinicSettingsBLL _settingsBLL;
        private List<AppointmentDTO> _allAppointments;
        private List<AppointmentDTO> _filteredAppointments;
        private decimal _examinationFee; 

        // Pagination
        private PaginationHelper _pagination;
        private Timer _debounceTimer;

        public ExaminationFeeForm()
        {
            _visitBLL = new VisitBLL();
            _invoiceBLL = new InvoiceBLL();
            _appointmentBLL = new AppointmentBLL();
            _settingsBLL = new ClinicSettingsBLL();
            _allAppointments = new List<AppointmentDTO>();
            _filteredAppointments = new List<AppointmentDTO>();

            // Load fee
            try 
            {
                _examinationFee = _settingsBLL.GetClinicHours().ExaminationFee;
            }
            catch
            {
                _examinationFee = 100000; // Fallback
            }

            InitializeComponent();
            
            // Set skin
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            
            lblFee.Text = $"Phí khám: {_examinationFee:N0} VNĐ";
            dtpDate.EditValue = DateTime.Now;

            // Setup Pagination
            _pagination = new PaginationHelper(20);
            _pagination.PageChanged += OnPageChanged;
            
            // Add Pagination Panel to Footer or Bottom of Grid
            Panel pagingPanel = _pagination.CreatePaginationPanel();
            pagingPanel.Dock = DockStyle.Bottom;
            pagingPanel.Parent = this.pnlFooter; 
            // The footer panel already contains lblCount. We might need to adjust layout.
            // Let's add it to the main form controls, docked bottom, above footer.
            // Or simpler: Add to pnlFooter and rearrange.
            
            // Better approach for existing PanelControl layout: 
            // Add to pnlFooter, dock Right or fill remaining space?
            // Since pnlFooter is Dock=Bottom, let's put pagingPanel inside it.
            // But pnlFooter is small (40px). Paging panel is also 40px.
            // Check pnlFooter contents: lblCount.
            
            pnlFooter.Controls.Add(pagingPanel);
            pagingPanel.BringToFront();
            pagingPanel.Dock = DockStyle.Right; // Navigation on the right

            // Adjust lblCount position if needed
            lblCount.Dock = DockStyle.Left;
            lblCount.Padding = new Padding(10, 10, 0, 0);

            // Search filtering
            SetupSearch();
            
            LoadData();
        }

        private void SetupSearch()
        {
            _debounceTimer = new Timer();
            _debounceTimer.Interval = 300;
            _debounceTimer.Tick += DebounceTimer_Tick;
            txtSearch.EditValueChanged += TxtSearch_EditValueChanged;
        }

        private void TxtSearch_EditValueChanged(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            PerformSearch();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dtpDate.EditValue == null) return;
                
                DateTime date = (DateTime)dtpDate.EditValue;

                // Load confirmed appointments
                var allAppointments = _appointmentBLL.GetAppointmentsByDate(date);
                _allAppointments = allAppointments.Where(a => a.Status == "Confirmed").ToList();
                
                // Copy to filtered (no search keyword on load)
                _filteredAppointments = new List<AppointmentDTO>(_allAppointments);

                // Debug title
                this.Text = $"Thu phí khám - Ngày {date:dd/MM/yyyy} - Tìm thấy: {_allAppointments.Count}";

                // Update pagination and bind directly
                _pagination.SetTotalItems(_filteredAppointments.Count);
                LoadCurrentPage();
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

        private void PerformSearch()
        {
            string keyword = (txtSearch.Text ?? "").Trim().ToLower();
            
            if (string.IsNullOrWhiteSpace(keyword))
            {
                _filteredAppointments = new List<AppointmentDTO>(_allAppointments);
            }
            else
            {
                _filteredAppointments = _allAppointments.Where(a => 
                    (a.AppointmentId.ToString()).Contains(keyword) ||
                    (a.PatientName ?? "").ToLower().Contains(keyword) ||
                    (a.PatientPhone ?? "").Contains(keyword)
                ).ToList();
            }
            
            // Update pagination
            _pagination.SetTotalItems(_filteredAppointments.Count);
            _pagination.ResetToFirstPage();
            
            // Bind data to grid
            LoadCurrentPage();
        }

        private void OnPageChanged(int page)
        {
            LoadCurrentPage();
        }

        private void LoadCurrentPage()
        {
            int skip = _pagination.GetSkipCount();
            var pageData = _filteredAppointments.Skip(skip).Take(_pagination.PageSize).ToList();
            BindData(pageData);
        }

        private void BindData(List<AppointmentDTO> appointments)
        {
            gridVisits.DataSource = null;
            if (appointments == null || appointments.Count == 0)
            {
                lblCount.Text = $"Hiển thị: 0 / Tổng: {_filteredAppointments?.Count ?? 0} bệnh nhân";
                return;
            }
            
            gridVisits.DataSource = appointments;
            lblCount.Text = $"Hiển thị: {appointments.Count} / Tổng: {_filteredAppointments.Count} bệnh nhân";
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            new ExaminationInvoiceHistoryForm().ShowDialog();
        }

        private void BtnPayCash_Click(object sender, EventArgs e)
        {
            ProcessPayment("Cash");
        }

        private void BtnPayCard_Click(object sender, EventArgs e)
        {
            ProcessPayment("Card");
        }

        private void BtnPayTransfer_Click(object sender, EventArgs e)
        {
            ProcessPayment("Transfer");
        }

        private void ProcessPayment(string paymentMethod)
        {
            var selectedApp = gridViewVisits.GetFocusedRow() as AppointmentDTO;
            
            if (selectedApp == null)
            {
                XtraMessageBox.Show("Vui lòng chọn lượt khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string methodDisplay = "";
                switch (paymentMethod)
                {
                    case "Cash": methodDisplay = "Tiền mặt"; break;
                    case "Card": methodDisplay = "Thẻ"; break;
                    case "Transfer": methodDisplay = "Chuyển khoản"; break;
                }

                if (XtraMessageBox.Show(
                    $"Xác nhận thu phí khám:\n\n" +
                    $"Bệnh nhân: {selectedApp.PatientName}\n" +
                    $"Số tiền: {_examinationFee:N0} VNĐ\n" +
                    $"Phương thức: {methodDisplay}",
                    "Xác nhận thanh toán",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // 1. Create visit from appointment
                    int visitId = _visitBLL.CreateVisitFromAppointment(selectedApp.AppointmentId);

                    // 2. Create invoice and mark as paid
                    int invoiceId = _invoiceBLL.CreateInvoice(visitId, paymentMethod); 
                    _invoiceBLL.MarkAsPaid(invoiceId);

                    // 3. Update visit status to Waiting
                    _visitBLL.UpdateVisitStatus(visitId, "Waiting");

                    // Ask to print
                    if (XtraMessageBox.Show(
                        $"✅ Thanh toán thành công!\n\n" +
                        $"Mã hóa đơn: {invoiceId}\n" +
                        $"Số tiền: {_examinationFee:N0} VNĐ\n\n" +
                        "Bạn có muốn in hóa đơn không?",
                        "Thành công",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        new InvoicePrintForm(invoiceId).ShowDialog();
                    }

                    LoadData();
                }
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
