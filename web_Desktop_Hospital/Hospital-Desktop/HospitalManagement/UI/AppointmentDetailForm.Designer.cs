namespace HospitalManagement.UI
{
    partial class AppointmentDetailForm
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
            this.lblPatient = new DevExpress.XtraEditors.LabelControl();
            this.lkePatient = new DevExpress.XtraEditors.LookUpEdit();
            this.lblDoctor = new DevExpress.XtraEditors.LabelControl();
            this.lkeDoctor = new DevExpress.XtraEditors.LookUpEdit();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            this.dtpDate = new DevExpress.XtraEditors.DateEdit();
            this.lblTime = new DevExpress.XtraEditors.LabelControl();
            this.timeEdit = new DevExpress.XtraEditors.TimeEdit();
            this.lblReason = new DevExpress.XtraEditors.LabelControl();
            this.txtReason = new DevExpress.XtraEditors.MemoEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblClinicHours = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkePatient.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeDoctor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).BeginInit();
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
            this.pnlHeader.Size = new System.Drawing.Size(430, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(152, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📅 Thêm Lịch hẹn mới";
            // 
            // lblPatient
            // 
            this.lblPatient.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPatient.Location = new System.Drawing.Point(30, 70);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(65, 19);
            this.lblPatient.TabIndex = 1;
            this.lblPatient.Text = "Bệnh nhân:";
            // 
            // lkePatient
            // 
            this.lkePatient.Location = new System.Drawing.Point(130, 68);
            this.lkePatient.Name = "lkePatient";
            this.lkePatient.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lkePatient.Properties.Appearance.Options.UseFont = true;
            this.lkePatient.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkePatient.Properties.NullText = "Chọn bệnh nhân...";
            this.lkePatient.Size = new System.Drawing.Size(260, 26);
            this.lkePatient.TabIndex = 2;
            // 
            // lblDoctor
            // 
            this.lblDoctor.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDoctor.Location = new System.Drawing.Point(30, 110);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(39, 19);
            this.lblDoctor.TabIndex = 3;
            this.lblDoctor.Text = "Bác sĩ:";
            // 
            // lkeDoctor
            // 
            this.lkeDoctor.Location = new System.Drawing.Point(130, 108);
            this.lkeDoctor.Name = "lkeDoctor";
            this.lkeDoctor.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lkeDoctor.Properties.Appearance.Options.UseFont = true;
            this.lkeDoctor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkeDoctor.Properties.NullText = "Chọn bác sĩ...";
            this.lkeDoctor.Size = new System.Drawing.Size(260, 26);
            this.lkeDoctor.TabIndex = 4;
            // 
            // lblDate
            // 
            this.lblDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.Location = new System.Drawing.Point(30, 150);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(37, 19);
            this.lblDate.TabIndex = 5;
            this.lblDate.Text = "Ngày:";
            // 
            // dtpDate
            // 
            this.dtpDate.EditValue = null;
            this.dtpDate.Location = new System.Drawing.Point(130, 148);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDate.Properties.Appearance.Options.UseFont = true;
            this.dtpDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDate.Size = new System.Drawing.Size(130, 26);
            this.dtpDate.TabIndex = 6;
            // 
            // lblTime
            // 
            this.lblTime.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTime.Location = new System.Drawing.Point(30, 190);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(26, 19);
            this.lblTime.TabIndex = 7;
            this.lblTime.Text = "Giờ:";
            // 
            // timeEdit
            // 
            this.timeEdit.EditValue = new System.DateTime(2023, 10, 1, 8, 0, 0, 0);
            this.timeEdit.Location = new System.Drawing.Point(130, 188);
            this.timeEdit.Name = "timeEdit";
            this.timeEdit.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.timeEdit.Properties.Appearance.Options.UseFont = true;
            this.timeEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit.Properties.Mask.EditMask = "t";
            this.timeEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.timeEdit.Size = new System.Drawing.Size(100, 26);
            this.timeEdit.TabIndex = 8;
            // 
            // lblReason
            // 
            this.lblReason.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblReason.Location = new System.Drawing.Point(30, 230);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(37, 19);
            this.lblReason.TabIndex = 9;
            this.lblReason.Text = "Lý do:";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(130, 228);
            this.txtReason.Name = "txtReason";
            this.txtReason.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReason.Properties.Appearance.Options.UseFont = true;
            this.txtReason.Size = new System.Drawing.Size(260, 60);
            this.txtReason.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(130, 310);
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
            this.btnCancel.Location = new System.Drawing.Point(230, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblClinicHours
            // 
            this.lblClinicHours.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblClinicHours.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblClinicHours.Location = new System.Drawing.Point(240, 194);
            this.lblClinicHours.Name = "lblClinicHours";
            this.lblClinicHours.Size = new System.Drawing.Size(117, 15);
            this.lblClinicHours.TabIndex = 13;
            // 
            // AppointmentDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 370);
            this.Controls.Add(this.lblClinicHours);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.timeEdit);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lkeDoctor);
            this.Controls.Add(this.lblDoctor);
            this.Controls.Add(this.lkePatient);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppointmentDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm Lịch hẹn mới";
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkePatient.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeDoctor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblPatient;
        private DevExpress.XtraEditors.LookUpEdit lkePatient;
        private DevExpress.XtraEditors.LabelControl lblDoctor;
        private DevExpress.XtraEditors.LookUpEdit lkeDoctor;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.DateEdit dtpDate;
        private DevExpress.XtraEditors.LabelControl lblTime;
        private DevExpress.XtraEditors.TimeEdit timeEdit;
        private DevExpress.XtraEditors.LabelControl lblReason;
        private DevExpress.XtraEditors.MemoEdit txtReason;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblClinicHours;
    }
}
