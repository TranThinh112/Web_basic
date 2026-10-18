namespace HospitalManagement.UI
{
    partial class MedicineSaleInvoiceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlButtons = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlItems = new DevExpress.XtraGrid.GridControl();
            this.gridViewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItemNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rootGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblLogo = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItemLogo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblClinicName = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItemClinicName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAddress = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItemAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblHotline = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItemHotline = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItemTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblInfoInvoiceNo = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItemInfoInvoiceNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblInfoDate = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItemInfoDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblInfoStaff = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItemInfoStaff = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblInfoPatient = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItemInfoPatient = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblFooter1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItemFooter1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).BeginInit();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClinicName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHotline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfoInvoiceNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfoDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfoStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfoPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFooter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Controls.Add(this.btnPrint);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 581);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(448, 58);
            this.pnlButtons.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(230, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 38);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Appearance.Options.UseBackColor = true;
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Location = new System.Drawing.Point(100, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 38);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "🖨️ In hóa đơn";
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.lblLogo);
            this.layoutControl.Controls.Add(this.lblClinicName);
            this.layoutControl.Controls.Add(this.lblAddress);
            this.layoutControl.Controls.Add(this.lblHotline);
            this.layoutControl.Controls.Add(this.lblTitle);
            this.layoutControl.Controls.Add(this.lblInfoInvoiceNo);
            this.layoutControl.Controls.Add(this.lblInfoDate);
            this.layoutControl.Controls.Add(this.lblInfoStaff);
            this.layoutControl.Controls.Add(this.lblInfoPatient);
            this.layoutControl.Controls.Add(this.gridControlItems);
            this.layoutControl.Controls.Add(this.lblFooter1);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.rootGroup;
            this.layoutControl.Size = new System.Drawing.Size(448, 581);
            this.layoutControl.TabIndex = 2;
            // 
            // gridControlItems
            // 
            this.gridControlItems.Location = new System.Drawing.Point(42, 32);
            this.gridControlItems.MainView = this.gridViewItems;
            this.gridControlItems.Name = "gridControlItems";
            this.gridControlItems.Size = new System.Drawing.Size(364, 296);
            this.gridControlItems.TabIndex = 4;
            this.gridControlItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItems});
            // 
            // gridViewItems
            // 
            this.gridViewItems.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemNo,
            this.colItemName,
            this.colItemUnit,
            this.colItemQty,
            this.colItemPrice,
            this.colItemTotal});
            this.gridViewItems.GridControl = this.gridControlItems;
            this.gridViewItems.Name = "gridViewItems";
            this.gridViewItems.OptionsView.ShowGroupPanel = false;
            this.gridViewItems.OptionsView.ShowIndicator = false;
            // 
            // colItemNo
            // 
            this.colItemNo.Caption = "STT";
            this.colItemNo.FieldName = "No";
            this.colItemNo.Name = "colItemNo";
            this.colItemNo.Visible = true;
            this.colItemNo.VisibleIndex = 0;
            this.colItemNo.Width = 35;
            // 
            // colItemName
            // 
            this.colItemName.Caption = "Tên thuốc/Vật tư";
            this.colItemName.FieldName = "MedicineName";
            this.colItemName.Name = "colItemName";
            this.colItemName.Visible = true;
            this.colItemName.VisibleIndex = 1;
            // 
            // colItemUnit
            // 
            this.colItemUnit.Caption = "ĐVT";
            this.colItemUnit.FieldName = "Unit";
            this.colItemUnit.Name = "colItemUnit";
            this.colItemUnit.Visible = true;
            this.colItemUnit.VisibleIndex = 2;
            this.colItemUnit.Width = 45;
            // 
            // colItemQty
            // 
            this.colItemQty.Caption = "SL";
            this.colItemQty.FieldName = "Quantity";
            this.colItemQty.Name = "colItemQty";
            this.colItemQty.Visible = true;
            this.colItemQty.VisibleIndex = 3;
            this.colItemQty.Width = 40;
            // 
            // colItemPrice
            // 
            this.colItemPrice.Caption = "Đơn giá";
            this.colItemPrice.DisplayFormat.FormatString = "N0";
            this.colItemPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colItemPrice.FieldName = "UnitPrice";
            this.colItemPrice.Name = "colItemPrice";
            this.colItemPrice.Visible = true;
            this.colItemPrice.VisibleIndex = 4;
            // 
            // colItemTotal
            // 
            this.colItemTotal.Caption = "Thành tiền";
            this.colItemTotal.DisplayFormat.FormatString = "N0";
            this.colItemTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colItemTotal.FieldName = "LineTotal";
            this.colItemTotal.Name = "colItemTotal";
            this.colItemTotal.Visible = true;
            this.colItemTotal.VisibleIndex = 5;
            // 
            // rootGroup
            // 
            this.rootGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.rootGroup.GroupBordersVisible = false;
            this.rootGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemLogo,
            this.layoutControlItemClinicName,
            this.layoutControlItemAddress,
            this.layoutControlItemHotline,
            this.emptySpaceItem1,
            this.layoutControlItemTitle,
            this.layoutControlItemInfoInvoiceNo,
            this.layoutControlItemInfoDate,
            this.layoutControlItemInfoStaff,
            this.layoutControlItemInfoPatient,
            this.layoutControlItemGrid,
            this.emptySpaceItem2,
            this.layoutControlItemFooter1});
            this.rootGroup.Name = "rootGroup";
            this.rootGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(40, 40, 30, 30);
            this.rootGroup.Size = new System.Drawing.Size(448, 581);
            this.rootGroup.TextVisible = false;
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gridControlItems;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 290);
            this.layoutControlItemGrid.MaxSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.MinSize = new System.Drawing.Size(1, 150);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(368, 220); // Adjusted height
            this.layoutControlItemGrid.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // lblLogo
            // 
            this.lblLogo.Appearance.Font = new System.Drawing.Font("Segoe UI Emoji", 40F);
            this.lblLogo.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblLogo.Appearance.Options.UseFont = true;
            this.lblLogo.Appearance.Options.UseForeColor = true;
            this.lblLogo.Appearance.Options.UseTextOptions = true;
            this.lblLogo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblLogo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLogo.Location = new System.Drawing.Point(12, 12);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(424, 71);
            this.lblLogo.StyleController = this.layoutControl;
            this.lblLogo.TabIndex = 5;
            this.lblLogo.Text = "🏥";
            // 
            // layoutControlItemLogo
            // 
            this.layoutControlItemLogo.Control = this.lblLogo;
            this.layoutControlItemLogo.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemLogo.Name = "layoutControlItemLogo";
            this.layoutControlItemLogo.Size = new System.Drawing.Size(368, 75);
            this.layoutControlItemLogo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemLogo.TextVisible = false;
            // 
            // lblClinicName
            // 
            this.lblClinicName.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblClinicName.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.lblClinicName.Appearance.Options.UseFont = true;
            this.lblClinicName.Appearance.Options.UseForeColor = true;
            this.lblClinicName.Appearance.Options.UseTextOptions = true;
            this.lblClinicName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblClinicName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblClinicName.Location = new System.Drawing.Point(12, 87);
            this.lblClinicName.Name = "lblClinicName";
            this.lblClinicName.Size = new System.Drawing.Size(424, 30);
            this.lblClinicName.StyleController = this.layoutControl;
            this.lblClinicName.TabIndex = 6;
            this.lblClinicName.Text = "PHÒNG KHÁM ĐA KHOA";
            // 
            // layoutControlItemClinicName
            // 
            this.layoutControlItemClinicName.Control = this.lblClinicName;
            this.layoutControlItemClinicName.Location = new System.Drawing.Point(0, 75);
            this.layoutControlItemClinicName.Name = "layoutControlItemClinicName";
            this.layoutControlItemClinicName.Size = new System.Drawing.Size(368, 34);
            this.layoutControlItemClinicName.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemClinicName.TextVisible = false;
            // 
            // lblAddress
            // 
            this.lblAddress.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAddress.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblAddress.Appearance.Options.UseFont = true;
            this.lblAddress.Appearance.Options.UseForeColor = true;
            this.lblAddress.Appearance.Options.UseTextOptions = true;
            this.lblAddress.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAddress.Location = new System.Drawing.Point(12, 121);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(424, 15);
            this.lblAddress.StyleController = this.layoutControl;
            this.lblAddress.TabIndex = 7;
            this.lblAddress.Text = "Địa chỉ: 123 Đường ABC, Quận XYZ, TP.HCM";
            // 
            // layoutControlItemAddress
            // 
            this.layoutControlItemAddress.Control = this.lblAddress;
            this.layoutControlItemAddress.Location = new System.Drawing.Point(0, 109);
            this.layoutControlItemAddress.Name = "layoutControlItemAddress";
            this.layoutControlItemAddress.Size = new System.Drawing.Size(368, 19);
            this.layoutControlItemAddress.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAddress.TextVisible = false;
            // 
            // lblHotline
            // 
            this.lblHotline.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHotline.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblHotline.Appearance.Options.UseFont = true;
            this.lblHotline.Appearance.Options.UseForeColor = true;
            this.lblHotline.Appearance.Options.UseTextOptions = true;
            this.lblHotline.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblHotline.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblHotline.Location = new System.Drawing.Point(12, 140);
            this.lblHotline.Name = "lblHotline";
            this.lblHotline.Size = new System.Drawing.Size(424, 15);
            this.lblHotline.StyleController = this.layoutControl;
            this.lblHotline.TabIndex = 8;
            this.lblHotline.Text = "Hotline: 1900 8888";
            // 
            // layoutControlItemHotline
            // 
            this.layoutControlItemHotline.Control = this.lblHotline;
            this.layoutControlItemHotline.Location = new System.Drawing.Point(0, 128);
            this.layoutControlItemHotline.Name = "layoutControlItemHotline";
            this.layoutControlItemHotline.Size = new System.Drawing.Size(368, 19);
            this.layoutControlItemHotline.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemHotline.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 147);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 20);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 20);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(368, 20);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Location = new System.Drawing.Point(12, 179);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(424, 37);
            this.lblTitle.StyleController = this.layoutControl;
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "HÓA ĐƠN THUỐC";
            // 
            // layoutControlItemTitle
            // 
            this.layoutControlItemTitle.Control = this.lblTitle;
            this.layoutControlItemTitle.Location = new System.Drawing.Point(0, 167);
            this.layoutControlItemTitle.Name = "layoutControlItemTitle";
            this.layoutControlItemTitle.Size = new System.Drawing.Size(368, 41);
            this.layoutControlItemTitle.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTitle.TextVisible = false;
            // 
            // lblInfoInvoiceNo
            // 
            this.lblInfoInvoiceNo.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfoInvoiceNo.Appearance.Options.UseFont = true;
            this.lblInfoInvoiceNo.Location = new System.Drawing.Point(12, 226);
            this.lblInfoInvoiceNo.Name = "lblInfoInvoiceNo";
            this.lblInfoInvoiceNo.Size = new System.Drawing.Size(200, 20);
            this.lblInfoInvoiceNo.StyleController = this.layoutControl;
            this.lblInfoInvoiceNo.TabIndex = 12;
            this.lblInfoInvoiceNo.Text = "Mã hóa đơn: ---";
            // 
            // layoutControlItemInfoInvoiceNo
            // 
            this.layoutControlItemInfoInvoiceNo.Control = this.lblInfoInvoiceNo;
            this.layoutControlItemInfoInvoiceNo.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItemInfoInvoiceNo.Name = "layoutControlItemInfoInvoiceNo";
            this.layoutControlItemInfoInvoiceNo.Size = new System.Drawing.Size(212, 24);
            this.layoutControlItemInfoInvoiceNo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInfoInvoiceNo.TextVisible = false;
            // 
            // lblInfoDate
            // 
            this.lblInfoDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfoDate.Appearance.Options.UseFont = true;
            this.lblInfoDate.Location = new System.Drawing.Point(12, 250);
            this.lblInfoDate.Name = "lblInfoDate";
            this.lblInfoDate.Size = new System.Drawing.Size(200, 20);
            this.lblInfoDate.StyleController = this.layoutControl;
            this.lblInfoDate.TabIndex = 13;
            this.lblInfoDate.Text = "Ngày bán: ---";
            // 
            // layoutControlItemInfoDate
            // 
            this.layoutControlItemInfoDate.Control = this.lblInfoDate;
            this.layoutControlItemInfoDate.Location = new System.Drawing.Point(0, 238);
            this.layoutControlItemInfoDate.Name = "layoutControlItemInfoDate";
            this.layoutControlItemInfoDate.Size = new System.Drawing.Size(212, 24);
            this.layoutControlItemInfoDate.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInfoDate.TextVisible = false;
            // 
            // lblInfoStaff
            // 
            this.lblInfoStaff.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfoStaff.Appearance.Options.UseFont = true;
            this.lblInfoStaff.Location = new System.Drawing.Point(12, 274);
            this.lblInfoStaff.Name = "lblInfoStaff";
            this.lblInfoStaff.Size = new System.Drawing.Size(200, 20);
            this.lblInfoStaff.StyleController = this.layoutControl;
            this.lblInfoStaff.TabIndex = 14;
            this.lblInfoStaff.Text = "Dược sĩ: Admin";
            // 
            // layoutControlItemInfoStaff
            // 
            this.layoutControlItemInfoStaff.Control = this.lblInfoStaff;
            this.layoutControlItemInfoStaff.Location = new System.Drawing.Point(0, 262);
            this.layoutControlItemInfoStaff.Name = "layoutControlItemInfoStaff";
            this.layoutControlItemInfoStaff.Size = new System.Drawing.Size(212, 24);
            this.layoutControlItemInfoStaff.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInfoStaff.TextVisible = false;
            // 
            // lblInfoPatient
            // 
            this.lblInfoPatient.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfoPatient.Appearance.Options.UseFont = true;
            this.lblInfoPatient.Location = new System.Drawing.Point(224, 226);
            this.lblInfoPatient.Name = "lblInfoPatient";
            this.lblInfoPatient.Size = new System.Drawing.Size(200, 20);
            this.lblInfoPatient.StyleController = this.layoutControl;
            this.lblInfoPatient.TabIndex = 15;
            this.lblInfoPatient.Text = "Khách hàng: Khách lẻ";
            // 
            // layoutControlItemInfoPatient
            // 
            this.layoutControlItemInfoPatient.Control = this.lblInfoPatient;
            this.layoutControlItemInfoPatient.Location = new System.Drawing.Point(212, 214);
            this.layoutControlItemInfoPatient.Name = "layoutControlItemInfoPatient";
            this.layoutControlItemInfoPatient.Size = new System.Drawing.Size(156, 72); // Span multiple rows? Or simplify structure
            this.layoutControlItemInfoPatient.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInfoPatient.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 510);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(0, 20);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(10, 20);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(368, 20);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblFooter1
            // 
            this.lblFooter1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFooter1.Appearance.Options.UseFont = true;
            this.lblFooter1.Appearance.Options.UseTextOptions = true;
            this.lblFooter1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblFooter1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFooter1.Location = new System.Drawing.Point(12, 542);
            this.lblFooter1.Name = "lblFooter1";
            this.lblFooter1.Size = new System.Drawing.Size(424, 15);
            this.lblFooter1.StyleController = this.layoutControl;
            this.lblFooter1.TabIndex = 10;
            this.lblFooter1.Text = "Cảm ơn Quý khách và Hẹn gặp lại!";
            // 
            // layoutControlItemFooter1
            // 
            this.layoutControlItemFooter1.Control = this.lblFooter1;
            this.layoutControlItemFooter1.Location = new System.Drawing.Point(0, 530);
            this.layoutControlItemFooter1.Name = "layoutControlItemFooter1";
            this.layoutControlItemFooter1.Size = new System.Drawing.Size(368, 19);
            this.layoutControlItemFooter1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemFooter1.TextVisible = false;
            // 
            // MedicineSaleInvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 639);
            this.Controls.Add(this.layoutControl);
            this.Controls.Add(this.pnlButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MedicineSaleInvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hóa đơn thuốc";
            ((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClinicName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHotline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfoInvoiceNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfoDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfoStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfoPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFooter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlButtons;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup rootGroup;
        private DevExpress.XtraGrid.GridControl gridControlItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraGrid.Columns.GridColumn colItemNo;
        private DevExpress.XtraGrid.Columns.GridColumn colItemName;
        private DevExpress.XtraGrid.Columns.GridColumn colItemUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colItemQty;
        private DevExpress.XtraGrid.Columns.GridColumn colItemPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colItemTotal;
        private DevExpress.XtraEditors.LabelControl lblLogo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLogo;
        private DevExpress.XtraEditors.LabelControl lblClinicName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClinicName;
        private DevExpress.XtraEditors.LabelControl lblAddress;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAddress;
        private DevExpress.XtraEditors.LabelControl lblHotline;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemHotline;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTitle;
        private DevExpress.XtraEditors.LabelControl lblInfoInvoiceNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInfoInvoiceNo;
        private DevExpress.XtraEditors.LabelControl lblInfoDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInfoDate;
        private DevExpress.XtraEditors.LabelControl lblInfoStaff;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInfoStaff;
        private DevExpress.XtraEditors.LabelControl lblInfoPatient;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInfoPatient;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.LabelControl lblFooter1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFooter1;
    }
}
