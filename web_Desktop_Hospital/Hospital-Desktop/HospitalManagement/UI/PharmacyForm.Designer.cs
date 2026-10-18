namespace HospitalManagement.UI
{
    partial class PharmacyForm
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
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.dtpDate = new DevExpress.XtraEditors.DateEdit();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabDirectSales = new DevExpress.XtraTab.XtraTabPage();
            this.gridDirectSales = new DevExpress.XtraGrid.GridControl();
            this.gridViewDirectSales = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDS_SaleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDS_PatientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDS_TotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDS_Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDS_Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnViewSale = new DevExpress.XtraEditors.SimpleButton();
            this.btnNewSale = new DevExpress.XtraEditors.SimpleButton();
            this.tabPendingSales = new DevExpress.XtraTab.XtraTabPage();
            this.gridPendingSales = new DevExpress.XtraGrid.GridControl();
            this.gridViewPendingSales = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPS_SaleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPS_PatientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPS_TotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPS_Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPS_Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.tabPaidSales = new DevExpress.XtraTab.XtraTabPage();
            this.gridPaidSales = new DevExpress.XtraGrid.GridControl();
            this.gridViewPaidSales = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPaid_SaleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaid_PatientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaid_TotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaid_Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaid_Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnDispense = new DevExpress.XtraEditors.SimpleButton();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabDirectSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDirectSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDirectSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.tabPendingSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPendingSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPendingSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.tabPaidSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPaidSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPaidSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Appearance.Options.UseBackColor = true;
            this.pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlHeader.Controls.Add(this.btnSearch);
            this.pnlHeader.Controls.Add(this.txtSearch);
            this.pnlHeader.Controls.Add(this.lblDate);
            this.pnlHeader.Controls.Add(this.dtpDate);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.BackColor = System.Drawing.Color.White;
            this.btnSearch.Appearance.Options.UseBackColor = true;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Location = new System.Drawing.Point(645, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(40, 26);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(460, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Properties.NullText = "Mã đơn, tên KH...";
            this.txtSearch.Size = new System.Drawing.Size(180, 24);
            this.txtSearch.TabIndex = 3;
            // 
            // lblDate
            // 
            this.lblDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblDate.Appearance.Options.UseFont = true;
            this.lblDate.Appearance.Options.UseForeColor = true;
            this.lblDate.Location = new System.Drawing.Point(260, 16);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(34, 17);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Ngày:";
            // 
            // dtpDate
            // 
            this.dtpDate.EditValue = null;
            this.dtpDate.Location = new System.Drawing.Point(308, 13);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDate.Properties.Appearance.Options.UseFont = true;
            this.dtpDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Size = new System.Drawing.Size(130, 24);
            this.dtpDate.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(151, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "💊 QUẦY THUỐC";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 50);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabDirectSales;
            this.xtraTabControl1.Size = new System.Drawing.Size(1000, 510);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabDirectSales,
            this.tabPendingSales,
            this.tabPaidSales});
            // 
            // tabDirectSales
            // 
            this.tabDirectSales.Controls.Add(this.gridDirectSales);
            this.tabDirectSales.Controls.Add(this.panelControl1);
            this.tabDirectSales.Name = "tabDirectSales";
            this.tabDirectSales.Size = new System.Drawing.Size(998, 485);
            this.tabDirectSales.Text = "📝 Bán thuốc trực tiếp";
            // 
            // gridDirectSales
            // 
            this.gridDirectSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDirectSales.Location = new System.Drawing.Point(0, 0);
            this.gridDirectSales.MainView = this.gridViewDirectSales;
            this.gridDirectSales.Name = "gridDirectSales";
            this.gridDirectSales.Size = new System.Drawing.Size(878, 485);
            this.gridDirectSales.TabIndex = 1;
            this.gridDirectSales.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDirectSales});
            // 
            // gridViewDirectSales
            // 
            this.gridViewDirectSales.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDS_SaleCode,
            this.colDS_PatientName,
            this.colDS_TotalAmount,
            this.colDS_Status,
            this.colDS_Date});
            this.gridViewDirectSales.GridControl = this.gridDirectSales;
            this.gridViewDirectSales.Name = "gridViewDirectSales";
            this.gridViewDirectSales.OptionsBehavior.Editable = false;
            this.gridViewDirectSales.OptionsView.ShowGroupPanel = false;
            this.gridViewDirectSales.OptionsView.ShowIndicator = false;
            // 
            // colDS_SaleCode
            // 
            this.colDS_SaleCode.Caption = "Mã đơn";
            this.colDS_SaleCode.FieldName = "SaleCode";
            this.colDS_SaleCode.Name = "colDS_SaleCode";
            this.colDS_SaleCode.Visible = true;
            this.colDS_SaleCode.VisibleIndex = 0;
            // 
            // colDS_PatientName
            // 
            this.colDS_PatientName.Caption = "Khách hàng";
            this.colDS_PatientName.FieldName = "PatientName";
            this.colDS_PatientName.Name = "colDS_PatientName";
            this.colDS_PatientName.Visible = true;
            this.colDS_PatientName.VisibleIndex = 1;
            // 
            // colDS_TotalAmount
            // 
            this.colDS_TotalAmount.Caption = "Tổng tiền";
            this.colDS_TotalAmount.DisplayFormat.FormatString = "N0";
            this.colDS_TotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDS_TotalAmount.FieldName = "TotalAmount";
            this.colDS_TotalAmount.Name = "colDS_TotalAmount";
            this.colDS_TotalAmount.Visible = true;
            this.colDS_TotalAmount.VisibleIndex = 2;
            // 
            // colDS_Status
            // 
            this.colDS_Status.Caption = "Trạng thái";
            this.colDS_Status.FieldName = "StatusDisplay";
            this.colDS_Status.Name = "colDS_Status";
            this.colDS_Status.Visible = true;
            this.colDS_Status.VisibleIndex = 3;
            // 
            // colDS_Date
            // 
            this.colDS_Date.Caption = "Ngày tạo";
            this.colDS_Date.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.colDS_Date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDS_Date.FieldName = "SaleDate";
            this.colDS_Date.Name = "colDS_Date";
            this.colDS_Date.Visible = true;
            this.colDS_Date.VisibleIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnViewSale);
            this.panelControl1.Controls.Add(this.btnNewSale);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(878, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(120, 485);
            this.panelControl1.TabIndex = 0;
            // 
            // btnViewSale
            // 
            this.btnViewSale.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnViewSale.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnViewSale.Appearance.Options.UseBackColor = true;
            this.btnViewSale.Appearance.Options.UseFont = true;
            this.btnViewSale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewSale.Location = new System.Drawing.Point(6, 56);
            this.btnViewSale.Name = "btnViewSale";
            this.btnViewSale.Size = new System.Drawing.Size(110, 40);
            this.btnViewSale.TabIndex = 1;
            this.btnViewSale.Text = "Xem chi tiết";
            this.btnViewSale.Click += new System.EventHandler(this.BtnViewDirectSale_Click);
            // 
            // btnNewSale
            // 
            this.btnNewSale.Appearance.BackColor = System.Drawing.Color.SeaGreen;
            this.btnNewSale.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewSale.Appearance.Options.UseBackColor = true;
            this.btnNewSale.Appearance.Options.UseFont = true;
            this.btnNewSale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewSale.Location = new System.Drawing.Point(5, 10);
            this.btnNewSale.Name = "btnNewSale";
            this.btnNewSale.Size = new System.Drawing.Size(110, 40);
            this.btnNewSale.TabIndex = 0;
            this.btnNewSale.Text = "Tạo đơn mới";
            this.btnNewSale.Click += new System.EventHandler(this.BtnNewDirectSale_Click);
            // 
            // tabPendingSales
            // 
            this.tabPendingSales.Controls.Add(this.gridPendingSales);
            this.tabPendingSales.Controls.Add(this.panelControl2);
            this.tabPendingSales.Name = "tabPendingSales";
            this.tabPendingSales.Size = new System.Drawing.Size(998, 485);
            this.tabPendingSales.Text = "⏳ Chờ thanh toán";
            // 
            // gridPendingSales
            // 
            this.gridPendingSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPendingSales.Location = new System.Drawing.Point(0, 0);
            this.gridPendingSales.MainView = this.gridViewPendingSales;
            this.gridPendingSales.Name = "gridPendingSales";
            this.gridPendingSales.Size = new System.Drawing.Size(878, 485);
            this.gridPendingSales.TabIndex = 2;
            this.gridPendingSales.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPendingSales});
            // 
            // gridViewPendingSales
            // 
            this.gridViewPendingSales.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPS_SaleCode,
            this.colPS_PatientName,
            this.colPS_TotalAmount,
            this.colPS_Status,
            this.colPS_Date});
            this.gridViewPendingSales.GridControl = this.gridPendingSales;
            this.gridViewPendingSales.Name = "gridViewPendingSales";
            this.gridViewPendingSales.OptionsBehavior.Editable = false;
            this.gridViewPendingSales.OptionsView.ShowGroupPanel = false;
            this.gridViewPendingSales.OptionsView.ShowIndicator = false;
            // 
            // colPS_SaleCode
            // 
            this.colPS_SaleCode.Caption = "Mã đơn";
            this.colPS_SaleCode.FieldName = "SaleCode";
            this.colPS_SaleCode.Name = "colPS_SaleCode";
            this.colPS_SaleCode.Visible = true;
            this.colPS_SaleCode.VisibleIndex = 0;
            // 
            // colPS_PatientName
            // 
            this.colPS_PatientName.Caption = "Khách hàng";
            this.colPS_PatientName.FieldName = "PatientName";
            this.colPS_PatientName.Name = "colPS_PatientName";
            this.colPS_PatientName.Visible = true;
            this.colPS_PatientName.VisibleIndex = 1;
            // 
            // colPS_TotalAmount
            // 
            this.colPS_TotalAmount.Caption = "Tổng tiền";
            this.colPS_TotalAmount.DisplayFormat.FormatString = "N0";
            this.colPS_TotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPS_TotalAmount.FieldName = "TotalAmount";
            this.colPS_TotalAmount.Name = "colPS_TotalAmount";
            this.colPS_TotalAmount.Visible = true;
            this.colPS_TotalAmount.VisibleIndex = 2;
            // 
            // colPS_Status
            // 
            this.colPS_Status.Caption = "Trạng thái";
            this.colPS_Status.FieldName = "StatusDisplay";
            this.colPS_Status.Name = "colPS_Status";
            this.colPS_Status.Visible = true;
            this.colPS_Status.VisibleIndex = 3;
            // 
            // colPS_Date
            // 
            this.colPS_Date.Caption = "Ngày tạo";
            this.colPS_Date.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.colPS_Date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colPS_Date.FieldName = "SaleDate";
            this.colPS_Date.Name = "colPS_Date";
            this.colPS_Date.Visible = true;
            this.colPS_Date.VisibleIndex = 4;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(878, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(120, 485);
            this.panelControl2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(5, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "❌ Hủy đơn";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelSale_Click);
            // 
            // tabPaidSales
            // 
            this.tabPaidSales.Controls.Add(this.gridPaidSales);
            this.tabPaidSales.Controls.Add(this.panelControl3);
            this.tabPaidSales.Name = "tabPaidSales";
            this.tabPaidSales.Size = new System.Drawing.Size(998, 485);
            this.tabPaidSales.Text = "💊 Chờ phát thuốc";
            // 
            // gridPaidSales
            // 
            this.gridPaidSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPaidSales.Location = new System.Drawing.Point(0, 0);
            this.gridPaidSales.MainView = this.gridViewPaidSales;
            this.gridPaidSales.Name = "gridPaidSales";
            this.gridPaidSales.Size = new System.Drawing.Size(878, 485);
            this.gridPaidSales.TabIndex = 2;
            this.gridPaidSales.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPaidSales});
            // 
            // gridViewPaidSales
            // 
            this.gridViewPaidSales.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPaid_SaleCode,
            this.colPaid_PatientName,
            this.colPaid_TotalAmount,
            this.colPaid_Status,
            this.colPaid_Date});
            this.gridViewPaidSales.GridControl = this.gridPaidSales;
            this.gridViewPaidSales.Name = "gridViewPaidSales";
            this.gridViewPaidSales.OptionsBehavior.Editable = false;
            this.gridViewPaidSales.OptionsView.ShowGroupPanel = false;
            this.gridViewPaidSales.OptionsView.ShowIndicator = false;
            // 
            // colPaid_SaleCode
            // 
            this.colPaid_SaleCode.Caption = "Mã đơn";
            this.colPaid_SaleCode.FieldName = "SaleCode";
            this.colPaid_SaleCode.Name = "colPaid_SaleCode";
            this.colPaid_SaleCode.Visible = true;
            this.colPaid_SaleCode.VisibleIndex = 0;
            // 
            // colPaid_PatientName
            // 
            this.colPaid_PatientName.Caption = "Khách hàng";
            this.colPaid_PatientName.FieldName = "PatientName";
            this.colPaid_PatientName.Name = "colPaid_PatientName";
            this.colPaid_PatientName.Visible = true;
            this.colPaid_PatientName.VisibleIndex = 1;
            // 
            // colPaid_TotalAmount
            // 
            this.colPaid_TotalAmount.Caption = "Tổng tiền";
            this.colPaid_TotalAmount.DisplayFormat.FormatString = "N0";
            this.colPaid_TotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPaid_TotalAmount.FieldName = "TotalAmount";
            this.colPaid_TotalAmount.Name = "colPaid_TotalAmount";
            this.colPaid_TotalAmount.Visible = true;
            this.colPaid_TotalAmount.VisibleIndex = 2;
            // 
            // colPaid_Status
            // 
            this.colPaid_Status.Caption = "Trạng thái";
            this.colPaid_Status.FieldName = "StatusDisplay";
            this.colPaid_Status.Name = "colPaid_Status";
            this.colPaid_Status.Visible = true;
            this.colPaid_Status.VisibleIndex = 3;
            // 
            // colPaid_Date
            // 
            this.colPaid_Date.Caption = "Ngày tạo";
            this.colPaid_Date.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.colPaid_Date.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colPaid_Date.FieldName = "SaleDate";
            this.colPaid_Date.Name = "colPaid_Date";
            this.colPaid_Date.Visible = true;
            this.colPaid_Date.VisibleIndex = 4;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnDispense);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl3.Location = new System.Drawing.Point(878, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(120, 485);
            this.panelControl3.TabIndex = 1;
            // 
            // btnDispense
            // 
            this.btnDispense.Appearance.BackColor = System.Drawing.Color.SeaGreen;
            this.btnDispense.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDispense.Appearance.Options.UseBackColor = true;
            this.btnDispense.Appearance.Options.UseFont = true;
            this.btnDispense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDispense.Location = new System.Drawing.Point(5, 10);
            this.btnDispense.Name = "btnDispense";
            this.btnDispense.Size = new System.Drawing.Size(110, 60);
            this.btnDispense.TabIndex = 1;
            this.btnDispense.Text = "Phát thuốc";
            this.btnDispense.Click += new System.EventHandler(this.BtnDispense_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnRefresh);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 560);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1000, 60);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(896, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 40);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.btnRefresh.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Appearance.Options.UseBackColor = true;
            this.btnRefresh.Appearance.Options.UseFont = true;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Location = new System.Drawing.Point(785, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 40);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // PharmacyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Name = "PharmacyForm";
            this.Text = "Quầy Thuốc";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabDirectSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDirectSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDirectSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.tabPendingSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPendingSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPendingSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.tabPaidSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPaidSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPaidSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.DateEdit dtpDate;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabDirectSales;
        private DevExpress.XtraGrid.GridControl gridDirectSales;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDirectSales;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnNewSale;
        private DevExpress.XtraEditors.SimpleButton btnViewSale;
        private DevExpress.XtraTab.XtraTabPage tabPendingSales;
        private DevExpress.XtraGrid.GridControl gridPendingSales;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPendingSales;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraTab.XtraTabPage tabPaidSales;
        private DevExpress.XtraGrid.GridControl gridPaidSales;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPaidSales;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnDispense;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraGrid.Columns.GridColumn colDS_SaleCode;
        private DevExpress.XtraGrid.Columns.GridColumn colDS_PatientName;
        private DevExpress.XtraGrid.Columns.GridColumn colDS_TotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colDS_Status;
        private DevExpress.XtraGrid.Columns.GridColumn colDS_Date;
        private DevExpress.XtraGrid.Columns.GridColumn colPS_SaleCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPS_PatientName;
        private DevExpress.XtraGrid.Columns.GridColumn colPS_TotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colPS_Status;
        private DevExpress.XtraGrid.Columns.GridColumn colPS_Date;
        private DevExpress.XtraGrid.Columns.GridColumn colPaid_SaleCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPaid_PatientName;
        private DevExpress.XtraGrid.Columns.GridColumn colPaid_TotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colPaid_Status;
        private DevExpress.XtraGrid.Columns.GridColumn colPaid_Date;
    }
}
