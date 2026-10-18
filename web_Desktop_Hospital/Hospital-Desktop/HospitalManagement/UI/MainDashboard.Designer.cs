namespace HospitalManagement.UI
{
    partial class MainDashboard
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
            this.mainContainer = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.layoutControlDashboard = new DevExpress.XtraLayout.LayoutControl();
            this.gridDashboard = new DevExpress.XtraGrid.GridControl();
            this.gridViewDashboard = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDashboardTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDashboardPatient = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDashboardDoctor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDashboardReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDashboardStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tileControlStats = new DevExpress.XtraEditors.TileControl();
            this.tileGroupStats = new DevExpress.XtraEditors.TileGroup();
            this.tileGroupActions = new DevExpress.XtraEditors.TileGroup();
            this.tileItemPatient = new DevExpress.XtraEditors.TileItem();
            this.tileItemEmployee = new DevExpress.XtraEditors.TileItem();
            this.tileItemSchedule = new DevExpress.XtraEditors.TileItem();
            this.tileItemRevenue = new DevExpress.XtraEditors.TileItem();
            this.tileItemVisit = new DevExpress.XtraEditors.TileItem();
            this.lblWelcome = new DevExpress.XtraEditors.LabelControl();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemTiles = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemWelcome = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.aceDashboard = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            // Clinical Group
            this.aceGroupClinical = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acePatient = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceAppointment = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceVisit = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            // Finance Group
            this.aceGroupFinance = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceExaminationFee = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceMedicinePayment = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceRevenueStats = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            // Pharmacy Group
            this.aceGroupPharmacy = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acePharmacy = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceMedicine = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceDisease = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            // Admin Group
            this.aceGroupAdmin = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceUser = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceEmployee = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceClinicSettings = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            // User Group
            this.aceGroupUser = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceChangePassword = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aceLogout = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.mainContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDashboard)).BeginInit();
            this.layoutControlDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemWelcome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.Controls.Add(this.layoutControlDashboard);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(260, 31);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Size = new System.Drawing.Size(1107, 669);
            this.mainContainer.TabIndex = 0;
            // 
            // layoutControlDashboard
            // 
            this.layoutControlDashboard.Controls.Add(this.gridDashboard);
            this.layoutControlDashboard.Controls.Add(this.tileControlStats);
            this.layoutControlDashboard.Controls.Add(this.lblWelcome);
            this.layoutControlDashboard.Controls.Add(this.lblDate);
            this.layoutControlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlDashboard.Location = new System.Drawing.Point(0, 0);
            this.layoutControlDashboard.Name = "layoutControlDashboard";
            this.layoutControlDashboard.Root = this.Root;
            this.layoutControlDashboard.Size = new System.Drawing.Size(1107, 669);
            this.layoutControlDashboard.TabIndex = 0;
            // 
            // gridDashboard
            // 
            this.gridDashboard.Location = new System.Drawing.Point(12, 277);
            this.gridDashboard.MainView = this.gridViewDashboard;
            this.gridDashboard.Name = "gridDashboard";
            this.gridDashboard.Size = new System.Drawing.Size(1083, 380);
            this.gridDashboard.TabIndex = 5;
            this.gridDashboard.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDashboard});
            // 
            // gridViewDashboard
            // 
            this.gridViewDashboard.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDashboardTime,
            this.colDashboardPatient,
            this.colDashboardDoctor,
            this.colDashboardReason,
            this.colDashboardStatus});
            this.gridViewDashboard.GridControl = this.gridDashboard;
            this.gridViewDashboard.Name = "gridViewDashboard";
            this.gridViewDashboard.OptionsBehavior.Editable = false;
            this.gridViewDashboard.OptionsView.ShowGroupPanel = false;
            this.gridViewDashboard.OptionsView.ShowIndicator = false;
            // 
            // colDashboardTime
            // 
            this.colDashboardTime.Caption = "Thời gian";
            this.colDashboardTime.DisplayFormat.FormatString = "HH:mm";
            this.colDashboardTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDashboardTime.FieldName = "AppointmentDate";
            this.colDashboardTime.Name = "colDashboardTime";
            this.colDashboardTime.Visible = true;
            this.colDashboardTime.VisibleIndex = 0;
            this.colDashboardTime.Width = 60;
            // 
            // colDashboardPatient
            // 
            this.colDashboardPatient.Caption = "Bệnh nhân";
            this.colDashboardPatient.FieldName = "PatientName";
            this.colDashboardPatient.Name = "colDashboardPatient";
            this.colDashboardPatient.Visible = true;
            this.colDashboardPatient.VisibleIndex = 1;
            this.colDashboardPatient.Width = 150;
            // 
            // colDashboardDoctor
            // 
            this.colDashboardDoctor.Caption = "Bác sĩ";
            this.colDashboardDoctor.FieldName = "DoctorName";
            this.colDashboardDoctor.Name = "colDashboardDoctor";
            this.colDashboardDoctor.Visible = true;
            this.colDashboardDoctor.VisibleIndex = 2;
            this.colDashboardDoctor.Width = 150;
            // 
            // colDashboardReason
            // 
            this.colDashboardReason.Caption = "Lý do khám";
            this.colDashboardReason.FieldName = "Reason";
            this.colDashboardReason.Name = "colDashboardReason";
            this.colDashboardReason.Visible = true;
            this.colDashboardReason.VisibleIndex = 3;
            this.colDashboardReason.Width = 200;
            // 
            // colDashboardStatus
            // 
            this.colDashboardStatus.Caption = "Trạng thái";
            this.colDashboardStatus.FieldName = "Status";
            this.colDashboardStatus.Name = "colDashboardStatus";
            this.colDashboardStatus.Visible = true;
            this.colDashboardStatus.VisibleIndex = 4;
            this.colDashboardStatus.Width = 100;
            // 
            // tileControlStats
            // 
            this.tileControlStats.AllowDrag = false;
            this.tileControlStats.AllowSelectedItem = true;
            this.tileControlStats.Groups.Add(this.tileGroupStats);
            this.tileControlStats.Groups.Add(this.tileGroupActions);
            this.tileControlStats.Location = new System.Drawing.Point(12, 53);
            this.tileControlStats.MaxId = 10;
            this.tileControlStats.Name = "tileControlStats";
            this.tileControlStats.Size = new System.Drawing.Size(1083, 180);
            this.tileControlStats.TabIndex = 4;
            // 
            // tileGroupStats
            // 
            this.tileGroupStats.Items.Add(this.tileItemPatient);
            this.tileGroupStats.Items.Add(this.tileItemEmployee);
            this.tileGroupStats.Items.Add(this.tileItemSchedule);
            this.tileGroupStats.Items.Add(this.tileItemRevenue);
            this.tileGroupStats.Name = "tileGroupStats";
            // 
            // tileItemPatient - Bệnh nhân
            // 
            this.tileItemPatient.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.tileItemPatient.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileItemPatient.Id = 10;
            this.tileItemPatient.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.tileItemPatient.Name = "tileItemPatient";
            DevExpress.XtraEditors.TileItemElement tileItemPatientCount = new DevExpress.XtraEditors.TileItemElement();
            tileItemPatientCount.Text = "0";
            tileItemPatientCount.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight;
            tileItemPatientCount.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            DevExpress.XtraEditors.TileItemElement tileItemPatientIcon = new DevExpress.XtraEditors.TileItemElement();
            tileItemPatientIcon.Text = "👤";
            tileItemPatientIcon.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemPatientIcon.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 28F);
            DevExpress.XtraEditors.TileItemElement tileItemPatientTitle = new DevExpress.XtraEditors.TileItemElement();
            tileItemPatientTitle.Text = "Bệnh nhân";
            tileItemPatientTitle.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileItemPatientTitle.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tileItemPatient.Elements.Add(tileItemPatientCount);
            this.tileItemPatient.Elements.Add(tileItemPatientIcon);
            this.tileItemPatient.Elements.Add(tileItemPatientTitle);
            // 
            // tileItemEmployee - Nhân viên
            // 
            this.tileItemEmployee.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.tileItemEmployee.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileItemEmployee.Id = 11;
            this.tileItemEmployee.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.tileItemEmployee.Name = "tileItemEmployee";
            DevExpress.XtraEditors.TileItemElement tileItemEmployeeCount = new DevExpress.XtraEditors.TileItemElement();
            tileItemEmployeeCount.Text = "0";
            tileItemEmployeeCount.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight;
            tileItemEmployeeCount.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            DevExpress.XtraEditors.TileItemElement tileItemEmployeeIcon = new DevExpress.XtraEditors.TileItemElement();
            tileItemEmployeeIcon.Text = "👥";
            tileItemEmployeeIcon.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemEmployeeIcon.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 28F);
            DevExpress.XtraEditors.TileItemElement tileItemEmployeeTitle = new DevExpress.XtraEditors.TileItemElement();
            tileItemEmployeeTitle.Text = "Nhân viên";
            tileItemEmployeeTitle.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileItemEmployeeTitle.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tileItemEmployee.Elements.Add(tileItemEmployeeCount);
            this.tileItemEmployee.Elements.Add(tileItemEmployeeIcon);
            this.tileItemEmployee.Elements.Add(tileItemEmployeeTitle);
            // 
            // tileItemSchedule - Lịch hôm nay
            // 
            this.tileItemSchedule.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.tileItemSchedule.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileItemSchedule.Id = 12;
            this.tileItemSchedule.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.tileItemSchedule.Name = "tileItemSchedule";
            DevExpress.XtraEditors.TileItemElement tileItemScheduleCount = new DevExpress.XtraEditors.TileItemElement();
            tileItemScheduleCount.Text = "0";
            tileItemScheduleCount.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight;
            tileItemScheduleCount.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            DevExpress.XtraEditors.TileItemElement tileItemScheduleIcon = new DevExpress.XtraEditors.TileItemElement();
            tileItemScheduleIcon.Text = "📅";
            tileItemScheduleIcon.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemScheduleIcon.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 28F);
            DevExpress.XtraEditors.TileItemElement tileItemScheduleTitle = new DevExpress.XtraEditors.TileItemElement();
            tileItemScheduleTitle.Text = "Lịch hôm nay";
            tileItemScheduleTitle.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileItemScheduleTitle.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tileItemSchedule.Elements.Add(tileItemScheduleCount);
            this.tileItemSchedule.Elements.Add(tileItemScheduleIcon);
            this.tileItemSchedule.Elements.Add(tileItemScheduleTitle);
            // 
            // tileItemRevenue - Doanh thu ngày
            // 
            this.tileItemRevenue.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.tileItemRevenue.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileItemRevenue.Id = 13;
            this.tileItemRevenue.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.tileItemRevenue.Name = "tileItemRevenue";
            DevExpress.XtraEditors.TileItemElement tileItemRevenueCount = new DevExpress.XtraEditors.TileItemElement();
            tileItemRevenueCount.Text = "0 đ";
            tileItemRevenueCount.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight;
            tileItemRevenueCount.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            DevExpress.XtraEditors.TileItemElement tileItemRevenueIcon = new DevExpress.XtraEditors.TileItemElement();
            tileItemRevenueIcon.Text = "💰";
            tileItemRevenueIcon.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemRevenueIcon.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 28F);
            DevExpress.XtraEditors.TileItemElement tileItemRevenueTitle = new DevExpress.XtraEditors.TileItemElement();
            tileItemRevenueTitle.Text = "Doanh thu ngày";
            tileItemRevenueTitle.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileItemRevenueTitle.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tileItemRevenue.Elements.Add(tileItemRevenueCount);
            this.tileItemRevenue.Elements.Add(tileItemRevenueIcon);
            this.tileItemRevenue.Elements.Add(tileItemRevenueTitle);
            // 
            // tileGroupActions
            // 
            this.tileGroupActions.Items.Add(this.tileItemVisit);
            this.tileGroupActions.Name = "tileGroupActions";
            // 
            // tileItemVisit - Khám bệnh tile with count
            // 
            this.tileItemVisit.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.tileItemVisit.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileItemVisit.Id = 2;
            this.tileItemVisit.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.tileItemVisit.Name = "tileItemVisit";
            DevExpress.XtraEditors.TileItemElement tileItemVisitIcon = new DevExpress.XtraEditors.TileItemElement();
            tileItemVisitIcon.Text = "🩺";
            tileItemVisitIcon.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemVisitIcon.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 28F);
            DevExpress.XtraEditors.TileItemElement tileItemVisitTitle = new DevExpress.XtraEditors.TileItemElement();
            tileItemVisitTitle.Text = "Khám bệnh";
            tileItemVisitTitle.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.BottomCenter;
            tileItemVisitTitle.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tileItemVisit.Elements.Add(tileItemVisitIcon);
            this.tileItemVisit.Elements.Add(tileItemVisitTitle);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Appearance.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblWelcome.Appearance.Options.UseFont = true;
            this.lblWelcome.Appearance.Options.UseForeColor = true;
            this.lblWelcome.Location = new System.Drawing.Point(12, 12);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(209, 37);
            this.lblWelcome.StyleController = this.layoutControlDashboard;
            this.lblWelcome.TabIndex = 6;
            this.lblWelcome.Text = "Xin chào, Admin";
            // 
            // lblDate
            // 
            this.lblDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblDate.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblDate.Appearance.Options.UseFont = true;
            this.lblDate.Appearance.Options.UseForeColor = true;
            this.lblDate.Location = new System.Drawing.Point(225, 25);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(187, 21);
            this.lblDate.StyleController = this.layoutControlDashboard;
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "Thứ Hai, 01/01/2026 08:00";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTiles,
            this.layoutControlItemGrid,
            this.layoutControlItemWelcome,
            this.layoutControlItemDate,
            this.simpleLabelItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1107, 669);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemTiles
            // 
            this.layoutControlItemTiles.Control = this.tileControlStats;
            this.layoutControlItemTiles.Location = new System.Drawing.Point(0, 41);
            this.layoutControlItemTiles.MaxSize = new System.Drawing.Size(0, 184);
            this.layoutControlItemTiles.MinSize = new System.Drawing.Size(104, 184);
            this.layoutControlItemTiles.Name = "layoutControlItemTiles";
            this.layoutControlItemTiles.Size = new System.Drawing.Size(1087, 184);
            this.layoutControlItemTiles.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemTiles.TextVisible = false;
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gridDashboard;
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 265);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(1087, 384);
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // layoutControlItemWelcome
            // 
            this.layoutControlItemWelcome.Control = this.lblWelcome;
            this.layoutControlItemWelcome.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemWelcome.Name = "layoutControlItemWelcome";
            this.layoutControlItemWelcome.Size = new System.Drawing.Size(213, 41);
            this.layoutControlItemWelcome.TextVisible = false;
            // 
            // layoutControlItemDate
            // 
            this.layoutControlItemDate.ContentVertAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.layoutControlItemDate.Control = this.lblDate;
            this.layoutControlItemDate.Location = new System.Drawing.Point(213, 0);
            this.layoutControlItemDate.Name = "layoutControlItemDate";
            this.layoutControlItemDate.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 5);
            this.layoutControlItemDate.Size = new System.Drawing.Size(874, 41);
            this.layoutControlItemDate.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 225);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 15, 5);
            this.simpleLabelItem1.Size = new System.Drawing.Size(1087, 40);
            this.simpleLabelItem1.Text = "📅 Lịch trình hôm nay";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(157, 20);
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.aceDashboard,
            this.aceGroupClinical,
            this.aceGroupFinance,
            this.aceGroupPharmacy,
            this.aceGroupAdmin,
            this.aceGroupUser});
            this.accordionControl1.Location = new System.Drawing.Point(0, 31);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(260, 669);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // aceDashboard
            // 
            this.aceDashboard.Name = "aceDashboard";
            this.aceDashboard.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceDashboard.Text = "Dashboard";
            // 
            // aceGroupClinical - Lâm sàng
            // 
            this.aceGroupClinical.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acePatient,
            this.aceAppointment,
            this.aceVisit});
            this.aceGroupClinical.Name = "aceGroupClinical";
            this.aceGroupClinical.Text = "Lâm sàng";
            // 
            // acePatient
            // 
            this.acePatient.Name = "acePatient";
            this.acePatient.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acePatient.Text = "Bệnh nhân";
            // 
            // aceAppointment
            // 
            this.aceAppointment.Name = "aceAppointment";
            this.aceAppointment.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceAppointment.Text = "Lịch hẹn";
            // 
            // aceVisit
            // 
            this.aceVisit.Name = "aceVisit";
            this.aceVisit.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceVisit.Text = "Khám bệnh";
            // 
            // aceGroupFinance - Tài chính
            // 
            this.aceGroupFinance.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.aceExaminationFee,
            this.aceMedicinePayment,
            this.aceRevenueStats});
            this.aceGroupFinance.Name = "aceGroupFinance";
            this.aceGroupFinance.Text = "Tài chính";
            // 
            // aceExaminationFee
            // 
            this.aceExaminationFee.Name = "aceExaminationFee";
            this.aceExaminationFee.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceExaminationFee.Text = "Thu phí khám";
            // 
            // aceMedicinePayment
            // 
            this.aceMedicinePayment.Name = "aceMedicinePayment";
            this.aceMedicinePayment.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceMedicinePayment.Text = "Thu tiền thuốc";
            // 
            // aceRevenueStats
            // 
            this.aceRevenueStats.Name = "aceRevenueStats";
            this.aceRevenueStats.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceRevenueStats.Text = "Thống kê Doanh thu";
            // 
            // aceGroupPharmacy - Dược
            // 
            this.aceGroupPharmacy.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acePharmacy,
            this.aceMedicine,
            this.aceDisease});
            this.aceGroupPharmacy.Name = "aceGroupPharmacy";
            this.aceGroupPharmacy.Text = "Dược";
            // 
            // acePharmacy
            // 
            this.acePharmacy.Name = "acePharmacy";
            this.acePharmacy.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acePharmacy.Text = "Quầy thuốc";
            // 
            // aceMedicine
            // 
            this.aceMedicine.Name = "aceMedicine";
            this.aceMedicine.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceMedicine.Text = "Kho thuốc";
            // 
            // aceDisease
            // 
            this.aceDisease.Name = "aceDisease";
            this.aceDisease.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceDisease.Text = "Danh mục Bệnh";
            // 
            // aceGroupAdmin - Hệ thống
            // 
            this.aceGroupAdmin.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.aceUser,
            this.aceEmployee,
            this.aceClinicSettings});
            this.aceGroupAdmin.Name = "aceGroupAdmin";
            this.aceGroupAdmin.Text = "Hệ thống";
            // 
            // aceUser
            // 
            this.aceUser.Name = "aceUser";
            this.aceUser.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceUser.Text = "Người dùng";
            // 
            // aceEmployee
            // 
            this.aceEmployee.Name = "aceEmployee";
            this.aceEmployee.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceEmployee.Text = "Nhân viên";
            // 
            // aceClinicSettings
            // 
            this.aceClinicSettings.Name = "aceClinicSettings";
            this.aceClinicSettings.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceClinicSettings.Text = "Cài đặt Phòng khám";
            // 
            // aceGroupUser - Tài khoản
            // 
            this.aceGroupUser.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.aceChangePassword,
            this.aceLogout});
            this.aceGroupUser.Name = "aceGroupUser";
            this.aceGroupUser.Text = "Tài khoản";
            // 
            // aceChangePassword
            // 
            this.aceChangePassword.Name = "aceChangePassword";
            this.aceChangePassword.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceChangePassword.Text = "Đổi mật khẩu";
            // 
            // aceLogout
            // 
            this.aceLogout.Name = "aceLogout";
            this.aceLogout.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aceLogout.Text = "Đăng xuất";
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(1367, 31);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            // 
            // MainDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 700);
            this.ControlContainer = this.mainContainer;
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Name = "MainDashboard";
            this.NavigationControl = this.accordionControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hospital Management System";
            this.Load += new System.EventHandler(this.MainDashboard_Load);
            this.mainContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDashboard)).EndInit();
            this.layoutControlDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDashboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDashboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemWelcome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer mainContainer;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceDashboard;
        // Clinical Group
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceGroupClinical;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acePatient;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceAppointment;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceVisit;
        // Finance Group
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceGroupFinance;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceExaminationFee;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceMedicinePayment;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceRevenueStats;
        // Pharmacy Group
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceGroupPharmacy;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acePharmacy;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceMedicine;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceDisease;
        // Admin Group
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceGroupAdmin;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceUser;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceEmployee;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceClinicSettings;
        // User Group
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceGroupUser;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceChangePassword;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aceLogout;
        private DevExpress.XtraLayout.LayoutControl layoutControlDashboard;
        private DevExpress.XtraGrid.GridControl gridDashboard;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDashboard;
        private DevExpress.XtraGrid.Columns.GridColumn colDashboardTime;
        private DevExpress.XtraGrid.Columns.GridColumn colDashboardPatient;
        private DevExpress.XtraGrid.Columns.GridColumn colDashboardDoctor;
        private DevExpress.XtraGrid.Columns.GridColumn colDashboardReason;
        private DevExpress.XtraGrid.Columns.GridColumn colDashboardStatus;
        private DevExpress.XtraEditors.TileControl tileControlStats;
        private DevExpress.XtraEditors.TileGroup tileGroupStats;
        private DevExpress.XtraEditors.TileGroup tileGroupActions;
        private DevExpress.XtraEditors.TileItem tileItemVisit;
        private DevExpress.XtraEditors.TileItem tileItemPatient;
        private DevExpress.XtraEditors.TileItem tileItemEmployee;
        private DevExpress.XtraEditors.TileItem tileItemSchedule;
        private DevExpress.XtraEditors.TileItem tileItemRevenue;
        private DevExpress.XtraEditors.LabelControl lblWelcome;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTiles;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemWelcome;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDate;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
    }
}
