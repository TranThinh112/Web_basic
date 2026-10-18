namespace HospitalManagement.UI
{
    partial class MedicinePaymentForm
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
            this.btnHistory = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grpList = new DevExpress.XtraEditors.GroupControl();
            this.gridSales = new DevExpress.XtraGrid.GridControl();
            this.gridViewSales = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSaleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPatientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSaleDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpDetails = new DevExpress.XtraEditors.GroupControl();
            this.lblTotal = new DevExpress.XtraEditors.LabelControl();
            this.btnPayTransfer = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayCard = new DevExpress.XtraEditors.SimpleButton();
            this.btnPayCash = new DevExpress.XtraEditors.SimpleButton();
            this.gridDetails = new DevExpress.XtraGrid.GridControl();
            this.gridViewDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDetailName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetailQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetailPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetailTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpList)).BeginInit();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDetails)).BeginInit();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Appearance.Options.UseBackColor = true;
            this.pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlHeader.Controls.Add(this.btnHistory);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.btnHistory.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHistory.Appearance.Options.UseBackColor = true;
            this.btnHistory.Appearance.Options.UseFont = true;
            this.btnHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistory.Location = new System.Drawing.Point(830, 9);
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
            this.lblTitle.Size = new System.Drawing.Size(193, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "💊 THU TIỀN THUỐC";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 50);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.grpList);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grpDetails);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1000, 500);
            this.splitContainerControl1.SplitterPosition = 550;
            this.splitContainerControl1.TabIndex = 1;
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.gridSales);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Location = new System.Drawing.Point(0, 0);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(550, 500);
            this.grpList.TabIndex = 0;
            this.grpList.Text = "Danh sách đơn thuốc chờ thanh toán";
            // 
            // gridSales
            // 
            this.gridSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSales.Location = new System.Drawing.Point(2, 23);
            this.gridSales.MainView = this.gridViewSales;
            this.gridSales.Name = "gridSales";
            this.gridSales.Size = new System.Drawing.Size(546, 475);
            this.gridSales.TabIndex = 0;
            this.gridSales.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSales});
            // 
            // gridViewSales
            // 
            this.gridViewSales.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSaleCode,
            this.colPatientName,
            this.colTotalAmount,
            this.colSaleDate});
            this.gridViewSales.GridControl = this.gridSales;
            this.gridViewSales.Name = "gridViewSales";
            this.gridViewSales.OptionsBehavior.Editable = false;
            this.gridViewSales.OptionsFind.AlwaysVisible = true;
            this.gridViewSales.OptionsView.ShowGroupPanel = false;
            this.gridViewSales.OptionsView.ShowIndicator = false;
            // 
            // colSaleCode
            // 
            this.colSaleCode.Caption = "Mã đơn";
            this.colSaleCode.FieldName = "SaleCode";
            this.colSaleCode.Name = "colSaleCode";
            this.colSaleCode.Visible = true;
            this.colSaleCode.VisibleIndex = 0;
            this.colSaleCode.Width = 80;
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
            // colTotalAmount
            // 
            this.colTotalAmount.Caption = "Tổng tiền";
            this.colTotalAmount.DisplayFormat.FormatString = "N0";
            this.colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalAmount.FieldName = "TotalAmount";
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.Visible = true;
            this.colTotalAmount.VisibleIndex = 2;
            this.colTotalAmount.Width = 100;
            // 
            // colSaleDate
            // 
            this.colSaleDate.Caption = "Ngày";
            this.colSaleDate.DisplayFormat.FormatString = "dd/MM HH:mm";
            this.colSaleDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colSaleDate.FieldName = "SaleDate";
            this.colSaleDate.Name = "colSaleDate";
            this.colSaleDate.Visible = true;
            this.colSaleDate.VisibleIndex = 3;
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.lblTotal);
            this.grpDetails.Controls.Add(this.btnPayTransfer);
            this.grpDetails.Controls.Add(this.btnPayCard);
            this.grpDetails.Controls.Add(this.btnPayCash);
            this.grpDetails.Controls.Add(this.gridDetails);
            this.grpDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetails.Location = new System.Drawing.Point(0, 0);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(440, 500);
            this.grpDetails.TabIndex = 0;
            this.grpDetails.Text = "Chi tiết đơn thuốc";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblTotal.Appearance.Options.UseFont = true;
            this.lblTotal.Appearance.Options.UseForeColor = true;
            this.lblTotal.Location = new System.Drawing.Point(10, 395);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(155, 21);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "TỔNG TIỀN: 0 VNĐ";
            // 
            // btnPayTransfer
            // 
            this.btnPayTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPayTransfer.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnPayTransfer.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPayTransfer.Appearance.Options.UseBackColor = true;
            this.btnPayTransfer.Appearance.Options.UseFont = true;
            this.btnPayTransfer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayTransfer.Location = new System.Drawing.Point(232, 435);
            this.btnPayTransfer.Name = "btnPayTransfer";
            this.btnPayTransfer.Size = new System.Drawing.Size(100, 45);
            this.btnPayTransfer.TabIndex = 3;
            this.btnPayTransfer.Text = "🏦 CK";
            this.btnPayTransfer.Click += new System.EventHandler(this.BtnPayTransfer_Click);
            // 
            // btnPayCard
            // 
            this.btnPayCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPayCard.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnPayCard.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPayCard.Appearance.Options.UseBackColor = true;
            this.btnPayCard.Appearance.Options.UseFont = true;
            this.btnPayCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayCard.Location = new System.Drawing.Point(121, 435);
            this.btnPayCard.Name = "btnPayCard";
            this.btnPayCard.Size = new System.Drawing.Size(100, 45);
            this.btnPayCard.TabIndex = 2;
            this.btnPayCard.Text = "💳 Thẻ";
            this.btnPayCard.Click += new System.EventHandler(this.BtnPayCard_Click);
            // 
            // btnPayCash
            // 
            this.btnPayCash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPayCash.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnPayCash.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPayCash.Appearance.Options.UseBackColor = true;
            this.btnPayCash.Appearance.Options.UseFont = true;
            this.btnPayCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayCash.Location = new System.Drawing.Point(10, 435);
            this.btnPayCash.Name = "btnPayCash";
            this.btnPayCash.Size = new System.Drawing.Size(100, 45);
            this.btnPayCash.TabIndex = 1;
            this.btnPayCash.Text = "💵 Tiền mặt";
            this.btnPayCash.Click += new System.EventHandler(this.BtnPayCash_Click);
            // 
            // gridDetails
            // 
            this.gridDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDetails.Location = new System.Drawing.Point(2, 23);
            this.gridDetails.MainView = this.gridViewDetails;
            this.gridDetails.Name = "gridDetails";
            this.gridDetails.Size = new System.Drawing.Size(436, 360);
            this.gridDetails.TabIndex = 0;
            this.gridDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDetails});
            // 
            // gridViewDetails
            // 
            this.gridViewDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDetailName,
            this.colDetailQty,
            this.colDetailPrice,
            this.colDetailTotal});
            this.gridViewDetails.GridControl = this.gridDetails;
            this.gridViewDetails.Name = "gridViewDetails";
            this.gridViewDetails.OptionsBehavior.Editable = false;
            this.gridViewDetails.OptionsView.ShowGroupPanel = false;
            this.gridViewDetails.OptionsView.ShowIndicator = false;
            // 
            // colDetailName
            // 
            this.colDetailName.Caption = "Thuốc";
            this.colDetailName.FieldName = "MedicineName";
            this.colDetailName.Name = "colDetailName";
            this.colDetailName.Visible = true;
            this.colDetailName.VisibleIndex = 0;
            this.colDetailName.Width = 150;
            // 
            // colDetailQty
            // 
            this.colDetailQty.Caption = "SL";
            this.colDetailQty.FieldName = "Quantity";
            this.colDetailQty.Name = "colDetailQty";
            this.colDetailQty.Visible = true;
            this.colDetailQty.VisibleIndex = 1;
            this.colDetailQty.Width = 50;
            // 
            // colDetailPrice
            // 
            this.colDetailPrice.Caption = "Đơn giá";
            this.colDetailPrice.DisplayFormat.FormatString = "N0";
            this.colDetailPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDetailPrice.FieldName = "UnitPrice";
            this.colDetailPrice.Name = "colDetailPrice";
            this.colDetailPrice.Visible = true;
            this.colDetailPrice.VisibleIndex = 2;
            this.colDetailPrice.Width = 80;
            // 
            // colDetailTotal
            // 
            this.colDetailTotal.Caption = "Thành tiền";
            this.colDetailTotal.DisplayFormat.FormatString = "N0";
            this.colDetailTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDetailTotal.FieldName = "LineTotal";
            this.colDetailTotal.Name = "colDetailTotal";
            this.colDetailTotal.Visible = true;
            this.colDetailTotal.VisibleIndex = 3;
            this.colDetailTotal.Width = 100;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnRefresh);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 550);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1000, 50);
            this.pnlBottom.TabIndex = 2;
            this.pnlBottom.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(890, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 35);
            this.btnClose.TabIndex = 1;
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
            this.btnRefresh.Location = new System.Drawing.Point(780, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // MedicinePaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Name = "MedicinePaymentForm";
            this.Text = "Thu tiền thuốc";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpList)).EndInit();
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDetails)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.SimpleButton btnHistory;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl grpList;
        private DevExpress.XtraGrid.GridControl gridSales;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSales;
        private DevExpress.XtraGrid.Columns.GridColumn colSaleCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPatientName;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colSaleDate;
        private DevExpress.XtraEditors.GroupControl grpDetails;
        private DevExpress.XtraGrid.GridControl gridDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailName;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailQty;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDetailTotal;
        private DevExpress.XtraEditors.SimpleButton btnPayCash;
        private DevExpress.XtraEditors.SimpleButton btnPayCard;
        private DevExpress.XtraEditors.SimpleButton btnPayTransfer;
        private DevExpress.XtraEditors.LabelControl lblTotal;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
    }
}
