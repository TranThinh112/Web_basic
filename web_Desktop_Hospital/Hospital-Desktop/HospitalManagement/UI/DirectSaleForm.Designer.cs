namespace HospitalManagement.UI
{
    partial class DirectSaleForm
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
            this.grpCustomer = new DevExpress.XtraEditors.GroupControl();
            this.slkPatient = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblPatient = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.chkWalkIn = new DevExpress.XtraEditors.CheckEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grpMedicines = new DevExpress.XtraEditors.GroupControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.spnQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridMedicines = new DevExpress.XtraGrid.GridControl();
            this.gridViewMedicines = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMedId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMedName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMedPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMedStock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpCart = new DevExpress.XtraEditors.GroupControl();
            this.lblTotal = new DevExpress.XtraEditors.LabelControl();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.gridCart = new DevExpress.XtraGrid.GridControl();
            this.gridViewCart = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCartMedId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCartName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCartQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCartPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCartTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCustomer)).BeginInit();
            this.grpCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkPatient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWalkIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMedicines)).BeginInit();
            this.grpMedicines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMedicines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMedicines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCart)).BeginInit();
            this.grpCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
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
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(252, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📝 TẠO ĐƠN BÁN THUỐC";
            // 
            // grpCustomer
            // 
            this.grpCustomer.Controls.Add(this.slkPatient);
            this.grpCustomer.Controls.Add(this.lblPatient);
            this.grpCustomer.Controls.Add(this.txtCustomerName);
            this.grpCustomer.Controls.Add(this.lblCustomer);
            this.grpCustomer.Controls.Add(this.chkWalkIn);
            this.grpCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCustomer.Location = new System.Drawing.Point(0, 50);
            this.grpCustomer.Name = "grpCustomer";
            this.grpCustomer.Size = new System.Drawing.Size(1000, 75);
            this.grpCustomer.TabIndex = 1;
            this.grpCustomer.Text = "Thông tin khách hàng";
            // 
            // slkPatient
            // 
            this.slkPatient.Location = new System.Drawing.Point(520, 38);
            this.slkPatient.Name = "slkPatient";
            this.slkPatient.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.slkPatient.Properties.Appearance.Options.UseFont = true;
            this.slkPatient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slkPatient.Properties.NullText = "Chọn bệnh nhân...";
            this.slkPatient.Properties.PopupView = this.searchLookUpEdit1View;
            this.slkPatient.Size = new System.Drawing.Size(250, 24);
            this.slkPatient.TabIndex = 4;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // lblPatient
            // 
            this.lblPatient.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPatient.Appearance.Options.UseFont = true;
            this.lblPatient.Location = new System.Drawing.Point(420, 41);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(91, 19);
            this.lblPatient.TabIndex = 3;
            this.lblPatient.Text = "Hoặc chọn BN:";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Enabled = false;
            this.txtCustomerName.Location = new System.Drawing.Point(235, 38);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCustomerName.Properties.Appearance.Options.UseFont = true;
            this.txtCustomerName.Size = new System.Drawing.Size(170, 24);
            this.txtCustomerName.TabIndex = 2;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCustomer.Appearance.Options.UseFont = true;
            this.lblCustomer.Location = new System.Drawing.Point(165, 41);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(64, 19);
            this.lblCustomer.TabIndex = 1;
            this.lblCustomer.Text = "Tên khách:";
            // 
            // chkWalkIn
            // 
            this.chkWalkIn.Location = new System.Drawing.Point(12, 38);
            this.chkWalkIn.Name = "chkWalkIn";
            this.chkWalkIn.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkWalkIn.Properties.Appearance.Options.UseFont = true;
            this.chkWalkIn.Properties.Caption = "Khách vãng lai";
            this.chkWalkIn.Size = new System.Drawing.Size(130, 22);
            this.chkWalkIn.TabIndex = 0;
            this.chkWalkIn.CheckedChanged += new System.EventHandler(this.ChkWalkIn_CheckedChanged);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 125);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.grpMedicines);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.grpCart);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1000, 475);
            this.splitContainerControl1.SplitterPosition = 500;
            this.splitContainerControl1.TabIndex = 2;
            // 
            // grpMedicines
            // 
            this.grpMedicines.Controls.Add(this.btnAdd);
            this.grpMedicines.Controls.Add(this.spnQuantity);
            this.grpMedicines.Controls.Add(this.labelControl1);
            this.grpMedicines.Controls.Add(this.gridMedicines);
            this.grpMedicines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMedicines.Location = new System.Drawing.Point(0, 0);
            this.grpMedicines.Name = "grpMedicines";
            this.grpMedicines.Size = new System.Drawing.Size(500, 475);
            this.grpMedicines.TabIndex = 0;
            this.grpMedicines.Text = "Danh sách thuốc";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Appearance.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Appearance.Options.UseBackColor = true;
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(120, 435);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 32);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Thêm vào đơn";
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // spnQuantity
            // 
            this.spnQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.spnQuantity.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnQuantity.Location = new System.Drawing.Point(40, 440);
            this.spnQuantity.Name = "spnQuantity";
            this.spnQuantity.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.spnQuantity.Properties.Appearance.Options.UseFont = true;
            this.spnQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnQuantity.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spnQuantity.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnQuantity.Size = new System.Drawing.Size(60, 24);
            this.spnQuantity.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 443);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(20, 19);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "SL:";
            // 
            // gridMedicines
            // 
            this.gridMedicines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridMedicines.Location = new System.Drawing.Point(2, 23);
            this.gridMedicines.MainView = this.gridViewMedicines;
            this.gridMedicines.Name = "gridMedicines";
            this.gridMedicines.Size = new System.Drawing.Size(496, 400);
            this.gridMedicines.TabIndex = 0;
            this.gridMedicines.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMedicines});
            // 
            // gridViewMedicines
            // 
            this.gridViewMedicines.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMedId,
            this.colMedName,
            this.colMedPrice,
            this.colMedStock});
            this.gridViewMedicines.GridControl = this.gridMedicines;
            this.gridViewMedicines.Name = "gridViewMedicines";
            this.gridViewMedicines.OptionsBehavior.Editable = false;
            this.gridViewMedicines.OptionsFind.AlwaysVisible = true;
            this.gridViewMedicines.OptionsView.ShowGroupPanel = false;
            this.gridViewMedicines.OptionsView.ShowIndicator = false;
            // 
            // colMedId
            // 
            this.colMedId.FieldName = "MedicineId";
            this.colMedId.Name = "colMedId";
            // 
            // colMedName
            // 
            this.colMedName.Caption = "Tên thuốc";
            this.colMedName.FieldName = "MedicineName";
            this.colMedName.Name = "colMedName";
            this.colMedName.Visible = true;
            this.colMedName.VisibleIndex = 0;
            this.colMedName.Width = 200;
            // 
            // colMedPrice
            // 
            this.colMedPrice.Caption = "Giá";
            this.colMedPrice.DisplayFormat.FormatString = "N0";
            this.colMedPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMedPrice.FieldName = "Price";
            this.colMedPrice.Name = "colMedPrice";
            this.colMedPrice.Visible = true;
            this.colMedPrice.VisibleIndex = 1;
            this.colMedPrice.Width = 100;
            // 
            // colMedStock
            // 
            this.colMedStock.Caption = "Tồn";
            this.colMedStock.FieldName = "StockQuantity";
            this.colMedStock.Name = "colMedStock";
            this.colMedStock.Visible = true;
            this.colMedStock.VisibleIndex = 2;
            this.colMedStock.Width = 60;
            // 
            // grpCart
            // 
            this.grpCart.Controls.Add(this.lblTotal);
            this.grpCart.Controls.Add(this.btnRemove);
            this.grpCart.Controls.Add(this.gridCart);
            this.grpCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCart.Location = new System.Drawing.Point(0, 0);
            this.grpCart.Name = "grpCart";
            this.grpCart.Size = new System.Drawing.Size(490, 475);
            this.grpCart.TabIndex = 0;
            this.grpCart.Text = "Giỏ hàng";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblTotal.Appearance.Options.UseFont = true;
            this.lblTotal.Appearance.Options.UseForeColor = true;
            this.lblTotal.Location = new System.Drawing.Point(120, 440);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(117, 21);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "TỔNG: 0 VNĐ";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnRemove.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRemove.Appearance.Options.UseBackColor = true;
            this.btnRemove.Appearance.Options.UseFont = true;
            this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemove.Location = new System.Drawing.Point(12, 435);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(80, 32);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "❌ Xóa";
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // gridCart
            // 
            this.gridCart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridCart.Location = new System.Drawing.Point(2, 23);
            this.gridCart.MainView = this.gridViewCart;
            this.gridCart.Name = "gridCart";
            this.gridCart.Size = new System.Drawing.Size(486, 400);
            this.gridCart.TabIndex = 0;
            this.gridCart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCart});
            // 
            // gridViewCart
            // 
            this.gridViewCart.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCartMedId,
            this.colCartName,
            this.colCartQty,
            this.colCartPrice,
            this.colCartTotal});
            this.gridViewCart.GridControl = this.gridCart;
            this.gridViewCart.Name = "gridViewCart";
            this.gridViewCart.OptionsBehavior.Editable = false;
            this.gridViewCart.OptionsView.ShowGroupPanel = false;
            this.gridViewCart.OptionsView.ShowIndicator = false;
            // 
            // colCartMedId
            // 
            this.colCartMedId.FieldName = "MedicineId";
            this.colCartMedId.Name = "colCartMedId";
            // 
            // colCartName
            // 
            this.colCartName.Caption = "Tên thuốc";
            this.colCartName.FieldName = "MedicineName";
            this.colCartName.Name = "colCartName";
            this.colCartName.Visible = true;
            this.colCartName.VisibleIndex = 0;
            this.colCartName.Width = 150;
            // 
            // colCartQty
            // 
            this.colCartQty.Caption = "SL";
            this.colCartQty.FieldName = "Quantity";
            this.colCartQty.Name = "colCartQty";
            this.colCartQty.Visible = true;
            this.colCartQty.VisibleIndex = 1;
            this.colCartQty.Width = 50;
            // 
            // colCartPrice
            // 
            this.colCartPrice.Caption = "Giá";
            this.colCartPrice.DisplayFormat.FormatString = "N0";
            this.colCartPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCartPrice.FieldName = "UnitPrice";
            this.colCartPrice.Name = "colCartPrice";
            this.colCartPrice.Visible = true;
            this.colCartPrice.VisibleIndex = 2;
            this.colCartPrice.Width = 80;
            // 
            // colCartTotal
            // 
            this.colCartTotal.Caption = "Thành tiền";
            this.colCartTotal.DisplayFormat.FormatString = "N0";
            this.colCartTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCartTotal.FieldName = "LineTotal";
            this.colCartTotal.Name = "colCartTotal";
            this.colCartTotal.Visible = true;
            this.colCartTotal.VisibleIndex = 3;
            this.colCartTotal.Width = 100;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnCreate);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 600);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1000, 60);
            this.pnlBottom.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(888, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 42);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCreate.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCreate.Appearance.Options.UseBackColor = true;
            this.btnCreate.Appearance.Options.UseFont = true;
            this.btnCreate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreate.Location = new System.Drawing.Point(740, 10);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(140, 42);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Tạo đơn";
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // DirectSaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 660);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.grpCustomer);
            this.Controls.Add(this.pnlHeader);
            this.Name = "DirectSaleForm";
            this.Text = "Tạo đơn bán thuốc";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpCustomer)).EndInit();
            this.grpCustomer.ResumeLayout(false);
            this.grpCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkPatient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWalkIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpMedicines)).EndInit();
            this.grpMedicines.ResumeLayout(false);
            this.grpMedicines.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMedicines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMedicines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCart)).EndInit();
            this.grpCart.ResumeLayout(false);
            this.grpCart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.GroupControl grpCustomer;
        private DevExpress.XtraEditors.CheckEdit chkWalkIn;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private DevExpress.XtraEditors.LabelControl lblPatient;
        private DevExpress.XtraEditors.SearchLookUpEdit slkPatient;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl grpMedicines;
        private DevExpress.XtraGrid.GridControl gridMedicines;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMedicines;
        private DevExpress.XtraGrid.Columns.GridColumn colMedId;
        private DevExpress.XtraGrid.Columns.GridColumn colMedName;
        private DevExpress.XtraGrid.Columns.GridColumn colMedPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colMedStock;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit spnQuantity;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.GroupControl grpCart;
        private DevExpress.XtraGrid.GridControl gridCart;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCart;
        private DevExpress.XtraGrid.Columns.GridColumn colCartMedId;
        private DevExpress.XtraGrid.Columns.GridColumn colCartName;
        private DevExpress.XtraGrid.Columns.GridColumn colCartQty;
        private DevExpress.XtraGrid.Columns.GridColumn colCartPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colCartTotal;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.LabelControl lblTotal;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}
