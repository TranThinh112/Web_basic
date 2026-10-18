using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.DAL;
using HospitalManagement.DTO;

namespace HospitalManagement.UI
{
    public partial class DiseaseForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly DiseaseDAL _diseaseDAL;
        private List<DiseaseDTO> _allDiseases;
        private List<DiseaseDTO> _filteredDiseases;
        private Timer _debounceTimer;

        public DiseaseForm()
        {
            _diseaseDAL = new DiseaseDAL();
            _allDiseases = new List<DiseaseDTO>();
            _filteredDiseases = new List<DiseaseDTO>();
            
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            gridView.OptionsBehavior.AutoPopulateColumns = false;
        }

        private void DiseaseForm_Load(object sender, EventArgs e)
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
                _filteredDiseases = new List<DiseaseDTO>(_allDiseases);
            }
            else
            {
                int searchType = cboSearchType.SelectedIndex;
                _filteredDiseases = _allDiseases.Where(d =>
                {
                    switch (searchType)
                    {
                        case 1: return (d.DiseaseName ?? "").ToLower().Contains(keyword);
                        case 2: return (d.Category ?? "").ToLower().Contains(keyword);
                        default: return (d.DiseaseName ?? "").ToLower().Contains(keyword) ||
                                       (d.Category ?? "").ToLower().Contains(keyword) ||
                                       (d.Description ?? "").ToLower().Contains(keyword);
                    }
                }).ToList();
            }

            BindData(_filteredDiseases);
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _allDiseases = _diseaseDAL.GetAll();
                _filteredDiseases = new List<DiseaseDTO>(_allDiseases);
                BindData(_filteredDiseases);
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

        private void BindData(List<DiseaseDTO> diseases)
        {
            gridDiseases.DataSource = diseases;

            if (gridView.Columns.Count > 0)
            {
                if (gridView.Columns["DiseaseId"] != null)
                {
                    gridView.Columns["DiseaseId"].Caption = "Mã";
                    gridView.Columns["DiseaseId"].Width = 60;
                }
                if (gridView.Columns["DiseaseName"] != null)
                    gridView.Columns["DiseaseName"].Caption = "Tên bệnh";
                if (gridView.Columns["Category"] != null)
                    gridView.Columns["Category"].Caption = "Phân loại";
                if (gridView.Columns["Description"] != null)
                    gridView.Columns["Description"].Caption = "Mô tả";
            }

            lblCount.Text = $"Hiển thị: {diseases.Count} / Tổng: {_allDiseases.Count} bệnh";

            // Clear selection to avoid auto-selecting first row
            gridView.ClearSelection();
            gridView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DiseaseDetailForm form = new DiseaseDetailForm();
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn bệnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(gridView.GetRowCellValue(selectedRows[0], "DiseaseId"));
            DiseaseDetailForm form = new DiseaseDetailForm(_diseaseDAL.GetById(id));
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn bệnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (XtraMessageBox.Show("Xác nhận xóa bệnh này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(gridView.GetRowCellValue(selectedRows[0], "DiseaseId"));
                    _diseaseDAL.Delete(id);
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
