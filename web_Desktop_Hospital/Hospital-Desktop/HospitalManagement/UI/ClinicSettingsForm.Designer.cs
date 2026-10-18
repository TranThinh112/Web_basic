namespace HospitalManagement.UI
{
    partial class ClinicSettingsForm
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
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblOpeningTime = new DevExpress.XtraEditors.LabelControl();
            this.timeOpening = new DevExpress.XtraEditors.TimeEdit();
            this.lblClosingTime = new DevExpress.XtraEditors.LabelControl();
            this.timeClosing = new DevExpress.XtraEditors.TimeEdit();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.lblFee = new DevExpress.XtraEditors.LabelControl();
            this.txtFee = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeOpening.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeClosing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFee.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.pnlHeader.Appearance.Options.UseBackColor = true;
            this.pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(350, 45);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(15, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(235, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⚙️ CÀI ĐẶT PHÒNG KHÁM";
            // 
            // lblOpeningTime
            // 
            this.lblOpeningTime.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOpeningTime.Appearance.Options.UseFont = true;
            this.lblOpeningTime.Location = new System.Drawing.Point(25, 60);
            this.lblOpeningTime.Name = "lblOpeningTime";
            this.lblOpeningTime.Size = new System.Drawing.Size(73, 19);
            this.lblOpeningTime.TabIndex = 1;
            this.lblOpeningTime.Text = "Giờ mở cửa:";
            // 
            // timeOpening
            // 
            this.timeOpening.EditValue = null;
            this.timeOpening.Location = new System.Drawing.Point(135, 57);
            this.timeOpening.Name = "timeOpening";
            this.timeOpening.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.timeOpening.Properties.Appearance.Options.UseFont = true;
            this.timeOpening.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeOpening.Properties.MaskSettings.Set("mask", "t");
            this.timeOpening.Size = new System.Drawing.Size(130, 24);
            this.timeOpening.TabIndex = 2;
            // 
            // lblClosingTime
            // 
            this.lblClosingTime.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblClosingTime.Appearance.Options.UseFont = true;
            this.lblClosingTime.Location = new System.Drawing.Point(25, 95);
            this.lblClosingTime.Name = "lblClosingTime";
            this.lblClosingTime.Size = new System.Drawing.Size(85, 19);
            this.lblClosingTime.TabIndex = 3;
            this.lblClosingTime.Text = "Giờ đóng cửa:";
            // 
            // timeClosing
            // 
            this.timeClosing.EditValue = null;
            this.timeClosing.Location = new System.Drawing.Point(135, 92);
            this.timeClosing.Name = "timeClosing";
            this.timeClosing.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.timeClosing.Properties.Appearance.Options.UseFont = true;
            this.timeClosing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeClosing.Properties.MaskSettings.Set("mask", "t");
            this.timeClosing.Size = new System.Drawing.Size(130, 24);
            this.timeClosing.TabIndex = 4;
            // 
            // lblInfo
            // 
            this.lblInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblInfo.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Appearance.Options.UseFont = true;
            this.lblInfo.Appearance.Options.UseForeColor = true;
            this.lblInfo.Location = new System.Drawing.Point(25, 125);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(262, 15);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "Lễ tân chỉ có thể đặt lịch hẹn trong khung giờ này";
            // 
            // lblFee
            // 
            this.lblFee.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFee.Appearance.Options.UseFont = true;
            this.lblFee.Location = new System.Drawing.Point(25, 155);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(55, 17);
            this.lblFee.TabIndex = 6;
            this.lblFee.Text = "Phí khám:";
            // 
            // txtFee
            // 
            this.txtFee.Location = new System.Drawing.Point(135, 152);
            this.txtFee.Name = "txtFee";
            this.txtFee.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFee.Properties.Appearance.Options.UseFont = true;
            this.txtFee.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtFee.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtFee.Properties.MaskSettings.Set("mask", "N0");
            this.txtFee.Properties.UseMaskAsDisplayFormat = true;
            this.txtFee.Size = new System.Drawing.Size(130, 24);
            this.txtFee.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(70, 195);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 40);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "💾 Lưu";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.Color.Gray;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(185, 195);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 40);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // ClinicSettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(350, 260);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtFee);
            this.Controls.Add(this.lblFee);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.timeClosing);
            this.Controls.Add(this.lblClosingTime);
            this.Controls.Add(this.timeOpening);
            this.Controls.Add(this.lblOpeningTime);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ClinicSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cài đặt Phòng khám";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeOpening.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeClosing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFee.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblOpeningTime;
        private DevExpress.XtraEditors.TimeEdit timeOpening;
        private DevExpress.XtraEditors.LabelControl lblClosingTime;
        private DevExpress.XtraEditors.TimeEdit timeClosing;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.LabelControl lblFee;
        private DevExpress.XtraEditors.TextEdit txtFee;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}
