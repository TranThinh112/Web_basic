namespace HospitalManagement.UI
{
    partial class MedicineDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.lblUnit = new DevExpress.XtraEditors.LabelControl();
            this.cboUnit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblPrice = new DevExpress.XtraEditors.LabelControl();
            this.txtPrice = new DevExpress.XtraEditors.TextEdit();
            this.lblStock = new DevExpress.XtraEditors.LabelControl();
            this.spnStock = new DevExpress.XtraEditors.SpinEdit();
            this.lblMinStock = new DevExpress.XtraEditors.LabelControl();
            this.spnMinStock = new DevExpress.XtraEditors.SpinEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinStock.Properties)).BeginInit();
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
            this.pnlHeader.Size = new System.Drawing.Size(400, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(130, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "💊 Thêm thuốc mới";
            // 
            // lblName
            // 
            this.lblName.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.Location = new System.Drawing.Point(20, 70);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(66, 19);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Tên thuốc:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 68);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.Size = new System.Drawing.Size(250, 26);
            this.txtName.TabIndex = 2;
            // 
            // lblUnit
            // 
            this.lblUnit.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUnit.Location = new System.Drawing.Point(20, 110);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(46, 19);
            this.lblUnit.TabIndex = 3;
            this.lblUnit.Text = "Đơn vị:";
            // 
            // cboUnit
            // 
            this.cboUnit.Location = new System.Drawing.Point(120, 108);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboUnit.Properties.Appearance.Options.UseFont = true;
            this.cboUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUnit.Properties.Items.AddRange(new object[] {
            "Viên",
            "Gói",
            "Chai",
            "Ống",
            "Hộp",
            "Vỉ"});
            this.cboUnit.Size = new System.Drawing.Size(120, 26);
            this.cboUnit.TabIndex = 4;
            // 
            // lblPrice
            // 
            this.lblPrice.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPrice.Location = new System.Drawing.Point(20, 150);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(54, 19);
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "Đơn giá:";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(120, 148);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPrice.Properties.Appearance.Options.UseFont = true;
            this.txtPrice.Properties.Mask.EditMask = "n0";
            this.txtPrice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPrice.Size = new System.Drawing.Size(150, 26);
            this.txtPrice.TabIndex = 6;
            // 
            // lblStock
            // 
            this.lblStock.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStock.Location = new System.Drawing.Point(20, 190);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(60, 19);
            this.lblStock.TabIndex = 7;
            this.lblStock.Text = "Số lượng:";
            // 
            // spnStock
            // 
            this.spnStock.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnStock.Location = new System.Drawing.Point(120, 188);
            this.spnStock.Name = "spnStock";
            this.spnStock.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.spnStock.Properties.Appearance.Options.UseFont = true;
            this.spnStock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnStock.Properties.IsFloatValue = false;
            this.spnStock.Properties.Mask.EditMask = "N0";
            this.spnStock.Size = new System.Drawing.Size(100, 26);
            this.spnStock.TabIndex = 8;
            // 
            // lblMinStock
            // 
            this.lblMinStock.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMinStock.Location = new System.Drawing.Point(20, 230);
            this.lblMinStock.Name = "lblMinStock";
            this.lblMinStock.Size = new System.Drawing.Size(91, 19);
            this.lblMinStock.TabIndex = 9;
            this.lblMinStock.Text = "Mức tối thiểu:";
            // 
            // spnMinStock
            // 
            this.spnMinStock.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spnMinStock.Location = new System.Drawing.Point(120, 228);
            this.spnMinStock.Name = "spnMinStock";
            this.spnMinStock.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.spnMinStock.Properties.Appearance.Options.UseFont = true;
            this.spnMinStock.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnMinStock.Properties.IsFloatValue = false;
            this.spnMinStock.Properties.Mask.EditMask = "N0";
            this.spnMinStock.Size = new System.Drawing.Size(100, 26);
            this.spnMinStock.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(120, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "💾 Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.Location = new System.Drawing.Point(220, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MedicineDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 340);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.spnMinStock);
            this.Controls.Add(this.lblMinStock);
            this.Controls.Add(this.spnStock);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.cboUnit);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MedicineDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm thuốc mới";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMinStock.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl lblUnit;
        private DevExpress.XtraEditors.ComboBoxEdit cboUnit;
        private DevExpress.XtraEditors.LabelControl lblPrice;
        private DevExpress.XtraEditors.TextEdit txtPrice;
        private DevExpress.XtraEditors.LabelControl lblStock;
        private DevExpress.XtraEditors.SpinEdit spnStock;
        private DevExpress.XtraEditors.LabelControl lblMinStock;
        private DevExpress.XtraEditors.SpinEdit spnMinStock;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}
