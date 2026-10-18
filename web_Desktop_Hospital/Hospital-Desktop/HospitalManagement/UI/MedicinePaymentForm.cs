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
    /// Form thu tiền thuốc - Thu ngân sử dụng
    /// </summary>
    public partial class MedicinePaymentForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly MedicineSaleBLL _saleBLL;
        private List<MedicineSaleDTO> _allSales;
        private List<MedicineSaleDTO> _filteredSales;

        // Pagination
        private PaginationHelper _pagination;

        public MedicinePaymentForm()
        {
            _saleBLL = new MedicineSaleBLL();
            _allSales = new List<MedicineSaleDTO>();
            _filteredSales = new List<MedicineSaleDTO>();

            InitializeComponent();
            
            // Set skin
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            
            // GridView Selection Changed
            gridViewSales.FocusedRowChanged += GridViewSales_FocusedRowChanged;
            
            // UI Helper
            UIHelper.ConfigureModernForm(this);
            UIHelper.ConfigureDevExpressGridView(gridViewSales);
            // gridDetails might need config too?
            // UIHelper.ConfigureDevExpressGridView(gridDetailsView); // Assuming gridDetails has a view. Let's skip for now or verify.
            
            // Button Styling
            UIHelper.StyleSecondaryButton(btnHistory);
            btnHistory.Text = "📋 Hóa đơn cũ";
            
            UIHelper.StyleSecondaryButton(btnRefresh);
            btnRefresh.Text = "Làm mới";

            UIHelper.StyleSuccessButton(btnPayCash);
            btnPayCash.Text = "💵 Tiền mặt";

            UIHelper.StylePrimaryButton(btnPayCard);
            btnPayCard.Text = "💳 Thẻ";

            UIHelper.StyleSecondaryButton(btnPayTransfer);
            btnPayTransfer.Text = "🏦 Chuyển khoản";

            UIHelper.StyleCloseButton(btnClose);
            
            // Setup Pagination
            _pagination = new PaginationHelper(20);
            _pagination.PageChanged += OnPageChanged;
            
            // Add Pagination Panel
            // Add to grpList which holds the grid
            Panel pagingPanel = _pagination.CreatePaginationPanel();
            pagingPanel.Dock = DockStyle.Bottom;
            
            // We need to add it to grpList. The GridControl is Dock=Fill.
            // If we add panel with Dock=Bottom, it will take space at bottom.
            grpList.Controls.Add(pagingPanel);
            
            // Ensure proper control order so grid fills remaining space
            gridSales.BringToFront(); 

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _allSales = _saleBLL.GetPendingPayments();
                _filteredSales = new List<MedicineSaleDTO>(_allSales);
                
                // Search filtering logic if needed (currently no search box in UI, assuming all pending displayed)
                // If search functionality is needed, it should be added here similar to other forms.
                // For now, pagination simply pages _allSales.
                
                _pagination.SetTotalItems(_filteredSales.Count);
                LoadCurrentPage();
                
                // Clear details
                gridDetails.DataSource = null;
                UpdateTotal(0);
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
            int skip = _pagination.GetSkipCount();
            var pageData = _filteredSales.Skip(skip).Take(_pagination.PageSize).ToList();
            
            gridSales.DataSource = pageData;
            
            // If current selection is lost due to paging, clear details
            if (pageData.Count > 0)
            {
                // Optionally select first row
                // gridViewSales.FocusedRowHandle = 0;
            }
            else
            {
                gridDetails.DataSource = null;
                UpdateTotal(0);
            }
        }

        private void GridViewSales_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var selectedSale = gridViewSales.GetRow(e.FocusedRowHandle) as MedicineSaleDTO;
            if (selectedSale == null) return;

            LoadDetails(selectedSale.SaleId);
        }

        private void LoadDetails(int saleId)
        {
            try
            {
                var details = _saleBLL.GetSaleDetails(saleId);
                gridDetails.DataSource = details;

                decimal total = details.Sum(d => d.LineTotal);
                UpdateTotal(total);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Silent fail or log
            }
        }

        private void UpdateTotal(decimal total)
        {
            lblTotal.Text = $"TỔNG TIỀN: {total:N0} VNĐ";
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            // Assuming InvoiceHistoryForm handles medicine invoices too, or strictly examination?
            // Original code used InvoiceHistoryForm. 
            // If there's a specific MedicineInvoiceHistoryForm, use that.
            // Based on context, there is ExaminationInvoiceHistoryForm and InvoiceHistoryForm.
            // Let's use InvoiceHistoryForm as seen in original code.
            new InvoiceHistoryForm().ShowDialog();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void BtnPayCash_Click(object sender, EventArgs e)
        {
            ProcessPayment("Cash");
        }

        private void BtnPayCard_Click(object sender, EventArgs e)
        {
            ProcessPayment("Card");
        }

        private void BtnPayTransfer_Click(object sender, EventArgs e)
        {
            ProcessPayment("Transfer");
        }

        private void ProcessPayment(string paymentMethod)
        {
            var selectedSale = gridViewSales.GetFocusedRow() as MedicineSaleDTO;
            
            if (selectedSale == null)
            {
                XtraMessageBox.Show("Vui lòng chọn đơn thuốc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string methodDisplay = "";
                switch (paymentMethod)
                {
                    case "Cash": methodDisplay = "Tiền mặt"; break;
                    case "Card": methodDisplay = "Thẻ"; break;
                    case "Transfer": methodDisplay = "Chuyển khoản"; break;
                }

                if (XtraMessageBox.Show(
                    $"Xác nhận thu tiền thuốc:\n\n" +
                    $"Bệnh nhân: {selectedSale.PatientName}\n" +
                    $"Số tiền: {selectedSale.TotalAmount:N0} VNĐ\n" +
                    $"Phương thức: {methodDisplay}",
                    "Xác nhận thanh toán",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int cashierId = SessionManager.CurrentUser?.UserId ?? 1;
                    _saleBLL.ProcessPayment(selectedSale.SaleId, cashierId, paymentMethod);

                    // Get updated sale info
                    var sale = _saleBLL.GetSaleById(selectedSale.SaleId);
                    string saleCode = sale?.SaleCode ?? selectedSale.SaleId.ToString();

                    XtraMessageBox.Show(
                        $"✅ Thanh toán thành công!\n\nMã đơn: {saleCode}\nYêu cầu bệnh nhân quay lại quầy thuốc để nhận thuốc.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Print invoice
                    try
                    {
                        new MedicineSaleInvoiceForm(selectedSale.SaleId).ShowDialog();
                    }
                    catch
                    {
                        XtraMessageBox.Show($"Hóa đơn thuốc: {saleCode}\nTổng tiền: {selectedSale.TotalAmount:N0} VNĐ", "Hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    LoadData();
                }
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
