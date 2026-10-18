namespace HospitalManagement.UI
{
    partial class ForceChangePasswordForm
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
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.txtNewPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtConfirmPassword = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.rootGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.itemHeader = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemNewPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemConfirmPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.itemBtnSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemNewPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemConfirmPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBtnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.txtNewPassword);
            this.layoutControl.Controls.Add(this.txtConfirmPassword);
            this.layoutControl.Controls.Add(this.btnSave);
            this.layoutControl.Controls.Add(this.lblHeader);
            this.layoutControl.Controls.Add(this.lblInfo);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.rootGroup;
            this.layoutControl.Size = new System.Drawing.Size(400, 300);
            this.layoutControl.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.Appearance.Options.UseForeColor = true;
            this.lblHeader.Location = new System.Drawing.Point(22, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(356, 30);
            this.lblHeader.StyleController = this.layoutControl;
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "🔐 ĐỔI MẬT KHẨU BẮT BUỘC";
            // 
            // lblInfo
            // 
            this.lblInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInfo.Appearance.Options.UseFont = true;
            this.lblInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblInfo.Location = new System.Drawing.Point(22, 56);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(356, 38);
            this.lblInfo.StyleController = this.layoutControl;
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "Bạn cần đổi mật khẩu trước khi tiếp tục sử dụng hệ thống.\r\nMật khẩu phải có ít nhất 8 ký tự, gồm chữ hoa, chữ thường, số và ký tự đặc biệt.";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(22, 127);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Properties.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(356, 20);
            this.txtNewPassword.StyleController = this.layoutControl;
            this.txtNewPassword.TabIndex = 6;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(22, 180);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Properties.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(356, 20);
            this.txtConfirmPassword.StyleController = this.layoutControl;
            this.txtConfirmPassword.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(22, 234);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(356, 44);
            this.btnSave.StyleController = this.layoutControl;
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "💾 Đổi Mật Khẩu";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // rootGroup
            // 
            this.rootGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.rootGroup.GroupBordersVisible = false;
            this.rootGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.itemHeader,
            this.itemInfo,
            this.itemNewPassword,
            this.itemConfirmPassword,
            this.emptySpaceItem1,
            this.itemBtnSave});
            this.rootGroup.Name = "Root";
            this.rootGroup.Size = new System.Drawing.Size(400, 300);
            this.rootGroup.TextVisible = false;
            // 
            // itemHeader
            // 
            this.itemHeader.Control = this.lblHeader;
            this.itemHeader.Location = new System.Drawing.Point(0, 0);
            this.itemHeader.Name = "itemHeader";
            this.itemHeader.Size = new System.Drawing.Size(360, 34);
            this.itemHeader.TextSize = new System.Drawing.Size(0, 0);
            this.itemHeader.TextVisible = false;
            // 
            // itemInfo
            // 
            this.itemInfo.Control = this.lblInfo;
            this.itemInfo.Location = new System.Drawing.Point(0, 34);
            this.itemInfo.Name = "itemInfo";
            this.itemInfo.Size = new System.Drawing.Size(360, 42);
            this.itemInfo.TextSize = new System.Drawing.Size(0, 0);
            this.itemInfo.TextVisible = false;
            // 
            // itemNewPassword
            // 
            this.itemNewPassword.Control = this.txtNewPassword;
            this.itemNewPassword.Location = new System.Drawing.Point(0, 86);
            this.itemNewPassword.Name = "itemNewPassword";
            this.itemNewPassword.Size = new System.Drawing.Size(360, 43);
            this.itemNewPassword.Text = "Mật khẩu mới";
            this.itemNewPassword.TextLocation = DevExpress.Utils.Locations.Top;
            this.itemNewPassword.TextSize = new System.Drawing.Size(89, 13);
            // 
            // itemConfirmPassword
            // 
            this.itemConfirmPassword.Control = this.txtConfirmPassword;
            this.itemConfirmPassword.Location = new System.Drawing.Point(0, 129);
            this.itemConfirmPassword.Name = "itemConfirmPassword";
            this.itemConfirmPassword.Size = new System.Drawing.Size(360, 43);
            this.itemConfirmPassword.Text = "Xác nhận mật khẩu";
            this.itemConfirmPassword.TextLocation = DevExpress.Utils.Locations.Top;
            this.itemConfirmPassword.TextSize = new System.Drawing.Size(89, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 172);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(360, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // itemBtnSave
            // 
            this.itemBtnSave.Control = this.btnSave;
            this.itemBtnSave.Location = new System.Drawing.Point(0, 212);
            this.itemBtnSave.Name = "itemBtnSave";
            this.itemBtnSave.Size = new System.Drawing.Size(360, 48);
            this.itemBtnSave.TextSize = new System.Drawing.Size(0, 0);
            this.itemBtnSave.TextVisible = false;
            // 
            // ForceChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.ControlBox = false;
            this.Controls.Add(this.layoutControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForceChangePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đổi Mật Khẩu Bắt Buộc";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemNewPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemConfirmPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBtnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup rootGroup;
        private DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.TextEdit txtNewPassword;
        private DevExpress.XtraEditors.TextEdit txtConfirmPassword;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem itemHeader;
        private DevExpress.XtraLayout.LayoutControlItem itemInfo;
        private DevExpress.XtraLayout.LayoutControlItem itemNewPassword;
        private DevExpress.XtraLayout.LayoutControlItem itemConfirmPassword;
        private DevExpress.XtraLayout.LayoutControlItem itemBtnSave;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
