using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HospitalManagement.Utils
{
    /// <summary>
    /// Helper class cho thiết kế UI hiện đại
    /// </summary>
    public static class UIHelper
    {
        // Color Palette - Modern Blue Theme
        public static Color PrimaryColor = Color.FromArgb(41, 128, 185);      // Blue
        public static Color PrimaryDark = Color.FromArgb(31, 97, 141);        // Dark Blue
        public static Color PrimaryLight = Color.FromArgb(52, 152, 219);      // Light Blue
        public static Color AccentColor = Color.FromArgb(46, 204, 113);       // Green
        public static Color WarningColor = Color.FromArgb(241, 196, 15);      // Yellow
        public static Color DangerColor = Color.FromArgb(231, 76, 60);        // Red
        public static Color InfoColor = Color.FromArgb(52, 152, 219);         // Light Blue
        public static Color SuccessColor = Color.FromArgb(46, 204, 113);      // Green
        public static Color TextDark = Color.FromArgb(44, 62, 80);            // Dark Gray
        public static Color TextLight = Color.FromArgb(127, 140, 141);        // Light Gray
        public static Color BackgroundColor = Color.FromArgb(245, 247, 250);  // Light Background
        public static Color CardColor = Color.White;
        public static Color SidebarColor = Color.FromArgb(44, 62, 80);        // Dark Sidebar

        // Fonts
        public static Font TitleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        public static Font SubtitleFont = new Font("Segoe UI", 14, FontStyle.Regular);
        public static Font HeaderFont = new Font("Segoe UI Semibold", 12);
        public static Font RegularFont = new Font("Segoe UI", 10);
        public static Font SmallFont = new Font("Segoe UI", 9);

        /// <summary>
        /// Style cho Button chính (Primary)
        /// </summary>
        public static void StylePrimaryButton(Button btn)
        {
            btn.BackColor = PrimaryColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI Semibold", 10);
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(10, 5, 10, 5);

            // Hover effect
            btn.MouseEnter += (s, e) => btn.BackColor = PrimaryDark;
            btn.MouseLeave += (s, e) => btn.BackColor = PrimaryColor;
        }

        /// <summary>
        /// Style cho Button thành công (Success)
        /// </summary>
        public static void StyleSuccessButton(Button btn)
        {
            btn.BackColor = AccentColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI Semibold", 10);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(39, 174, 96);
            btn.MouseLeave += (s, e) => btn.BackColor = AccentColor;
        }

        /// <summary>
        /// Style cho Button nguy hiểm (Danger)
        /// </summary>
        public static void StyleDangerButton(Button btn)
        {
            btn.BackColor = DangerColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI Semibold", 10);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(192, 57, 43);
            btn.MouseLeave += (s, e) => btn.BackColor = DangerColor;
        }

        /// <summary>
        /// Style cho Button phụ (Secondary)
        /// </summary>
        public static void StyleSecondaryButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(149, 165, 166);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI Semibold", 10);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(127, 140, 141);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(149, 165, 166);
        }

        // --- Standard Action Buttons (WinForms) ---

        public static void StyleAddButton(Button btn)
        {
            StyleSuccessButton(btn);
            btn.Text = "Thêm";
            btn.Size = new Size(110, 38);
        }

        public static void StyleEditButton(Button btn)
        {
            StylePrimaryButton(btn);
            btn.BackColor = Color.FromArgb(243, 156, 18);
            btn.Text = "Sửa";
            btn.Size = new Size(100, 38);
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(214, 137, 16);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(243, 156, 18);
        }

        public static void StyleDeleteButton(Button btn)
        {
            StyleDangerButton(btn);
            btn.Text = "Xóa";
            btn.Size = new Size(100, 38);
        }

        public static void StyleSaveButton(Button btn)
        {
            StyleSuccessButton(btn);
            btn.Text = "Lưu";
            btn.Size = new Size(100, 38);
        }

        public static void StyleCancelButton(Button btn)
        {
            StyleSecondaryButton(btn);
            btn.Text = "Hủy";
            btn.Size = new Size(100, 38);
        }

        public static void StyleCloseButton(Button btn)
        {
             StyleSecondaryButton(btn);
             btn.Text = "Đóng";
             btn.Size = new Size(100, 40);
        }

        // --- DevExpress SimpleButton Support ---

        public static void StylePrimaryButton(SimpleButton btn)
        {
            btn.Appearance.BackColor = PrimaryColor;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Font = new Font("Segoe UI Semibold", 10);
            btn.Cursor = Cursors.Hand;
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        }

        public static void StyleSuccessButton(SimpleButton btn)
        {
            btn.Appearance.BackColor = AccentColor;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Font = new Font("Segoe UI Semibold", 10);
            btn.Cursor = Cursors.Hand;
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        }

        public static void StyleDangerButton(SimpleButton btn)
        {
            btn.Appearance.BackColor = DangerColor;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Font = new Font("Segoe UI Semibold", 10);
            btn.Cursor = Cursors.Hand;
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        }

        public static void StyleSecondaryButton(SimpleButton btn)
        {
            btn.Appearance.BackColor = Color.FromArgb(149, 165, 166);
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Font = new Font("Segoe UI Semibold", 10);
            btn.Cursor = Cursors.Hand;
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        }

        public static void StyleAddButton(SimpleButton btn)
        {
            StyleSuccessButton(btn);
            btn.Text = "Thêm";
            btn.Size = new Size(110, 38);
        }

        public static void StyleEditButton(SimpleButton btn)
        {
            StylePrimaryButton(btn);
            btn.Appearance.BackColor = Color.FromArgb(243, 156, 18);
            btn.Text = "Sửa";
            btn.Size = new Size(100, 38);
        }

        public static void StyleDeleteButton(SimpleButton btn)
        {
            StyleDangerButton(btn);
            btn.Text = "Xóa";
            btn.Size = new Size(100, 38);
        }

        public static void StyleSaveButton(SimpleButton btn)
        {
            StyleSuccessButton(btn);
            btn.Text = "Lưu";
            btn.Size = new Size(100, 38);
        }

        public static void StyleCancelButton(SimpleButton btn)
        {
            StyleSecondaryButton(btn);
            btn.Text = "Hủy";
            btn.Size = new Size(100, 38);
        }
        
        public static void StyleCloseButton(SimpleButton btn)
        {
             StyleSecondaryButton(btn);
             btn.Text = "Đóng";
             btn.Size = new Size(100, 40);
        }

        /// <summary>
        /// Style cho TextBox hiện đại
        /// </summary>
        public static void StyleTextBox(TextBox txt)
        {
            txt.Font = RegularFont;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Padding = new Padding(5);
        }

        /// <summary>
        /// Style cho SearchLookUpEdit (DevExpress)
        /// </summary>
        public static void StyleSearchLookUpEdit(DevExpress.XtraEditors.SearchLookUpEdit slk)
        {
            slk.Properties.Appearance.Font = RegularFont;
            slk.Properties.AutoHeight = false;
            slk.Height = 30; // Match textbox height roughly
            // Remove border or style it? DevExpress handles borders via skin mostly.
            // But we can ensure font consistency.
        }

        /// <summary>
        /// Style cho DataGridView hiện đại
        /// </summary>
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 230, 230);
            dgv.EnableHeadersVisualStyles = false;

            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = HeaderFont;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgv.ColumnHeadersHeight = 45;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Row style
            dgv.DefaultCellStyle.Font = RegularFont;
            dgv.DefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 245, 253);
            dgv.DefaultCellStyle.SelectionForeColor = TextDark;
            dgv.RowTemplate.Height = 40;

            // Alternating row
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 252);

            // Row headers
            dgv.RowHeadersVisible = false;

            // Selection
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Tạo Panel Card với shadow effect
        /// </summary>
        public static Panel CreateCard(int x, int y, int width, int height)
        {
            Panel card = new Panel();
            card.Location = new Point(x, y);
            card.Size = new Size(width, height);
            card.BackColor = CardColor;
            card.Padding = new Padding(15);
            return card;
        }

        /// <summary>
        /// Style cho Form
        /// </summary>
        public static void StyleForm(Form form)
        {
            form.BackColor = BackgroundColor;
            form.Font = RegularFont;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
        }

        /// <summary>
        /// Tạo Header Panel
        /// </summary>
        public static Panel CreateHeaderPanel(string title, int width)
        {
            Panel header = new Panel();
            header.Dock = DockStyle.Top;
            header.Height = 60;
            header.BackColor = PrimaryColor;
            header.Padding = new Padding(20, 0, 0, 0);

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = TitleFont;
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 15);

            header.Controls.Add(lblTitle);
            return header;
        }

        /// <summary>
        /// Tạo Search Panel
        /// </summary>
        public static Panel CreateSearchPanel(out TextBox txtSearch, out Button btnSearch, out Button btnAdd)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Height = 60;
            panel.BackColor = CardColor;
            panel.Padding = new Padding(15);

            // Search icon + textbox
            txtSearch = new TextBox();
            txtSearch.Location = new Point(15, 15);
            txtSearch.Size = new Size(300, 35);
            txtSearch.Font = RegularFont;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            panel.Controls.Add(txtSearch);

            // Search button
            btnSearch = new Button();
            btnSearch.Text = "Tìm kiếm";
            btnSearch.Location = new Point(325, 12);
            btnSearch.Size = new Size(100, 35);
            StylePrimaryButton(btnSearch);
            panel.Controls.Add(btnSearch);

            // Add button
            btnAdd = new Button();
            btnAdd.Text = "Thêm mới";
            btnAdd.Location = new Point(435, 12);
            btnAdd.Size = new Size(110, 35);
            StyleSuccessButton(btnAdd);
            panel.Controls.Add(btnAdd);

            return panel;
        }

        /// <summary>
        /// Tạo Action Button Panel
        /// </summary>
        public static FlowLayoutPanel CreateActionPanel()
        {
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Bottom;
            panel.Height = 60;
            panel.BackColor = CardColor;
            panel.FlowDirection = FlowDirection.RightToLeft;
            panel.Padding = new Padding(10);
            panel.WrapContents = false;
            return panel;
        }

        /// <summary>
        /// Tạo Sidebar Button
        /// </summary>
        public static Button CreateSidebarButton(string text, string icon, int top)
        {
            Button btn = new Button();
            btn.Text = $"  {icon}  {text}";
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Location = new Point(0, top);
            btn.Size = new Size(220, 45);
            btn.BackColor = SidebarColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10);
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(10, 0, 0, 0);

            // Hover
            btn.MouseEnter += (s, e) => btn.BackColor = PrimaryColor;
            btn.MouseLeave += (s, e) => btn.BackColor = SidebarColor;

            return btn;
        }

        /// <summary>
        /// Tạo Stat Card cho Dashboard
        /// </summary>
        public static Panel CreateStatCard(string title, string value, string icon, Color color, int x, int y)
        {
            Panel card = new Panel();
            card.Location = new Point(x, y);
            card.Size = new Size(200, 100);
            card.BackColor = CardColor;

            // Icon background
            Panel iconPanel = new Panel();
            iconPanel.Location = new Point(15, 20);
            iconPanel.Size = new Size(50, 50);
            iconPanel.BackColor = color;

            Label lblIcon = new Label();
            lblIcon.Text = icon;
            lblIcon.Font = new Font("Segoe UI", 20);
            lblIcon.ForeColor = Color.White;
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;
            lblIcon.Dock = DockStyle.Fill;
            iconPanel.Controls.Add(lblIcon);
            card.Controls.Add(iconPanel);

            // Value
            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblValue.ForeColor = TextDark;
            lblValue.Location = new Point(75, 18);
            lblValue.AutoSize = true;
            card.Controls.Add(lblValue);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = SmallFont;
            lblTitle.ForeColor = TextLight;
            lblTitle.Location = new Point(75, 55);
            lblTitle.AutoSize = true;
            card.Controls.Add(lblTitle);

            return card;
        }

        /// <summary>
        /// Configure DevExpress GridView with modern professional styling
        /// </summary>
        public static void ConfigureDevExpressGridView(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            if (gridView == null) return;

            // Appearance
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ShowIndicator = false;
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.OptionsView.RowAutoHeight = false;
            gridView.OptionsView.EnableAppearanceEvenRow = true;
            gridView.OptionsView.EnableAppearanceOddRow = true;
            
            // Behavior
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsBehavior.ReadOnly = true;
            gridView.OptionsBehavior.AllowIncrementalSearch = true;
            
            // Selection
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView.OptionsSelection.MultiSelect = false;
            
            // Row height
            gridView.RowHeight = 30;
            
            // Font
            gridView.Appearance.Row.Font = new Font("Segoe UI", 9.5F);
            gridView.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.5F);
            
            // Selection colors using DXSkinColors for theme compatibility
            gridView.Appearance.FocusedRow.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            gridView.Appearance.FocusedRow.ForeColor = Color.White;
            gridView.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView.Appearance.FocusedRow.Options.UseForeColor = true;
            
            gridView.Appearance.SelectedRow.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            gridView.Appearance.SelectedRow.ForeColor = Color.White;
            gridView.Appearance.SelectedRow.Options.UseBackColor = true;
            gridView.Appearance.SelectedRow.Options.UseForeColor = true;
            
            // Auto-format tất cả cột DateTime sau khi data được load
            gridView.DataSourceChanged += (s, e) => FormatDateTimeColumns(gridView);
        }

        /// <summary>
        /// Format tất cả cột DateTime trong GridView với format dd/MM/yyyy HH:mm:ss
        /// </summary>
        public static void FormatDateTimeColumns(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            if (gridView == null) return;
            
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView.Columns)
            {
                // Check if column type is DateTime or DateTime?
                if (col.ColumnType == typeof(System.DateTime) || col.ColumnType == typeof(System.DateTime?))
                {
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                }
            }
        }

        /// <summary>
        /// Clear GridView selection (no row selected on load)
        /// </summary>
        public static void ClearGridViewSelection(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            if (gridView == null) return;
            gridView.ClearSelection();
            gridView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }

        /// <summary>
        /// Configure form for modern DevExpress style
        /// </summary>
        public static void ConfigureModernForm(DevExpress.XtraEditors.XtraForm form)
        {
            if (form == null) return;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimumSize = new Size(1000, 600); // Increased from 800x500
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.LookAndFeel.SkinStyle.WXI);
        }
    }
}
