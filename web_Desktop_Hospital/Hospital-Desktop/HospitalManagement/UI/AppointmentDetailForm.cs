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
    public partial class AppointmentDetailForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly AppointmentBLL _appointmentBLL;
        private readonly PatientBLL _patientBLL;
        private readonly DoctorBLL _doctorBLL;
        private readonly ClinicSettingsBLL _settingsBLL;

        private TimeSpan _openingTime;
        private TimeSpan _closingTime;

        public AppointmentDetailForm()
        {
            InitializeComponent();
            _appointmentBLL = new AppointmentBLL();
            _patientBLL = new PatientBLL();
            _doctorBLL = new DoctorBLL();
            _settingsBLL = new ClinicSettingsBLL();
            
            // Form Style
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Ensure buttons are visible at Designer positions
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnSave.BringToFront();
            btnCancel.BringToFront();
            
            // Style buttons
            UIHelper.StylePrimaryButton(btnSave);
            btnSave.Text = "Lưu lịch hẹn";
            btnSave.Size = new Size(110, 35);
            
            UIHelper.StyleDangerButton(btnCancel);
            btnCancel.Text = "Hủy bỏ";
            btnCancel.Size = new Size(90, 35);
            
            LoadClinicHours();
            LoadCombos();
            
            dtpDate.EditValue = DateTime.Today;
            dtpDate.Properties.MinValue = DateTime.Today;
            timeEdit.EditValue = DateTime.Today.Add(_openingTime);
        }

        private void LoadClinicHours()
        {
            try
            {
                var settings = _settingsBLL.GetClinicHours();
                _openingTime = settings.OpeningTime;
                _closingTime = settings.ClosingTime;
                lblClinicHours.Text = $"Giờ làm việc: {_openingTime:hh\\:mm} - {_closingTime:hh\\:mm}";
            }
            catch
            {
                _openingTime = new TimeSpan(8, 0, 0);
                _closingTime = new TimeSpan(17, 0, 0);
                lblClinicHours.Text = "Giờ làm việc: 08:00 - 17:00";
            }
        }

        private void LoadCombos()
        {
            try
            {
                var patients = _patientBLL.GetAllPatients();
                lkePatient.Properties.DataSource = patients;
                lkePatient.Properties.DisplayMember = "FullName";
                lkePatient.Properties.ValueMember = "PatientId";
                
                // Configure Columns for Patient - only show name
                lkePatient.Properties.Columns.Clear();
                lkePatient.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FullName", "Tên bệnh nhân", 250));
                lkePatient.Properties.PopupWidth = 280;

                var doctors = _doctorBLL.GetAllDoctors();
                lkeDoctor.Properties.DataSource = doctors;
                lkeDoctor.Properties.DisplayMember = "FullName";
                lkeDoctor.Properties.ValueMember = "DoctorId";

                // Configure Columns for Doctor
                lkeDoctor.Properties.Columns.Clear();
                lkeDoctor.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DoctorId", "Mã BS", 50));
                lkeDoctor.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FullName", "Họ tên", 150));
                lkeDoctor.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Specialization", "Chuyên khoa", 100));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lkePatient.EditValue == null || lkeDoctor.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn bệnh nhân và bác sĩ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate time within clinic hours
            DateTime selectedDateTime = ((DateTime)timeEdit.EditValue);
            TimeSpan selectedTime = selectedDateTime.TimeOfDay;
            
            if (selectedTime < _openingTime || selectedTime > _closingTime)
            {
                XtraMessageBox.Show(
                    $"Giờ hẹn phải trong khung giờ làm việc!\n\nGiờ làm việc: {_openingTime:hh\\:mm} - {_closingTime:hh\\:mm}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DateTime date = ((DateTime)dtpDate.EditValue).Date;
                DateTime appointmentDate = date.Add(selectedTime);

                AppointmentDTO appointment = new AppointmentDTO
                {
                    PatientId = (int)lkePatient.EditValue,
                    DoctorId = (int)lkeDoctor.EditValue,
                    AppointmentDate = appointmentDate,
                    Reason = txtReason.Text.Trim()
                };

                _appointmentBLL.CreateAppointment(appointment);
                XtraMessageBox.Show("Đặt lịch hẹn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
