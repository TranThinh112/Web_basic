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
    public partial class MedicineForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly MedicineBLL _medicineBLL;
        
        private List<MedicineDTO> _allMedicines;
        private List<MedicineDTO> _filteredMedicines;
        private List<MedicineDTO> _currentPageData;

        // Pagination
        private PaginationHelper _pagination;
        private Timer _debounceTimer;

        public MedicineForm()
        {
            InitializeComponent();
            _medicineBLL = new MedicineBLL();
            _allMedicines = new List<MedicineDTO>();
            _filteredMedicines = new List<MedicineDTO>();
            _currentPageData = new List<MedicineDTO>();

            // UI Helper
            UIHelper.ConfigureModernForm(this);
            UIHelper.ConfigureDevExpressGridView(gridView);
            UIHelper.ClearGridViewSelection(gridView);
            
            // Standardize Buttons
            UIHelper.StyleAddButton(btnAdd);
            UIHelper.StyleEditButton(btnEdit);
            UIHelper.StyleDeleteButton(btnDelete);
            
            UIHelper.StyleSecondaryButton(btnRefresh);
            btnRefresh.Text = "Làm mới";
            
            UIHelper.StyleCloseButton(btnClose);

            // Setup Pagination
            _pagination = new PaginationHelper(20);
            _pagination.PageChanged += OnPageChanged;

            // Add Pagination Panel
            Panel pagingPanel = _pagination.CreatePaginationPanel();
            pagingPanel.Parent = this;
            
            DevExpress.XtraLayout.LayoutControlItem item = layoutControl.Root.AddItem();
            item.Control = pagingPanel;
            item.TextVisible = false;
            
            item.Move(layoutControlItemGrid, DevExpress.XtraLayout.Utils.InsertType.Bottom);

            // Button State Logic
            gridView.FocusedRowChanged += GridView_FocusedRowChanged;
            gridView.Click += (s, e) => UpdateButtonStates();
            UpdateButtonStates();
        }

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            int[] selectedRows = gridView.GetSelectedRows();
            bool hasSelection = selectedRows.Length > 0 && selectedRows[0] >= 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }

        private void MedicineForm_Load(object sender, EventArgs e)
        {
            cboSearchType.SelectedIndex = 0;
            SetupSearch();
            LoadData();
        }

        private void SetupSearch()
        {
            _debounceTimer = new Timer();
            _debounceTimer.Interval = 300;
            _debounceTimer.Tick += DebounceTimer_Tick;
            txtSearch.EditValueChanged += TxtSearch_EditValueChanged;
        }

        private void TxtSearch_EditValueChanged(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            PerformSearch();
        }

        private void cboSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            string keyword = (txtSearch.Text ?? "").Trim().ToLower();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                _filteredMedicines = new List<MedicineDTO>(_allMedicines);
            }
            else
            {
                int searchType = cboSearchType.SelectedIndex;
                _filteredMedicines = _allMedicines.Where(m =>
                {
                    switch (searchType)
                    {
                        case 1: return (m.MedicineName ?? "").ToLower().Contains(keyword);
                        case 2: return (m.Unit ?? "").ToLower().Contains(keyword);
                        default: return (m.MedicineName ?? "").ToLower().Contains(keyword) ||
                                       (m.Unit ?? "").ToLower().Contains(keyword);
                    }
                }).ToList();
            }

            // Update pagination
            _pagination.SetTotalItems(_filteredMedicines.Count);
            _pagination.ResetToFirstPage();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _allMedicines = _medicineBLL.GetAllMedicines();
                _filteredMedicines = new List<MedicineDTO>(_allMedicines);
                
                _pagination.SetTotalItems(_filteredMedicines.Count);
                LoadCurrentPage();
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

        private void OnPageChanged(int page)
        {
            LoadCurrentPage();
        }

        private void LoadCurrentPage()
        {
            try
            {
                int skip = _pagination.GetSkipCount();
                _currentPageData = _filteredMedicines.Skip(skip).Take(_pagination.PageSize).ToList();
                BindData(_currentPageData);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi phân trang: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindData(List<MedicineDTO> medicines)
        {
            gridMedicines.DataSource = medicines;

            if (gridView.Columns.Count > 0)
            {
                if (gridView.Columns["MedicineId"] != null)
                {
                    gridView.Columns["MedicineId"].Caption = "Mã";
                    gridView.Columns["MedicineId"].Width = 60;
                }
                if (gridView.Columns["MedicineName"] != null)
                    gridView.Columns["MedicineName"].Caption = "Tên thuốc";
                if (gridView.Columns["Unit"] != null)
                    gridView.Columns["Unit"].Caption = "Đơn vị";
                if (gridView.Columns["Price"] != null)
                {
                    gridView.Columns["Price"].Caption = "Giá";
                    gridView.Columns["Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView.Columns["Price"].DisplayFormat.FormatString = "N0";
                }
            }

            lblCount.Text = $"Hiển thị: {medicines.Count} / Tổng: {_filteredMedicines.Count} thuốc";

            // Clear selection to avoid auto-selecting first row
            gridView.ClearSelection();
            gridView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MedicineDetailForm form = new MedicineDetailForm();
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn thuốc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(gridView.GetRowCellValue(selectedRows[0], "MedicineId"));
            MedicineDetailForm form = new MedicineDetailForm(_medicineBLL.GetMedicineById(id));
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn thuốc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (XtraMessageBox.Show("Xác nhận xóa thuốc này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(gridView.GetRowCellValue(selectedRows[0], "MedicineId"));
                    _medicineBLL.DeleteMedicine(id);
                    XtraMessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cboSearchType.SelectedIndex = 0;
            txtSearch.Text = "";
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
