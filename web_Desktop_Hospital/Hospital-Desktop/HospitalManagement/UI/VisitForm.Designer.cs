namespace HospitalManagement.UI
{
    partial class VisitForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _debounceTimer?.Dispose();
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.dtpDate = new DevExpress.XtraEditors.DateEdit();
            this.btnFilter = new DevExpress.XtraEditors.SimpleButton();
            this.btnToday = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.lblLegend = new DevExpress.XtraEditors.LabelControl();
            this.gridVisits = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVisitId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVisitDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPatientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDoctorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusDisplay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSymptoms = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiagnosis = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiseaseName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnExamine = new DevExpress.XtraEditors.SimpleButton();
            this.btnComplete = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnToday = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSearch = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemLegend = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemToolbar = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemExamine = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemComplete = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCount = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemBottom = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemClose = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnToday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLegend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemToolbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExamine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemComplete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClose)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.lblTitle);
            this.layoutControl.Controls.Add(this.dtpDate);
            this.layoutControl.Controls.Add(this.btnFilter);
            this.layoutControl.Controls.Add(this.btnToday);
            this.layoutControl.Controls.Add(this.txtSearch);
            this.layoutControl.Controls.Add(this.lblLegend);
            this.layoutControl.Controls.Add(this.gridVisits);
            this.layoutControl.Controls.Add(this.btnExamine);
            this.layoutControl.Controls.Add(this.btnComplete);
            this.layoutControl.Controls.Add(this.btnRefresh);
            this.layoutControl.Controls.Add(this.btnClose);
            this.layoutControl.Controls.Add(this.lblCount);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1148, 389, 650, 400);
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(1100, 650);
            this.layoutControl.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(266, 30);
            this.lblTitle.StyleController = this.layoutControl;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DANH SÁCH LƯỢT KHÁM";
            // 
            // dtpDate
            // 
            this.dtpDate.EditValue = null;
            this.dtpDate.Location = new System.Drawing.Point(53, 46);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Size = new System.Drawing.Size(109, 20);
            this.dtpDate.StyleController = this.layoutControl;
            this.dtpDate.TabIndex = 1;
            // 
            // btnFilter
            // 
            this.btnFilter.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnFilter.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilter.Appearance.Options.UseBackColor = true;
            this.btnFilter.Appearance.Options.UseFont = true;
            this.btnFilter.Location = new System.Drawing.Point(166, 46);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(150, 22);
            this.btnFilter.StyleController = this.layoutControl;
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnToday
            // 
            this.btnToday.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnToday.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnToday.Appearance.Options.UseBackColor = true;
            this.btnToday.Appearance.Options.UseFont = true;
            this.btnToday.Location = new System.Drawing.Point(320, 46);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(150, 22);
            this.btnToday.StyleController = this.layoutControl;
            this.btnToday.TabIndex = 3;
            this.btnToday.Text = "Hôm nay";
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(474, 46);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.NullValuePrompt = "Tìm bệnh nhân, bác sĩ...";
            this.txtSearch.Size = new System.Drawing.Size(250, 20);
            this.txtSearch.StyleController = this.layoutControl;
            this.txtSearch.TabIndex = 4;
            // 
            // lblLegend
            // 
            this.lblLegend.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLegend.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblLegend.Appearance.Options.UseFont = true;
            this.lblLegend.Appearance.Options.UseForeColor = true;
            this.lblLegend.Location = new System.Drawing.Point(12, 72);
            this.lblLegend.Name = "lblLegend";
            this.lblLegend.Size = new System.Drawing.Size(1076, 15);
            this.lblLegend.StyleController = this.layoutControl;
            this.lblLegend.TabIndex = 5;
            this.lblLegend.Visible = false;
            // 
            // gridVisits
            // 
            this.gridVisits.Location = new System.Drawing.Point(12, 91);
            this.gridVisits.MainView = this.gridView;
            this.gridVisits.Name = "gridVisits";
            this.gridVisits.Size = new System.Drawing.Size(1076, 511);
            this.gridVisits.TabIndex = 6;
            this.gridVisits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVisitId,
            this.colVisitDate,
            this.colPatientName,
            this.colDoctorName,
            this.colStatusDisplay,
            this.colStatus,
            this.colSymptoms,
            this.colDiagnosis,
            this.colDiseaseName});
            this.gridView.GridControl = this.gridVisits;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            this.gridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView_RowStyle);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            // 
            // colVisitId
            // 
            this.colVisitId.FieldName = "VisitId";
            this.colVisitId.Name = "colVisitId";
            // 
            // colVisitDate
            // 
            this.colVisitDate.Caption = "Ngày khám";
            this.colVisitDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.colVisitDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colVisitDate.FieldName = "VisitDate";
            this.colVisitDate.Name = "colVisitDate";
            this.colVisitDate.Visible = true;
            this.colVisitDate.VisibleIndex = 0;
            this.colVisitDate.Width = 120;
            // 
            // colPatientName
            // 
            this.colPatientName.Caption = "Bệnh nhân";
            this.colPatientName.FieldName = "PatientName";
            this.colPatientName.Name = "colPatientName";
            this.colPatientName.Visible = true;
            this.colPatientName.VisibleIndex = 1;
            this.colPatientName.Width = 150;
            // 
            // colDoctorName
            // 
            this.colDoctorName.Caption = "Bác sĩ";
            this.colDoctorName.FieldName = "DoctorName";
            this.colDoctorName.Name = "colDoctorName";
            this.colDoctorName.Visible = true;
            this.colDoctorName.VisibleIndex = 2;
            this.colDoctorName.Width = 150;
            // 
            // colStatusDisplay
            // 
            this.colStatusDisplay.Caption = "Trạng thái";
            this.colStatusDisplay.FieldName = "StatusDisplay";
            this.colStatusDisplay.Name = "colStatusDisplay";
            this.colStatusDisplay.Visible = true;
            this.colStatusDisplay.VisibleIndex = 3;
            this.colStatusDisplay.Width = 100;
            // 
            // colStatus
            // 
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            // 
            // colSymptoms
            // 
            this.colSymptoms.Caption = "Triệu chứng";
            this.colSymptoms.FieldName = "Symptoms";
            this.colSymptoms.Name = "colSymptoms";
            this.colSymptoms.Visible = true;
            this.colSymptoms.VisibleIndex = 4;
            this.colSymptoms.Width = 150;
            // 
            // colDiagnosis
            // 
            this.colDiagnosis.Caption = "Chẩn đoán";
            this.colDiagnosis.FieldName = "Diagnosis";
            this.colDiagnosis.Name = "colDiagnosis";
            this.colDiagnosis.Visible = true;
            this.colDiagnosis.VisibleIndex = 5;
            this.colDiagnosis.Width = 150;
            // 
            // colDiseaseName
            // 
            this.colDiseaseName.Caption = "Bệnh lý";
            this.colDiseaseName.FieldName = "DiseaseName";
            this.colDiseaseName.Name = "colDiseaseName";
            this.colDiseaseName.Visible = true;
            this.colDiseaseName.VisibleIndex = 6;
            this.colDiseaseName.Width = 120;
            // 
            // btnExamine
            // 
            this.btnExamine.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnExamine.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExamine.Appearance.Options.UseBackColor = true;
            this.btnExamine.Appearance.Options.UseFont = true;
            this.btnExamine.Location = new System.Drawing.Point(728, 46);
            this.btnExamine.Name = "btnExamine";
            this.btnExamine.Size = new System.Drawing.Size(117, 22);
            this.btnExamine.StyleController = this.layoutControl;
            this.btnExamine.TabIndex = 7;
            this.btnExamine.Text = "Xem chi tiết";
            this.btnExamine.Click += new System.EventHandler(this.btnExamine_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnComplete.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnComplete.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnComplete.Appearance.Options.UseBackColor = true;
            this.btnComplete.Appearance.Options.UseFont = true;
            this.btnComplete.Appearance.Options.UseForeColor = true;
            this.btnComplete.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(220)))), ((int)(((byte)(180)))));
            this.btnComplete.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
            this.btnComplete.AppearanceDisabled.Options.UseBackColor = true;
            this.btnComplete.AppearanceDisabled.Options.UseForeColor = true;
            this.btnComplete.Location = new System.Drawing.Point(849, 46);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(117, 22);
            this.btnComplete.StyleController = this.layoutControl;
            this.btnComplete.TabIndex = 8;
            this.btnComplete.Text = "Hoàn thành";
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnRefresh.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Appearance.Options.UseBackColor = true;
            this.btnRefresh.Appearance.Options.UseFont = true;
            this.btnRefresh.Appearance.Options.UseForeColor = true;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Location = new System.Drawing.Point(998, 606);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 32);
            this.btnRefresh.StyleController = this.layoutControl;
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(904, 606);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 32);
            this.btnClose.StyleController = this.layoutControl;
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblCount
            // 
            this.lblCount.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCount.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.lblCount.Appearance.Options.UseFont = true;
            this.lblCount.Appearance.Options.UseForeColor = true;
            this.lblCount.Location = new System.Drawing.Point(12, 606);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(112, 19);
            this.lblCount.StyleController = this.layoutControl;
            this.lblCount.TabIndex = 11;
            this.lblCount.Text = "Tổng: 0 lượt khám";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTitle,
            this.layoutControlItemDate,
            this.layoutControlItemBtnFilter,
            this.layoutControlItemBtnToday,
            this.layoutControlItemSearch,
            this.layoutControlItemLegend,
            this.emptySpaceItemToolbar,
            this.layoutControlItemExamine,
            this.layoutControlItemComplete,
            this.layoutControlItemRefresh,
            this.layoutControlItemGrid,
            this.layoutControlItemCount,
            this.emptySpaceItemBottom,
            this.layoutControlItemClose});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1100, 650);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemTitle
            // 
            this.layoutControlItemTitle.Control = this.lblTitle;
            this.layoutControlItemTitle.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemTitle.Name = "layoutControlItemTitle";
            this.layoutControlItemTitle.Size = new System.Drawing.Size(1080, 34);
            this.layoutControlItemTitle.TextVisible = false;
            // 
            // layoutControlItemDate
            // 
            this.layoutControlItemDate.Control = this.dtpDate;
            this.layoutControlItemDate.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItemDate.Name = "layoutControlItemDate";
            this.layoutControlItemDate.Size = new System.Drawing.Size(154, 26);
            this.layoutControlItemDate.Text = "Ngày:";
            this.layoutControlItemDate.TextSize = new System.Drawing.Size(29, 13);
            // 
            // layoutControlItemBtnFilter
            // 
            this.layoutControlItemBtnFilter.Control = this.btnFilter;
            this.layoutControlItemBtnFilter.Location = new System.Drawing.Point(154, 34);
            this.layoutControlItemBtnFilter.Name = "layoutControlItemBtnFilter";
            this.layoutControlItemBtnFilter.Size = new System.Drawing.Size(154, 26);
            this.layoutControlItemBtnFilter.TextVisible = false;
            // 
            // layoutControlItemBtnToday
            // 
            this.layoutControlItemBtnToday.Control = this.btnToday;
            this.layoutControlItemBtnToday.Location = new System.Drawing.Point(308, 34);
            this.layoutControlItemBtnToday.Name = "layoutControlItemBtnToday";
            this.layoutControlItemBtnToday.Size = new System.Drawing.Size(154, 26);
            this.layoutControlItemBtnToday.TextVisible = false;
            // 
            // layoutControlItemSearch
            // 
            this.layoutControlItemSearch.Control = this.txtSearch;
            this.layoutControlItemSearch.Location = new System.Drawing.Point(462, 34);
            this.layoutControlItemSearch.MinSize = new System.Drawing.Size(254, 26);
            this.layoutControlItemSearch.Name = "layoutControlItemSearch";
            this.layoutControlItemSearch.Size = new System.Drawing.Size(254, 26);
            this.layoutControlItemSearch.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemSearch.TextVisible = false;
            // 
            // layoutControlItemLegend
            // 
            this.layoutControlItemLegend.Control = this.lblLegend;
            this.layoutControlItemLegend.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItemLegend.Name = "layoutControlItemLegend";
            this.layoutControlItemLegend.Size = new System.Drawing.Size(1080, 19);
            this.layoutControlItemLegend.TextVisible = false;
            // 
            // emptySpaceItemToolbar
            // 
            this.emptySpaceItemToolbar.Location = new System.Drawing.Point(958, 34);
            this.emptySpaceItemToolbar.Name = "emptySpaceItemToolbar";
            this.emptySpaceItemToolbar.Size = new System.Drawing.Size(122, 26);
            // 
            // layoutControlItemExamine
            // 
            this.layoutControlItemExamine.Control = this.btnExamine;
            this.layoutControlItemExamine.Location = new System.Drawing.Point(716, 34);
            this.layoutControlItemExamine.Name = "layoutControlItemExamine";
            this.layoutControlItemExamine.Size = new System.Drawing.Size(121, 26);
            this.layoutControlItemExamine.TextVisible = false;
            // 
            // layoutControlItemComplete
            // 
            this.layoutControlItemComplete.Control = this.btnComplete;
            this.layoutControlItemComplete.Location = new System.Drawing.Point(837, 34);
            this.layoutControlItemComplete.Name = "layoutControlItemComplete";
            this.layoutControlItemComplete.Size = new System.Drawing.Size(121, 26);
            this.layoutControlItemComplete.TextVisible = false;
            // 
            // layoutControlItemRefresh
            // 
            this.layoutControlItemRefresh.Control = this.btnRefresh;
            this.layoutControlItemRefresh.Location = new System.Drawing.Point(986, 594);
            this.layoutControlItemRefresh.MaxSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemRefresh.MinSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemRefresh.Name = "layoutControlItemRefresh";
            this.layoutControlItemRefresh.Size = new System.Drawing.Size(94, 36);
            this.layoutControlItemRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemRefresh.TextVisible = false;
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gridVisits;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 79);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(1080, 515);
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // layoutControlItemCount
            // 
            this.layoutControlItemCount.Control = this.lblCount;
            this.layoutControlItemCount.Location = new System.Drawing.Point(0, 594);
            this.layoutControlItemCount.Name = "layoutControlItemCount";
            this.layoutControlItemCount.Size = new System.Drawing.Size(116, 36);
            this.layoutControlItemCount.TextVisible = false;
            // 
            // emptySpaceItemBottom
            // 
            this.emptySpaceItemBottom.Location = new System.Drawing.Point(116, 594);
            this.emptySpaceItemBottom.Name = "emptySpaceItemBottom";
            this.emptySpaceItemBottom.Size = new System.Drawing.Size(776, 36);
            // 
            // layoutControlItemClose
            // 
            this.layoutControlItemClose.Control = this.btnClose;
            this.layoutControlItemClose.Location = new System.Drawing.Point(892, 594);
            this.layoutControlItemClose.MaxSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemClose.MinSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemClose.Name = "layoutControlItemClose";
            this.layoutControlItemClose.Size = new System.Drawing.Size(94, 36);
            this.layoutControlItemClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemClose.TextVisible = false;
            // 
            // VisitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.layoutControl);
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Name = "VisitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách Lượt khám";
            this.Load += new System.EventHandler(this.VisitForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnToday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLegend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemToolbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemExamine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemComplete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.DateEdit dtpDate;
        private DevExpress.XtraEditors.SimpleButton btnFilter;
        private DevExpress.XtraEditors.SimpleButton btnToday;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.LabelControl lblLegend;
        private DevExpress.XtraGrid.GridControl gridVisits;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.SimpleButton btnExamine;
        private DevExpress.XtraEditors.SimpleButton btnComplete;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTitle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnToday;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLegend;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemExamine;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemComplete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRefresh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCount;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemToolbar;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemBottom;


        private DevExpress.XtraGrid.Columns.GridColumn colVisitId;
        private DevExpress.XtraGrid.Columns.GridColumn colVisitDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPatientName;
        private DevExpress.XtraGrid.Columns.GridColumn colDoctorName;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusDisplay;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colSymptoms;
        private DevExpress.XtraGrid.Columns.GridColumn colDiagnosis;
        private DevExpress.XtraGrid.Columns.GridColumn colDiseaseName;
    }
}
