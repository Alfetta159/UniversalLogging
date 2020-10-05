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
        public new virtual DbSet<Entry> Entry { get; set; }
        public virtual DbSet<Environment> Environment { get; set; }
        public virtual DbSet<Severity> Severity { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=meyerinfrastructure.database.windows.net;Initial Catalog=InfrastructureDev;Integrated Security=False;User ID=meyerdev;Password={DC721A29-6D7D-4F7D-91A6-BD037A7D627B};Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Entry>(entity =>
            {
                entity.HasKey(e => new { e.EnvironmentName, e.Created, e.UserId, e.ClientApplicationId });

                entity.ToTable("Entry", "log");

                entity.HasIndex(e => e.ClientApplicationId);

                entity.HasIndex(e => e.SeverityName);

                entity.Property(e => e.EnvironmentName).HasMaxLength(32);

                entity.Property(e => e.UserId).HasDefaultValueSql("(N'')");

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.SeverityName)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.HasOne(d => d.ClientApplication)
                    .WithMany(p => p.Entry)
                    .HasForeignKey(d => d.ClientApplicationId);

                entity.HasOne(d => d.Environment)
                    .WithMany(p => p.Entry)
                    .HasForeignKey(d => d.EnvironmentName);

                entity.HasOne(d => d.Severity)
                    .WithMany(p => p.Entry)
                    .HasForeignKey(d => d.SeverityName);
            });

            modelBuilder.Entity<Environment>(entity =>
            {
                entity.HasKey(e => e.DisplayName);

                entity.ToTable("Environment", "log");

                entity.Property(e => e.DisplayName).HasMaxLength(32);

                entity.Property(e => e.Description).HasMaxLength(256);
            });

            modelBuilder.Entity<Severity>(entity =>
            {
                entity.HasKey(e => e.DisplayName);

                entity.ToTable("Severity", "log");

                entity.Property(e => e.DisplayName).HasMaxLength(16);

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

                entity.Property(e => e.Id).HasMaxLength(36);

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
