namespace HospitalManagement.UI
{
    partial class LoginForm
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
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.lblTagline = new DevExpress.XtraEditors.LabelControl();
            this.lblAppName = new DevExpress.XtraEditors.LabelControl();
            this.lblIcon = new DevExpress.XtraEditors.LabelControl();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblSubtitle = new DevExpress.XtraEditors.LabelControl();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.lblMessage = new DevExpress.XtraEditors.LabelControl();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSubtitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemUsername = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemLogin = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemMessage = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemInfo = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemTop = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItemBottom = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSubtitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUsername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBottom)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.pnlLeft.Appearance.Options.UseBackColor = true;
            this.pnlLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlLeft.Controls.Add(this.lblTagline);
            this.pnlLeft.Controls.Add(this.lblAppName);
            this.pnlLeft.Controls.Add(this.lblIcon);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(350, 500);
            this.pnlLeft.TabIndex = 0;
            // 
            // lblTagline
            // 
            this.lblTagline.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTagline.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblTagline.Appearance.Options.UseFont = true;
            this.lblTagline.Appearance.Options.UseForeColor = true;
            this.lblTagline.Location = new System.Drawing.Point(60, 280);
            this.lblTagline.Name = "lblTagline";
            this.lblTagline.Size = new System.Drawing.Size(196, 38);
            this.lblTagline.TabIndex = 2;
            this.lblTagline.Text = "Hệ thống quản lý phòng khám\r\nchuyên nghiệp và hiện đại";
            // 
            // lblAppName
            // 
            this.lblAppName.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 26F);
            this.lblAppName.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Appearance.Options.UseFont = true;
            this.lblAppName.Appearance.Options.UseForeColor = true;
            this.lblAppName.Location = new System.Drawing.Point(80, 180);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(194, 94);
            this.lblAppName.TabIndex = 1;
            this.lblAppName.Text = "Hospital\r\nManagement";
            // 
            // lblIcon
            // 
            this.lblIcon.Appearance.Font = new System.Drawing.Font("Segoe UI", 50F);
            this.lblIcon.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblIcon.Appearance.Options.UseFont = true;
            this.lblIcon.Appearance.Options.UseForeColor = true;
            this.lblIcon.Location = new System.Drawing.Point(130, 90);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(90, 89);
            this.lblIcon.TabIndex = 0;
            this.lblIcon.Text = "🏥";
            // 
            // layoutControl
            // 
            this.layoutControl.BackColor = System.Drawing.Color.White;
            this.layoutControl.Controls.Add(this.btnClose);
            this.layoutControl.Controls.Add(this.lblTitle);
            this.layoutControl.Controls.Add(this.lblSubtitle);
            this.layoutControl.Controls.Add(this.txtUsername);
            this.layoutControl.Controls.Add(this.txtPassword);
            this.layoutControl.Controls.Add(this.btnLogin);
            this.layoutControl.Controls.Add(this.lblMessage);
            this.layoutControl.Controls.Add(this.lblInfo);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(350, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1008, 234, 650, 400);
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(500, 500);
            this.layoutControl.TabIndex = 1;
            this.layoutControl.Text = "layoutControl1";
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(458, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 28);
            this.btnClose.StyleController = this.layoutControl;
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "✕";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 94);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblTitle.Size = new System.Drawing.Size(476, 55);
            this.lblTitle.StyleController = this.layoutControl;
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Đăng nhập";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Appearance.Options.UseFont = true;
            this.lblSubtitle.Appearance.Options.UseForeColor = true;
            this.lblSubtitle.Location = new System.Drawing.Point(12, 153);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lblSubtitle.Size = new System.Drawing.Size(476, 40);
            this.lblSubtitle.StyleController = this.layoutControl;
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Chào mừng bạn quay trở lại!";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(12, 219);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUsername.Properties.Appearance.Options.UseFont = true;
            this.txtUsername.Properties.AutoHeight = true;
            // this.txtUsername.Size = new System.Drawing.Size(476, 36);
            this.txtUsername.StyleController = this.layoutControl;
            this.txtUsername.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(12, 281);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.AutoHeight = true;
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            // this.txtPassword.Size = new System.Drawing.Size(476, 36);
            this.txtPassword.StyleController = this.layoutControl;
            this.txtPassword.TabIndex = 4;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Appearance.Options.UseBackColor = true;
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Appearance.Options.UseForeColor = true;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Location = new System.Drawing.Point(12, 345);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(476, 40);
            this.btnLogin.StyleController = this.layoutControl;
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "ĐĂNG NHẬP";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMessage.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Appearance.Options.UseFont = true;
            this.lblMessage.Appearance.Options.UseForeColor = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 321);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(476, 20);
            this.lblMessage.StyleController = this.layoutControl;
            this.lblMessage.TabIndex = 6;
            // 
            // lblInfo
            // 
            this.lblInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfo.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Appearance.Options.UseFont = true;
            this.lblInfo.Appearance.Options.UseForeColor = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 409);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(476, 15);
            this.lblInfo.StyleController = this.layoutControl;
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "💡 Tài khoản mặc định: admin / admin123";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemClose,
            this.layoutControlItemTitle,
            this.layoutControlItemSubtitle,
            this.layoutControlItemUsername,
            this.layoutControlItemPassword,
            this.layoutControlItemLogin,
            this.layoutControlItemMessage,
            this.layoutControlItemInfo,
            this.emptySpaceItemTop,
            this.emptySpaceItemBottom});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(500, 500);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemClose
            // 
            this.layoutControlItemClose.Control = this.btnClose;
            this.layoutControlItemClose.Location = new System.Drawing.Point(446, 0);
            this.layoutControlItemClose.Name = "layoutControlItemClose";
            this.layoutControlItemClose.Size = new System.Drawing.Size(34, 32);
            this.layoutControlItemClose.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemClose.TextVisible = false;
            // 
            // layoutControlItemTitle
            // 
            this.layoutControlItemTitle.Control = this.lblTitle;
            this.layoutControlItemTitle.Location = new System.Drawing.Point(0, 82);
            this.layoutControlItemTitle.Name = "layoutControlItemTitle";
            this.layoutControlItemTitle.Size = new System.Drawing.Size(480, 59);
            this.layoutControlItemTitle.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTitle.TextVisible = false;
            // 
            // layoutControlItemSubtitle
            // 
            this.layoutControlItemSubtitle.Control = this.lblSubtitle;
            this.layoutControlItemSubtitle.Location = new System.Drawing.Point(0, 141);
            this.layoutControlItemSubtitle.Name = "layoutControlItemSubtitle";
            this.layoutControlItemSubtitle.Size = new System.Drawing.Size(480, 44);
            this.layoutControlItemSubtitle.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSubtitle.TextVisible = false;
            // 
            // layoutControlItemUsername
            // 
            this.layoutControlItemUsername.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.layoutControlItemUsername.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemUsername.Control = this.txtUsername;
            this.layoutControlItemUsername.Location = new System.Drawing.Point(0, 185);
            this.layoutControlItemUsername.Name = "layoutControlItemUsername";
            this.layoutControlItemUsername.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 20);
            this.layoutControlItemUsername.Size = new System.Drawing.Size(480, 61);
            // this.layoutControlItemUsername.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemUsername.Text = "Tên đăng nhập";
            this.layoutControlItemUsername.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemUsername.TextSize = new System.Drawing.Size(91, 17);
            // 
            // layoutControlItemPassword
            // 
            this.layoutControlItemPassword.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.layoutControlItemPassword.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItemPassword.Control = this.txtPassword;
            this.layoutControlItemPassword.Location = new System.Drawing.Point(0, 246);
            this.layoutControlItemPassword.Name = "layoutControlItemPassword";
            this.layoutControlItemPassword.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 20);
            this.layoutControlItemPassword.Size = new System.Drawing.Size(480, 63);
            // this.layoutControlItemPassword.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemPassword.Text = "Mật khẩu";
            this.layoutControlItemPassword.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemPassword.TextSize = new System.Drawing.Size(91, 17);
            // 
            // layoutControlItemLogin
            // 
            this.layoutControlItemLogin.Control = this.btnLogin;
            this.layoutControlItemLogin.Location = new System.Drawing.Point(0, 333);
            this.layoutControlItemLogin.MinSize = new System.Drawing.Size(78, 26);
            this.layoutControlItemLogin.Name = "layoutControlItemLogin";
            this.layoutControlItemLogin.Size = new System.Drawing.Size(480, 44);
            this.layoutControlItemLogin.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemLogin.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemLogin.TextVisible = false;
            // 
            // layoutControlItemMessage
            // 
            this.layoutControlItemMessage.Control = this.lblMessage;
            this.layoutControlItemMessage.Location = new System.Drawing.Point(0, 309);
            this.layoutControlItemMessage.MinSize = new System.Drawing.Size(1, 17);
            this.layoutControlItemMessage.Name = "layoutControlItemMessage";
            this.layoutControlItemMessage.Size = new System.Drawing.Size(480, 24);
            this.layoutControlItemMessage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemMessage.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemMessage.TextVisible = false;
            // 
            // layoutControlItemInfo
            // 
            this.layoutControlItemInfo.Control = this.lblInfo;
            this.layoutControlItemInfo.Location = new System.Drawing.Point(0, 377);
            this.layoutControlItemInfo.MinSize = new System.Drawing.Size(1, 17);
            this.layoutControlItemInfo.Name = "layoutControlItemInfo";
            this.layoutControlItemInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 20, 2);
            this.layoutControlItemInfo.Size = new System.Drawing.Size(480, 103);
            this.layoutControlItemInfo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemInfo.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemInfo.TextVisible = false;
            // 
            // emptySpaceItemTop
            // 
            this.emptySpaceItemTop.AllowHotTrack = false;
            this.emptySpaceItemTop.Location = new System.Drawing.Point(0, 32);
            this.emptySpaceItemTop.Name = "emptySpaceItemTop";
            this.emptySpaceItemTop.Size = new System.Drawing.Size(480, 50);
            this.emptySpaceItemTop.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItemBottom
            // 
            this.emptySpaceItemBottom.AllowHotTrack = false;
            this.emptySpaceItemBottom.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItemBottom.Name = "emptySpaceItemBottom";
            this.emptySpaceItemBottom.Size = new System.Drawing.Size(446, 32);
            this.emptySpaceItemBottom.TextSize = new System.Drawing.Size(0, 0);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 500);
            this.Controls.Add(this.layoutControl);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - Hospital Management";
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSubtitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUsername)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemBottom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.LabelControl lblTagline;
        private DevExpress.XtraEditors.LabelControl lblAppName;
        private DevExpress.XtraEditors.LabelControl lblIcon;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblSubtitle;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.LabelControl lblMessage;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTitle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSubtitle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemUsername;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLogin;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMessage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemInfo;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemTop;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemBottom;
    }
}
