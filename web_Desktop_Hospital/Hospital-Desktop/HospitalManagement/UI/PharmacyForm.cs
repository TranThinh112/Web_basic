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
    /// Form quầy thuốc - Dược sĩ sử dụng
    /// </summary>
    public partial class PharmacyForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly MedicineSaleBLL _saleBLL;
        private List<MedicineSaleDTO> _allDirectSales;
        private List<MedicineSaleDTO> _allPendingSales;
        private List<MedicineSaleDTO> _allPaidSales;

        public PharmacyForm()
        {
            _saleBLL = new MedicineSaleBLL();
            InitializeComponent();
            
            // Form Style
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            UIHelper.ConfigureModernForm(this);

            // Init date
            dtpDate.EditValue = DateTime.Now;
            dtpDate.EditValueChanged += (s, e) => LoadData();
            
            // Tab change logic
            xtraTabControl1.SelectedPageChanged += (s, e) => UpdateButtonVisibility();
            UpdateButtonVisibility(); // Initial state

            LoadData();
        }

        private void UpdateButtonVisibility()
        {
            var selectedTab = xtraTabControl1.SelectedTabPage;
            
            if (selectedTab == tabDirectSales)
            {
                btnNewSale.Visible = true;
                btnViewSale.Visible = true;
                btnCancel.Visible = false;
                btnDispense.Visible = false;
            }
            else if (selectedTab == tabPendingSales)
            {
                btnNewSale.Visible = false;
                btnViewSale.Visible = false;
                btnCancel.Visible = true;
                btnDispense.Visible = false;
            }
            else if (selectedTab == tabPaidSales)
            {
                btnNewSale.Visible = false;
                btnViewSale.Visible = false;
                btnCancel.Visible = false;
                btnDispense.Visible = true;
            }
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                LoadDirectSales();
                LoadPendingSales();
                LoadPaidSales();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void LoadDirectSales()
        {
            if (dtpDate.EditValue == null) return;
            DateTime date = (DateTime)dtpDate.EditValue;
            _allDirectSales = _saleBLL.GetSalesByDate(date);
            BindDirectSales(_allDirectSales);
        }

        private void LoadPendingSales()
        {
            if (dtpDate.EditValue == null) return;
            DateTime date = (DateTime)dtpDate.EditValue;
            
            // Pending sales - filter by date
            var allPending = _saleBLL.GetPendingPayments();
            _allPendingSales = allPending.Where(s => s.SaleDate.Date == date.Date).ToList();
            BindPendingSales(_allPendingSales);
        }

        private void LoadPaidSales()
        {
            if (dtpDate.EditValue == null) return;
            DateTime date = (DateTime)dtpDate.EditValue;

            // Paid sales waiting for dispensing - filter by date
            var allPaid = _saleBLL.GetPaidNotDispensed();
            _allPaidSales = allPaid.Where(s => s.SaleDate.Date == date.Date).ToList();
            BindPaidSales(_allPaidSales);
        }

        private void BindDirectSales(List<MedicineSaleDTO> sales)
        {
            gridDirectSales.DataSource = null;
            if (sales == null || sales.Count == 0) return;
            gridDirectSales.DataSource = sales;
        }

        private void BindPendingSales(List<MedicineSaleDTO> sales)
        {
            gridPendingSales.DataSource = null;
            if (sales == null || sales.Count == 0) return;
            gridPendingSales.DataSource = sales;
        }

        private void BindPaidSales(List<MedicineSaleDTO> sales)
        {
            gridPaidSales.DataSource = null;
            if (sales == null || sales.Count == 0) return;
            gridPaidSales.DataSource = sales;
        }

        private void PerformSearch()
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(keyword))
            {
                // No search - show all data for selected date
                BindDirectSales(_allDirectSales);
                BindPendingSales(_allPendingSales);
                BindPaidSales(_allPaidSales);
                return;
            }

            // Filter by keyword
            var filteredDirect = _allDirectSales?.Where(s => 
                (s.SaleCode ?? "").ToLower().Contains(keyword) ||
                (s.PatientName ?? "").ToLower().Contains(keyword) ||
                (s.WalkInCustomerName ?? "").ToLower().Contains(keyword)
            ).ToList() ?? new List<MedicineSaleDTO>();

            var filteredPending = _allPendingSales?.Where(s => 
                (s.SaleCode ?? "").ToLower().Contains(keyword) ||
                (s.PatientName ?? "").ToLower().Contains(keyword) ||
                (s.WalkInCustomerName ?? "").ToLower().Contains(keyword)
            ).ToList() ?? new List<MedicineSaleDTO>();

            var filteredPaid = _allPaidSales?.Where(s => 
                (s.SaleCode ?? "").ToLower().Contains(keyword) ||
                (s.PatientName ?? "").ToLower().Contains(keyword) ||
                (s.WalkInCustomerName ?? "").ToLower().Contains(keyword)
            ).ToList() ?? new List<MedicineSaleDTO>();

            BindDirectSales(filteredDirect);
            BindPendingSales(filteredPending);
            BindPaidSales(filteredPaid);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void BtnNewDirectSale_Click(object sender, EventArgs e)
        {
            var form = new DirectSaleForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                xtraTabControl1.SelectedTabPage = tabPendingSales; // Switch to pending tab
            }
        }

        private void BtnViewDirectSale_Click(object sender, EventArgs e)
        {
            var selectedRow = gridViewDirectSales.GetFocusedRow() as MedicineSaleDTO;
            if (selectedRow == null)
            {
                XtraMessageBox.Show("Vui lòng chọn đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                new MedicineSaleDetailForm(selectedRow.SaleId).ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelSale_Click(object sender, EventArgs e)
        {
            var selectedRow = gridViewPendingSales.GetFocusedRow() as MedicineSaleDTO;
            if (selectedRow == null)
            {
                XtraMessageBox.Show("Vui lòng chọn đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (XtraMessageBox.Show("Xác nhận hủy đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _saleBLL.CancelSale(selectedRow.SaleId);
                    XtraMessageBox.Show("Đã hủy đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDispense_Click(object sender, EventArgs e)
        {
            var selectedRow = gridViewPaidSales.GetFocusedRow() as MedicineSaleDTO;
            if (selectedRow == null)
            {
                XtraMessageBox.Show("Vui lòng chọn đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (XtraMessageBox.Show(
                    $"Xác nhận đã phát thuốc cho:\n\n{selectedRow.PatientName}\nMã đơn: {selectedRow.SaleCode}",
                    "Xác nhận phát thuốc",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _saleBLL.DispenseMedicine(selectedRow.SaleId);
                    XtraMessageBox.Show("✅ Đã phát thuốc và trừ tồn kho!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
