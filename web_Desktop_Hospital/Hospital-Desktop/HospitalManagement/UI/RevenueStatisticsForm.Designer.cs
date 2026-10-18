namespace HospitalManagement.UI
{
    partial class RevenueStatisticsForm
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
            this.components = new System.ComponentModel.Container();
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlFilter = new DevExpress.XtraEditors.PanelControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnFilter = new DevExpress.XtraEditors.SimpleButton();
            this.deToDate = new DevExpress.XtraEditors.DateEdit();
            this.lblToDate = new DevExpress.XtraEditors.LabelControl();
            this.deFromDate = new DevExpress.XtraEditors.DateEdit();
            this.lblFromDate = new DevExpress.XtraEditors.LabelControl();
            this.pnlSummary = new DevExpress.XtraEditors.PanelControl();
            this.lblTransactionCount = new DevExpress.XtraEditors.LabelControl();
            this.lblTransactionCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.lblGrandTotal = new DevExpress.XtraEditors.LabelControl();
            this.lblGrandTotalLabel = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalMedicine = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalMedicineLabel = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalExamination = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalExaminationLabel = new DevExpress.XtraEditors.LabelControl();
            this.splitContainer = new DevExpress.XtraEditors.SplitContainerControl();
            this.grpSummary = new DevExpress.XtraEditors.GroupControl();
            this.gridSummary = new DevExpress.XtraGrid.GridControl();
            this.gridViewSummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSummaryDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSummaryRevenue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpDetails = new DevExpress.XtraEditors.GroupControl();
            this.gridDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDetailId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetailDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetailPatient = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetailAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetailType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetailStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilter)).BeginInit();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSummary)).BeginInit();
            this.pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSummary)).BeginInit();
            this.grpSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDetails)).BeginInit();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.pnlHeader.Appearance.Options.UseBackColor = true;
            this.pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 THỐNG KÊ DOANH THU";
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.btnExport);
            this.pnlFilter.Controls.Add(this.btnFilter);
            this.pnlFilter.Controls.Add(this.deToDate);
            this.pnlFilter.Controls.Add(this.lblToDate);
            this.pnlFilter.Controls.Add(this.deFromDate);
            this.pnlFilter.Controls.Add(this.lblFromDate);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 50);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1000, 60);
            this.pnlFilter.TabIndex = 1;
            // 
            // btnExport
            // 
            this.btnExport.Appearance.BackColor = System.Drawing.Color.Green;
            this.btnExport.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExport.Appearance.Options.UseBackColor = true;
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Location = new System.Drawing.Point(550, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Xuất Excel";
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnFilter.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilter.Appearance.Options.UseBackColor = true;
            this.btnFilter.Appearance.Options.UseFont = true;
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.Location = new System.Drawing.Point(440, 15);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(100, 30);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Xem báo cáo";
            this.btnFilter.Click += new System.EventHandler(this.BtnFilter_Click);
            // 
            // deToDate
            // 
            this.deToDate.EditValue = null;
            this.deToDate.Location = new System.Drawing.Point(280, 18);
            this.deToDate.Name = "deToDate";
            this.deToDate.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.deToDate.Properties.Appearance.Options.UseFont = true;
            this.deToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deToDate.Size = new System.Drawing.Size(130, 24);
            this.deToDate.TabIndex = 3;
            // 
            // lblToDate
            // 
            this.lblToDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblToDate.Appearance.Options.UseFont = true;
            this.lblToDate.Location = new System.Drawing.Point(210, 21);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(60, 17);
            this.lblToDate.TabIndex = 2;
            this.lblToDate.Text = "Đến ngày:";
            // 
            // deFromDate
            // 
            this.deFromDate.EditValue = null;
            this.deFromDate.Location = new System.Drawing.Point(70, 18);
            this.deFromDate.Name = "deFromDate";
            this.deFromDate.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.deFromDate.Properties.Appearance.Options.UseFont = true;
            this.deFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFromDate.Size = new System.Drawing.Size(130, 24);
            this.deFromDate.TabIndex = 1;
            // 
            // lblFromDate
            // 
            this.lblFromDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFromDate.Appearance.Options.UseFont = true;
            this.lblFromDate.Location = new System.Drawing.Point(15, 21);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(52, 17);
            this.lblFromDate.TabIndex = 0;
            this.lblFromDate.Text = "Từ ngày:";
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.lblTransactionCount);
            this.pnlSummary.Controls.Add(this.lblTransactionCountLabel);
            this.pnlSummary.Controls.Add(this.lblGrandTotal);
            this.pnlSummary.Controls.Add(this.lblGrandTotalLabel);
            this.pnlSummary.Controls.Add(this.lblTotalMedicine);
            this.pnlSummary.Controls.Add(this.lblTotalMedicineLabel);
            this.pnlSummary.Controls.Add(this.lblTotalExamination);
            this.pnlSummary.Controls.Add(this.lblTotalExaminationLabel);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(0, 110);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1000, 70);
            this.pnlSummary.TabIndex = 2;
            // 
            // lblTransactionCount
            // 
            this.lblTransactionCount.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTransactionCount.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTransactionCount.Appearance.Options.UseFont = true;
            this.lblTransactionCount.Appearance.Options.UseForeColor = true;
            this.lblTransactionCount.Location = new System.Drawing.Point(850, 35);
            this.lblTransactionCount.Name = "lblTransactionCount";
            this.lblTransactionCount.Size = new System.Drawing.Size(9, 21);
            this.lblTransactionCount.TabIndex = 7;
            this.lblTransactionCount.Text = "0";
            // 
            // lblTransactionCountLabel
            // 
            this.lblTransactionCountLabel.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTransactionCountLabel.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTransactionCountLabel.Appearance.Options.UseFont = true;
            this.lblTransactionCountLabel.Appearance.Options.UseForeColor = true;
            this.lblTransactionCountLabel.Location = new System.Drawing.Point(850, 12);
            this.lblTransactionCountLabel.Name = "lblTransactionCountLabel";
            this.lblTransactionCountLabel.Size = new System.Drawing.Size(68, 17);
            this.lblTransactionCountLabel.TabIndex = 6;
            this.lblTransactionCountLabel.Text = "Số giao dịch";
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblGrandTotal.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblGrandTotal.Appearance.Options.UseFont = true;
            this.lblGrandTotal.Appearance.Options.UseForeColor = true;
            this.lblGrandTotal.Location = new System.Drawing.Point(550, 32);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(37, 25);
            this.lblGrandTotal.TabIndex = 5;
            this.lblGrandTotal.Text = "0 đ";
            // 
            // lblGrandTotalLabel
            // 
            this.lblGrandTotalLabel.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGrandTotalLabel.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblGrandTotalLabel.Appearance.Options.UseFont = true;
            this.lblGrandTotalLabel.Appearance.Options.UseForeColor = true;
            this.lblGrandTotalLabel.Location = new System.Drawing.Point(550, 12);
            this.lblGrandTotalLabel.Name = "lblGrandTotalLabel";
            this.lblGrandTotalLabel.Size = new System.Drawing.Size(92, 17);
            this.lblGrandTotalLabel.TabIndex = 4;
            this.lblGrandTotalLabel.Text = "TỔNG DOANH THU";
            // 
            // lblTotalMedicine
            // 
            this.lblTotalMedicine.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalMedicine.Appearance.ForeColor = System.Drawing.Color.Green;
            this.lblTotalMedicine.Appearance.Options.UseFont = true;
            this.lblTotalMedicine.Appearance.Options.UseForeColor = true;
            this.lblTotalMedicine.Location = new System.Drawing.Point(280, 35);
            this.lblTotalMedicine.Name = "lblTotalMedicine";
            this.lblTotalMedicine.Size = new System.Drawing.Size(26, 21);
            this.lblTotalMedicine.TabIndex = 3;
            this.lblTotalMedicine.Text = "0 đ";
            // 
            // lblTotalMedicineLabel
            // 
            this.lblTotalMedicineLabel.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalMedicineLabel.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTotalMedicineLabel.Appearance.Options.UseFont = true;
            this.lblTotalMedicineLabel.Appearance.Options.UseForeColor = true;
            this.lblTotalMedicineLabel.Location = new System.Drawing.Point(280, 12);
            this.lblTotalMedicineLabel.Name = "lblTotalMedicineLabel";
            this.lblTotalMedicineLabel.Size = new System.Drawing.Size(107, 17);
            this.lblTotalMedicineLabel.TabIndex = 2;
            this.lblTotalMedicineLabel.Text = "Doanh thu thuốc";
            // 
            // lblTotalExamination
            // 
            this.lblTotalExamination.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalExamination.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblTotalExamination.Appearance.Options.UseFont = true;
            this.lblTotalExamination.Appearance.Options.UseForeColor = true;
            this.lblTotalExamination.Location = new System.Drawing.Point(15, 35);
            this.lblTotalExamination.Name = "lblTotalExamination";
            this.lblTotalExamination.Size = new System.Drawing.Size(26, 21);
            this.lblTotalExamination.TabIndex = 1;
            this.lblTotalExamination.Text = "0 đ";
            // 
            // lblTotalExaminationLabel
            // 
            this.lblTotalExaminationLabel.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalExaminationLabel.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTotalExaminationLabel.Appearance.Options.UseFont = true;
            this.lblTotalExaminationLabel.Appearance.Options.UseForeColor = true;
            this.lblTotalExaminationLabel.Location = new System.Drawing.Point(15, 12);
            this.lblTotalExaminationLabel.Name = "lblTotalExaminationLabel";
            this.lblTotalExaminationLabel.Size = new System.Drawing.Size(169, 17);
            this.lblTotalExaminationLabel.TabIndex = 0;
            this.lblTotalExaminationLabel.Text = "Doanh thu khám bệnh";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Horizontal = false;
            this.splitContainer.Location = new System.Drawing.Point(0, 180);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1.Controls.Add(this.grpSummary);
            this.splitContainer.Panel1.Text = "Panel1";
            this.splitContainer.Panel2.Controls.Add(this.grpDetails);
            this.splitContainer.Panel2.Text = "Panel2";
            this.splitContainer.Size = new System.Drawing.Size(1000, 520);
            this.splitContainer.SplitterPosition = 200;
            this.splitContainer.TabIndex = 3;
            this.splitContainer.Text = "splitContainerControl1";
            // 
            // grpSummary
            // 
            this.grpSummary.Controls.Add(this.gridSummary);
            this.grpSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSummary.Location = new System.Drawing.Point(0, 0);
            this.grpSummary.Name = "grpSummary";
            this.grpSummary.Size = new System.Drawing.Size(1000, 200);
            this.grpSummary.TabIndex = 0;
            this.grpSummary.Text = "Doanh thu theo ngày";
            // 
            // gridSummary
            // 
            this.gridSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSummary.Location = new System.Drawing.Point(2, 23);
            this.gridSummary.MainView = this.gridViewSummary;
            this.gridSummary.Name = "gridSummary";
            this.gridSummary.Size = new System.Drawing.Size(996, 175);
            this.gridSummary.TabIndex = 0;
            this.gridSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSummary});
            // 
            // gridViewSummary
            // 
            this.gridViewSummary.GridControl = this.gridSummary;
            this.gridViewSummary.Name = "gridViewSummary";
            // 
            // gridViewSummary
            // 
            this.gridViewSummary.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSummaryDate,
            this.colSummaryRevenue});
            this.gridViewSummary.GridControl = this.gridSummary;
            this.gridViewSummary.Name = "gridViewSummary";
            this.gridViewSummary.OptionsView.ShowGroupPanel = false;
            this.gridViewSummary.OptionsView.ShowIndicator = false;
            // 
            // colSummaryDate
            // 
            this.colSummaryDate.Caption = "Ngày";
            this.colSummaryDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.colSummaryDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colSummaryDate.FieldName = "Date";
            this.colSummaryDate.Name = "colSummaryDate";
            this.colSummaryDate.Visible = true;
            this.colSummaryDate.VisibleIndex = 0;
            // 
            // colSummaryRevenue
            // 
            this.colSummaryRevenue.Caption = "Doanh thu";
            this.colSummaryRevenue.DisplayFormat.FormatString = "N0";
            this.colSummaryRevenue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSummaryRevenue.FieldName = "TotalRevenue";
            this.colSummaryRevenue.Name = "colSummaryRevenue";
            this.colSummaryRevenue.Visible = true;
            this.colSummaryRevenue.VisibleIndex = 1;
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.gridDetails);
            this.grpDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetails.Location = new System.Drawing.Point(0, 0);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(1000, 310);
            this.grpDetails.TabIndex = 1;
            this.grpDetails.Text = "Chi tiết giao dịch";
            // 
            // gridDetails
            // 
            this.gridDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetails.Location = new System.Drawing.Point(2, 23);
            this.gridDetails.MainView = this.gridViewDetails;
            this.gridDetails.Name = "gridDetails";
            this.gridDetails.Size = new System.Drawing.Size(996, 285);
            this.gridDetails.TabIndex = 0;
            this.gridDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDetails});
            // 
            // gridViewDetails
            // 
            this.gridViewDetails.GridControl = this.gridDetails;
            this.gridViewDetails.Name = "gridViewDetails";
            // 
            // gridViewDetails
            // 
            this.gridViewDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDetailId,
            this.colDetailDate,
            this.colDetailPatient,
            this.colDetailAmount,
            this.colDetailType,
            this.colDetailStatus});
            this.gridViewDetails.GridControl = this.gridDetails;
            this.gridViewDetails.Name = "gridViewDetails";
            this.gridViewDetails.OptionsView.ShowGroupPanel = false;
            this.gridViewDetails.OptionsView.ShowIndicator = false;
            // 
            // colDetailId
            // 
            this.colDetailId.Caption = "Mã hóa đơn";
            this.colDetailId.FieldName = "InvoiceId";
            this.colDetailId.Name = "colDetailId";
            this.colDetailId.Visible = true;
            this.colDetailId.VisibleIndex = 0;
            this.colDetailId.Width = 80;
            // 
            // colDetailDate
            // 
            this.colDetailDate.Caption = "Ngày";
            this.colDetailDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.colDetailDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDetailDate.FieldName = "PaymentDate";
            this.colDetailDate.Name = "colDetailDate";
            this.colDetailDate.Visible = true;
            this.colDetailDate.VisibleIndex = 1;
            this.colDetailDate.Width = 120;
            // 
            // colDetailPatient
            // 
            this.colDetailPatient.Caption = "Bệnh nhân";
            this.colDetailPatient.FieldName = "PatientName";
            this.colDetailPatient.Name = "colDetailPatient";
            this.colDetailPatient.Visible = true;
            this.colDetailPatient.VisibleIndex = 2;
            this.colDetailPatient.Width = 150;
            // 
            // colDetailAmount
            // 
            this.colDetailAmount.Caption = "Số tiền";
            this.colDetailAmount.DisplayFormat.FormatString = "N0";
            this.colDetailAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDetailAmount.FieldName = "TotalAmount";
            this.colDetailAmount.Name = "colDetailAmount";
            this.colDetailAmount.Visible = true;
            this.colDetailAmount.VisibleIndex = 3;
            this.colDetailAmount.Width = 100;
            // 
            // colDetailType
            // 
            this.colDetailType.Caption = "Loại";
            this.colDetailType.FieldName = "PaymentMethod";
            this.colDetailType.Name = "colDetailType";
            this.colDetailType.Visible = true;
            this.colDetailType.VisibleIndex = 4;
            this.colDetailType.Width = 80;
            // 
            // colDetailStatus
            // 
            this.colDetailStatus.Caption = "Trạng thái";
            this.colDetailStatus.FieldName = "Status";
            this.colDetailStatus.Name = "colDetailStatus";
            this.colDetailStatus.Visible = true;
            this.colDetailStatus.VisibleIndex = 5;
            this.colDetailStatus.Width = 80;
            // 
            // RevenueStatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RevenueStatisticsForm";
            this.Text = "Thống kê doanh thu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilter)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSummary)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).EndInit();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpSummary)).EndInit();
            this.grpSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDetails)).EndInit();
            this.grpDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.PanelControl pnlFilter;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SimpleButton btnFilter;
        private DevExpress.XtraEditors.DateEdit deToDate;
        private DevExpress.XtraEditors.LabelControl lblToDate;
        private DevExpress.XtraEditors.DateEdit deFromDate;
        private DevExpress.XtraEditors.LabelControl lblFromDate;
        private DevExpress.XtraEditors.PanelControl pnlSummary;
        private DevExpress.XtraEditors.LabelControl lblTransactionCount;
        private DevExpress.XtraEditors.LabelControl lblTransactionCountLabel;
        private DevExpress.XtraEditors.LabelControl lblGrandTotal;
        private DevExpress.XtraEditors.LabelControl lblGrandTotalLabel;
        private DevExpress.XtraEditors.LabelControl lblTotalMedicine;
        private DevExpress.XtraEditors.LabelControl lblTotalMedicineLabel;
        private DevExpress.XtraEditors.LabelControl lblTotalExamination;
        private DevExpress.XtraEditors.LabelControl lblTotalExaminationLabel;
        private DevExpress.XtraEditors.SplitContainerControl splitContainer;
        private DevExpress.XtraEditors.GroupControl grpSummary;
        private DevExpress.XtraGrid.GridControl gridSummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSummary;
        private DevExpress.XtraGrid.Columns.GridColumn colSummaryDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSummaryRevenue;
        private DevExpress.XtraEditors.GroupControl grpDetails;
        private DevExpress.XtraGrid.GridControl gridDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailId;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailPatient;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailType;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailStatus;
    }
}
