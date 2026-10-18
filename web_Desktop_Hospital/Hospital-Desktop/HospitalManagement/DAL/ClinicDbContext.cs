using System.Data.Entity;
using HospitalManagement.DTO;

namespace HospitalManagement.DAL
{
    /// <summary>
    /// Entity Framework DbContext cho Hospital Management System
    /// </summary>
    public class ClinicDbContext : DbContext
    {
        /// <summary>
        /// Constructor - sử dụng connection string "HospitalDB" từ App.config
        /// </summary>
        public ClinicDbContext() : base("name=HospitalDB")
        {
            // Tắt tự động tạo database
            Database.SetInitializer<ClinicDbContext>(null);
        }

        // === DbSets cho tất cả entities ===
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<PatientDTO> Patients { get; set; }
        public DbSet<DoctorDTO> Doctors { get; set; }
        public DbSet<DiseaseDTO> Diseases { get; set; }
        public DbSet<MedicineDTO> Medicines { get; set; }
        public DbSet<VisitDTO> Visits { get; set; }
        public DbSet<PrescriptionDTO> Prescriptions { get; set; }
        public DbSet<PrescriptionDetailDTO> PrescriptionDetails { get; set; }
        public DbSet<AppointmentDTO> Appointments { get; set; }
        public DbSet<InvoiceDTO> Invoices { get; set; }
        public DbSet<EmployeeDTO> Employees { get; set; }
        public DbSet<MedicineSaleDTO> MedicineSales { get; set; }
        public DbSet<MedicineSaleDetailDTO> MedicineSaleDetails { get; set; }
        public DbSet<ClinicSettingsDTO> ClinicSettings { get; set; }

        /// <summary>
        /// Cấu hình Fluent API cho các entities
        /// Chú ý: TẤT CẢ navigation/computed properties phải được Ignore
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === User === (No navigation properties)
            modelBuilder.Entity<UserDTO>()
                .ToTable("Users")
                .HasKey(u => u.UserId);

            // === Patient ===
            modelBuilder.Entity<PatientDTO>()
                .ToTable("Patients")
                .HasKey(p => p.PatientId);
            modelBuilder.Entity<PatientDTO>()
                .Ignore(p => p.Age); // Computed property

            // === Doctor ===
            modelBuilder.Entity<DoctorDTO>()
                .ToTable("Doctors")
                .HasKey(d => d.DoctorId);
            modelBuilder.Entity<DoctorDTO>()
                .Ignore(d => d.Username); // Navigation property

            // === Disease === (No navigation properties)
            modelBuilder.Entity<DiseaseDTO>()
                .ToTable("Diseases")
                .HasKey(d => d.DiseaseId);

            // === Medicine === (No navigation properties)
            modelBuilder.Entity<MedicineDTO>()
                .ToTable("Medicines")
                .HasKey(m => m.MedicineId);

            // === Visit ===
            modelBuilder.Entity<VisitDTO>()
                .ToTable("Visits")
                .HasKey(v => v.VisitId);
            modelBuilder.Entity<VisitDTO>()
                .Ignore(v => v.PatientName)
                .Ignore(v => v.DoctorName)
                .Ignore(v => v.DiseaseName)
                .Ignore(v => v.MedicalHistory)
                .Ignore(v => v.StatusDisplay);

            // === Prescription ===
            modelBuilder.Entity<PrescriptionDTO>()
                .ToTable("Prescriptions")
                .HasKey(p => p.PrescriptionId);
            modelBuilder.Entity<PrescriptionDTO>()
                .Ignore(p => p.PatientName)
                .Ignore(p => p.DoctorName);

            // === PrescriptionDetail (composite key) ===
            modelBuilder.Entity<PrescriptionDetailDTO>()
                .ToTable("PrescriptionDetails")
                .HasKey(pd => new { pd.PrescriptionId, pd.MedicineId });
            modelBuilder.Entity<PrescriptionDetailDTO>()
                .Ignore(pd => pd.MedicineName)
                .Ignore(pd => pd.Unit)
                .Ignore(pd => pd.UnitPrice)
                .Ignore(pd => pd.Price)
                .Ignore(pd => pd.LineTotal)
                .Ignore(pd => pd.TotalPrice);

            // === Appointment ===
            modelBuilder.Entity<AppointmentDTO>()
                .ToTable("Appointments")
                .HasKey(a => a.AppointmentId);
            modelBuilder.Entity<AppointmentDTO>()
                .Ignore(a => a.PatientName)
                .Ignore(a => a.DoctorName)
                .Ignore(a => a.PatientPhone);

            // === Invoice ===
            modelBuilder.Entity<InvoiceDTO>()
                .ToTable("Invoices")
                .HasKey(i => i.InvoiceId);
            modelBuilder.Entity<InvoiceDTO>()
                .Ignore(i => i.PatientName)
                .Ignore(i => i.DoctorName);

            // === Employee ===
            modelBuilder.Entity<EmployeeDTO>()
                .ToTable("Employees")
                .HasKey(e => e.EmployeeId);
            modelBuilder.Entity<EmployeeDTO>()
                .Ignore(e => e.Username);

            // === MedicineSale ===
            modelBuilder.Entity<MedicineSaleDTO>()
                .ToTable("MedicineSales")
                .HasKey(ms => ms.SaleId);
            modelBuilder.Entity<MedicineSaleDTO>()
                .Ignore(ms => ms.PatientName)
                .Ignore(ms => ms.PharmacistName)
                .Ignore(ms => ms.CashierName)
                .Ignore(ms => ms.StatusDisplay);

            // === MedicineSaleDetail ===
            modelBuilder.Entity<MedicineSaleDetailDTO>()
                .ToTable("MedicineSaleDetails")
                .HasKey(msd => msd.SaleDetailId);
            modelBuilder.Entity<MedicineSaleDetailDTO>()
                .Ignore(msd => msd.MedicineName)
                .Ignore(msd => msd.Unit)
                .Ignore(msd => msd.LineTotal);

            // === ClinicSettings ===
            modelBuilder.Entity<ClinicSettingsDTO>()
                .ToTable("ClinicSettings")
                .HasKey(cs => cs.SettingId);
            modelBuilder.Entity<ClinicSettingsDTO>()
                .Ignore(cs => cs.OpeningTimeDisplay)
                .Ignore(cs => cs.ClosingTimeDisplay);
        }
    }
}
