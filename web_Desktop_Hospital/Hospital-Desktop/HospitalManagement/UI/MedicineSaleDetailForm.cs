using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.BLL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    /// <summary>
    /// Form xem chi tiết phiếu thuốc (cho dược sĩ)
    /// </summary>
    public partial class MedicineSaleDetailForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly MedicineSaleDTO _sale;
        private readonly MedicineSaleBLL _saleBLL;

        public MedicineSaleDetailForm(int saleId)
        {
            _saleBLL = new MedicineSaleBLL();
            _sale = _saleBLL.GetSaleById(saleId);
            
            if (_sale == null)
                throw new Exception("Không tìm thấy đơn thuốc");
            
            InitializeComponent();
            
            // Form Style
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            LoadData();
        }

        private void LoadData()
        {
            // Info
            lblCode.Text = _sale.SaleCode ?? $"MS-{_sale.SaleId}";
            
            string customerName = _sale.IsWalkIn 
                ? (_sale.WalkInCustomerName ?? "Khách vãng lai") 
                : (_sale.PatientName ?? "---");
            lblCustomer.Text = customerName;
            
            lblDate.Text = _sale.SaleDate.ToString("dd/MM/yyyy HH:mm");

            // Status
            lblStatus.Text = GetStatusText(_sale.Status);
            lblStatus.ForeColor = GetStatusColor(_sale.Status);

            // Details
            var details = _saleBLL.GetSaleDetails(_sale.SaleId);
            
            // Grid binding
            // Note: DevExpress GridView auto-generates columns if DataSource is list of objects.
            // But we defined columns in Designer. We should map FieldNames correctly in Designer.
            // In Designer.cs:
            // colMedicine.FieldName = "MedicineName"
            // colQuantity.FieldName = "Quantity"
            // colPrice.FieldName = "UnitPrice"
            // colTotal.FieldName = "LineTotal"
            // These match DTO properties.
            
            gridDetails.DataSource = details;

            // Total
            lblTotal.Text = $"TỔNG TIỀN: {_sale.TotalAmount:N0} VNĐ";
        }

        private string GetStatusText(string status)
        {
            switch (status)
            {
                case "Pending": return "⏳ Chờ thanh toán";
                case "Paid": return "💰 Đã thanh toán";
                case "Dispensed": return "✅ Đã phát thuốc";
                case "Cancelled": return "❌ Đã hủy";
                default: return status ?? "";
            }
        }

        private Color GetStatusColor(string status)
        {
            switch (status)
            {
                case "Pending": return Color.Orange;
                case "Paid": return Color.Blue;
                case "Dispensed": return Color.Green;
                case "Cancelled": return Color.Red;
                default: return Color.Gray;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
