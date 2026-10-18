using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.DTO;
using HospitalManagement.BLL;
using HospitalManagement.Utils;
using System.Collections.Generic;

namespace HospitalManagement.UI
{
    /// <summary>
    /// Form hiển thị và in hóa đơn (Sử dụng DevExpress XtraForm)
    /// </summary>
    /// <summary>
    /// Form hiển thị và in hóa đơn (Sử dụng DevExpress LayoutControl & GridControl)
    /// </summary>
    public partial class InvoicePrintForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly InvoiceDTO _invoice;
        private readonly PatientDTO _patient;
        private readonly InvoiceBreakdown _breakdown;
        
        // DevExpress Controls
        // DevExpress Controls are now in Designer


        public InvoicePrintForm(int invoiceId)
        {
            var invoiceBLL = new InvoiceBLL();
            var patientBLL = new PatientBLL();

            _invoice = invoiceBLL.GetInvoiceById(invoiceId);
            if (_invoice != null)
            {
                _patient = patientBLL.GetPatientById(_invoice.PatientId);
                _breakdown = invoiceBLL.GetInvoiceBreakdown(_invoice.VisitId);
            }

            InitializeComponent();
            // InitializeDevExpressControls(); // Removed
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            BuildInvoiceLayout();
        }

        public InvoicePrintForm(InvoiceDTO invoice, PatientDTO patient, InvoiceBreakdown breakdown)
        {
            _invoice = invoice;
            _patient = patient;
            _breakdown = breakdown;
            InitializeComponent();
            // InitializeDevExpressControls(); // Removed
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
            BuildInvoiceLayout();
        }

        // InitializeDevExpressControls method removed as controls are now in Designer

        private void BuildInvoiceLayout()
        {
            if (_invoice == null) return;

            // 1. Info Section (Data Binding to Designer Controls)
            lblInfoInvoiceNo.Text = $"Số hóa đơn: HD{_invoice.InvoiceId:D6}";
            lblInfoDate.Text = $"Ngày lập: {(_invoice.PaymentDate?.ToString("dd/MM/yyyy HH:mm") ?? DateTime.Now.ToString("dd/MM/yyyy HH:mm"))}";
            
            string staffName = SessionManager.CurrentEmployee?.FullName 
                             ?? SessionManager.CurrentDoctor?.FullName 
                             ?? SessionManager.CurrentUser?.Username 
                             ?? "Admin";
            lblInfoCashier.Text = $"Thu ngân: {staffName}";

            lblInfoPatient.Text = $"Bệnh nhân: {_patient?.FullName ?? "---"}";
            lblInfoPatientCode.Text = $"Mã BN: {_patient?.PatientCode ?? "---"}";

            // 2. Grid Items
            var items = new List<InvoicePrintItem>();
            int stt = 1;
            if (_breakdown != null)
            {
                if (_breakdown.ExaminationFee > 0)
                    items.Add(new InvoicePrintItem { No = stt++, Name = "Phí khám bệnh", Unit = "Lần", Price = _breakdown.ExaminationFee, Qty = 1, Total = _breakdown.ExaminationFee });
                
                if (_breakdown.MedicineDetails != null)
                {
                    foreach (var med in _breakdown.MedicineDetails)
                    {
                        items.Add(new InvoicePrintItem { No = stt++, Name = med.MedicineName, Unit = med.Unit, Price = med.UnitPrice, Qty = med.Quantity, Total = med.LineTotal });
                    }
                }
            }
            gridControlItems.DataSource = items;
            
            // 3. Total Section (Dynamic)
            rootGroup.BeginUpdate();
            
            decimal total = _breakdown?.GrandTotal ?? (_invoice?.TotalAmount ?? 0);
            
            var groupTotal = rootGroup.AddGroup();
            groupTotal.GroupBordersVisible = true;
            groupTotal.TextVisible = false;
            // Use specific color if needed, or default
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
            lblTotalTitle.Text = "TỔNG CỘNG:  ";
            lblTotalTitle.Appearance.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTotalTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            
            var itemTitle = groupTotal.AddItem();
            itemTitle.Control = lblTotalTitle;
            itemTitle.TextVisible = false;
            itemTitle.OptionsTableLayoutItem.ColumnIndex = 1;
            
            // Value
            DevExpress.XtraEditors.LabelControl lblTotalValue = new DevExpress.XtraEditors.LabelControl();
            lblTotalValue.Text = $"{total:N0} VNĐ";
            lblTotalValue.Appearance.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTotalValue.Appearance.ForeColor = UIHelper.DangerColor;
            lblTotalValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            
            var itemValue = groupTotal.AddItem();
            itemValue.Control = lblTotalValue;
            itemValue.TextVisible = false;
            itemValue.OptionsTableLayoutItem.ColumnIndex = 2;

            // Move Total below Grid
            groupTotal.Move(layoutControlItemGrid, DevExpress.XtraLayout.Utils.InsertType.Bottom);
            // Move Footer below Total
            // Note: Footer is layoutControlItemFooter1
            // But layoutControlItemFooter1 needs to be accessed. Assuming it's accessible.
            // If Footer was static in Designer, we might need to find it if Move is required.
            // But since we appended Total, and Total moved below Grid...
            // Footer is probably after Grid. Total inserted below Grid -> Total pushes Footer down?
            // Or Total sits between Grid and Footer?
            // "InsertType.Bottom" of Grid means: Put Total right after Grid.
            // If Footer was after Grid, Total will be between Grid and Footer. Correct.
            
            rootGroup.EndUpdate();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            ShowPrintPreview();
        }

        public void ShowPrintPreview()
        {
            // Ensure layout is ready even if form is not visible
            if (!layoutControl.IsHandleCreated)
            {
                // Force handle creation
                var h = layoutControl.Handle; 
            }
            
            // Use DevExpress Printing System
            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem);
            
            link.Component = layoutControl; // Validate Layout Control as Printable
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

    public class InvoicePrintItem
    {
        public int No { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal Total { get; set; }
    }
}
