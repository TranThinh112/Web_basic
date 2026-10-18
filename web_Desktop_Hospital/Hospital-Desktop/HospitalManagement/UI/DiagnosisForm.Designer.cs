namespace HospitalManagement.UI
{
    partial class DiagnosisForm
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
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblPatientName = new DevExpress.XtraEditors.LabelControl();
            this.lblVisitDate = new DevExpress.XtraEditors.LabelControl();
            this.txtSymptoms = new DevExpress.XtraEditors.MemoEdit();
            this.lkeDisease = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDiagnosis = new DevExpress.XtraEditors.MemoEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrescription = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemPatient = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupDiagnosis = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemSymptoms = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDisease = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDiagnosis = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemPrescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItemButtons = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymptoms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeDisease.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiagnosis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupDiagnosis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSymptoms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDisease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDiagnosis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPrescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemButtons)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.lblTitle);
            this.layoutControl.Controls.Add(this.lblPatientName);
            this.layoutControl.Controls.Add(this.lblVisitDate);
            this.layoutControl.Controls.Add(this.txtSymptoms);
            this.layoutControl.Controls.Add(this.lkeDisease);
            this.layoutControl.Controls.Add(this.txtDiagnosis);
            this.layoutControl.Controls.Add(this.btnSave);
            this.layoutControl.Controls.Add(this.btnPrescription);
            this.layoutControl.Controls.Add(this.btnClose);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(550, 520);
            this.layoutControl.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(526, 30);
            this.lblTitle.StyleController = this.layoutControl;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHẨN ĐOÁN BỆNH NHÂN";
            // 
            // lblPatientName
            // 
            this.lblPatientName.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPatientName.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Hyperlink;
            this.lblPatientName.Appearance.Options.UseFont = true;
            this.lblPatientName.Appearance.Options.UseForeColor = true;
            this.lblPatientName.Location = new System.Drawing.Point(99, 81);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(107, 20);
            this.lblPatientName.StyleController = this.layoutControl;
            this.lblPatientName.TabIndex = 1;
            this.lblPatientName.Text = "[Patient Name]";
            // 
            // lblVisitDate
            // 
            this.lblVisitDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblVisitDate.Location = new System.Drawing.Point(313, 81);
            this.lblVisitDate.Name = "lblVisitDate";
            this.lblVisitDate.Size = new System.Drawing.Size(76, 20);
            this.lblVisitDate.StyleController = this.layoutControl;
            this.lblVisitDate.TabIndex = 2;
            this.lblVisitDate.Text = "[Visit Date]";
            // 
            // txtSymptoms
            // 
            this.txtSymptoms.Location = new System.Drawing.Point(24, 155);
            this.txtSymptoms.Name = "txtSymptoms";
            this.txtSymptoms.Size = new System.Drawing.Size(502, 60);
            this.txtSymptoms.StyleController = this.layoutControl;
            this.txtSymptoms.TabIndex = 3;
            // 
            // lkeDisease
            // 
            this.lkeDisease.Location = new System.Drawing.Point(95, 219);
            this.lkeDisease.Name = "lkeDisease";
            this.lkeDisease.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkeDisease.Properties.NullText = "-- Chọn bệnh --";
            this.lkeDisease.Size = new System.Drawing.Size(431, 20);
            this.lkeDisease.StyleController = this.layoutControl;
            this.lkeDisease.TabIndex = 4;
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.Location = new System.Drawing.Point(24, 263);
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(502, 80);
            this.txtDiagnosis.StyleController = this.layoutControl;
            this.txtDiagnosis.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(12, 468);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(162, 40);
            this.btnSave.StyleController = this.layoutControl;
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "💾 Lưu chẩn đoán";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrescription
            // 
            this.btnPrescription.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(16)))), ((int)(((byte)(242)))));
            this.btnPrescription.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrescription.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnPrescription.Appearance.Options.UseBackColor = true;
            this.btnPrescription.Appearance.Options.UseFont = true;
            this.btnPrescription.Appearance.Options.UseForeColor = true;
            this.btnPrescription.Location = new System.Drawing.Point(178, 468);
            this.btnPrescription.Name = "btnPrescription";
            this.btnPrescription.Size = new System.Drawing.Size(178, 40);
            this.btnPrescription.StyleController = this.layoutControl;
            this.btnPrescription.TabIndex = 7;
            this.btnPrescription.Text = "💊 Kê đơn thuốc";
            this.btnPrescription.Click += new System.EventHandler(this.btnPrescription_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Location = new System.Drawing.Point(360, 468);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(178, 40);
            this.btnClose.StyleController = this.layoutControl;
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTitle,
            this.groupInfo,
            this.groupDiagnosis,
            this.layoutControlItemSave,
            this.layoutControlItemPrescription,
            this.layoutControlItemClose,
            this.emptySpaceItemButtons});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(550, 520);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemTitle
            // 
            this.layoutControlItemTitle.Control = this.lblTitle;
            this.layoutControlItemTitle.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemTitle.Name = "layoutControlItemTitle";
            this.layoutControlItemTitle.Size = new System.Drawing.Size(530, 34);
            this.layoutControlItemTitle.Text = "Title";
            this.layoutControlItemTitle.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTitle.TextVisible = false;
            // 
            // groupInfo
            // 
            this.groupInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemPatient,
            this.layoutControlItemDate});
            this.groupInfo.Location = new System.Drawing.Point(0, 34);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.Size = new System.Drawing.Size(530, 71);
            this.groupInfo.Text = "Thông tin chung";
            // 
            // layoutControlItemPatient
            // 
            this.layoutControlItemPatient.Control = this.lblPatientName;
            this.layoutControlItemPatient.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemPatient.Name = "layoutControlItemPatient";
            this.layoutControlItemPatient.Size = new System.Drawing.Size(194, 24);
            this.layoutControlItemPatient.Text = "Bệnh nhân:";
            this.layoutControlItemPatient.TextSize = new System.Drawing.Size(71, 13);
            // 
            // layoutControlItemDate
            // 
            this.layoutControlItemDate.Control = this.lblVisitDate;
            this.layoutControlItemDate.Location = new System.Drawing.Point(194, 0);
            this.layoutControlItemDate.Name = "layoutControlItemDate";
            this.layoutControlItemDate.Size = new System.Drawing.Size(312, 24);
            this.layoutControlItemDate.Text = "Thời gian:";
            this.layoutControlItemDate.TextSize = new System.Drawing.Size(71, 13);
            // 
            // groupDiagnosis
            // 
            this.groupDiagnosis.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemSymptoms,
            this.layoutControlItemDisease,
            this.layoutControlItemDiagnosis});
            this.groupDiagnosis.Location = new System.Drawing.Point(0, 105);
            this.groupDiagnosis.Name = "groupDiagnosis";
            this.groupDiagnosis.Size = new System.Drawing.Size(530, 242);
            this.groupDiagnosis.Text = "Kết quả khám";
            // 
            // layoutControlItemSymptoms
            // 
            this.layoutControlItemSymptoms.Control = this.txtSymptoms;
            this.layoutControlItemSymptoms.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemSymptoms.Name = "layoutControlItemSymptoms";
            this.layoutControlItemSymptoms.Size = new System.Drawing.Size(506, 80);
            this.layoutControlItemSymptoms.Text = "Triệu chứng";
            this.layoutControlItemSymptoms.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemSymptoms.TextSize = new System.Drawing.Size(71, 13);
            // 
            // layoutControlItemDisease
            // 
            this.layoutControlItemDisease.Control = this.lkeDisease;
            this.layoutControlItemDisease.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItemDisease.Name = "layoutControlItemDisease";
            this.layoutControlItemDisease.Size = new System.Drawing.Size(506, 24);
            this.layoutControlItemDisease.Text = "Bệnh (ICD)";
            this.layoutControlItemDisease.TextSize = new System.Drawing.Size(71, 13);
            // 
            // layoutControlItemDiagnosis
            // 
            this.layoutControlItemDiagnosis.Control = this.txtDiagnosis;
            this.layoutControlItemDiagnosis.Location = new System.Drawing.Point(0, 88);
            this.layoutControlItemDiagnosis.Name = "layoutControlItemDiagnosis";
            this.layoutControlItemDiagnosis.Size = new System.Drawing.Size(506, 107);
            this.layoutControlItemDiagnosis.Text = "Chẩn đoán";
            this.layoutControlItemDiagnosis.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemDiagnosis.TextSize = new System.Drawing.Size(71, 13);
            // 
            // layoutControlItemSave
            // 
            this.layoutControlItemSave.Control = this.btnSave;
            this.layoutControlItemSave.Location = new System.Drawing.Point(0, 456);
            this.layoutControlItemSave.Name = "layoutControlItemSave";
            this.layoutControlItemSave.Size = new System.Drawing.Size(166, 44);
            this.layoutControlItemSave.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSave.TextVisible = false;
            // 
            // layoutControlItemPrescription
            // 
            this.layoutControlItemPrescription.Control = this.btnPrescription;
            this.layoutControlItemPrescription.Location = new System.Drawing.Point(166, 456);
            this.layoutControlItemPrescription.Name = "layoutControlItemPrescription";
            this.layoutControlItemPrescription.Size = new System.Drawing.Size(182, 44);
            this.layoutControlItemPrescription.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemPrescription.TextVisible = false;
            // 
            // layoutControlItemClose
            // 
            this.layoutControlItemClose.Control = this.btnClose;
            this.layoutControlItemClose.Location = new System.Drawing.Point(348, 456);
            this.layoutControlItemClose.Name = "layoutControlItemClose";
            this.layoutControlItemClose.Size = new System.Drawing.Size(182, 44);
            this.layoutControlItemClose.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemClose.TextVisible = false;
            // 
            // emptySpaceItemButtons
            // 
            this.emptySpaceItemButtons.AllowHotTrack = false;
            this.emptySpaceItemButtons.Location = new System.Drawing.Point(0, 347);
            this.emptySpaceItemButtons.Name = "emptySpaceItemButtons";
            this.emptySpaceItemButtons.Size = new System.Drawing.Size(530, 109);
            this.emptySpaceItemButtons.TextSize = new System.Drawing.Size(0, 0);
            // 
            // DiagnosisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 520);
            this.Controls.Add(this.layoutControl);
            this.Name = "DiagnosisForm";
            this.Text = "Chẩn đoán bệnh nhân";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSymptoms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkeDisease.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiagnosis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupDiagnosis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSymptoms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDisease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDiagnosis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPrescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItemButtons)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblPatientName;
        private DevExpress.XtraEditors.LabelControl lblVisitDate;
        private DevExpress.XtraEditors.MemoEdit txtSymptoms;
        private DevExpress.XtraEditors.LookUpEdit lkeDisease;
        private DevExpress.XtraEditors.MemoEdit txtDiagnosis;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnPrescription;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTitle;
        private DevExpress.XtraLayout.LayoutControlGroup groupInfo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPatient;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDate;
        private DevExpress.XtraLayout.LayoutControlGroup groupDiagnosis;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSymptoms;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDisease;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDiagnosis;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPrescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemClose;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemButtons;
    }
}
