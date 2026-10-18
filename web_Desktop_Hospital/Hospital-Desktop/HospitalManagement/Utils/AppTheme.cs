using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace HospitalManagement.Utils
{
    /// <summary>
    /// Centralized theme management for the entire application.
    /// Provides consistent colors, fonts, and styling methods.
    /// </summary>
    public static class AppTheme
    {
        #region Colors - Modern Blue Theme

        // Primary Colors
        public static readonly Color Primary = Color.FromArgb(0, 123, 255);
        public static readonly Color PrimaryDark = Color.FromArgb(0, 86, 179);
        public static readonly Color PrimaryLight = Color.FromArgb(102, 175, 255);

        // Semantic Colors
        public static readonly Color Success = Color.FromArgb(40, 167, 69);
        public static readonly Color Warning = Color.FromArgb(255, 193, 7);
        public static readonly Color Danger = Color.FromArgb(220, 53, 69);
        public static readonly Color Info = Color.FromArgb(23, 162, 184);

        // Neutral Colors
        public static readonly Color TextDark = Color.FromArgb(33, 37, 41);
        public static readonly Color TextMuted = Color.FromArgb(108, 117, 125);
        public static readonly Color TextLight = Color.FromArgb(173, 181, 189);
        
        // Background Colors
        public static readonly Color Background = Color.FromArgb(248, 249, 250);
        public static readonly Color Surface = Color.White;
        public static readonly Color SurfaceAlt = Color.FromArgb(233, 236, 239);

        // Gradient Pairs
        public static readonly Color GradientStart = Color.FromArgb(0, 123, 255);
        public static readonly Color GradientEnd = Color.FromArgb(111, 66, 193);

        #endregion

        #region Fonts

        public static readonly Font FontTitle = new Font("Segoe UI", 18F, FontStyle.Bold);
        public static readonly Font FontSubtitle = new Font("Segoe UI", 14F, FontStyle.Regular);
        public static readonly Font FontHeader = new Font("Segoe UI Semibold", 12F);
        public static readonly Font FontBody = new Font("Segoe UI", 10F);
        public static readonly Font FontSmall = new Font("Segoe UI", 9F);
        public static readonly Font FontButton = new Font("Segoe UI Semibold", 10F);

        #endregion

        #region Global Initialization

        /// <summary>
        /// Initialize global DevExpress theming. Call once in Program.cs.
        /// </summary>
        public static void Initialize()
        {
            // Enable high DPI support
            DevExpress.Utils.AppearanceObject.DefaultFont = FontBody;
            
            // Set global skin - WXI is Windows 11 inspired
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);
        }

        #endregion

        #region Button Styling

        public static void StylePrimaryButton(SimpleButton btn)
        {
            btn.Appearance.BackColor = Primary;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Font = FontButton;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        public static void StyleSuccessButton(SimpleButton btn)
        {
            btn.Appearance.BackColor = Success;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Font = FontButton;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        public static void StyleDangerButton(SimpleButton btn)
        {
            btn.Appearance.BackColor = Danger;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Font = FontButton;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        public static void StyleSecondaryButton(SimpleButton btn)
        {
            btn.Appearance.BackColor = SurfaceAlt;
            btn.Appearance.ForeColor = TextDark;
            btn.Appearance.Font = FontButton;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        #endregion

        #region Grid Styling

        public static void StyleGridView(GridView gridView)
        {
            // Header
            gridView.Appearance.HeaderPanel.BackColor = Primary;
            gridView.Appearance.HeaderPanel.ForeColor = Color.White;
            gridView.Appearance.HeaderPanel.Font = FontHeader;
            gridView.Appearance.HeaderPanel.Options.UseBackColor = true;
            gridView.Appearance.HeaderPanel.Options.UseForeColor = true;
            gridView.Appearance.HeaderPanel.Options.UseFont = true;

            // Rows
            gridView.Appearance.Row.Font = FontBody;
            gridView.Appearance.Row.Options.UseFont = true;

            // Alternating rows
            gridView.Appearance.EvenRow.BackColor = Color.FromArgb(248, 249, 250);
            gridView.Appearance.EvenRow.Options.UseBackColor = true;
            gridView.Appearance.OddRow.BackColor = Color.White;
            gridView.Appearance.OddRow.Options.UseBackColor = true;

            // Selection
            gridView.Appearance.FocusedRow.BackColor = Color.FromArgb(232, 240, 254);
            gridView.Appearance.FocusedRow.ForeColor = TextDark;
            gridView.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView.Appearance.FocusedRow.Options.UseForeColor = true;

            // Options
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.EnableAppearanceEvenRow = true;
            gridView.OptionsView.EnableAppearanceOddRow = true;
            gridView.OptionsView.ColumnAutoWidth = true;
            gridView.OptionsBehavior.Editable = false;
            gridView.RowHeight = 36;
        }

        #endregion

        #region Panel Styling

        public static void StyleHeaderPanel(PanelControl panel, LabelControl titleLabel = null)
        {
            panel.Appearance.BackColor = Primary;
            panel.Appearance.Options.UseBackColor = true;
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            if (titleLabel != null)
            {
                titleLabel.Appearance.Font = FontTitle;
                titleLabel.Appearance.ForeColor = Color.White;
                titleLabel.Appearance.Options.UseFont = true;
                titleLabel.Appearance.Options.UseForeColor = true;
            }
        }

        #endregion

        #region Gradient Helper

        public static LinearGradientBrush CreateGradientBrush(Rectangle rect, Color start, Color end)
        {
            return new LinearGradientBrush(rect, start, end, LinearGradientMode.Horizontal);
        }

        #endregion
    }
}
