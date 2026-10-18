using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagement.Utils;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DTO;

namespace HospitalManagement.UI
{
    public partial class UserDetailForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly UserBLL _userBLL;
        private UserDTO _user;
        private bool _isEdit;

        public UserDetailForm()
        {
            _userBLL = new UserBLL();
            _isEdit = false;
            InitializeComponent();
            
            // Form Style
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            cboRole.SelectedIndex = 1; // Default Receptionist
        }

        public UserDetailForm(UserDTO user) : this()
        {
            _user = user;
            _isEdit = true;
            this.Text = "Sửa thông tin người dùng";
            lblTitle.Text = "✏️ Sửa người dùng";
            LoadData();
        }

        private void LoadData()
        {
            if (_user != null)
            {
                txtUsername.Text = _user.Username;
                txtUsername.Enabled = false;
                
                cboRole.EditValue = _user.Role;
                chkIsActive.Checked = _user.IsActive;

                // Hide password fields in edit mode
                lblPassword.Visible = false;
                txtPassword.Visible = false;
                lblConfirmPassword.Visible = false;
                txtConfirmPassword.Visible = false;

                // Move other controls up using simple logic via code, or just hide/show
                // Since this is DevExpress, layout control would be better, but for now we manually adjust or just leave gaps
                
                // Manual adjustment to look better
                int gap = 40;
                int startY = 110; 
                
                lblRole.Location = new Point(20, startY);
                cboRole.Location = new Point(130, startY - 2);

                lblStatus.Location = new Point(20, startY + gap);
                chkIsActive.Location = new Point(130, startY + gap - 1);

                btnSave.Location = new Point(130, startY + gap * 2);
                btnCancel.Location = new Point(230, startY + gap * 2);
                
                this.Height = 300;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (!_isEdit)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    XtraMessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    XtraMessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmPassword.Focus();
                    return;
                }

                if (txtPassword.Text.Length < 6)
                {
                    XtraMessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }
            }

            try
            {
                if (_isEdit)
                {
                    _user.Role = cboRole.EditValue.ToString();
                    _user.IsActive = chkIsActive.Checked;
                    _userBLL.UpdateUser(_user);
                    XtraMessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var result = _userBLL.RegisterWithTempPassword(txtUsername.Text.Trim(), cboRole.EditValue.ToString());
                    XtraMessageBox.Show($"Tạo tài khoản thành công!\nMật khẩu tạm: {result.TempPassword}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
