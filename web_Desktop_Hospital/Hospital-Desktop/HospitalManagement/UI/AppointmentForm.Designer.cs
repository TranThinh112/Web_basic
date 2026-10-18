namespace HospitalManagement.UI
{
    partial class AppointmentForm
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
            this.btnToday = new DevExpress.XtraEditors.SimpleButton();
            this.btnFilter = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.gridAppointments = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAppointmentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAppointmentDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPatientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDoctorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnFilter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBtnToday = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSearch = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAdd = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemConfirm = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRefresh = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCount = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemToolbar = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemBottom = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAppointments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnToday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemConfirm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemToolbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBottom)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.lblTitle);
            this.layoutControl.Controls.Add(this.dtpDate);
            this.layoutControl.Controls.Add(this.btnFilter);
            this.layoutControl.Controls.Add(this.btnToday);
            this.layoutControl.Controls.Add(this.txtSearch);
            this.layoutControl.Controls.Add(this.btnAdd);
            this.layoutControl.Controls.Add(this.gridAppointments);
            this.layoutControl.Controls.Add(this.btnConfirm);
            this.layoutControl.Controls.Add(this.btnCancel);
            this.layoutControl.Controls.Add(this.btnRefresh);
            this.layoutControl.Controls.Add(this.btnClose);
            this.layoutControl.Controls.Add(this.lblCount);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(1000, 600);
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
            this.lblTitle.Size = new System.Drawing.Size(976, 30);
            this.lblTitle.StyleController = this.layoutControl;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ LỊCH HẸN";
            // 
            // dtpDate
            // 
            this.dtpDate.EditValue = null;
            this.dtpDate.Location = new System.Drawing.Point(62, 46);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Size = new System.Drawing.Size(120, 20);
            this.dtpDate.StyleController = this.layoutControl;
            this.dtpDate.TabIndex = 1;
            // 
            // btnFilter
            // 
            this.btnFilter.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnFilter.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilter.Appearance.Options.UseBackColor = true;
            this.btnFilter.Appearance.Options.UseFont = true;
            this.btnFilter.Location = new System.Drawing.Point(186, 46);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(60, 22);
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
            this.btnToday.Location = new System.Drawing.Point(250, 46);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(80, 22);
            this.btnToday.StyleController = this.layoutControl;
            this.btnToday.TabIndex = 3;
            this.btnToday.Text = "Hôm nay";
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(334, 46);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.NullValuePrompt = "Tìm kiếm lịch hẹn...";
            this.txtSearch.Size = new System.Drawing.Size(250, 20);
            this.txtSearch.StyleController = this.layoutControl;
            this.txtSearch.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Appearance.Options.UseBackColor = true;
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Location = new System.Drawing.Point(898, 46);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 22);
            this.btnAdd.StyleController = this.layoutControl;
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Tạo lịch hẹn";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gridAppointments
            // 
            this.gridAppointments.Location = new System.Drawing.Point(12, 72);
            this.gridAppointments.MainView = this.gridView;
            this.gridAppointments.Name = "gridAppointments";
            this.gridAppointments.Size = new System.Drawing.Size(976, 480);
            this.gridAppointments.TabIndex = 6;
            this.gridAppointments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAppointmentId,
            this.colAppointmentDate,
            this.colPatientName,
            this.colDoctorName,
            this.colPhone,
            this.colReason,
            this.colStatus});
            this.gridView.GridControl = this.gridAppointments;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ShowGroupPanel = false;

            // colAppointmentId
            this.colAppointmentId.FieldName = "AppointmentId";
            this.colAppointmentId.Name = "colAppointmentId";
            // colAppointmentDate
            this.colAppointmentDate.Caption = "Ngày hẹn";
            this.colAppointmentDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.colAppointmentDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colAppointmentDate.FieldName = "AppointmentDate";
            this.colAppointmentDate.Name = "colAppointmentDate";
            this.colAppointmentDate.Visible = true;
            this.colAppointmentDate.VisibleIndex = 0;
            this.colAppointmentDate.Width = 120;
            // colPatientName
            this.colPatientName.Caption = "Bệnh nhân";
            this.colPatientName.FieldName = "PatientName";
            this.colPatientName.Name = "colPatientName";
            this.colPatientName.Visible = true;
            this.colPatientName.VisibleIndex = 1;
            this.colPatientName.Width = 150;
            // colDoctorName
            this.colDoctorName.Caption = "Bác sĩ";
            this.colDoctorName.FieldName = "DoctorName";
            this.colDoctorName.Name = "colDoctorName";
            this.colDoctorName.Visible = true;
            this.colDoctorName.VisibleIndex = 2;
            this.colDoctorName.Width = 150;
            // colPhone
            this.colPhone.Caption = "SĐT";
            this.colPhone.FieldName = "PatientPhone";
            this.colPhone.Name = "colPhone";
            this.colPhone.Visible = true;
            this.colPhone.VisibleIndex = 3;
            this.colPhone.Width = 100;
            // colReason
            this.colReason.Caption = "Lý do khám";
            this.colReason.FieldName = "Reason";
            this.colReason.Name = "colReason";
            this.colReason.Visible = true;
            this.colReason.VisibleIndex = 4;
            this.colReason.Width = 200;
            // colStatus
            this.colStatus.Caption = "Trạng thái";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 5;
            this.colStatus.Width = 100;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnConfirm.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Appearance.Options.UseBackColor = true;
            this.btnConfirm.Appearance.Options.UseFont = true;
            this.btnConfirm.Location = new System.Drawing.Point(706, 556);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 32);
            this.btnConfirm.StyleController = this.layoutControl;
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "Xác nhận";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(810, 556);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 32);
            this.btnCancel.StyleController = this.layoutControl;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnRefresh.Location = new System.Drawing.Point(894, 556);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(94, 36);
            this.btnRefresh.StyleController = this.layoutControl;
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "🔄 Làm mới";
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
            this.btnClose.Location = new System.Drawing.Point(894, 582);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 36);
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
            this.lblCount.Location = new System.Drawing.Point(12, 556);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(150, 19);
            this.lblCount.StyleController = this.layoutControl;
            this.lblCount.TabIndex = 11;
            this.lblCount.Text = "Tổng: 0 lịch hẹn";
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
            this.layoutControlItemAdd,
            this.emptySpaceItemToolbar,
            this.layoutControlItemGrid,
            this.layoutControlItemCount,
            this.emptySpaceItemBottom,
            this.layoutControlItemConfirm,
            this.layoutControlItemCancel,
            this.layoutControlItemRefresh,
            this.layoutControlItemClose});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1000, 600);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemTitle
            // 
            this.layoutControlItemTitle.Control = this.lblTitle;
            this.layoutControlItemTitle.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemTitle.Name = "layoutControlItemTitle";
            this.layoutControlItemTitle.Size = new System.Drawing.Size(980, 34);
            this.layoutControlItemTitle.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTitle.TextVisible = false;
            // 
            // layoutControlItemDate
            // 
            this.layoutControlItemDate.Control = this.dtpDate;
            this.layoutControlItemDate.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItemDate.Name = "layoutControlItemDate";
            this.layoutControlItemDate.Size = new System.Drawing.Size(174, 26);
            this.layoutControlItemDate.Text = "Ngày:";
            this.layoutControlItemDate.TextSize = new System.Drawing.Size(47, 13);
            // 
            // layoutControlItemBtnFilter
            // 
            this.layoutControlItemBtnFilter.Control = this.btnFilter;
            this.layoutControlItemBtnFilter.Location = new System.Drawing.Point(174, 34);
            this.layoutControlItemBtnFilter.Name = "layoutControlItemBtnFilter";
            this.layoutControlItemBtnFilter.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItemBtnFilter.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnFilter.TextVisible = false;
            // 
            // layoutControlItemBtnToday
            // 
            this.layoutControlItemBtnToday.Control = this.btnToday;
            this.layoutControlItemBtnToday.Location = new System.Drawing.Point(238, 34);
            this.layoutControlItemBtnToday.Name = "layoutControlItemBtnToday";
            this.layoutControlItemBtnToday.Size = new System.Drawing.Size(84, 26);
            this.layoutControlItemBtnToday.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBtnToday.TextVisible = false;
            // 
            // layoutControlItemSearch
            // 
            this.layoutControlItemSearch.Control = this.txtSearch;
            this.layoutControlItemSearch.Location = new System.Drawing.Point(322, 34);
            this.layoutControlItemSearch.Name = "layoutControlItemSearch";
            this.layoutControlItemSearch.Size = new System.Drawing.Size(254, 26);
            this.layoutControlItemSearch.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSearch.TextVisible = false;
            // 
            // layoutControlItemAdd
            // 
            this.layoutControlItemAdd.Control = this.btnAdd;
            this.layoutControlItemAdd.Location = new System.Drawing.Point(886, 34);
            this.layoutControlItemAdd.Name = "layoutControlItemAdd";
            this.layoutControlItemAdd.Size = new System.Drawing.Size(94, 26);
            this.layoutControlItemAdd.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAdd.TextVisible = false;
            // 
            // emptySpaceItemToolbar
            // 
            this.emptySpaceItemToolbar.AllowHotTrack = false;
            this.emptySpaceItemToolbar.Location = new System.Drawing.Point(576, 34);
            this.emptySpaceItemToolbar.Name = "emptySpaceItemToolbar";
            this.emptySpaceItemToolbar.Size = new System.Drawing.Size(310, 26);
            this.emptySpaceItemToolbar.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gridAppointments;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 60);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(980, 484);
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // layoutControlItemConfirm
            // 
            this.layoutControlItemConfirm.Control = this.btnConfirm;
            this.layoutControlItemConfirm.Location = new System.Drawing.Point(600, 544);
            this.layoutControlItemConfirm.MaxSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemConfirm.MinSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemConfirm.Name = "layoutControlItemConfirm";
            this.layoutControlItemConfirm.Size = new System.Drawing.Size(94, 36);
            this.layoutControlItemConfirm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemConfirm.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemConfirm.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(694, 544);
            this.layoutControlItemCancel.MaxSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemCancel.MinSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(94, 36);
            this.layoutControlItemCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // layoutControlItemRefresh
            // 
            this.layoutControlItemRefresh.Control = this.btnRefresh;
            this.layoutControlItemRefresh.Location = new System.Drawing.Point(788, 544);
            this.layoutControlItemRefresh.MaxSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemRefresh.MinSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemRefresh.Name = "layoutControlItemRefresh";
            this.layoutControlItemRefresh.Size = new System.Drawing.Size(94, 36);
            this.layoutControlItemRefresh.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemRefresh.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemRefresh.TextVisible = false;
            // 
            // layoutControlItemClose
            // 
            this.layoutControlItemClose.Control = this.btnClose;
            this.layoutControlItemClose.Location = new System.Drawing.Point(882, 544);
            this.layoutControlItemClose.MaxSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemClose.MinSize = new System.Drawing.Size(94, 36);
            this.layoutControlItemClose.Name = "layoutControlItemClose";
            this.layoutControlItemClose.Size = new System.Drawing.Size(94, 36);
            this.layoutControlItemClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemClose.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemClose.TextVisible = false;
            // 
            // layoutControlItemCount
            // 
            this.layoutControlItemCount.Control = this.lblCount;
            this.layoutControlItemCount.Location = new System.Drawing.Point(0, 544);
            this.layoutControlItemCount.Name = "layoutControlItemCount";
            this.layoutControlItemCount.Size = new System.Drawing.Size(200, 52);
            this.layoutControlItemCount.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCount.TextVisible = false;
            // 
            // emptySpaceItemBottom
            // 
            this.emptySpaceItemBottom.AllowHotTrack = false;
            this.emptySpaceItemBottom.Location = new System.Drawing.Point(200, 544);
            this.emptySpaceItemBottom.Name = "emptySpaceItemBottom";
            this.emptySpaceItemBottom.Size = new System.Drawing.Size(400, 36);
            this.emptySpaceItemBottom.TextSize = new System.Drawing.Size(0, 0);
            // 
            // AppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.layoutControl);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "AppointmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Lịch hẹn";
            this.Load += new System.EventHandler(this.AppointmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAppointments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBtnToday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemConfirm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemToolbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBottom)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.DateEdit dtpDate;
        private DevExpress.XtraEditors.SimpleButton btnToday;
        private DevExpress.XtraEditors.SimpleButton btnFilter;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraGrid.GridControl gridAppointments;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.LabelControl lblDate; // Unused but declaration kept to minimize errors if referenced in code, though removed from InitializeComponent
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTitle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBtnToday;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAdd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemConfirm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRefresh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCount;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemToolbar;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemBottom;
        private DevExpress.XtraGrid.Columns.GridColumn colAppointmentId;
        private DevExpress.XtraGrid.Columns.GridColumn colAppointmentDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPatientName;
        private DevExpress.XtraGrid.Columns.GridColumn colDoctorName;
        private DevExpress.XtraGrid.Columns.GridColumn colPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
    }
}
