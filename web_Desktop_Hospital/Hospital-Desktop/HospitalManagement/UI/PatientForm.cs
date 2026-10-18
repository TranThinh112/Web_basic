using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    public partial class PatientForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly PatientBLL _patientBLL;
        
        // Pagination
        private PaginationHelper _pagination;
        private string _currentKeyword = "";
        
        // Cache for current page
        private List<PatientDTO> _currentPageData;
        private Timer _debounceTimer;
        private const int DEBOUNCE_DELAY = 300;
        
        // Sorting
        private string _currentSortColumn = "";
        private bool _sortAscending = true;

        public PatientForm()
        {
            _patientBLL = new PatientBLL();
            _pagination = new PaginationHelper(20);
            _pagination.PageChanged += OnPageChanged;
            _currentPageData = new List<PatientDTO>();
            
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            gridView.OptionsBehavior.AutoPopulateColumns = false;
        }

        private void PatientForm_Load(object sender, EventArgs e)
        {
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

            cboSearchType.SelectedIndex = 0;
            
            // Button state management
            gridView.FocusedRowChanged += GridView_FocusedRowChanged;
            gridView.Click += (s, args) => UpdateButtonStates(); // Handle click to ensure state update
            UpdateButtonStates();

            SetupSearch();
            LoadData();
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

        private void SetupSearch()
        {
            _debounceTimer = new Timer();
            _debounceTimer.Interval = DEBOUNCE_DELAY;
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
            _currentKeyword = txtSearch.Text?.Trim() ?? "";
            int totalCount = _patientBLL.GetPatientCount(_currentKeyword);
            _pagination.SetTotalItems(totalCount);
            _pagination.ResetToFirstPage();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _currentKeyword = txtSearch.Text?.Trim() ?? "";
                int totalCount = _patientBLL.GetPatientCount(_currentKeyword);
                _pagination.SetTotalItems(totalCount);
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

        private void LoadCurrentPage()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _currentPageData = _patientBLL.GetPatientsPaged(
                    _currentKeyword, 
                    _pagination.CurrentPage, 
                    _pagination.PageSize,
                    _currentSortColumn,
                    _sortAscending);
                BindData(_currentPageData);
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

        private void OnPageChanged(int newPage)
        {
            LoadCurrentPage();
        }

        private void BindData(List<PatientDTO> patients)
        {
            gridPatients.DataSource = patients;
            
            // Clear selection to avoid auto-selecting first row
            gridView.ClearSelection();
            gridView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PatientDetailForm form = new PatientDetailForm();
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(gridView.GetRowCellValue(selectedRows[0], "PatientId"));
            PatientDTO patient = _patientBLL.GetPatientById(id);

            PatientDetailForm form = new PatientDetailForm(patient);
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (XtraMessageBox.Show("Xác nhận xóa bệnh nhân này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(gridView.GetRowCellValue(selectedRows[0], "PatientId"));
                    _patientBLL.DeletePatient(id);
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
            _currentSortColumn = "";
            _sortAscending = true;
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
