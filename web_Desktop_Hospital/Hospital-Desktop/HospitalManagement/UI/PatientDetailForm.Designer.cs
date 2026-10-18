namespace HospitalManagement.UI
{
    partial class PatientDetailForm
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
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.txtFullName = new DevExpress.XtraEditors.TextEdit();
            this.dtpDateOfBirth = new DevExpress.XtraEditors.DateEdit();
            this.cboGender = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtPhone = new DevExpress.XtraEditors.TextEdit();
            this.txtCCCD = new DevExpress.XtraEditors.TextEdit();
            this.txtAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtMedicalHistory = new DevExpress.XtraEditors.MemoEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDOB = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGender = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemPhone = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCCCD = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemHistory = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateOfBirth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateOfBirth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCCD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMedicalHistory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDOB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCCCD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.lblTitle);
            this.layoutControl.Controls.Add(this.txtFullName);
            this.layoutControl.Controls.Add(this.dtpDateOfBirth);
            this.layoutControl.Controls.Add(this.cboGender);
            this.layoutControl.Controls.Add(this.txtPhone);
            this.layoutControl.Controls.Add(this.txtCCCD);
            this.layoutControl.Controls.Add(this.txtAddress);
            this.layoutControl.Controls.Add(this.txtMedicalHistory);
            this.layoutControl.Controls.Add(this.btnSave);
            this.layoutControl.Controls.Add(this.btnCancel);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(500, 480);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(90, 80);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(398, 20);
            this.txtFullName.StyleController = this.layoutControl;
            this.txtFullName.TabIndex = 2;
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.EditValue = null;
            this.dtpDateOfBirth.Location = new System.Drawing.Point(90, 104);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDateOfBirth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDateOfBirth.Size = new System.Drawing.Size(158, 20);
            this.dtpDateOfBirth.StyleController = this.layoutControl;
            this.dtpDateOfBirth.TabIndex = 3;
            // 
            // cboGender
            // 
            this.cboGender.Location = new System.Drawing.Point(320, 104);
            this.cboGender.Name = "cboGender";
            this.cboGender.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGender.Properties.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            this.cboGender.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboGender.Size = new System.Drawing.Size(168, 20);
            this.cboGender.StyleController = this.layoutControl;
            this.cboGender.TabIndex = 4;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(90, 128);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(158, 20);
            this.txtPhone.StyleController = this.layoutControl;
            this.txtPhone.TabIndex = 5;
            // 
            // txtCCCD
            // 
            this.txtCCCD.Location = new System.Drawing.Point(320, 128);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(168, 20);
            this.txtCCCD.StyleController = this.layoutControl;
            this.txtCCCD.TabIndex = 6;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(90, 152);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(398, 20);
            this.txtAddress.StyleController = this.layoutControl;
            this.txtAddress.TabIndex = 7;
            // 
            // txtMedicalHistory
            // 
            this.txtMedicalHistory.Location = new System.Drawing.Point(90, 176);
            this.txtMedicalHistory.Name = "txtMedicalHistory";
            this.txtMedicalHistory.Size = new System.Drawing.Size(398, 80);
            this.txtMedicalHistory.StyleController = this.layoutControl;
            this.txtMedicalHistory.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(260, 428);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 40);
            this.btnSave.StyleController = this.layoutControl;
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(372, 428);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 40);
            this.btnCancel.StyleController = this.layoutControl;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(248, 30);
            this.lblTitle.StyleController = this.layoutControl;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thông tin Bệnh nhân";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTitle,
            this.groupInfo,
            this.layoutControlItemSave,
            this.layoutControlItemCancel,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(500, 480);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemTitle
            // 
            this.layoutControlItemTitle.Control = this.lblTitle;
            this.layoutControlItemTitle.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemTitle.Name = "layoutControlItemTitle";
            this.layoutControlItemTitle.Size = new System.Drawing.Size(480, 34);
            this.layoutControlItemTitle.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTitle.TextVisible = false;
            // 
            // groupInfo
            // 
            this.groupInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemName,
            this.layoutControlItemDOB,
            this.layoutControlItemGender,
            this.layoutControlItemAddress,
            this.layoutControlItemPhone,
            this.layoutControlItemCCCD,
            this.layoutControlItemHistory});
            this.groupInfo.Location = new System.Drawing.Point(0, 34);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.Size = new System.Drawing.Size(480, 260);
            this.groupInfo.Text = "Thông tin chi tiết";
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.txtFullName;
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(456, 24);
            this.layoutControlItemName.Text = "Họ tên *";
            this.layoutControlItemName.TextSize = new System.Drawing.Size(42, 13);
            // 
            // layoutControlItemDOB
            // 
            this.layoutControlItemDOB.Control = this.dtpDateOfBirth;
            this.layoutControlItemDOB.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemDOB.Name = "layoutControlItemDOB";
            this.layoutControlItemDOB.Size = new System.Drawing.Size(230, 24);
            this.layoutControlItemDOB.Text = "Ngày sinh";
            this.layoutControlItemDOB.TextSize = new System.Drawing.Size(42, 13);
            // 
            // layoutControlItemGender
            // 
            this.layoutControlItemGender.Control = this.cboGender;
            this.layoutControlItemGender.Location = new System.Drawing.Point(230, 24);
            this.layoutControlItemGender.Name = "layoutControlItemGender";
            this.layoutControlItemGender.Size = new System.Drawing.Size(226, 24);
            this.layoutControlItemGender.Text = "Giới tính";
            this.layoutControlItemGender.TextSize = new System.Drawing.Size(42, 13);
            // 
            // layoutControlItemAddress
            // 
            this.layoutControlItemAddress.Control = this.txtAddress;
            this.layoutControlItemAddress.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemAddress.Name = "layoutControlItemAddress";
            this.layoutControlItemAddress.Size = new System.Drawing.Size(456, 24);
            this.layoutControlItemAddress.Text = "Địa chỉ";
            this.layoutControlItemAddress.TextSize = new System.Drawing.Size(42, 13);
            // 
            // layoutControlItemPhone
            // 
            this.layoutControlItemPhone.Control = this.txtPhone;
            this.layoutControlItemPhone.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemPhone.Name = "layoutControlItemPhone";
            this.layoutControlItemPhone.Size = new System.Drawing.Size(230, 24);
            this.layoutControlItemPhone.Text = "SĐT";
            this.layoutControlItemPhone.TextSize = new System.Drawing.Size(42, 13);
            // 
            // layoutControlItemCCCD
            // 
            this.layoutControlItemCCCD.Control = this.txtCCCD;
            this.layoutControlItemCCCD.Location = new System.Drawing.Point(230, 48);
            this.layoutControlItemCCCD.Name = "layoutControlItemCCCD";
            this.layoutControlItemCCCD.Size = new System.Drawing.Size(226, 24);
            this.layoutControlItemCCCD.Text = "CCCD";
            this.layoutControlItemCCCD.TextSize = new System.Drawing.Size(42, 13);
            // 
            // layoutControlItemHistory
            // 
            this.layoutControlItemHistory.Control = this.txtMedicalHistory;
            this.layoutControlItemHistory.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemHistory.Name = "layoutControlItemHistory";
            this.layoutControlItemHistory.Size = new System.Drawing.Size(456, 119);
            this.layoutControlItemHistory.Text = "Tiền sử";
            this.layoutControlItemHistory.TextSize = new System.Drawing.Size(42, 13);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(248, 416);
            this.layoutControlItemSave.MinSize = new System.Drawing.Size(80, 26);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(112, 44);
            this.layoutControlItemSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemCancel
            // 
            this.layoutControlItemCancel.Control = this.btnCancel;
            this.layoutControlItemCancel.Location = new System.Drawing.Point(360, 416);
            this.layoutControlItemCancel.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItemCancel.Name = "layoutControlItemCancel";
            this.layoutControlItemCancel.Size = new System.Drawing.Size(120, 44);
            this.layoutControlItemCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemCancel.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 294);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(480, 122);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // PatientDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 480);
            this.Controls.Add(this.layoutControl);
            this.Name = "PatientDetailForm";
            this.Text = "Thông tin Bệnh nhân";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFullName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateOfBirth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDateOfBirth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCCD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMedicalHistory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDOB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCCCD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.TextEdit txtFullName;
        private DevExpress.XtraEditors.DateEdit dtpDateOfBirth;
        private DevExpress.XtraEditors.ComboBoxEdit cboGender;
        private DevExpress.XtraEditors.TextEdit txtPhone;
        private DevExpress.XtraEditors.TextEdit txtCCCD;
        private DevExpress.XtraEditors.TextEdit txtAddress;
        private DevExpress.XtraEditors.MemoEdit txtMedicalHistory;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTitle;
        private DevExpress.XtraLayout.LayoutControlGroup groupInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDOB;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGender;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAddress;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPhone;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCCCD;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemHistory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
