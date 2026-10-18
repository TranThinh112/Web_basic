using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    public partial class MedicineDetailForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly MedicineBLL _medicineBLL;
        private MedicineDTO _medicine;
        private bool _isEdit;

        public MedicineDetailForm()
        {
            InitializeComponent();
            _medicineBLL = new MedicineBLL();
            _isEdit = false;
            
            // Form Style
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Initialize default values
            cboUnit.SelectedIndex = 0; // Default Select "Viên"
            spnMinStock.Value = 10;
        }

        public MedicineDetailForm(MedicineDTO medicine) : this()
        {
            _medicine = medicine;
            _isEdit = true;
            this.Text = "Sửa thuốc";
            lblTitle.Text = "✏️ Sửa thuốc";
            LoadData();
        }

        private void LoadData()
        {
            if (_medicine != null)
            {
                txtName.Text = _medicine.MedicineName;
                cboUnit.EditValue = _medicine.Unit;
                txtPrice.EditValue = _medicine.Price;
                spnStock.Value = _medicine.StockQuantity;
                spnMinStock.Value = _medicine.MinStockLevel;
            }
            
            // Restrict price editing for non-admins
            if (!SessionManager.IsAdmin())
            {
                txtPrice.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập tên thuốc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }
            
            if (cboUnit.EditValue == null || string.IsNullOrWhiteSpace(cboUnit.Text))
            {
                XtraMessageBox.Show("Vui lòng chọn đơn vị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboUnit.Focus();
                return;
            }

            try
            {
                MedicineDTO med = _medicine ?? new MedicineDTO();
                med.MedicineName = txtName.Text.Trim();
                med.Unit = cboUnit.Text.Trim();
                
                decimal.TryParse(txtPrice.Text?.Replace(",", "").Replace(".", ""), out decimal price);
                // Note: TextEdit with Numeric mask might return formatted string. 
                // Using EditValue is safer for SpinEdit/TextEdit with mask
                if (txtPrice.EditValue != null)
                {
                   price = Convert.ToDecimal(txtPrice.EditValue);
                }
                med.Price = price;
                
                med.StockQuantity = Convert.ToInt32(spnStock.Value);
                med.MinStockLevel = Convert.ToInt32(spnMinStock.Value);

                if (_isEdit) _medicineBLL.UpdateMedicine(med);
                else _medicineBLL.AddMedicine(med);

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
