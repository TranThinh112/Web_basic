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
    public partial class PatientDetailForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly PatientBLL _patientBLL;
        private PatientDTO _patient;
        private bool _isEdit;

        public PatientDetailForm()
        {
            _patientBLL = new PatientBLL();
            _isEdit = false;
            InitializeComponent();
            
            // Form Style
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Ensure buttons are visible using LayoutControl (built-in)
            btnSave.Visible = true;
            btnCancel.Visible = true;
            layoutControlItemSave.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItemCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            
            // Style buttons
            UIHelper.StylePrimaryButton(btnSave);
            btnSave.Text = "Lưu thông tin";
            
            UIHelper.StyleDangerButton(btnCancel);
            btnCancel.Text = "Hủy bỏ";
            
            dtpDateOfBirth.EditValue = DateTime.Today.AddYears(-30);
            cboGender.SelectedIndex = 0;
        }

        public PatientDetailForm(PatientDTO patient) : this()
        {
            _patient = patient;
            _isEdit = true;
            this.Text = "Sửa thông tin Bệnh nhân";
            lblTitle.Text = "✏️ Sửa thông tin Bệnh nhân";
            LoadPatientData();
        }

        private void LoadPatientData()
        {
            if (_patient != null)
            {
                txtFullName.Text = _patient.FullName;
                dtpDateOfBirth.EditValue = _patient.DateOfBirth;
                cboGender.EditValue = _patient.Gender;
                txtPhone.Text = _patient.Phone;
                txtCCCD.Text = _patient.CCCD;
                txtAddress.Text = _patient.Address;
                txtMedicalHistory.Text = _patient.MedicalHistory;
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
                PatientDTO patient = _patient ?? new PatientDTO();
                patient.FullName = txtFullName.Text.Trim();
                patient.DateOfBirth = dtpDateOfBirth.DateTime;
                patient.Gender = cboGender.EditValue?.ToString();
                patient.Phone = txtPhone.Text.Trim();
                patient.CCCD = txtCCCD.Text.Trim();
                patient.Address = txtAddress.Text.Trim();
                patient.MedicalHistory = txtMedicalHistory.Text.Trim();

                if (_isEdit)
                {
                    _patientBLL.UpdatePatient(patient);
                    XtraMessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _patientBLL.RegisterPatient(patient);
                    XtraMessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
