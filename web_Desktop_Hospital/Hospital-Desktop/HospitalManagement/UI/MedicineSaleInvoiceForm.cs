using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.Collections.Generic;
using System.Linq;
using HospitalManagement.BLL;
using HospitalManagement.DTO;
using HospitalManagement.Utils;

namespace HospitalManagement.UI
{
    /// <summary>
    /// Form in hóa đơn thuốc (DevExpress XtraForm)
    /// </summary>
    /// <summary>
    /// Form in hóa đơn thuốc (Sử dụng DevExpress LayoutControl & GridControl)
    /// </summary>
    public partial class MedicineSaleInvoiceForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly MedicineSaleDTO _sale;
        private readonly List<MedicineSaleDetailDTO> _details;

        // DevExpress Controls
        // DevExpress Controls are now in Designer


        public MedicineSaleInvoiceForm(int saleId)
        {
            var saleBLL = new MedicineSaleBLL();
            _sale = saleBLL.GetSaleById(saleId);
            _details = saleBLL.GetSaleDetails(saleId);
            
            if (_sale == null)
                throw new Exception("Không tìm thấy đơn thuốc");
            
            InitializeComponent();
            // InitializeDevExpressControls(); // Removed
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            BuildInvoiceLayout();
        }

        // InitializeDevExpressControls removed


        private void BuildInvoiceLayout()
        {
            if (_sale == null) return;

            // 1. Info Section (Data Binding)
            string invoiceCode = _sale.SaleCode ?? $"MS-{_sale.SaleId:D6}";
            lblInfoInvoiceNo.Text = $"Mã đơn: {invoiceCode}";
            
            lblInfoDate.Text = $"Ngày bán: {_sale.SaleDate.ToString("dd/MM/yyyy HH:mm")}";
            
            string pharmacistName = _sale.PharmacistName ?? SessionManager.CurrentEmployee?.FullName ?? "Dược sĩ";
            lblInfoStaff.Text = $"Dược sĩ: {pharmacistName}";

            string customerName = _sale.IsWalkIn ? (_sale.WalkInCustomerName ?? "Khách vãng lai") : _sale.PatientName;
            if (string.IsNullOrWhiteSpace(customerName))
                customerName = "Khách vãng lai";
            lblInfoPatient.Text = $"Khách hàng: {customerName}";
            
            // 2. Grid Items
            var items = new List<SalePrintItem>();
            if (_details != null)
            {
                int stt = 1;
                foreach(var d in _details)
                {
                    items.Add(new SalePrintItem { 
                        No = stt++,
                        MedicineName = d.MedicineName, 
                        Unit = d.Unit,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        LineTotal = d.LineTotal 
                    });
                }
            }
            gridControlItems.DataSource = items;

            // 3. Total Section (Dynamic)
            rootGroup.BeginUpdate();
            
            var groupTotal = rootGroup.AddGroup();
            groupTotal.GroupBordersVisible = true;
            groupTotal.TextVisible = false;
            groupTotal.AppearanceGroup.BackColor = Color.FromArgb(245, 245, 245);
            groupTotal.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
            groupTotal.OptionsTableLayoutGroup.ColumnDefinitions.Clear();
            groupTotal.OptionsTableLayoutGroup.RowDefinitions.Clear();
            
            groupTotal.OptionsTableLayoutGroup.ColumnDefinitions.Add(new DevExpress.XtraLayout.ColumnDefinition { SizeType = SizeType.Percent, Width = 100 }); 
            groupTotal.OptionsTableLayoutGroup.ColumnDefinitions.Add(new DevExpress.XtraLayout.ColumnDefinition { SizeType = SizeType.AutoSize });
            groupTotal.OptionsTableLayoutGroup.ColumnDefinitions.Add(new DevExpress.XtraLayout.ColumnDefinition { SizeType = SizeType.AutoSize });
            groupTotal.OptionsTableLayoutGroup.RowDefinitions.Add(new DevExpress.XtraLayout.RowDefinition { SizeType = SizeType.AutoSize });

            // Label
            DevExpress.XtraEditors.LabelControl lblTotalTitle = new DevExpress.XtraEditors.LabelControl();
            lblTotalTitle.Text = "TỔNG THANH TOÁN:  ";
            lblTotalTitle.Appearance.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTotalTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far; 
            
            var itemTitle = groupTotal.AddItem();
            itemTitle.Control = lblTotalTitle;
            itemTitle.TextVisible = false;
            itemTitle.OptionsTableLayoutItem.ColumnIndex = 1;

            // Value
            DevExpress.XtraEditors.LabelControl lblTotalValue = new DevExpress.XtraEditors.LabelControl();
            lblTotalValue.Text = $"{_sale.TotalAmount:N0} VNĐ";
            lblTotalValue.Appearance.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTotalValue.Appearance.ForeColor = UIHelper.DangerColor;
            lblTotalValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            var itemValue = groupTotal.AddItem();
            itemValue.Control = lblTotalValue;
            itemValue.TextVisible = false;
            itemValue.OptionsTableLayoutItem.ColumnIndex = 2;

            // Move Total below Grid
            groupTotal.Move(layoutControlItemGrid, DevExpress.XtraLayout.Utils.InsertType.Bottom);
            
            // Footer is static in Designer (layoutControlItemFooter1)
            // Ensure Footer is below Total (since Total inserted below Grid, and Footer was presumably below Grid)
            // If Footer was designed below Grid, inserting Total below Grid pushes Footer down. Correct.

            rootGroup.EndUpdate();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            ShowPrintPreview();
        }

        public void ShowPrintPreview()
        {
            if (!layoutControl.IsHandleCreated)
            {
                var h = layoutControl.Handle;
            }

            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem);
            
            link.Component = layoutControl;
            link.Landscape = false;
            // Fix: Use DXPaperKind
            link.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A5;
            link.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);
            
            link.ShowRibbonPreview(UserLookAndFeel.Default);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

    public class SalePrintItem
    {
        public int No { get; set; }
        public string MedicineName { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}

