using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using HospitalManagement.Utils;
using HospitalManagement.BLL;

namespace HospitalManagement.UI
{
    public partial class MainDashboard : FluentDesignForm
    {
        private Timer _refreshTimer;

        public MainDashboard()
        {
            InitializeComponent();
            EnableAcrylicAccent = false; // Disable transparency effect
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXI);

            // Setup Auto-Refresh Timer
            _refreshTimer = new Timer();
            _refreshTimer.Interval = 30000; // 30 seconds
            _refreshTimer.Tick += (s, e) => ShowDashboard();
        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {
            BuildMenu();
            ShowDashboard();
            _refreshTimer.Start();
        }

        private void BuildMenu()
        {
            var role = SessionManager.CurrentRole ?? "Admin";

            // Attach Click Handlers
            aceDashboard.Click += (s, e) => ShowDashboard();

            // Clinical
            acePatient.Click += (s, e) => OpenForm(new PatientForm());
            aceAppointment.Click += (s, e) => OpenForm(new AppointmentForm());
            aceVisit.Click += (s, e) => OpenForm(new VisitForm());

            // Wire up tile ItemClick handler ONCE here
            tileItemVisit.ItemClick += (s, e) => OpenForm(new VisitForm());

            // Finance
            aceExaminationFee.Click += (s, e) => OpenForm(new ExaminationFeeForm());
            aceMedicinePayment.Click += (s, e) => OpenForm(new MedicinePaymentForm());
            aceRevenueStats.Click += (s, e) => OpenForm(new RevenueStatisticsForm());

            // Pharmacy
            acePharmacy.Click += (s, e) => OpenForm(new PharmacyForm());
            aceMedicine.Click += (s, e) => OpenForm(new MedicineForm());
            aceDisease.Click += (s, e) => OpenForm(new DiseaseForm());

            // Admin
            aceUser.Click += (s, e) => OpenForm(new UserForm());
            aceEmployee.Click += (s, e) => OpenForm(new EmployeeForm());
            aceClinicSettings.Click += (s, e) => OpenForm(new ClinicSettingsForm());

            // User Account
            aceChangePassword.Click += (s, e) => new ChangePasswordForm().ShowDialog();
            aceLogout.Click += (s, e) => PerformLogout();

            // Role-based visibility
            if (role == "Admin")
            {
                // Admin sees all - no changes needed
            }
            else if (role == "Doctor")
            {
                aceGroupFinance.Visible = false;
                aceGroupAdmin.Visible = false;
                aceRevenueStats.Visible = false;
            }
            else if (role == "Receptionist")
            {
                aceGroupFinance.Visible = false;
                aceGroupPharmacy.Visible = false;
                aceGroupAdmin.Visible = false;
                aceVisit.Visible = false;
            }
            else if (role == "Cashier")
            {
                aceGroupClinical.Visible = false;
                aceGroupPharmacy.Visible = false;
                aceGroupAdmin.Visible = false;
                aceRevenueStats.Visible = false;
            }
            else if (role == "Pharmacist")
            {
                aceGroupClinical.Visible = false;
                aceGroupFinance.Visible = false;
                aceGroupAdmin.Visible = false;
                aceDisease.Visible = false;
            }

            accordionControl1.ExpandAll();
        }


        private void ShowDashboard()
        {
            var displayName = SessionManager.CurrentUser?.Username ?? "Admin";
            var role = SessionManager.CurrentRole ?? "Admin";

            // Try to get more specific name
            if (SessionManager.CurrentDoctor != null)
            {
                displayName = "Dr. " + SessionManager.CurrentDoctor.FullName;
            }
            else if (SessionManager.CurrentUser != null)
            {
                // Check if linked to an employee
                var empDAL = new DAL.EmployeeDAL();
                var emp = empDAL.GetByUserId(SessionManager.CurrentUser.UserId);
                if (emp != null) displayName = emp.FullName;
            }

            lblWelcome.Text = $"Xin chào, {displayName}! 👋";
            lblDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy HH:mm");

            // Update Stats Tiles with dynamic values
            try
            {
                // Wire up tile ItemClick handlers and update visibility based on role
                SetupTileClickHandlers();

                if (role == "Admin")
                {
                    // Show all tiles for Admin
                    tileItemPatient.Visible = true;
                    tileItemEmployee.Visible = true;
                    tileItemSchedule.Visible = true;
                    tileItemRevenue.Visible = true;

                    var patientBLL = new PatientBLL();
                    UpdateTileValue(tileItemPatient, patientBLL.GetPatientCount().ToString());

                    var employeeDAL = new DAL.EmployeeDAL();
                    UpdateTileValue(tileItemEmployee, employeeDAL.GetAll().Count.ToString());

                    var appointmentBLL = new AppointmentBLL();
                    UpdateTileValue(tileItemSchedule, appointmentBLL.GetPendingTodayAppointments().Count.ToString());

                    // Revenue - tính từ hóa đơn khám + bán thuốc
                    var invoiceBLL = new InvoiceBLL();
                    var saleBLL = new MedicineSaleBLL();
                    var invoiceRevenue = invoiceBLL.GetTotalRevenue(DateTime.Today, DateTime.Today);
                    decimal medicineRevenue = 0;
                    var todaySales = saleBLL.GetSalesByDateRange(DateTime.Today, DateTime.Today);
                    foreach (var sale in todaySales)
                        if (sale.Status == "Paid" || sale.Status == "Dispensed")
                            medicineRevenue += sale.TotalAmount;

                    var totalRevenue = invoiceRevenue + medicineRevenue;
                    UpdateTileValue(tileItemRevenue, totalRevenue.ToString("N0") + " đ");
                }
                else if (role == "Doctor")
                {
                    tileItemPatient.Visible = true;
                    tileItemEmployee.Visible = false;
                    tileItemSchedule.Visible = true;
                    tileItemRevenue.Visible = false;

                    var patientBLL = new PatientBLL();
                    UpdateTileValue(tileItemPatient, patientBLL.GetPatientCount().ToString());

                    var appointmentBLL = new AppointmentBLL();
                    var doctorId = SessionManager.CurrentDoctor?.DoctorId ?? 0;
                    var myAppts = appointmentBLL.GetAppointmentsByDoctor(doctorId, DateTime.Today);
                    UpdateTileValue(tileItemSchedule, myAppts.Count.ToString());
                }
                else if (role == "Receptionist")
                {
                    tileItemPatient.Visible = true;
                    tileItemEmployee.Visible = false;
                    tileItemSchedule.Visible = true;
                    tileItemRevenue.Visible = false;

                    var patientBLL = new PatientBLL();
                    UpdateTileValue(tileItemPatient, patientBLL.GetPatientCount().ToString());

                    var appointmentBLL = new AppointmentBLL();
                    UpdateTileValue(tileItemSchedule, appointmentBLL.GetPendingTodayAppointments().Count.ToString());
                }
                else if (role == "Cashier")
                {
                    tileItemPatient.Visible = false;
                    tileItemEmployee.Visible = false;
                    tileItemSchedule.Visible = false;
                    tileItemRevenue.Visible = true;

                    var invoiceBLL = new InvoiceBLL();
                    var allInvoices = invoiceBLL.GetInvoicesByDateRange(DateTime.Today, DateTime.Today);
                    var pendingInvoices = 0;
                    foreach (var inv in allInvoices)
                        if (inv.Status == "Pending")
                            pendingInvoices++;

                    UpdateTileValue(tileItemRevenue, pendingInvoices.ToString() + " chờ");
                }
                else if (role == "Pharmacist")
                {
                    tileItemPatient.Visible = false;
                    tileItemEmployee.Visible = false;
                    tileItemSchedule.Visible = false;
                    tileItemRevenue.Visible = false;
                }
                else
                {
                    // Default: hide all
                    tileItemPatient.Visible = false;
                    tileItemEmployee.Visible = false;
                    tileItemSchedule.Visible = false;
                    tileItemRevenue.Visible = false;
                }
            }
            catch
            {
            }

            // 2. Setup Action Tiles (ItemClick is set in BuildMenu, only update count here)
            // Get today's visit count and update the tile
            try
            {
                var visitBLL = new VisitBLL();
                var todayVisits = visitBLL.GetVisitsByDate(DateTime.Today);
                var visitCount = todayVisits?.Count ?? 0;

                // Find or create value element for count
                // Look for existing value element (at TopRight position)
                TileItemElement valueElement = null;
                for (var i = 0; i < tileItemVisit.Elements.Count; i++)
                {
                    var elem = tileItemVisit.Elements[i] as TileItemElement;
                    if (elem != null && elem.TextAlignment == TileItemContentAlignment.TopRight)
                    {
                        valueElement = elem;
                        break;
                    }
                }

                if (valueElement == null)
                {
                    valueElement = new TileItemElement();
                    valueElement.TextAlignment = TileItemContentAlignment.TopRight;
                    valueElement.Appearance.Normal.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                    tileItemVisit.Elements.Add(valueElement);
                }

                valueElement.Text = visitCount.ToString();
            }
            catch
            {
            }

            // Khám bệnh tile visible for Admin, Receptionist, Doctor
            tileItemVisit.Visible = role == "Admin" || role == "Receptionist" || role == "Doctor";

            // 3. Load Grid Data
            LoadGridData();
        }

        private void LoadGridData()
        {
            try
            {
                var role = SessionManager.CurrentRole ?? "Admin";

                if (role == "Doctor")
                {
                    // Doctors see their appointments
                    var apptBLL = new AppointmentBLL();
                    var doctorId = SessionManager.CurrentDoctor?.DoctorId ?? 0;
                    var myAppts = apptBLL.GetAppointmentsByDoctor(doctorId, DateTime.Today);
                    gridDashboard.DataSource = myAppts;

                    gridViewDashboard.PopulateColumns();
                    HideGridColumns("AppointmentId", "PatientId", "DoctorId",
                        "DoctorName"); // Hide DoctorName as it's me
                    FormatGridColumn("AppointmentDate", "Thời gian", "HH:mm");
                    SetGridCaption("PatientName", "Bệnh nhân");
                    SetGridCaption("Reason", "Lý do khám");
                    SetGridCaption("Status", "Trạng thái");
                }
                else if (role == "Cashier")
                {
                    // Cashiers see pending medicine sales to pay (or we could show invoices)
                    // Showing pending medicine sales for now as it's a key cashier task
                    var saleBLL = new MedicineSaleBLL();
                    gridDashboard.DataSource = saleBLL.GetPendingPayments();

                    gridViewDashboard.PopulateColumns();
                    HideGridColumns("SaleId", "PatientId", "VisitId", "PrescriptionId", "PharmacistId", "Notes");
                    FormatGridColumn("SaleDate", "Ghi nhận lúc", "HH:mm");
                    SetGridCaption("TotalAmount", "Tổng tiền");
                    SetGridCaption("Status", "Trạng thái");
                }
                else if (role == "Pharmacist")
                {
                    // Pharmacists see paid sales ready to dispense
                    var saleBLL = new MedicineSaleBLL();
                    gridDashboard.DataSource = saleBLL.GetPaidNotDispensed();

                    gridViewDashboard.PopulateColumns();
                    HideGridColumns("SaleId", "PatientId", "VisitId", "PrescriptionId", "PharmacistId", "Notes",
                        "CashierId");
                    FormatGridColumn("PaymentDate", "Thanh toán lúc", "HH:mm");
                    SetGridCaption("TotalAmount", "Tổng tiền");
                    SetGridCaption("Status", "Trạng thái");
                }
                else // Admin, Receptionist
                {
                    // Default view: Today's Appointments
                    var apptBLL = new AppointmentBLL();
                    var todayAppts = apptBLL.GetPendingTodayAppointments();
                    gridDashboard.DataSource = todayAppts;

                    gridViewDashboard.PopulateColumns();
                    HideGridColumns("AppointmentId", "PatientId", "DoctorId");
                    FormatGridColumn("AppointmentDate", "Thời gian", "HH:mm");
                    SetGridCaption("PatientName", "Bệnh nhân");
                    SetGridCaption("DoctorName", "Bác sĩ");
                    SetGridCaption("Status", "Trạng thái");
                    SetGridCaption("Reason", "Lý do khám");
                }
            }
            catch
            {
            }
        }

        // Helper methods for cleaner grid code
        private void HideGridColumns(params string[] columnNames)
        {
            foreach (var col in columnNames)
                if (gridViewDashboard.Columns[col] != null)
                    gridViewDashboard.Columns[col].Visible = false;
        }

        private void SetGridCaption(string columnName, string caption)
        {
            if (gridViewDashboard.Columns[columnName] != null) gridViewDashboard.Columns[columnName].Caption = caption;
        }

        private void FormatGridColumn(string columnName, string caption, string formatStr)
        {
            if (gridViewDashboard.Columns[columnName] != null)
            {
                gridViewDashboard.Columns[columnName].Caption = caption;
                gridViewDashboard.Columns[columnName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridViewDashboard.Columns[columnName].DisplayFormat.FormatString = formatStr;
            }
        }

        private bool _tileClickHandlersSetup = false;

        private void SetupTileClickHandlers()
        {
            if (_tileClickHandlersSetup) return;
            _tileClickHandlersSetup = true;

            tileItemPatient.ItemClick += (s, e) => OpenForm(new PatientForm());
            tileItemEmployee.ItemClick += (s, e) => OpenForm(new EmployeeForm());
            tileItemSchedule.ItemClick += (s, e) => OpenForm(new AppointmentForm());
            tileItemRevenue.ItemClick += (s, e) => OpenForm(new RevenueStatisticsForm());
        }

        private void UpdateTileValue(TileItem tile, string value)
        {
            if (tile == null || tile.Elements.Count == 0) return;

            // First element is the count/value (TopRight alignment)
            var valueElement = tile.Elements[0] as TileItemElement;
            if (valueElement != null) valueElement.Text = value;
        }

        private void AddTileItem(TileGroup group, string title, string value, string icon, Color color,
            Action onClick = null)
        {
            var item = new TileItem();
            item.AppearanceItem.Normal.BackColor = color;
            item.AppearanceItem.Normal.Options.UseBackColor = true;

            // Icon: Center of tile (large)
            var elementIcon = new TileItemElement();
            elementIcon.Text = icon;
            elementIcon.TextAlignment = TileItemContentAlignment.MiddleCenter;
            elementIcon.Appearance.Normal.Font = new Font("Segoe UI", 28);

            // Value: Top right corner
            var elementVal = new TileItemElement();
            elementVal.Text = value;
            elementVal.TextAlignment = TileItemContentAlignment.TopRight;
            elementVal.Appearance.Normal.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            // Title: Bottom center
            var elementTitle = new TileItemElement();
            elementTitle.Text = title;
            elementTitle.TextAlignment = TileItemContentAlignment.BottomCenter;
            elementTitle.Appearance.Normal.Font = new Font("Segoe UI", 9);

            item.Elements.Add(elementIcon);
            item.Elements.Add(elementVal);
            item.Elements.Add(elementTitle);

            item.ItemSize = TileItemSize.Medium;
            if (onClick != null) item.ItemClick += (s, e) => onClick.Invoke();

            group.Items.Add(item);
        }

        private void PerformLogout()
        {
            if (XtraMessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionManager.Logout();
                var loginForm = new LoginForm();
                loginForm.Show();
                loginForm.FormClosed += (s, args) => Application.Exit();
                Hide();
            }
        }

        private void OpenForm(Form form)
        {
            // Skip ConfigureModernForm for small dialog forms
            var smallDialogForms = new[] { "ClinicSettingsForm", "ChangePasswordForm" };
            bool isSmallDialog = smallDialogForms.Any(name => form.GetType().Name == name);
            
            if (form is XtraForm xtraForm && !isSmallDialog)
            {
                UIHelper.ConfigureModernForm(xtraForm);
            }
            else if (!isSmallDialog)
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimumSize = new Size(1000, 600);
            }
            else
            {
                // Small dialog forms - just center them
                form.StartPosition = FormStartPosition.CenterParent;
            }

            form.ShowDialog();

            // Refresh immediately after closing a dialog (to reflect changes)
            ShowDashboard();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}