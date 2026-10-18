namespace HospitalManagement.UI
{
    partial class ExaminationFeeForm
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
            this.lblFee = new DevExpress.XtraEditors.LabelControl();
            this.btnHistory = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlToolbar = new DevExpress.XtraEditors.PanelControl();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.btnFilter = new DevExpress.XtraEditors.SimpleButton();
            this.dtpDate = new DevExpress.XtraEditors.DateEdit();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.gridVisits = new DevExpress.XtraGrid.GridControl();
            this.gridViewVisits = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAppId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPatientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDoctor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlRight = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayTransfer = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayCard = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayCash = new DevExpress.XtraEditors.SimpleButton();
            this.pnlFooter = new DevExpress.XtraEditors.PanelControl();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolbar)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRight)).BeginInit();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Appearance.Options.UseBackColor = true;
            this.pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlHeader.Controls.Add(this.lblFee);
            this.pnlHeader.Controls.Add(this.btnHistory);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblFee
            // 
            this.lblFee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFee.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFee.Appearance.ForeColor = System.Drawing.Color.Fuchsia;
            this.lblFee.Appearance.Options.UseFont = true;
            this.lblFee.Appearance.Options.UseForeColor = true;
            this.lblFee.Location = new System.Drawing.Point(650, 15);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(132, 21);
            this.lblFee.TabIndex = 2;
            this.lblFee.Text = "Phí khám: ... VNĐ";
            // 
            // btnHistory
            // 
            this.btnHistory.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.btnHistory.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHistory.Appearance.Options.UseBackColor = true;
            this.btnHistory.Appearance.Options.UseFont = true;
            this.btnHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistory.Location = new System.Drawing.Point(360, 9);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(140, 32);
            this.btnHistory.TabIndex = 1;
            this.btnHistory.Text = "📋 Xem hóa đơn cũ";
            this.btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(225, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "💰 THU PHÍ KHÁM BỆNH";
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.txtSearch);
            this.pnlToolbar.Controls.Add(this.btnFilter);
            this.pnlToolbar.Controls.Add(this.dtpDate);
            this.pnlToolbar.Controls.Add(this.lblDate);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 50);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(900, 50);
            this.pnlToolbar.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(280, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Properties.Appearance.Options.UseFont = true;
            this.txtSearch.Properties.NullText = "Tìm kiếm...";
            this.txtSearch.Size = new System.Drawing.Size(200, 24);
            this.txtSearch.TabIndex = 3;
            // 
            // btnFilter
            // 
            this.btnFilter.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnFilter.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFilter.Appearance.Options.UseBackColor = true;
            this.btnFilter.Appearance.Options.UseFont = true;
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.Location = new System.Drawing.Point(195, 11);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(60, 28);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.Click += new System.EventHandler(this.BtnFilter_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.EditValue = null;
            this.dtpDate.Location = new System.Drawing.Point(55, 13);
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
            // lblDate
            // 
            this.lblDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.Appearance.Options.UseFont = true;
            this.lblDate.Location = new System.Drawing.Point(15, 16);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(34, 17);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Ngày:";
            // 
            // gridVisits
            // 
            this.gridVisits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVisits.Location = new System.Drawing.Point(0, 100);
            this.gridVisits.MainView = this.gridViewVisits;
            this.gridVisits.Name = "gridVisits";
            this.gridVisits.Size = new System.Drawing.Size(780, 410);
            this.gridVisits.TabIndex = 2;
            this.gridVisits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewVisits});
            // 
            // gridViewVisits
            // 
            this.gridViewVisits.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAppId,
            this.colPatientName,
            this.colTime,
            this.colDoctor,
            this.colReason});
            this.gridViewVisits.GridControl = this.gridVisits;
            this.gridViewVisits.Name = "gridViewVisits";
            this.gridViewVisits.OptionsBehavior.Editable = false;
            this.gridViewVisits.OptionsView.ShowGroupPanel = false;
            this.gridViewVisits.OptionsView.ShowIndicator = false;
            // 
            // colAppId
            // 
            this.colAppId.Caption = "Mã LH";
            this.colAppId.FieldName = "AppointmentId";
            this.colAppId.Name = "colAppId";
            this.colAppId.Visible = true;
            this.colAppId.VisibleIndex = 0;
            this.colAppId.Width = 60;
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
            // colTime
            // 
            this.colTime.Caption = "Giờ hẹn";
            this.colTime.DisplayFormat.FormatString = "dd/MM HH:mm";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "AppointmentDate";
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 2;
            this.colTime.Width = 100;
            // 
            // colDoctor
            // 
            this.colDoctor.Caption = "Bác sĩ";
            this.colDoctor.FieldName = "DoctorName";
            this.colDoctor.Name = "colDoctor";
            this.colDoctor.Visible = true;
            this.colDoctor.VisibleIndex = 3;
            // 
            // colReason
            // 
            this.colReason.Caption = "Lý do";
            this.colReason.FieldName = "Reason";
            this.colReason.Name = "colReason";
            this.colReason.Visible = true;
            this.colReason.VisibleIndex = 4;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.btnClose);
            this.pnlRight.Controls.Add(this.btnRefresh);
            this.pnlRight.Controls.Add(this.btnPayTransfer);
            this.pnlRight.Controls.Add(this.btnPayCard);
            this.pnlRight.Controls.Add(this.btnPayCash);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(780, 100);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(120, 410);
            this.pnlRight.TabIndex = 3;
            this.pnlRight.Visible = true;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(6, 360);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 40);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.btnRefresh.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Appearance.Options.UseBackColor = true;
            this.btnRefresh.Appearance.Options.UseFont = true;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Location = new System.Drawing.Point(6, 200);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(108, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.BtnFilter_Click);
            // 
            // btnPayTransfer
            // 
            this.btnPayTransfer.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnPayTransfer.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPayTransfer.Appearance.Options.UseBackColor = true;
            this.btnPayTransfer.Appearance.Options.UseFont = true;
            this.btnPayTransfer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayTransfer.Location = new System.Drawing.Point(6, 120);
            this.btnPayTransfer.Name = "btnPayTransfer";
            this.btnPayTransfer.Size = new System.Drawing.Size(108, 50);
            this.btnPayTransfer.TabIndex = 2;
            this.btnPayTransfer.Text = "🏦 Chuyển khoản";
            this.btnPayTransfer.Click += new System.EventHandler(this.BtnPayTransfer_Click);
            // 
            // btnPayCard
            // 
            this.btnPayCard.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnPayCard.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPayCard.Appearance.Options.UseBackColor = true;
            this.btnPayCard.Appearance.Options.UseFont = true;
            this.btnPayCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayCard.Location = new System.Drawing.Point(6, 60);
            this.btnPayCard.Name = "btnPayCard";
            this.btnPayCard.Size = new System.Drawing.Size(108, 50);
            this.btnPayCard.TabIndex = 1;
            this.btnPayCard.Text = "💳 Thẻ";
            this.btnPayCard.Click += new System.EventHandler(this.BtnPayCard_Click);
            // 
            // btnPayCash
            // 
            this.btnPayCash.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnPayCash.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPayCash.Appearance.Options.UseBackColor = true;
            this.btnPayCash.Appearance.Options.UseFont = true;
            this.btnPayCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayCash.Location = new System.Drawing.Point(6, 6);
            this.btnPayCash.Name = "btnPayCash";
            this.btnPayCash.Size = new System.Drawing.Size(108, 50);
            this.btnPayCash.TabIndex = 0;
            this.btnPayCash.Text = "💵 Tiền mặt";
            this.btnPayCash.Click += new System.EventHandler(this.BtnPayCash_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.lblCount);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 510);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(900, 40);
            this.pnlFooter.TabIndex = 4;
            // 
            // lblCount
            // 
            this.lblCount.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCount.Appearance.Options.UseFont = true;
            this.lblCount.Location = new System.Drawing.Point(20, 12);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(112, 19);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "Chờ thanh toán: 0";
            // 
            // ExaminationFeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.gridVisits);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ExaminationFeeForm";
            this.Text = "Thu phí khám";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolbar)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRight)).EndInit();
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.SimpleButton btnHistory;
        private DevExpress.XtraEditors.LabelControl lblFee;
        private DevExpress.XtraEditors.PanelControl pnlToolbar;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.DateEdit dtpDate;
        private DevExpress.XtraEditors.SimpleButton btnFilter;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraGrid.GridControl gridVisits;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewVisits;
        private DevExpress.XtraGrid.Columns.GridColumn colAppId;
        private DevExpress.XtraGrid.Columns.GridColumn colPatientName;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colDoctor;
        private DevExpress.XtraGrid.Columns.GridColumn colReason;
        private DevExpress.XtraEditors.PanelControl pnlRight;
        private DevExpress.XtraEditors.SimpleButton btnPayCash;
        private DevExpress.XtraEditors.SimpleButton btnPayCard;
        private DevExpress.XtraEditors.SimpleButton btnPayTransfer;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.PanelControl pnlFooter;
        private DevExpress.XtraEditors.LabelControl lblCount;
    }
}
