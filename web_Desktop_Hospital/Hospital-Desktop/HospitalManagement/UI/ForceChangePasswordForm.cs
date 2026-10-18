using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    /// <summary>
    /// Form đổi mật khẩu bắt buộc - Modal, không thể bypass
    /// Hiển thị khi user có MustChangePassword = true
    /// </summary>
    public partial class ForceChangePasswordForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly UserBLL _userBLL;
        private readonly UserDTO _user;
        private bool _passwordChanged = false;

        public ForceChangePasswordForm(UserDTO user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _userBLL = new UserBLL();
            
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            
            // Prevent closing without proper password change
            this.FormClosing += ForceChangePasswordForm_FormClosing;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string newPass = txtNewPassword.Text;
            string confirmPass = txtConfirmPassword.Text;

            // Validate
            if (string.IsNullOrWhiteSpace(newPass))
            {
                XtraMessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (newPass != confirmPass)
            {
                XtraMessageBox.Show("Mật khẩu xác nhận không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return;
            }

            // Check strong password policy
            if (!ValidationHelper.ValidateStrongPassword(newPass))
            {
                XtraMessageBox.Show(
                    "Mật khẩu phải có ít nhất 8 ký tự, bao gồm:\n" +
                    "• Chữ hoa (A-Z)\n" +
                    "• Chữ thường (a-z)\n" +
                    "• Số (0-9)\n" +
                    "• Ký tự đặc biệt (!@#$%^&*)",
                    "Mật khẩu yếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
                return;
            }

            try
            {
                _userBLL.ForceChangePassword(_user.UserId, newPass);
                _passwordChanged = true;
                
                XtraMessageBox.Show(
                    "✅ Đổi mật khẩu thành công!\n\nBạn có thể tiếp tục sử dụng hệ thống.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ForceChangePasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Prevent closing if password not changed (except if app is shutting down)
            if (!_passwordChanged && e.CloseReason == CloseReason.UserClosing)
            {
                XtraMessageBox.Show(
                    "Bạn phải đổi mật khẩu trước khi tiếp tục!\n\nĐây là yêu cầu bảo mật bắt buộc.",
                    "Không thể đóng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
    }
}
