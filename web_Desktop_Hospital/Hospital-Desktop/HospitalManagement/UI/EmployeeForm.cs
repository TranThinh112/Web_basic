using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.DAL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    public partial class EmployeeForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly EmployeeDAL _employeeDAL;
        
        private List<EmployeeDTO> _allEmployees;
        private List<EmployeeDTO> _filteredEmployees;
        private List<EmployeeDTO> _currentPageData;

        // Pagination
        private PaginationHelper _pagination;
        private Timer _debounceTimer;

        public EmployeeForm()
        {
            InitializeComponent();
            _employeeDAL = new EmployeeDAL();
            _allEmployees = new List<EmployeeDTO>();
            _filteredEmployees = new List<EmployeeDTO>();
            _currentPageData = new List<EmployeeDTO>();

            // Setup Pagination
            _pagination = new PaginationHelper(20);
            _pagination.PageChanged += OnPageChanged;

            // Add Pagination Panel
            Panel pagingPanel = _pagination.CreatePaginationPanel();
            pagingPanel.Parent = this; // Add to Form first
            
            // Add to LayoutControl
            DevExpress.XtraLayout.LayoutControlItem item = layoutControl.Root.AddItem();
            item.Control = pagingPanel;
            item.TextVisible = false;
            
            // Reorder layout items to put it at the bottom above buttons
            item.Move(layoutControlItemGrid, DevExpress.XtraLayout.Utils.InsertType.Bottom);
            // Move other bottom items below it? No, keeping it simple: Grid -> Paging -> Buttons

            // Better approach: Add it below the Grid
            
            // Apply skin
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            SetupSearch();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void SetupSearch()
        {
            _debounceTimer = new Timer();
            _debounceTimer.Interval = 300;
            _debounceTimer.Tick += (s, e) => { _debounceTimer.Stop(); PerformSearch(); };
            txtSearch.TextChanged += (s, e) => { _debounceTimer.Stop(); _debounceTimer.Start(); };
            cboSearchType.SelectedIndex = 0;
        }

        private void PerformSearch()
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            
            if (string.IsNullOrWhiteSpace(keyword))
            {
                _filteredEmployees = new List<EmployeeDTO>(_allEmployees);
            }
            else
            {
                int searchType = cboSearchType.SelectedIndex;
                _filteredEmployees = _allEmployees.Where(e =>
                {
                    switch (searchType)
                    {
                        case 1: return (e.FullName ?? "").ToLower().Contains(keyword);
                        case 2: return (e.Phone ?? "").Contains(keyword);
                        case 3: return (e.Position ?? "").ToLower().Contains(keyword);
                        case 4: return (e.Department ?? "").ToLower().Contains(keyword);
                        default: return (e.FullName ?? "").ToLower().Contains(keyword) ||
                                       (e.Phone ?? "").Contains(keyword) ||
                                       (e.Position ?? "").ToLower().Contains(keyword) ||
                                       (e.Department ?? "").ToLower().Contains(keyword);
                    }
                }).ToList();
            }
            
            // Update pagination
            _pagination.SetTotalItems(_filteredEmployees.Count);
            _pagination.ResetToFirstPage();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _allEmployees = _employeeDAL.GetAll();
                _filteredEmployees = new List<EmployeeDTO>(_allEmployees);
                
                _pagination.SetTotalItems(_filteredEmployees.Count);
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
                _currentPageData = _filteredEmployees.Skip(skip).Take(_pagination.PageSize).ToList();
                BindData(_currentPageData);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi phân trang: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindData(List<EmployeeDTO> employees)
        {
            gridEmployees.DataSource = employees;
            
            // Basic column config
            gridView.PopulateColumns();
            
            // Helper to hide columns
            void HideCol(string colName) { if (gridView.Columns[colName] != null) gridView.Columns[colName].Visible = false; }
            void SetCap(string colName, string caption) { if (gridView.Columns[colName] != null) gridView.Columns[colName].Caption = caption; }

            HideCol("DateOfBirth");
            HideCol("Gender");
            HideCol("Email");
            HideCol("Address");
            HideCol("HireDate");
            HideCol("UserId");

            SetCap("EmployeeId", "ID");
            SetCap("FullName", "Họ tên");
            SetCap("Phone", "SĐT");
            SetCap("Position", "Chức vụ");
            SetCap("Department", "Phòng ban");
            SetCap("Username", "Tài khoản");

            if (gridView.Columns["EmployeeId"] != null) gridView.Columns["EmployeeId"].Width = 50;

            // Update count label (optional, as PaginationHelper has its own info)
            lblCount.Text = $"Hiển thị: {employees.Count} / Tổng: {_filteredEmployees.Count} nhân viên";

            // Fix auto-selection
            gridView.ClearSelection();
            gridView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (new EmployeeDetailForm().ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int handle = gridView.FocusedRowHandle;
            if (handle < 0)
            {
                XtraMessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(gridView.GetRowCellValue(handle, "EmployeeId"));
            if (new EmployeeDetailForm(_employeeDAL.GetById(id)).ShowDialog() == DialogResult.OK) LoadData();
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

        private void cboSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }
    }
}
