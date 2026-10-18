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
    public partial class AppointmentForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly AppointmentBLL _appointmentBLL;
        private readonly VisitBLL _visitBLL;
        private readonly ClinicSettingsBLL _settingsBLL;
        
        private List<AppointmentDTO> _allAppointments;
        private List<AppointmentDTO> _filteredAppointments;
        private List<AppointmentDTO> _currentPageData;

        // Pagination
        private PaginationHelper _pagination;
        private Timer _debounceTimer;
        private TimeSpan _closingTime;

        public AppointmentForm()
        {
            _appointmentBLL = new AppointmentBLL();
            _visitBLL = new VisitBLL();
            _settingsBLL = new ClinicSettingsBLL();
            _allAppointments = new List<AppointmentDTO>();
            _filteredAppointments = new List<AppointmentDTO>();
            _currentPageData = new List<AppointmentDTO>();
            
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
            LoadClinicHours();
            gridView.OptionsBehavior.AutoPopulateColumns = false;
        }

        private void LoadClinicHours()
        {
            try
            {
                var settings = _settingsBLL.GetClinicHours();
                _closingTime = settings.ClosingTime;
            }
            catch
            {
                _closingTime = new TimeSpan(17, 0, 0); // Default closing at 5 PM
            }
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            UIHelper.ConfigureModernForm(this);
            UIHelper.ConfigureDevExpressGridView(gridView);
            UIHelper.ClearGridViewSelection(gridView);
            
            // Standardize Buttons
            UIHelper.StyleAddButton(btnAdd);
            UIHelper.StyleSuccessButton(btnConfirm); // No standard "Confirm" yet, reuse Success or make new? 
            // btnConfirm is "Tiếp nhận" -> Maybe style as primary or success?
            // Let's use StylePrimaryButton for custom actions if they don't fit Add/Edit/Delete
            UIHelper.StylePrimaryButton(btnConfirm);
            btnConfirm.Text = "Tiếp nhận";
            
            UIHelper.StyleDangerButton(btnCancel); // btnCancel logic is "Hủy lịch", so Danger fits.
            btnCancel.Text = "Hủy lịch";
            
            UIHelper.StyleSecondaryButton(btnRefresh);
            btnRefresh.Text = "Làm mới";
            
            UIHelper.StyleCloseButton(btnClose);

            dtpDate.EditValue = DateTime.Today;
            SetupSearch();
            gridView.FocusedRowChanged += GridView_FocusedRowChanged;
            gridView.Click += GridView_Click;
            LoadData();

            // Security: Role-based UI adjustments
            if (SessionManager.IsDoctor())
            {
                // Hide all action buttons for LayoutControl
                layoutControlItemAdd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; 
                layoutControlItemConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItemCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            UpdateButtonStates(gridView.FocusedRowHandle);
        }

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateButtonStates(e.FocusedRowHandle);
        }

        private void UpdateButtonStates(int rowHandle)
        {
            // Security: Doctors cannot take actions on appointments, only View
            if (SessionManager.IsDoctor())
            {
                btnConfirm.Enabled = false;
                btnCancel.Enabled = false;
                return;
            }

            if (rowHandle < 0)
            {
                btnConfirm.Enabled = false;
                btnCancel.Enabled = false;
                return;
            }

            string status = gridView.GetRowCellValue(rowHandle, "Status")?.ToString();
            DateTime? appointmentDate = gridView.GetRowCellValue(rowHandle, "AppointmentDate") as DateTime?;

            // Check if within 30 minutes of appointment time
            bool isTimeValid = IsWithinTimeWindow(appointmentDate);

            // Logic:
            // - Pending: Can Confirm (if time valid), Can Cancel
            // - Confirmed: Already confirmed (Disable Confirm), Can Cancel
            // - Completed: Final state (Disable All)
            // - Cancelled: Final state (Disable All)

            if (status == "Pending")
            {
                btnConfirm.Enabled = isTimeValid; // Only enable if within time window
                btnCancel.Enabled = true;
            }
            else if (status == "Confirmed")
            {
                btnConfirm.Enabled = false; // Already confirmed
                btnCancel.Enabled = true;
            }
            else // Completed or Cancelled
            {
                btnConfirm.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        /// <summary>
        /// Checks if current time is within 30 minutes before the appointment time.
        /// Returns true if: (appointment time - 30 min) <= current time <= clinic closing time
        /// </summary>
        private bool IsWithinTimeWindow(DateTime? appointmentDate)
        {
            if (!appointmentDate.HasValue) return false;

            DateTime now = DateTime.Now;
            DateTime appointmentTime = appointmentDate.Value;
            
            // Allow actions from 30 minutes BEFORE the appointment time onwards
            DateTime earliestAllowedTime = appointmentTime.AddMinutes(-30);
            
            // Use clinic closing time as the latest allowed time
            DateTime latestAllowedTime = appointmentTime.Date.Add(_closingTime);

            return now >= earliestAllowedTime && now <= latestAllowedTime;
        }

        private string GetTimeWindowMessage(DateTime? appointmentDate)
        {
            if (!appointmentDate.HasValue) return "Không có thông tin thời gian hẹn.";
            
            DateTime appointmentTime = appointmentDate.Value;
            DateTime earliestAllowedTime = appointmentTime.AddMinutes(-30);
            
            if (DateTime.Now < earliestAllowedTime)
            {
                TimeSpan remaining = earliestAllowedTime - DateTime.Now;
                if (remaining.TotalHours >= 1)
                    return $"Chưa đến giờ tiếp nhận!\n\nLịch hẹn: {appointmentTime:HH:mm}\nCó thể tiếp nhận từ: {earliestAllowedTime:HH:mm}\n\nCòn {remaining.Hours} giờ {remaining.Minutes} phút nữa.";
                else
                    return $"Chưa đến giờ tiếp nhận!\n\nLịch hẹn: {appointmentTime:HH:mm}\nCó thể tiếp nhận từ: {earliestAllowedTime:HH:mm}\n\nCòn {remaining.Minutes} phút nữa.";
            }
            
            return "Đã quá thời gian cho phép tiếp nhận.";
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
                _filteredAppointments = new List<AppointmentDTO>(_allAppointments);
            else
                _filteredAppointments = _allAppointments.Where(a =>
                    (a.PatientName ?? "").ToLower().Contains(keyword) ||
                    (a.DoctorName ?? "").ToLower().Contains(keyword) ||
                    (a.Reason ?? "").ToLower().Contains(keyword)
                ).ToList();
            
            // Update pagination
            _pagination.SetTotalItems(_filteredAppointments.Count);
            _pagination.ResetToFirstPage();
        }

        private void gridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            
            string status = gridView.GetRowCellValue(e.RowHandle, "Status")?.ToString() ?? "";
            
            switch (status)
            {
                case "Pending":
                    e.Appearance.BackColor = Color.FromArgb(255, 248, 225);
                    break;
                case "Confirmed":
                    e.Appearance.BackColor = Color.FromArgb(227, 242, 253);
                    break;
                case "Completed":
                    e.Appearance.BackColor = Color.FromArgb(232, 245, 233);
                    break;
                case "Cancelled":
                    e.Appearance.BackColor = Color.FromArgb(255, 235, 238);
                    e.Appearance.ForeColor = Color.Gray;
                    break;
            }
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DateTime selectedDate = dtpDate.DateTime;
                _allAppointments = _appointmentBLL.GetAppointmentsByDate(selectedDate);
                
                // Security: Filter for doctor
                string role = SessionManager.CurrentUser?.Role ?? "";
                
                if (SessionManager.IsDoctor())
                {
                    if (SessionManager.CurrentDoctor != null)
                    {
                        // Show own appointments
                        int currentDoctorId = SessionManager.CurrentDoctor.DoctorId;
                        _allAppointments = _allAppointments.Where(a => a.DoctorId == currentDoctorId).ToList();
                    }
                    else
                    {
                        // Doctor logged in but no profile found -> Show nothing for safety
                        _allAppointments = new List<AppointmentDTO>();
                    }
                }
                // Admin or Receptionist sees all
                
                _filteredAppointments = new List<AppointmentDTO>(_allAppointments);

                string keyword = (txtSearch.Text ?? "").Trim().ToLower();
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    _filteredAppointments = _allAppointments.Where(a =>
                        (a.PatientName ?? "").ToLower().Contains(keyword) ||
                        (a.DoctorName ?? "").ToLower().Contains(keyword)
                    ).ToList();
                }

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

        private void OnPageChanged(int page)
        {
            LoadCurrentPage();
        }

        private void LoadCurrentPage()
        {
            try
            {
                int skip = _pagination.GetSkipCount();
                _currentPageData = _filteredAppointments.Skip(skip).Take(_pagination.PageSize).ToList();
                BindData(_currentPageData);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi phân trang: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindData(List<AppointmentDTO> appointments)
        {
            gridAppointments.DataSource = appointments;
            
            if (gridView.Columns.Count > 0)
            {
                // Hide internal IDs
                if (gridView.Columns["PatientId"] != null)
                    gridView.Columns["PatientId"].Visible = false;
                if (gridView.Columns["DoctorId"] != null)
                    gridView.Columns["DoctorId"].Visible = false;
                if (gridView.Columns["PatientPhone"] != null)
                    gridView.Columns["PatientPhone"].Visible = false;
                if (gridView.Columns["VisitId"] != null)
                    gridView.Columns["VisitId"].Visible = false;

                // Logical order: ID → Time → Patient → Doctor → Reason → Status
                if (gridView.Columns["AppointmentId"] != null)
                {
                    gridView.Columns["AppointmentId"].Caption = "Mã";
                    gridView.Columns["AppointmentId"].VisibleIndex = 0;
                    gridView.Columns["AppointmentId"].Width = 50;
                }
                if (gridView.Columns["AppointmentDate"] != null)
                {
                    gridView.Columns["AppointmentDate"].Caption = "Thời gian";
                    gridView.Columns["AppointmentDate"].VisibleIndex = 1;
                    gridView.Columns["AppointmentDate"].Width = 110;
                }
                if (gridView.Columns["PatientName"] != null)
                {
                    gridView.Columns["PatientName"].Caption = "Bệnh nhân";
                    gridView.Columns["PatientName"].VisibleIndex = 2;
                    gridView.Columns["PatientName"].Width = 150;
                }
                if (gridView.Columns["DoctorName"] != null)
                {
                    gridView.Columns["DoctorName"].Caption = "Bác sĩ";
                    gridView.Columns["DoctorName"].VisibleIndex = 3;
                    gridView.Columns["DoctorName"].Width = 120;
                }
                if (gridView.Columns["Reason"] != null)
                {
                    gridView.Columns["Reason"].Caption = "Lý do";
                    gridView.Columns["Reason"].VisibleIndex = 4;
                }
                if (gridView.Columns["Status"] != null)
                {
                    gridView.Columns["Status"].Caption = "Trạng thái";
                    gridView.Columns["Status"].VisibleIndex = 5;
                    gridView.Columns["Status"].Width = 90;
                }
            }

            lblCount.Text = $"Hiển thị: {appointments.Count} / Tổng: {_filteredAppointments.Count} lịch hẹn";

            // Clear selection to avoid auto-selecting first row
            gridView.ClearSelection();
            gridView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
            UpdateButtonStates(DevExpress.XtraGrid.GridControl.InvalidRowHandle);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AppointmentDetailForm form = new AppointmentDetailForm();
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn lịch hẹn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int appointmentId = Convert.ToInt32(gridView.GetRowCellValue(selectedRows[0], "AppointmentId"));
                string currentStatus = gridView.GetRowCellValue(selectedRows[0], "Status")?.ToString();

                if (currentStatus == "Cancelled")
                {
                    XtraMessageBox.Show("Lịch hẹn đã bị hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (currentStatus == "Completed")
                {
                    XtraMessageBox.Show("Lịch hẹn này đã hoàn thành!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (currentStatus == "Confirmed")
                {
                    XtraMessageBox.Show("Bệnh nhân đã được tiếp nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _appointmentBLL.MarkAsArrived(appointmentId);
                XtraMessageBox.Show("Đã tiếp nhận bệnh nhân!\nVui lòng hướng dẫn bệnh nhân đóng phí khám.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn lịch hẹn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string currentStatus = gridView.GetRowCellValue(selectedRows[0], "Status")?.ToString();
            if (currentStatus == "Completed")
            {
                XtraMessageBox.Show("Không thể hủy lịch hẹn đã tiếp nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (XtraMessageBox.Show("Xác nhận hủy lịch hẹn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(gridView.GetRowCellValue(selectedRows[0], "AppointmentId"));
                    _appointmentBLL.UpdateAppointmentStatus(id, "Cancelled");
                    XtraMessageBox.Show("Đã hủy lịch hẹn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
