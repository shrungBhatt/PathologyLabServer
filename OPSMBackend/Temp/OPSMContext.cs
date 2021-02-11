using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OPSMBackend.Temp
{
    public partial class OPSMContext : DbContext
    {
        public OPSMContext()
        {
        }

        public OPSMContext(DbContextOptions<OPSMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientBill> PatientBill { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SHRUNG\\SQLEXPRESS;Database=OPSM;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patient");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.AgeInDays).HasColumnName("age_in_days");

                entity.Property(e => e.AgeInMonths).HasColumnName("age_in_months");

                entity.Property(e => e.AgeInYears).HasColumnName("age_in_years");

                entity.Property(e => e.CivilStatus).HasColumnName("civil_status");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.InitialId).HasColumnName("initial_id");

                entity.Property(e => e.IsFinished).HasColumnName("is_finished");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .HasColumnName("mobile_no")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Occupation)
                    .HasColumnName("occupation")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PatientCode)
                    .IsRequired()
                    .HasColumnName("patient_code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasColumnName("phone_no")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.ReferredBy)
                    .HasColumnName("referred_by")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnName("registration_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<PatientBill>(entity =>
            {
                entity.ToTable("patient_bill");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AmountPaid).HasColumnName("amount_paid");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.BillDate)
                    .HasColumnName("bill_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.BillNo).HasColumnName("bill_no");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.DiscountedAmount).HasColumnName("discounted_amount");

                entity.Property(e => e.Gst).HasColumnName("gst");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.TotalCharges).HasColumnName("total_charges");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientBill)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__patient_b__patie__1B9317B3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
