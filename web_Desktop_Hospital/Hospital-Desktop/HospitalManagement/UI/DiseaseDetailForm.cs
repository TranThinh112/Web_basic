using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagement.Utils;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.DAL;
using HospitalManagement.DTO;

namespace HospitalManagement.UI
{
    public partial class DiseaseDetailForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly DiseaseDAL _diseaseDAL;
        private readonly DiseaseDTO _disease;
        private readonly bool _isEdit;

        public DiseaseDetailForm()
        {
            _diseaseDAL = new DiseaseDAL();
            _isEdit = false;
            InitializeComponent();
            
            // Form Style
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public DiseaseDetailForm(DiseaseDTO disease) : this()
        {
            _disease = disease;
            _isEdit = true;
            this.Text = "Sửa bệnh";
            lblTitle.Text = "✏️ Sửa bệnh";
            LoadData();
        }

        private void LoadData()
        {
            if (_disease != null)
            {
                txtName.Text = _disease.DiseaseName;
                txtCategory.Text = _disease.Category;
                txtDescription.Text = _disease.Description;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập tên bệnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                DiseaseDTO dis = _disease ?? new DiseaseDTO();
                dis.DiseaseName = txtName.Text.Trim();
                dis.Category = txtCategory.Text.Trim();
                dis.Description = txtDescription.Text.Trim();

                if (_isEdit) _diseaseDAL.Update(dis);
                else _diseaseDAL.Insert(dis);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
