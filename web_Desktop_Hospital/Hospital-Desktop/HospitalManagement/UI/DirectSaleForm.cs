using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DAL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    /// <summary>
    /// Form tạo đơn bán thuốc trực tiếp
    /// </summary>
    public partial class DirectSaleForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly MedicineBLL _medicineBLL;
        private readonly MedicineSaleBLL _saleBLL;
        private readonly PatientBLL _patientBLL;
        private List<MedicineDTO> _allMedicines;
        private List<CartItem> _cart;

        public int? CreatedSaleId { get; private set; }

        public DirectSaleForm()
        {
            _medicineBLL = new MedicineBLL();
            _saleBLL = new MedicineSaleBLL();
            _patientBLL = new PatientBLL();
            _allMedicines = new List<MedicineDTO>();
            _cart = new List<CartItem>();
            
            InitializeComponent();
            
            // Set skin
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Load medicines
                _allMedicines = _medicineBLL.GetAllMedicines().Where(m => m.StockQuantity > 0).ToList();
                gridMedicines.DataSource = _allMedicines;

                // Load patients
                var patients = _patientBLL.GetAllPatients();
                slkPatient.Properties.DataSource = patients;
                slkPatient.Properties.DisplayMember = "FullName";
                slkPatient.Properties.ValueMember = "PatientId";
                
                // Configure columns for patient lookup
                slkPatient.Properties.View.Columns.Clear();
                slkPatient.Properties.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn { FieldName = "PatientCode", Caption = "Mã BN", Visible = true, VisibleIndex = 0 });
                slkPatient.Properties.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn { FieldName = "FullName", Caption = "Họ tên", Visible = true, VisibleIndex = 1 });
                slkPatient.Properties.View.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn { FieldName = "YearOfBirth", Caption = "Năm sinh", Visible = true, VisibleIndex = 2 });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChkWalkIn_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomerName.Enabled = chkWalkIn.Checked;
            slkPatient.Enabled = !chkWalkIn.Checked;
            
            if (chkWalkIn.Checked)
            {
                slkPatient.EditValue = null;
                txtCustomerName.Focus();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var selectedMedicine = gridViewMedicines.GetFocusedRow() as MedicineDTO;
            
            if (selectedMedicine == null)
            {
                XtraMessageBox.Show("Vui lòng chọn thuốc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int qty = (int)spnQuantity.Value;
            if (qty <= 0)
            {
                XtraMessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (qty > selectedMedicine.StockQuantity)
            {
                XtraMessageBox.Show($"Không đủ tồn kho! Còn {selectedMedicine.StockQuantity} {selectedMedicine.Unit}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if already in cart
            var existing = _cart.FirstOrDefault(c => c.MedicineId == selectedMedicine.MedicineId);
            if (existing != null)
            {
                if (existing.Quantity + qty > selectedMedicine.StockQuantity)
                {
                    XtraMessageBox.Show($"Tổng số lượng vượt tồn kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existing.Quantity += qty;
            }
            else
            {
                _cart.Add(new CartItem
                {
                    MedicineId = selectedMedicine.MedicineId,
                    MedicineName = selectedMedicine.MedicineName,
                    UnitPrice = selectedMedicine.Price,
                    Quantity = qty
                });
            }

            RefreshCart();
            spnQuantity.Value = 1;
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            var selectedItem = gridViewCart.GetFocusedRow() as CartItem;
            if (selectedItem == null)
            {
                XtraMessageBox.Show("Vui lòng chọn thuốc cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _cart.Remove(selectedItem);
            RefreshCart();
        }

        private void RefreshCart()
        {
            gridCart.DataSource = null;
            gridCart.DataSource = _cart;
            gridCart.RefreshDataSource();

            decimal total = _cart.Sum(c => c.LineTotal);
            lblTotal.Text = $"TỔNG: {total:N0} VNĐ";
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                XtraMessageBox.Show("Vui lòng thêm thuốc vào đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int pharmacistId = SessionManager.CurrentUser?.UserId ?? 1;
                int? patientId = null;
                string notes = "";

                if (chkWalkIn.Checked)
                {
                    notes = "Khách vãng lai: " + (string.IsNullOrWhiteSpace(txtCustomerName.Text) ? "Không tên" : txtCustomerName.Text);
                }
                else if (slkPatient.EditValue != null)
                {
                    patientId = (int)slkPatient.EditValue;
                }

                // Create sale
                int saleId = _saleBLL.CreateSale(patientId, null, null, pharmacistId, notes);

                // Add items
                foreach (var item in _cart)
                {
                    _saleBLL.AddItem(saleId, item.MedicineId, item.Quantity);
                }

                // Update sale with walk-in info
                if (chkWalkIn.Checked)
                {
                    var sale = _saleBLL.GetSaleById(saleId);
                    if (sale != null)
                    {
                        sale.IsWalkIn = true;
                        sale.WalkInCustomerName = string.IsNullOrWhiteSpace(txtCustomerName.Text) ? "Khách vãng lai" : txtCustomerName.Text;
                        new MedicineSaleDAL().Update(sale);
                    }
                }

                CreatedSaleId = saleId;

                XtraMessageBox.Show($"✅ Đã tạo đơn bán thuốc!\n\nTổng tiền: {_cart.Sum(c => c.LineTotal):N0} VNĐ\n\nYêu cầu khách đến thu ngân thanh toán.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            this.Close();
        }

        private class CartItem
        {
            public int MedicineId { get; set; }
            public string MedicineName { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public decimal LineTotal => UnitPrice * Quantity;
        }
    }
}
