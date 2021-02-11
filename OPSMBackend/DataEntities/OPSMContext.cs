using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OPSMBackend.DataEntities
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

        public virtual DbSet<Abbrevations> Abbrevations { get; set; }
        public virtual DbSet<Dealers> Dealers { get; set; }
        public virtual DbSet<EmployeeCategories> EmployeeCategories { get; set; }
        public virtual DbSet<EmployeeRoles> EmployeeRoles { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Genders> Genders { get; set; }
        public virtual DbSet<HdlRegistration> HdlRegistration { get; set; }
        public virtual DbSet<Initials> Initials { get; set; }
        public virtual DbSet<Inventories> Inventories { get; set; }
        public virtual DbSet<OtherTests> OtherTests { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<ReagentBillEntries> ReagentBillEntries { get; set; }
        public virtual DbSet<Reagents> Reagents { get; set; }
        public virtual DbSet<RegistrationCategories> RegistrationCategories { get; set; }
        public virtual DbSet<RegistrationTypes> RegistrationTypes { get; set; }
        public virtual DbSet<RoleTypes> RoleTypes { get; set; }
        public virtual DbSet<Salary> Salary { get; set; }
        public virtual DbSet<SignaturePrototypes> SignaturePrototypes { get; set; }
        public virtual DbSet<TestGroups> TestGroups { get; set; }
        public virtual DbSet<TestReagentRelation> TestReagentRelation { get; set; }
        public virtual DbSet<TestResults> TestResults { get; set; }
        public virtual DbSet<TestResultsView> TestResultsView { get; set; }
        public virtual DbSet<TestTitles> TestTitles { get; set; }
        public virtual DbSet<UserRights> UserRights { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<FieldOptions> FieldOptions { get; set; }
        public virtual DbSet<Fields> Fields { get; set; }
        public virtual DbSet<AccountHead> AccountHead { get; set; }
        public virtual DbSet<OtherIncome> OtherIncome { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<ExpensesAccountHead> ExpensesAccountHead { get; set; }
        public virtual DbSet<SalaryPayment> SalaryPayment { get; set; }
        public virtual DbSet<PatientCounts> PatientCounts { get; set; }
        public virtual DbSet<MonthlyRateList> MonthlyRateList { get; set; }
        public virtual DbSet<ReferringRateList> ReferringRateList { get; set; }
        public virtual DbSet<SpecializedLabRateList> SpecializedLabRateList { get; set; }
        public virtual DbSet<SpecializedLabSamples> SpecializedLabSamples { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abbrevations>(entity =>
            {
                entity.ToTable("abbrevations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abbreavation)
                    .IsRequired()
                    .HasColumnName("abbreavation")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Interpretation)
                    .IsRequired()
                    .HasColumnName("interpretation")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SerialNo).HasColumnName("serial_no");

                entity.Property(e => e.OtherTestId).HasColumnName("other_test_id");

                entity.HasOne(d => d.OtherTest)
                    .WithMany(p => p.Abbrevations)
                    .HasForeignKey(d => d.OtherTestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__abbrevati__other__3A179ED3");
            });

            modelBuilder.Entity<Dealers>(entity =>
            {
                entity.ToTable("dealers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(20)
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

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasColumnName("phone_no")
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeCategories>(entity =>
            {
                entity.ToTable("employee_categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmployeeCategory)
                    .IsRequired()
                    .HasColumnName("employee_category")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeRoles>(entity =>
            {
                entity.ToTable("employee_roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EmployeeRole)
                    .IsRequired()
                    .HasColumnName("employee_role")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpCategoryId).HasColumnName("emp_category_id");

                entity.Property(e => e.EmployeeRoleId).HasColumnName("employee_role_id");

                entity.Property(e => e.FieldOptionsId).HasColumnName("field_options_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GenderId).HasColumnName("gender_id");

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

                entity.Property(e => e.PhoneNo)
                    .HasColumnName("phone_no")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Supervisor).HasColumnName("supervisor");

                entity.HasOne(d => d.EmpCategory)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmpCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__employees__emp_c__4C364F0E");

                entity.HasOne(d => d.EmployeeRole)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__employees__emplo__4E1E9780");

                entity.HasOne(d => d.FieldOptions)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.FieldOptionsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__employees__field__4D2A7347");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__employees__gende__4B422AD5");
            });

            modelBuilder.Entity<Formulas>(entity =>
            {
                entity.ToTable("formulas");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Formula)
                    .IsRequired()
                    .HasColumnName("formula")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.TitleId).HasColumnName("title_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Formulas)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__formulas__group___09746778");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Formulas)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__formulas__test_i__756D6ECB");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.Formulas)
                    .HasForeignKey(d => d.TitleId)
                    .HasConstraintName("FK__formulas__title___0880433F");
            });

            modelBuilder.Entity<Genders>(entity =>
            {
                entity.ToTable("genders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HdlRegistration>(entity =>
            {
                entity.ToTable("hdl_registration");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdditionalNotes)
                    .HasColumnName("additional_notes")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .HasColumnName("mobile_no")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasColumnName("phone_no")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationCategoryId).HasColumnName("registration_category_id");

                entity.Property(e => e.RegistrationTypeId).HasColumnName("registration_type_id");

                entity.HasOne(d => d.RegistrationCategory)
                    .WithMany(p => p.HdlRegistration)
                    .HasForeignKey(d => d.RegistrationCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__hdl_regis__regis__4BAC3F29");

                entity.HasOne(d => d.RegistrationType)
                    .WithMany(p => p.HdlRegistration)
                    .HasForeignKey(d => d.RegistrationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__hdl_regis__regis__4AB81AF0");
            });

            modelBuilder.Entity<Initials>(entity =>
            {
                entity.ToTable("initials");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Initial)
                    .IsRequired()
                    .HasColumnName("initial")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inventories>(entity =>
            {
                entity.ToTable("inventories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasColumnName("item_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ItemType).HasColumnName("item_type");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<OtherTests>(entity =>
            {
                entity.ToTable("other_tests");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DefaultValue)
                    .HasColumnName("default_value")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.DescriptiveResult).HasColumnName("descriptive_result");

                entity.Property(e => e.DisplayInBoldFontInReport).HasColumnName("display_in_bold_font_in_report");

                entity.Property(e => e.DisplayInTestResult).HasColumnName("display_in_test_result");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasColumnName("modified_by")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Options)
                    .HasColumnName("options")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OrderBy).HasColumnName("order_by");

                entity.Property(e => e.TestGroupId).HasColumnName("test_group_id");

                entity.Property(e => e.TestTitleId).HasColumnName("test_title_id");

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValChild)
                    .HasColumnName("val_child")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ValFemale)
                    .HasColumnName("val_female")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ValMale)
                    .HasColumnName("val_male")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ValNoenatal)
                    .HasColumnName("val_noenatal")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.TestGroup)
                    .WithMany(p => p.OtherTests)
                    .HasForeignKey(d => d.TestGroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__other_tes__test___3B75D760");

                entity.HasOne(d => d.TestTitle)
                    .WithMany(p => p.OtherTests)
                    .HasForeignKey(d => d.TestTitleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__other_tes__test___3C69FB99");
            });

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

                entity.Property(e => e.IsFinished).HasColumnName("is_finished");

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

                entity.HasOne(d => d.CivilStatusNavigation)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.CivilStatus)
                    .HasConstraintName("FK__patient__civil_s__79FD19BE");

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.Gender)
                    .HasConstraintName("FK__patient__gender__2E1BDC42");

                entity.HasOne(d => d.Initial)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.InitialId)
                    .HasConstraintName("FK__patient__initial__2D27B809");
            });

            modelBuilder.Entity<ReagentBillEntries>(entity =>
            {
                entity.ToTable("reagent_bill_entries");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.BillDate)
                    .HasColumnName("bill_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.BillNo).HasColumnName("bill_no");

                entity.Property(e => e.DealerId).HasColumnName("dealer_id");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnName("delivery_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnName("expiry_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsPaid).HasColumnName("is_paid");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.ReagentId).HasColumnName("reagent_id");

                entity.HasOne(d => d.Dealer)
                    .WithMany(p => p.ReagentBillEntries)
                    .HasForeignKey(d => d.DealerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reagent_b__deale__72C60C4A");

                entity.HasOne(d => d.Reagent)
                    .WithMany(p => p.ReagentBillEntries)
                    .HasForeignKey(d => d.ReagentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reagent_b__reage__73BA3083");
            });

            modelBuilder.Entity<Reagents>(entity =>
            {
                entity.ToTable("reagents");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdditionalNotes)
                    .HasColumnName("additional_notes")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiryDate)
                    .HasColumnName("expiry_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PackingSize).HasColumnName("packing_size");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnName("purchase_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReorderLevel).HasColumnName("reorder_level");

                entity.Property(e => e.UnitInStock).HasColumnName("unit_in_stock");

                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
            });

            modelBuilder.Entity<RegistrationCategories>(entity =>
            {
                entity.ToTable("registration_categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RegistrationCategory)
                    .IsRequired()
                    .HasColumnName("registration_category")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RegistrationTypes>(entity =>
            {
                entity.ToTable("registration_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RegistrationType)
                    .IsRequired()
                    .HasColumnName("registration_type")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleTypes>(entity =>
            {
                entity.ToTable("role_types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("salary");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountNo).HasColumnName("account_no");

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasColumnName("bank_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BasicSalary).HasColumnName("basic_salary");

                entity.Property(e => e.CommunicationAllowance).HasColumnName("communication_allowance");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Hra).HasColumnName("hra");

                entity.Property(e => e.LearningAllowance).HasColumnName("learning_allowance");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SalaryAmount).HasColumnName("salary_amount");

                entity.Property(e => e.TransportationAllowance).HasColumnName("transportation_allowance");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__salary__employee__3552E9B6");
            });

            modelBuilder.Entity<SignaturePrototypes>(entity =>
            {
                entity.ToTable("signature_prototypes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Degree)
                    .IsRequired()
                    .HasColumnName("degree")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorName)
                    .IsRequired()
                    .HasColumnName("doctor_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Specialization)
                    .IsRequired()
                    .HasColumnName("specialization")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TestGroups>(entity =>
            {
                entity.ToTable("test_groups");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNo).HasColumnName("order_no");

                entity.Property(e => e.ShowTitleInReports).HasColumnName("show_title_in_reports");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__patient_b__patie__1B9317B3");
            });

            modelBuilder.Entity<TestReagentRelation>(entity =>
            {
                entity.ToTable("test_reagent_relation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OtherTestId).HasColumnName("other_test_id");

                entity.Property(e => e.QtyPerTest).HasColumnName("qty_per_test");

                entity.Property(e => e.ReagentId).HasColumnName("reagent_id");

                entity.Property(e => e.Unit).HasColumnName("unit");

                entity.HasOne(d => d.OtherTest)
                    .WithMany(p => p.TestReagentRelation)
                    .HasForeignKey(d => d.OtherTestId)
                    .HasConstraintName("FK__test_reag__other__2CBDA3B5");

                entity.HasOne(d => d.Reagent)
                    .WithMany(p => p.TestReagentRelation)
                    .HasForeignKey(d => d.ReagentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__test_reag__reage__4316F928");
            });

            modelBuilder.Entity<TestResults>(entity =>
            {
                entity.ToTable("test_results");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.LargeTestResult)
                    .HasColumnName("large_test_result")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.TestResult)
                    .HasColumnName("test_result")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PatientCode)
                    .HasColumnName("patient_code")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.TitleId).HasColumnName("title_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__test_resu__group__4F47C5E3");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__test_resu__test___51300E55");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.TitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__test_resu__title__503BEA1C");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TestResultsCollection)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__test_resu__patie__55009F39");
            });

            modelBuilder.Entity<TestResultsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("test_results_view");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("group_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LargeTestResult)
                    .HasColumnName("large_test_result")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OtherTestId).HasColumnName("other_test_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.TestName)
                    .IsRequired()
                    .HasColumnName("test_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TestResult)
                    .HasColumnName("test_result")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TitleId).HasColumnName("title_id");

                entity.Property(e => e.TestResultId).HasColumnName("test_result_id");

                entity.Property(e => e.TitleName)
                    .IsRequired()
                    .HasColumnName("title_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NormalRange)
                    .HasColumnName("normal_range")
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TestTitles>(entity =>
            {
                entity.ToTable("test_titles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Charges).HasColumnName("charges");

                entity.Property(e => e.OrderBy).HasColumnName("order_by");

                entity.Property(e => e.FooterNote)
                    .HasColumnName("footer_note")
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.HeaderNote)
                    .HasColumnName("header_note")
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ShowNormalRangeHeader).HasColumnName("show_normal_range_header");

                entity.Property(e => e.WordFormatResult).HasColumnName("word_format_result");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TestTitles)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__test_titl__group__37A5467C");
            });

            modelBuilder.Entity<UserRights>(entity =>
            {
                entity.ToTable("user_rights");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.About).HasColumnName("about");

                entity.Property(e => e.AddOption).HasColumnName("add_option");

                entity.Property(e => e.DeleteOption).HasColumnName("delete_option");

                entity.Property(e => e.EditOption).HasColumnName("edit_option");

                entity.Property(e => e.Finance).HasColumnName("finance");

                entity.Property(e => e.Help).HasColumnName("help");

                entity.Property(e => e.IncludeDigitalSignature).HasColumnName("include_digital_signature");

                entity.Property(e => e.Maintenance).HasColumnName("maintenance");

                entity.Property(e => e.Navigation).HasColumnName("navigation");

                entity.Property(e => e.Registration).HasColumnName("registration");

                entity.Property(e => e.Report).HasColumnName("report");

                entity.Property(e => e.RoleTypeId).HasColumnName("role_type_id");

                entity.Property(e => e.TestResult).HasColumnName("test_result");

                entity.Property(e => e.Tools).HasColumnName("tools");

                entity.Property(e => e.UserManagement).HasColumnName("user_management");

                entity.Property(e => e.Windows).HasColumnName("windows");

                entity.HasOne(d => d.RoleType)
                    .WithMany(p => p.UserRights)
                    .HasForeignKey(d => d.RoleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_righ__role___7F2BE32F");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Degree)
                    .HasColumnName("degree")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .HasColumnName("email_id")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Specialization)
                    .HasColumnName("specialization")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__users__role_id__145C0A3F");
            });

            modelBuilder.Entity<FieldOptions>(entity =>
            {
                entity.ToTable("field_options");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FieldId).HasColumnName("field_id");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.FieldOptions)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__field_opt__field__318258D2");
            });

            modelBuilder.Entity<Fields>(entity =>
            {
                entity.ToTable("fields");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<AccountHead>(entity =>
            {
                entity.ToTable("account_head");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OtherIncome>(entity =>
            {
                entity.ToTable("other_income");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountHeadId).HasColumnName("account_head_id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.BankName)
                    .HasColumnName("bank_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Branch)
                    .HasColumnName("branch")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CardMode).HasColumnName("card_mode");

                entity.Property(e => e.CardNo)
                    .HasColumnName("card_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CashMode).HasColumnName("cash_mode");

                entity.Property(e => e.ChequeDate)
                    .HasColumnName("cheque_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChequeMode).HasColumnName("cheque_mode");

                entity.Property(e => e.ChequeNo)
                    .HasColumnName("cheque_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IncomeDate)
                    .HasColumnName("income_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NatureOfWork)
                    .IsRequired()
                    .HasColumnName("nature_of_work")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PaidBy)
                    .HasColumnName("paid_by")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiptNo).HasColumnName("receipt_no");

                entity.HasOne(d => d.AccountHead)
                    .WithMany(p => p.OtherIncome)
                    .HasForeignKey(d => d.AccountHeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__other_inc__accou__56B3DD81");
            });

            modelBuilder.Entity<Expenses>(entity =>
            {
                entity.ToTable("expenses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountHeadId).HasColumnName("account_head_id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.BankName)
                    .HasColumnName("bank_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BillNo).HasColumnName("bill_no");

                entity.Property(e => e.Branch)
                    .HasColumnName("branch")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CardMode).HasColumnName("card_mode");

                entity.Property(e => e.CardNo)
                    .HasColumnName("card_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CashMode).HasColumnName("cash_mode");

                entity.Property(e => e.ChequeDate)
                    .HasColumnName("cheque_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChequeMode).HasColumnName("cheque_mode");

                entity.Property(e => e.ChequeNo)
                    .HasColumnName("cheque_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryDate)
                    .HasColumnName("delivery_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NatureOfWork)
                    .IsRequired()
                    .HasColumnName("nature_of_work")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaidBy)
                    .HasColumnName("paid_by")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.VoucherNo)
                    .HasColumnName("voucher_no")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccountHead)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.AccountHeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__expenses__accoun__69C6B1F5");
            });

            modelBuilder.Entity<ExpensesAccountHead>(entity =>
            {
                entity.ToTable("expenses_account_head");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SalaryPayment>(entity =>
            {
                entity.ToTable("salary_payment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BankName)
                    .HasColumnName("bank_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasColumnName("branch_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CardMode).HasColumnName("card_mode");

                entity.Property(e => e.CardNo)
                    .HasColumnName("card_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CashMode).HasColumnName("cash_mode");

                entity.Property(e => e.ChequeDate)
                    .HasColumnName("cheque_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChequeMode).HasColumnName("cheque_mode");

                entity.Property(e => e.ChequeNo)
                    .HasColumnName("cheque_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.LoanDeduction)
                    .HasColumnName("loan_deduction")
                    .HasColumnType("money");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.MonthName)
                    .IsRequired()
                    .HasColumnName("month_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProfessionalTax)
                    .HasColumnName("professional_tax")
                    .HasColumnType("money");

                entity.Property(e => e.ProvidentFund)
                    .HasColumnName("provident_fund")
                    .HasColumnType("money");

                entity.Property(e => e.SalaryDate)
                    .HasColumnName("salary_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SalaryPayable)
                    .HasColumnName("salary_payable")
                    .HasColumnType("money");

                entity.Property(e => e.WorkDays).HasColumnName("work_days");

                entity.Property(e => e.YearName).HasColumnName("year_name");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.SalaryPayment)
                    .HasForeignKey(d => d.EmpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__salary_pa__emp_i__6CA31EA0");
            });

            modelBuilder.Entity<PatientBillPayment>(entity =>
            {
                entity.ToTable("patient_bill_payment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Balance)
                    .HasColumnName("balance")
                    .HasColumnType("money");

                entity.Property(e => e.BankName)
                    .HasColumnName("bank_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BillId).HasColumnName("bill_id");

                entity.Property(e => e.BillPaidBy)
                    .HasColumnName("bill_paid_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasColumnName("branch_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CardMode).HasColumnName("card_mode");

                entity.Property(e => e.CardNo)
                    .HasColumnName("card_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CashMode).HasColumnName("cash_mode");

                entity.Property(e => e.ChequeDate)
                    .HasColumnName("cheque_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChequeMode).HasColumnName("cheque_mode");

                entity.Property(e => e.ChequeNo)
                    .HasColumnName("cheque_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnName("payment_amount")
                    .HasColumnType("money");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("payment_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReceiptNo).HasColumnName("receipt_no");

                entity.Property(e => e.PaymentType).HasColumnName("payment_type");

                entity.HasOne(d => d.PaymentTypeNavigation)
                    .WithMany(p => p.PatientBillPayment)
                    .HasForeignKey(d => d.PaymentType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__patient_b__payme__7720AD13");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.PatientBillPayment)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__patient_b__bill___762C88DA");
            });

            modelBuilder.Entity<PatientCounts>(entity =>
            {
                entity.ToTable("patient_counts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MonthName).HasColumnName("month_name");

                entity.Property(e => e.PatientCount).HasColumnName("patient_count");

                entity.Property(e => e.YearName).HasColumnName("year_name");
            });

            modelBuilder.Entity<MonthlyRateList>(entity =>
            {
                entity.ToTable("monthly_rate_list");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Charges).HasColumnName("charges");

                entity.Property(e => e.HdlId).HasColumnName("hdl_id");

                entity.Property(e => e.TestTitleId).HasColumnName("test_title_id");

                entity.HasOne(d => d.Hdl)
                    .WithMany(p => p.MonthlyRateList)
                    .HasForeignKey(d => d.HdlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__monthly_r__hdl_i__37FA4C37");

                entity.HasOne(d => d.TestTitle)
                    .WithMany(p => p.MonthlyRateList)
                    .HasForeignKey(d => d.TestTitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__monthly_r__test___38EE7070");
            });

            modelBuilder.Entity<ReferringRateList>(entity =>
            {
                entity.ToTable("referring_rate_list");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HdlId).HasColumnName("hdl_id");

                entity.Property(e => e.ReferringAmount).HasColumnName("referring_amount");

                entity.Property(e => e.ReferringPercentage).HasColumnName("referring_percentage");

                entity.Property(e => e.TestTitleId).HasColumnName("test_title_id");

                entity.HasOne(d => d.Hdl)
                    .WithMany(p => p.ReferringRateList)
                    .HasForeignKey(d => d.HdlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__referring__hdl_i__3429BB53");

                entity.HasOne(d => d.TestTitle)
                    .WithMany(p => p.ReferringRateList)
                    .HasForeignKey(d => d.TestTitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__referring__test___351DDF8C");
            });

            modelBuilder.Entity<SpecializedLabRateList>(entity =>
            {
                entity.ToTable("specialized_lab_rate_list");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Charges).HasColumnName("charges");

                entity.Property(e => e.HdlId).HasColumnName("hdl_id");

                entity.Property(e => e.TestTitleId).HasColumnName("test_title_id");

                entity.HasOne(d => d.Hdl)
                    .WithMany(p => p.SpecializedLabRateList)
                    .HasForeignKey(d => d.HdlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__specializ__hdl_i__3BCADD1B");

                entity.HasOne(d => d.TestTitle)
                    .WithMany(p => p.SpecializedLabRateList)
                    .HasForeignKey(d => d.TestTitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__specializ__test___3CBF0154");
            });

            modelBuilder.Entity<HdlBill>(entity =>
            {
                entity.ToTable("hdl_bill");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AmountPaid)
                    .HasColumnName("amount_paid")
                    .HasColumnType("money");

                entity.Property(e => e.Balance)
                    .HasColumnName("balance")
                    .HasColumnType("money");

                entity.Property(e => e.BillDate)
                    .HasColumnName("bill_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.BillNo).HasColumnName("bill_no");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Gst).HasColumnName("gst");

                entity.Property(e => e.HdlId).HasColumnName("hdl_id");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.TotalCharges)
                    .HasColumnName("total_charges")
                    .HasColumnType("money");

                entity.HasOne(d => d.Hdl)
                    .WithMany(p => p.HdlBill)
                    .HasForeignKey(d => d.HdlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__hdl_bill__hdl_id__5A4F643B");
            });

            modelBuilder.Entity<HdlBillPayment>(entity =>
            {
                entity.ToTable("hdl_bill_payment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Balance)
                    .HasColumnName("balance")
                    .HasColumnType("money");

                entity.Property(e => e.BankName)
                    .HasColumnName("bank_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BillId).HasColumnName("bill_id");

                entity.Property(e => e.BillPaidBy)
                    .HasColumnName("bill_paid_by")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasColumnName("branch_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CardMode).HasColumnName("card_mode");

                entity.Property(e => e.CardNo)
                    .HasColumnName("card_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CashMode).HasColumnName("cash_mode");

                entity.Property(e => e.ChequeDate)
                    .HasColumnName("cheque_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChequeMode).HasColumnName("cheque_mode");

                entity.Property(e => e.ChequeNo)
                    .HasColumnName("cheque_no")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnName("payment_amount")
                    .HasColumnType("money");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("payment_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentType).HasColumnName("payment_type");

                entity.Property(e => e.ReceiptNo).HasColumnName("receipt_no");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.HdlBillPayment)
                    .HasForeignKey(d => d.BillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__hdl_bill___bill___5D2BD0E6");

                entity.HasOne(d => d.PaymentTypeNavigation)
                    .WithMany(p => p.HdlBillPayment)
                    .HasForeignKey(d => d.PaymentType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__hdl_bill___payme__5E1FF51F");
            });

            modelBuilder.Entity<SpecializedLabSamples>(entity =>
            {
                entity.ToTable("specialized_lab_samples");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LabId).HasColumnName("lab_id");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.TestTitleId).HasColumnName("test_title_id");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.SpecializedLabSamples)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__specializ__lab_i__00750D23");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.SpecializedLabSamples)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__specializ__patie__0169315C");

                entity.HasOne(d => d.TestTitle)
                    .WithMany(p => p.SpecializedLabSamples)
                    .HasForeignKey(d => d.TestTitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__specializ__test___119F9925");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
