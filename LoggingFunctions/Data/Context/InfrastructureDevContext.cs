using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Meyer.Logging.Data.Context
{
    public partial class InfrastructureDevContext : DbContext
    {
        public InfrastructureDevContext()
        {
        }

        public InfrastructureDevContext(DbContextOptions<InfrastructureDevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClientApplication> ClientApplication { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public new virtual DbSet<Entry> Entry { get; set; }
        public virtual DbSet<Environment> Environment { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ClientApplication>(entity =>
            {
                entity.ToTable("ClientApplication", "identity");

                entity.HasIndex(e => e.DisplayName)
                    .IsUnique();

                entity.HasIndex(e => e.NormalizedName)
                    .IsUnique();

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "ent");

                entity.HasIndex(e => e.LodgingId);

                entity.HasIndex(e => e.ManagerId);

                entity.HasIndex(e => e.MileageId);

                entity.HasIndex(e => e.TransactionId);

                entity.HasIndex(e => e.TravelExpenseId);

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => e.VendorExpenseId);

                entity.Property(e => e.CompanyNumber).HasMaxLength(4);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.MiddleName).HasMaxLength(60);

                entity.Property(e => e.UltiProNumber).HasMaxLength(16);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Entry>(entity =>
            {
                entity.HasKey(e => new { e.EnvironmentName, e.Created, e.EmployeeId, e.ClientApplicationId });

                entity.ToTable("Entry", "log");

                entity.HasIndex(e => e.ClientApplicationId);

                entity.HasIndex(e => e.EmployeeId);

                entity.HasIndex(e => e.TypeName);

                entity.Property(e => e.EnvironmentName).HasMaxLength(32);

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.HasOne(d => d.ClientApplication)
                    .WithMany(p => p.Entry)
                    .HasForeignKey(d => d.ClientApplicationId);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Entry)
                    .HasForeignKey(d => d.EmployeeId);

                entity.HasOne(d => d.EnvironmentNameNavigation)
                    .WithMany(p => p.Entry)
                    .HasForeignKey(d => d.EnvironmentName);

                entity.HasOne(d => d.TypeNameNavigation)
                    .WithMany(p => p.Entry)
                    .HasForeignKey(d => d.TypeName);
            });

            modelBuilder.Entity<Environment>(entity =>
            {
                entity.HasKey(e => e.DisplayName);

                entity.ToTable("Environment", "log");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(32)
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(256);
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.DisplayName);

                entity.ToTable("Type", "log");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(16)
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(256);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "identity");

                entity.HasIndex(e => e.DisplayName)
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.HasIndex(e => e.NormalizedEmail)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.NormalizedEmail)
                    .IsRequired()
                    .HasMaxLength(128);
            });
        }
    }
}
