using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DTO;

namespace HospitalManagement.UI
{
    public partial class DiagnosisForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly VisitDTO _visit;
        private readonly VisitBLL _visitBLL;
        private readonly DiseaseBLL _diseaseBLL;

        private bool _isReadOnly = false;

        public DiagnosisForm(VisitDTO visit)
        {
            _visit = visit ?? throw new ArgumentNullException(nameof(visit));
            _visitBLL = new VisitBLL();
            _diseaseBLL = new DiseaseBLL();
            _isReadOnly = visit.Status == "Completed";
            
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            
            LoadData();
            CustomizeForMode();
        }

        private void CustomizeForMode()
        {
            if (_isReadOnly)
            {
                this.Text = "Xem chi tiết lượt khám";
                lblTitle.Text = "👁️ CHI TIẾT LƯỢT KHÁM";
                
                txtSymptoms.ReadOnly = true;
                txtDiagnosis.ReadOnly = true;
                lkeDisease.Enabled = false;
                
                btnSave.Visible = false;
                btnPrescription.Text = "📋 Xem đơn thuốc";
                
                // Repositioning handled by LayoutControl automatically
                // btnPrescription.Location = new Point(100, 0);
                // btnClose.Location = new Point(270, 0);
            }
        }

        private void LoadData()
        {
            // Patient Info
            lblPatientName.Text = _visit.PatientName;
            lblVisitDate.Text = _visit.VisitDate.ToString("dd/MM/yyyy HH:mm");

            // Load diseases
            try
            {
                var diseases = _diseaseBLL.GetAllDiseases();
                lkeDisease.Properties.DataSource = diseases;
                lkeDisease.Properties.DisplayMember = "DiseaseName";
                lkeDisease.Properties.ValueMember = "DiseaseId";
                
                // Configure columns
                lkeDisease.Properties.Columns.Clear();
                lkeDisease.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DiseaseName", "Tên bệnh", 150));
                lkeDisease.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Category", "Phân loại", 80));
                
                // Select disease
                if (_visit.DiseaseId.HasValue && _visit.DiseaseId.Value > 0)
                {
                    lkeDisease.EditValue = _visit.DiseaseId.Value;
                }
            }
            catch { }

            // Load visit data
            txtSymptoms.Text = _visit.Symptoms ?? "";
            txtDiagnosis.Text = _visit.Diagnosis ?? "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập chẩn đoán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiagnosis.Focus();
                return;
            }

            try
            {
                int? diseaseId = null;
                if (lkeDisease.EditValue != null && int.TryParse(lkeDisease.EditValue.ToString(), out int id))
                {
                    diseaseId = id;
                }

                _visitBLL.UpdateDiagnosis(_visit.VisitId, txtSymptoms.Text.Trim(), txtDiagnosis.Text.Trim(), diseaseId);

                // Update local visit object
                _visit.Symptoms = txtSymptoms.Text.Trim();
                _visit.Diagnosis = txtDiagnosis.Text.Trim();
                _visit.DiseaseId = diseaseId;

                XtraMessageBox.Show("Đã lưu chẩn đoán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrescription_Click(object sender, EventArgs e)
        {
            // Nếu chưa lưu chẩn đoán và đang ở chế độ edit, hỏi user
            if (!_isReadOnly && !string.IsNullOrWhiteSpace(txtDiagnosis.Text) && txtDiagnosis.Text != _visit.Diagnosis)
            {
                var result = XtraMessageBox.Show("Bạn có muốn lưu chẩn đoán trước khi kê đơn không?", 
                    "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes) btnSave_Click(sender, e);
            }

            using (var prescriptionForm = new PrescriptionForm(_visit))
            {
                prescriptionForm.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
