using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    /// <summary>
    /// Form kê đơn thuốc (Modern UI with LayoutControl)
    /// </summary>
    public partial class PrescriptionForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly VisitDTO _visit;
        private readonly PrescriptionBLL _prescriptionBLL;
        private readonly MedicineBLL _medicineBLL;
        
        private List<PrescriptionDetailDTO> _details = new List<PrescriptionDetailDTO>();
        private bool _isReadOnly = false;

        // Fields are now in Designer.cs partial class

        public PrescriptionForm(VisitDTO visit)
        {
            _visit = visit ?? throw new ArgumentNullException(nameof(visit));
            _prescriptionBLL = new PrescriptionBLL();
            _medicineBLL = new MedicineBLL();
            
            _isReadOnly = visit.Status == "Completed";
            
            InitializeComponent();
            ApplyUIHelperStyles(); 
            
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            
            LoadData();
            CustomizeForMode();
        }

        private void ApplyUIHelperStyles()
        {
            lblHeader.Appearance.Font = UIHelper.TitleFont;
            lblHeader.Appearance.ForeColor = UIHelper.PrimaryColor;
            
            lblSubHeader.Appearance.Font = UIHelper.RegularFont;
            lblSubHeader.Appearance.ForeColor = Color.Gray;

            lblPatientInfo.Appearance.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblPatientInfo.Appearance.ForeColor = UIHelper.TextDark;

            UIHelper.StyleSearchLookUpEdit(slkMedicine);
            UIHelper.StyleAddButton(btnAdd);
            UIHelper.ConfigureDevExpressGridView(gridViewPrescription);
            UIHelper.StyleDeleteButton(btnRemove);
            
            lblTotal.Appearance.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTotal.Appearance.ForeColor = UIHelper.DangerColor;

            UIHelper.StyleSaveButton(btnSave);
            UIHelper.StyleCloseButton(btnClose);
        }
        
        // --- Logic Methods ---

        private void CustomizeForMode()
        {
            if (_isReadOnly)
            {
                this.Text = "Xem Chi Tiết Đơn Thuốc (Chỉ xem)";
                
                // Disable Inputs
                slkMedicine.ReadOnly = true;
                spnQuantity.ReadOnly = true;
                txtUsage.ReadOnly = true;
                btnAdd.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                btnSave.Visible = false;
                
                // Adjust visibility
                layoutControl.Items.FindByName("Thêm thuốc")?.HideToCustomization(); // Hide input group? or just disable
                // Actually, hiding the group is cleaner
                // But LayoutGroups don't have direct Names easily unless set.
                // Let's iterate or finding via text is tricky.
                // Simpler: Just disable controls.
            }
        }

        private void LoadData()
        {
            // Patient Info
            lblPatientInfo.Text = $"Bệnh nhân: {_visit.PatientName}   |   Chẩn đoán: {_visit.Diagnosis ?? "Chưa có"}";

            // Medicines
            if (!_isReadOnly)
            {
                try
                {
                    var medicines = _medicineBLL.GetAllMedicines();
                    slkMedicine.Properties.DataSource = medicines;
                    slkMedicine.Properties.DisplayMember = "MedicineName";
                    slkMedicine.Properties.ValueMember = "MedicineId";
                    
                    // Columns are configured in Designer (slkView) - only MedicineName and Unit
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Lỗi tải thuốc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Existing Prescription
            try
            {
                var prescription = _prescriptionBLL.GetPrescriptionByVisit(_visit.VisitId);
                if (prescription != null)
                {
                    var details = _prescriptionBLL.GetPrescriptionDetails(prescription.PrescriptionId);
                    _details = details ?? new List<PrescriptionDetailDTO>();
                }
            }
            catch { }

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            var displayList = new List<PrescriptionDetailDTO>();
            decimal total = 0;

            foreach (var detail in _details)
            {
                total += detail.LineTotal;
                displayList.Add(detail);
            }

            gridControlPrescription.DataSource = null;
            gridControlPrescription.DataSource = displayList;

            // Configure Columns manually to ensure good look
            gridViewPrescription.PopulateColumns();
            
            // Only configure columns if they exist (when list is not empty)
            if (gridViewPrescription.Columns.Count > 0)
            {
                // Hide ID columns
                if (gridViewPrescription.Columns["PrescriptionDetailId"] != null)
                    gridViewPrescription.Columns["PrescriptionDetailId"].Visible = false;
                if (gridViewPrescription.Columns["PrescriptionId"] != null)
                    gridViewPrescription.Columns["PrescriptionId"].Visible = false;
                if (gridViewPrescription.Columns["MedicineId"] != null)
                    gridViewPrescription.Columns["MedicineId"].Visible = false;
                
                // Hide duplicate columns (from DTO calculated properties)
                if (gridViewPrescription.Columns["Price"] != null)
                    gridViewPrescription.Columns["Price"].Visible = false;
                if (gridViewPrescription.Columns["TotalPrice"] != null)
                    gridViewPrescription.Columns["TotalPrice"].Visible = false;
                
                // Configure visible columns with proper order and width
                if (gridViewPrescription.Columns["MedicineName"] != null)
                {
                    gridViewPrescription.Columns["MedicineName"].Caption = "Tên thuốc";
                    gridViewPrescription.Columns["MedicineName"].VisibleIndex = 0;
                    gridViewPrescription.Columns["MedicineName"].Width = 180;
                }
                if (gridViewPrescription.Columns["Quantity"] != null)
                {
                    gridViewPrescription.Columns["Quantity"].Caption = "Số lượng";
                    gridViewPrescription.Columns["Quantity"].VisibleIndex = 1;
                    gridViewPrescription.Columns["Quantity"].Width = 80;
                }
                if (gridViewPrescription.Columns["Unit"] != null)
                {
                    gridViewPrescription.Columns["Unit"].Caption = "Đơn vị";
                    gridViewPrescription.Columns["Unit"].VisibleIndex = 2;
                    gridViewPrescription.Columns["Unit"].Width = 80;
                }
                if (gridViewPrescription.Columns["Usage"] != null)
                {
                    gridViewPrescription.Columns["Usage"].Caption = "Cách dùng";
                    gridViewPrescription.Columns["Usage"].VisibleIndex = 3;
                    gridViewPrescription.Columns["Usage"].Width = 250;
                }
                
                // Hide price columns - Doctor only writes prescription, pharmacy handles pricing
                if (gridViewPrescription.Columns["UnitPrice"] != null)
                    gridViewPrescription.Columns["UnitPrice"].Visible = false;
                if (gridViewPrescription.Columns["LineTotal"] != null)
                    gridViewPrescription.Columns["LineTotal"].Visible = false;
            }

            // Hide total label for doctor's prescription form
            lblTotal.Visible = false;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (slkMedicine.EditValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn thuốc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int medicineId = (int)slkMedicine.EditValue;
            MedicineDTO selectedMedicine = _medicineBLL.GetAllMedicines().FirstOrDefault(m => m.MedicineId == medicineId);

            if (selectedMedicine == null) return;

            if (_details.Any(d => d.MedicineId == medicineId))
            {
                XtraMessageBox.Show("Thuốc này đã có trong đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedMedicine.StockQuantity < spnQuantity.Value)
            {
                XtraMessageBox.Show($"Thuốc '{selectedMedicine.MedicineName}' chỉ còn tồn kho {selectedMedicine.StockQuantity}!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            _details.Add(new PrescriptionDetailDTO
            {
                MedicineId = selectedMedicine.MedicineId,
                MedicineName = selectedMedicine.MedicineName,
                Quantity = (int)spnQuantity.Value,
                UnitPrice = selectedMedicine.Price,
                Unit = selectedMedicine.Unit,
                Usage = txtUsage.Text.Trim()
            });

            // Reset
            slkMedicine.EditValue = null;
            spnQuantity.Value = 1;
            txtUsage.Text = "";
            slkMedicine.Focus();

            RefreshGrid();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            var selectedRow = gridViewPrescription.GetFocusedRow() as PrescriptionDetailDTO;
            if (selectedRow != null)
            {
                _details.Remove(selectedRow);
                RefreshGrid();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_details.Count == 0)
            {
                XtraMessageBox.Show("Vui lòng thêm ít nhất một loại thuốc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _prescriptionBLL.CreateOrUpdatePrescription(_visit.VisitId, _details);
                XtraMessageBox.Show("Lưu đơn thuốc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
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
