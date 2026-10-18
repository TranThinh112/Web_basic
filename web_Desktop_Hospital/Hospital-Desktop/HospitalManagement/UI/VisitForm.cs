using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DAL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    public partial class VisitForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly VisitBLL _visitBLL;
        private readonly InvoiceDAL _invoiceDAL;
        
        private List<VisitDTO> _allVisits;
        private List<VisitDTO> _filteredVisits;
        private List<VisitDTO> _currentPageData;

        // Pagination
        private PaginationHelper _pagination;
        private Timer _debounceTimer;

        public VisitForm()
        {
            _visitBLL = new VisitBLL();
            _invoiceDAL = new InvoiceDAL();
            _allVisits = new List<VisitDTO>();
            _filteredVisits = new List<VisitDTO>();
            _currentPageData = new List<VisitDTO>();

            InitializeComponent();
            
            // Setup Pagination
            _pagination = new PaginationHelper(20);
            _pagination.PageChanged += OnPageChanged;

            // Add Pagination Panel
            Panel pagingPanel = _pagination.CreatePaginationPanel();
            pagingPanel.Parent = this;
            
            // Add to LayoutControl
            var item = layoutControl.Root.AddItem();
            item.Control = pagingPanel;
            item.TextVisible = false;
            
            // Move it to bottom above buttons/count
            item.Move(layoutControlItemGrid, DevExpress.XtraLayout.Utils.InsertType.Bottom);

            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            gridView.OptionsBehavior.AutoPopulateColumns = false;
        }

        private void VisitForm_Load(object sender, EventArgs e)
        {
            UIHelper.ConfigureModernForm(this);
            UIHelper.ConfigureDevExpressGridView(gridView);
            UIHelper.ClearGridViewSelection(gridView);
            
            // Standardize Buttons
            UIHelper.StylePrimaryButton(btnFilter);
            btnFilter.Text = "Lọc";
            
            UIHelper.StyleSecondaryButton(btnToday);
            btnToday.Text = "Hôm nay";
            
            UIHelper.StylePrimaryButton(btnExamine); // Base style
            UIHelper.StyleSuccessButton(btnComplete); // Base style
            btnComplete.Text = "Hoàn thành";

            UIHelper.StyleSecondaryButton(btnRefresh);
            btnRefresh.Text = "Làm mới";
            
            UIHelper.StyleCloseButton(btnClose);

            dtpDate.EditValue = DateTime.Today;
            SetupSearch();
            LoadData();
            UpdateButtonStates(null);
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

        private void PerformSearch()
        {
            string keyword = (txtSearch.Text ?? "").Trim().ToLower();
            
            if (string.IsNullOrWhiteSpace(keyword))
                _filteredVisits = new List<VisitDTO>(_allVisits);
            else
                _filteredVisits = _allVisits.Where(v =>
                    (v.PatientName ?? "").ToLower().Contains(keyword) ||
                    (v.DoctorName ?? "").ToLower().Contains(keyword) ||
                    (v.Diagnosis ?? "").ToLower().Contains(keyword)
                ).ToList();
            
            // Update pagination
            _pagination.SetTotalItems(_filteredVisits.Count);
            _pagination.ResetToFirstPage();
        }

        private void gridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            
            string status = gridView.GetRowCellValue(e.RowHandle, "Status")?.ToString() ?? "Waiting";
            
            switch (status)
            {
                case "Waiting":
                    e.Appearance.BackColor = Color.FromArgb(255, 248, 225);
                    break;
                case "InProgress":
                    e.Appearance.BackColor = Color.FromArgb(227, 242, 253);
                    break;
                case "Completed":
                    e.Appearance.BackColor = Color.FromArgb(232, 245, 233);
                    e.Appearance.ForeColor = Color.FromArgb(100, 100, 100);
                    break;
            }
        }

        private void gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "StatusDisplay") return;
            
            string status = gridView.GetRowCellValue(e.RowHandle, "Status")?.ToString() ?? "Waiting";
            string displayText = e.CellValue?.ToString() ?? "";
            
            // Define colors based on status
            Color bgColor, textColor;
            switch (status)
            {
                case "Waiting":
                    bgColor = Color.FromArgb(255, 193, 7);  // Yellow/Amber
                    textColor = Color.Black;
                    break;
                case "InProgress":
                    bgColor = Color.FromArgb(33, 150, 243); // Blue
                    textColor = Color.White;
                    break;
                case "Completed":
                    bgColor = Color.FromArgb(76, 175, 80);  // Green
                    textColor = Color.White;
                    break;
                default:
                    bgColor = Color.Gray;
                    textColor = Color.White;
                    break;
            }

            // Draw badge background
            Rectangle badgeRect = e.Bounds;
            badgeRect.Inflate(-4, -3);
            
            using (var brush = new SolidBrush(bgColor))
            {
                // Draw rounded rectangle
                int radius = 8;
                var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddArc(badgeRect.X, badgeRect.Y, radius, radius, 180, 90);
                path.AddArc(badgeRect.Right - radius, badgeRect.Y, radius, radius, 270, 90);
                path.AddArc(badgeRect.Right - radius, badgeRect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(badgeRect.X, badgeRect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();
                
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillPath(brush, path);
            }

            // Draw text centered
            using (var textBrush = new SolidBrush(textColor))
            {
                var format = new StringFormat 
                { 
                    Alignment = StringAlignment.Center, 
                    LineAlignment = StringAlignment.Center 
                };
                e.Graphics.DrawString(displayText, e.Appearance.Font, textBrush, badgeRect, format);
            }

            e.Handled = true;
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateButtonStates(GetSelectedVisit(false));
        }

        private void LoadData()
        {
            LoadDataSafeAsync();
        }

        private async void LoadDataSafeAsync()
        {
            try
            {
                btnExamine.Enabled = false;
                btnComplete.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                var timeoutTask = System.Threading.Tasks.Task.Delay(10000);
                System.Threading.Tasks.Task<List<VisitDTO>> loadTask;

                DateTime selectedDate = dtpDate.DateTime;

                if (SessionManager.IsDoctor() && SessionManager.CurrentDoctor != null)
                    loadTask = _visitBLL.GetVisitsByDoctorSafeAsync(SessionManager.CurrentDoctor.DoctorId, selectedDate);
                else
                    loadTask = _visitBLL.GetVisitsByDateSafeAsync(selectedDate);

                var completedTask = await System.Threading.Tasks.Task.WhenAny(loadTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    XtraMessageBox.Show("Load dữ liệu quá lâu, vui lòng thử lại!", "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Cursor = Cursors.Default;
                    return;
                }

                _allVisits = await loadTask;
                
                // Security: Filter for doctor
                string role = SessionManager.CurrentUser?.Role ?? "";
                
                if (SessionManager.IsDoctor())
                {
                    if (SessionManager.CurrentDoctor != null)
                    {
                        // Doctor with profile: Show own visits
                        int currentDoctorId = SessionManager.CurrentDoctor.DoctorId;
                        _allVisits = _allVisits.Where(v => v.DoctorId == currentDoctorId).ToList();
                        
                        // Also filter by paid/in-progress status
                        var paidVisitIds = _invoiceDAL.GetPaidVisitIds(selectedDate);
                        _allVisits = _allVisits.Where(v => 
                            paidVisitIds.Contains(v.VisitId) ||
                            v.Status == "InProgress" ||
                            v.Status == "Completed"
                        ).ToList();
                    }
                    else
                    {
                        // Doctor without profile (data error): Show nothing for safety
                        _allVisits = new List<VisitDTO>();
                    }
                }
                // Admin or Receptionist sees all (loaded above)
                
                _filteredVisits = new List<VisitDTO>(_allVisits);

                string keyword = (txtSearch.Text ?? "").Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    _filteredVisits = _allVisits.Where(v =>
                        (v.PatientName ?? "").ToLower().Contains(keyword) ||
                        (v.DoctorName ?? "").ToLower().Contains(keyword)
                    ).ToList();
                }

                _pagination.SetTotalItems(_filteredVisits.Count);
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

        private void OnPageChanged(int page)
        {
            LoadCurrentPage();
        }

        private void LoadCurrentPage()
        {
            try
            {
                int skip = _pagination.GetSkipCount();
                _currentPageData = _filteredVisits.Skip(skip).Take(_pagination.PageSize).ToList();
                BindData(_currentPageData);
                UpdateButtonStates(GetSelectedVisit(false));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi phân trang: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindData(List<VisitDTO> visits)
        {
            gridVisits.DataSource = visits;
            lblCount.Text = $"Hiển thị: {visits.Count} / Tổng: {_filteredVisits.Count} lượt khám";

            // Clear selection to avoid auto-selecting first row
            gridView.ClearSelection();
            gridView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void UpdateButtonStates(VisitDTO visit)
        {
            if (visit == null)
            {
                btnExamine.Enabled = false;
                btnComplete.Enabled = false;
                return;
            }

            string status = visit.Status ?? "Waiting";
            bool hasDiagnosis = !string.IsNullOrWhiteSpace(visit.Diagnosis);

            switch (status)
            {
                case "Waiting":
                    btnExamine.Enabled = true;
                    btnExamine.Text = "Bắt đầu khám";
                    btnExamine.Appearance.BackColor = Color.FromArgb(0, 123, 255);
                    btnComplete.Enabled = hasDiagnosis;
                    break;
                case "InProgress":
                    btnExamine.Enabled = true;
                    btnExamine.Text = "Tiếp tục khám";
                    btnExamine.Appearance.BackColor = UIHelper.PrimaryColor;
                    btnComplete.Enabled = hasDiagnosis;
                    break;
                case "Completed":
                    btnExamine.Enabled = true;
                    btnExamine.Text = "Xem chi tiết";
                    btnExamine.Appearance.BackColor = Color.FromArgb(108, 117, 125);
                    btnComplete.Enabled = false;
                    break;
            }
        }

        private VisitDTO GetSelectedVisit(bool showMessage = true)
        {
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                if (showMessage)
                    XtraMessageBox.Show("Vui lòng chọn lượt khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            try
            {
                int visitId = Convert.ToInt32(gridView.GetRowCellValue(selectedRows[0], "VisitId"));
                return _visitBLL.GetVisitById(visitId);
            }
            catch
            {
                if (showMessage)
                    XtraMessageBox.Show("Không thể lấy thông tin lượt khám!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpDate.EditValue = DateTime.Today;
            LoadData();
        }

        private void btnExamine_Click(object sender, EventArgs e)
        {
            var visit = GetSelectedVisit();
            if (visit == null) return;

            string status = visit.Status ?? "Waiting";

            if (status == "Waiting")
            {
                try
                {
                    _visitBLL.UpdateVisitStatus(visit.VisitId, "InProgress");
                    visit = _visitBLL.GetVisitById(visit.VisitId);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            using (var diagForm = new DiagnosisForm(visit))
            {
                diagForm.ShowDialog();
            }
            LoadData();
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            var visit = GetSelectedVisit();
            if (visit == null) return;

            if (string.IsNullOrWhiteSpace(visit.Diagnosis))
            {
                XtraMessageBox.Show("Vui lòng chẩn đoán trước khi hoàn thành!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = XtraMessageBox.Show(
                $"Xác nhận hoàn thành khám cho:\n\n" +
                $"Bệnh nhân: {visit.PatientName}\n" +
                $"Chẩn đoán: {visit.Diagnosis}\n" +
                $"Bệnh: {visit.DiseaseName ?? "Chưa xác định"}",
                "Hoàn thành lượt khám",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _visitBLL.UpdateVisitStatus(visit.VisitId, "Completed");
                    XtraMessageBox.Show("Đã hoàn thành lượt khám!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
