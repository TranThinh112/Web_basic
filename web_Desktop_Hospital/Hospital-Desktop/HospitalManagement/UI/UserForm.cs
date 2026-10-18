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
    public partial class UserForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly UserBLL _userBLL;
        
        private List<UserDTO> _allUsers;
        private List<UserDTO> _filteredUsers;
        private List<UserDTO> _currentPageData;

        // Pagination
        private PaginationHelper _pagination;
        private Timer _debounceTimer;

        public UserForm()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            _allUsers = new List<UserDTO>();
            _filteredUsers = new List<UserDTO>();
            _currentPageData = new List<UserDTO>();

            // UI Helper
            UIHelper.ConfigureModernForm(this);
            UIHelper.ConfigureDevExpressGridView(gridView);
            UIHelper.ClearGridViewSelection(gridView);
            
            // Standardize Buttons
            UIHelper.StyleAddButton(btnAdd);
            UIHelper.StyleEditButton(btnEdit);
            
            UIHelper.StylePrimaryButton(btnReset);
            btnReset.Text = "Reset Pass";
            
            UIHelper.StyleSuccessButton(btnToggleStatus);
            btnToggleStatus.Text = "Trạng thái";
            
            UIHelper.StyleSecondaryButton(btnRefresh);
            btnRefresh.Text = "Làm mới";
            
            UIHelper.StyleCloseButton(btnClose);

            // Setup Pagination
            _pagination = new PaginationHelper(20);
            _pagination.PageChanged += OnPageChanged;

            // Add Pagination Panel
            Panel pagingPanel = _pagination.CreatePaginationPanel();
            pagingPanel.Parent = this;
            
            DevExpress.XtraLayout.LayoutControlItem item = layoutControl.Root.AddItem();
            item.Control = pagingPanel;
            item.TextVisible = false;
            
            item.Move(layoutControlItemGrid, DevExpress.XtraLayout.Utils.InsertType.Bottom);

            // Button State Logic
            gridView.FocusedRowChanged += GridView_FocusedRowChanged;
            gridView.Click += (s, e) => UpdateButtonStates();
            UpdateButtonStates();
        }

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = gridView.FocusedRowHandle >= 0;
            btnEdit.Enabled = hasSelection;
            btnReset.Enabled = hasSelection;
            btnToggleStatus.Enabled = hasSelection;
            
            if (hasSelection)
            {
                var user = gridView.GetRow(gridView.FocusedRowHandle) as UserDTO;
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        btnToggleStatus.Text = "Vô hiệu hóa";
                        UIHelper.StyleDangerButton(btnToggleStatus);
                    }
                    else
                    {
                        btnToggleStatus.Text = "Kích hoạt";
                        UIHelper.StyleSuccessButton(btnToggleStatus);
                    }
                }
            }
            else
            {
                btnToggleStatus.Text = "Trạng thái";
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            cboSearchType.SelectedIndex = 0;
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

        private void cboSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            string keyword = (txtSearch.Text ?? "").Trim().ToLower();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                _filteredUsers = new List<UserDTO>(_allUsers);
            }
            else
            {
                int searchType = cboSearchType.SelectedIndex;
                _filteredUsers = _allUsers.Where(u =>
                {
                    switch (searchType)
                    {
                        case 1: return (u.Username ?? "").ToLower().Contains(keyword);
                        case 2: return (u.Role ?? "").ToLower().Contains(keyword);
                        default: return (u.Username ?? "").ToLower().Contains(keyword) ||
                                       (u.Role ?? "").ToLower().Contains(keyword);
                    }
                }).ToList();
            }

            // Update pagination
            _pagination.SetTotalItems(_filteredUsers.Count);
            _pagination.ResetToFirstPage();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _allUsers = _userBLL.GetAllUsers();
                _filteredUsers = new List<UserDTO>(_allUsers);
                
                _pagination.SetTotalItems(_filteredUsers.Count);
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
                _currentPageData = _filteredUsers.Skip(skip).Take(_pagination.PageSize).ToList();
                BindData(_currentPageData);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi phân trang: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindData(List<UserDTO> users)
        {
            gridUsers.DataSource = users;

            if (gridView.Columns.Count > 0)
            {
                if (gridView.Columns["UserId"] != null)
                {
                    gridView.Columns["UserId"].Caption = "ID";
                    gridView.Columns["UserId"].Width = 50;
                }
                if (gridView.Columns["Username"] != null)
                    gridView.Columns["Username"].Caption = "Tên đăng nhập";
                if (gridView.Columns["Role"] != null)
                    gridView.Columns["Role"].Caption = "Vai trò";
                if (gridView.Columns["IsActive"] != null)
                    gridView.Columns["IsActive"].Caption = "Hoạt động";
                if (gridView.Columns["PasswordHash"] != null)
                    gridView.Columns["PasswordHash"].Visible = false;
            }

            lblCount.Text = $"Hiển thị: {users.Count} / Tổng: {_filteredUsers.Count} người dùng";

            // Clear selection to avoid auto-selecting first row
            gridView.ClearSelection();
            gridView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private UserDTO GetSelectedUser()
        {
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            int id = Convert.ToInt32(gridView.GetRowCellValue(selectedRows[0], "UserId"));
            return _userBLL.GetUserById(id);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (new UserDetailForm().ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user != null && new UserDetailForm(user).ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null) return;

            if (XtraMessageBox.Show(
                $"Reset mật khẩu cho '{user.Username}'?\n\n" +
                "Hệ thống sẽ tạo mật khẩu tạm thời ngẫu nhiên.\n" +
                "User sẽ phải đổi mật khẩu khi đăng nhập lần tới.",
                "Xác nhận Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Generate random password and get it for one-time display
                    string tempPassword = _userBLL.ResetPasswordWithRandomPassword(user.UserId);
                    
                    // Copy to clipboard automatically
                    System.Windows.Forms.Clipboard.SetText(tempPassword);
                    
                    // Show password ONCE to admin with copy confirmation
                    XtraMessageBox.Show(
                        $"✅ Reset mật khẩu thành công!\n\n" +
                        $"👤 Tài khoản: {user.Username}\n" +
                        $"🔑 Mật khẩu tạm thời: {tempPassword}\n\n" +
                        "📋 Mật khẩu đã được COPY vào clipboard!\n\n" +
                        "⚠️ LƯU Ý: Mật khẩu này chỉ hiển thị MỘT LẦN DUY NHẤT.\n" +
                        "Hãy dán (Ctrl+V) và gửi cho user.\n" +
                        "User sẽ phải đổi mật khẩu khi đăng nhập.",
                        "Mật Khẩu Tạm Thời", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadData();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null) return;

            string action = user.IsActive ? "Vô hiệu hóa" : "Kích hoạt";
            if (XtraMessageBox.Show($"{action} tài khoản '{user.Username}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (user.IsActive)
                        _userBLL.DeleteUser(user.UserId);
                    else
                        _userBLL.ActivateUser(user.UserId);

                    XtraMessageBox.Show($"Đã {action.ToLower()} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex) { XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cboSearchType.SelectedIndex = 0;
            txtSearch.Text = "";
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
