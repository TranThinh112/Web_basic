using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly UserBLL _userBLL;

        public LoginForm()
        {
            _userBLL = new UserBLL();
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                lblMessage.Text = "Vui lòng nhập tên đăng nhập!";
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Vui lòng nhập mật khẩu!";
                txtPassword.Focus();
                return;
            }

            try
            {
                // Disable UI to prevent multiple clicks
                btnLogin.Enabled = false;
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                Cursor = Cursors.WaitCursor;

                // Async Login Call (Non-blocking)
                UserDTO user = await _userBLL.LoginAsync(username, password);
                
                SessionManager.Login(user);

                if (user.Role == "Doctor")
                {
                    DoctorBLL doctorBLL = new DoctorBLL();
                    DoctorDTO doctor = doctorBLL.GetDoctorByUserId(user.UserId);
                    SessionManager.Login(user, doctor);
                }
                else
                {
                    // Check if linked to Employee (Receptionist, Cashier, Pharmacist, Admin)
                    try 
                    {
                        var employeeDAL = new DAL.EmployeeDAL();
                        EmployeeDTO emp = employeeDAL.GetByUserId(user.UserId);
                        if (emp != null)
                        {
                            SessionManager.Login(user, emp);
                        }
                    }
                    catch { /* Ignore if DAL fails or table missing */ }
                }

                // Check if user must change password (after reset or first login)
                if (user.MustChangePassword)
                {
                    var forceChangeForm = new ForceChangePasswordForm(user);
                    if (forceChangeForm.ShowDialog() != DialogResult.OK)
                    {
                        // User failed to change password - log out
                        SessionManager.Logout();
                        lblMessage.Text = "Bạn phải đổi mật khẩu để tiếp tục!";
                        return;
                    }
                    // Password changed successfully, update the user object
                    user.MustChangePassword = false;
                }

                this.Hide();
                MainDashboard dashboard = new MainDashboard();
                dashboard.FormClosed += Dashboard_FormClosed;
                dashboard.Show();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                Logger.LogError("LoginForm.Login", ex);
            }
            finally
            {
                // Re-enable UI
                btnLogin.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
