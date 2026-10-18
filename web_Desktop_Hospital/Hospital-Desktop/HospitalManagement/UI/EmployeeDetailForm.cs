using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.DAL;
using HospitalManagement.DTO;
using HospitalManagement.BLL;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    public partial class EmployeeDetailForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly EmployeeDAL _employeeDAL;
        private readonly UserBLL _userBLL;
        private EmployeeDTO _employee;
        private bool _isEdit;

        public EmployeeDetailForm()
        {
            InitializeComponent();
            _employeeDAL = new EmployeeDAL();
            _userBLL = new UserBLL();
            _isEdit = false;
            
            // Form Style
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            LoadUsers();
            
            dtpHireDate.EditValue = DateTime.Today;
        }

        public EmployeeDetailForm(EmployeeDTO emp) : this()
        {
            _employee = emp;
            _isEdit = true;
            this.Text = "Sửa thông tin Nhân viên";
            lblTitle.Text = "✏️ Sửa Nhân viên";
            LoadData();
        }

        private void LoadUsers()
        {
            try
            {
                var users = _userBLL.GetAllUsers();
                // Create a list with a dummy item for "No Link" or just allow null selection
                // LookUpEdit supports null text. We just need to bind the list.
                // We'll project to a simpler structure for display
                var displayList = users.Select(u => new { 
                    u.UserId, 
                    DisplayText = $"{u.Username} ({u.Role})" 
                }).ToList();

                lkeUser.Properties.DataSource = displayList;
                lkeUser.Properties.DisplayMember = "DisplayText";
                lkeUser.Properties.ValueMember = "UserId";
                
                // Add columns
                lkeUser.Properties.PopulateColumns();
                if (lkeUser.Properties.Columns["UserId"] != null)
                    lkeUser.Properties.Columns["UserId"].Visible = false;
                
                lkeUser.Properties.NullText = "(Không liên kết)";
            }
            catch { }
        }

        private void LoadData()
        {
            if (_employee != null)
            {
                txtFullName.Text = _employee.FullName;
                dtpDateOfBirth.EditValue = _employee.DateOfBirth;
                cboGender.EditValue = _employee.Gender; // This matches string items "Nam", "Nữ"
                txtPhone.Text = _employee.Phone;
                txtEmail.Text = _employee.Email;
                txtAddress.Text = _employee.Address;
                txtPosition.Text = _employee.Position;
                txtDepartment.Text = _employee.Department;
                dtpHireDate.EditValue = _employee.HireDate;

                if (_employee.UserId.HasValue)
                {
                    lkeUser.EditValue = _employee.UserId.Value;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            try
            {
                EmployeeDTO emp = _employee ?? new EmployeeDTO();
                emp.FullName = txtFullName.Text.Trim();
                emp.DateOfBirth = dtpDateOfBirth.EditValue as DateTime?;
                emp.Gender = cboGender.EditValue?.ToString();
                emp.Phone = txtPhone.Text.Trim();
                emp.Email = txtEmail.Text.Trim();
                emp.Address = txtAddress.Text.Trim();
                emp.Position = txtPosition.Text.Trim();
                emp.Department = txtDepartment.Text.Trim();
                emp.HireDate = (dtpHireDate.EditValue as DateTime?) ?? DateTime.Now;

                if (lkeUser.EditValue != null && int.TryParse(lkeUser.EditValue.ToString(), out int userId))
                    emp.UserId = userId;
                else
                    emp.UserId = null;

                if (_isEdit)
                    _employeeDAL.Update(emp);
                else
                    _employeeDAL.Insert(emp);

                XtraMessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
