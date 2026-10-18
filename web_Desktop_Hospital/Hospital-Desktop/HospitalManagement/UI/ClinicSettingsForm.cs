using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    /// <summary>
    /// Form cài đặt giờ đóng/mở cửa phòng khám - Chỉ Admin mới có quyền truy cập
    /// </summary>
    public partial class ClinicSettingsForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly ClinicSettingsBLL _settingsBLL;

        public ClinicSettingsForm()
        {
            _settingsBLL = new ClinicSettingsBLL();
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                var settings = _settingsBLL.GetClinicHours();
                
                // Set time pickers
                DateTime baseDate = DateTime.Today;
                timeOpening.EditValue = baseDate.Add(settings.OpeningTime);
                timeClosing.EditValue = baseDate.Add(settings.ClosingTime);
                
                // TextEdit with configured Mask ("N0") handles numbers.
                // Setting EditValue directly (decimal/double) works best.
                txtFee.EditValue = settings.ExaminationFee;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi tải cài đặt: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (timeOpening.EditValue == null || timeClosing.EditValue == null)
                {
                    XtraMessageBox.Show("Vui lòng chọn giờ mở/đóng cửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime openDT = (DateTime)timeOpening.EditValue;
                DateTime closeDT = (DateTime)timeClosing.EditValue;
                
                TimeSpan openingTime = openDT.TimeOfDay;
                TimeSpan closingTime = closeDT.TimeOfDay;

                if (openingTime >= closingTime)
                {
                    XtraMessageBox.Show("Giờ mở cửa phải trước giờ đóng cửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal fee = 0;
                // Try parsing from EditValue first as it holds the underlying value
                if (txtFee.EditValue != null)
                {
                    decimal.TryParse(txtFee.EditValue.ToString(), out fee);
                }

                _settingsBLL.SaveClinicSettings(openingTime, closingTime, fee);

                XtraMessageBox.Show(
                    $"Đã lưu cài đặt:\n\nMở cửa: {openingTime:hh\\:mm}\nĐóng cửa: {closingTime:hh\\:mm}\nPhí khám: {fee:N0} VNĐ",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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
