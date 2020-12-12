using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Meyer.Logging.Data.Context
{
    public partial class InfrastructureContext : DbContext
    {
        public InfrastructureContext()
        {
        }

        public InfrastructureContext(DbContextOptions<InfrastructureContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClientApplication> ClientApplications { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<Severity> Severities { get; set; }
        public virtual DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
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
                entity.HasKey(e => new { e.Created, e.ClientApplicationId });

                entity.ToTable("Entry", "log");

                entity.HasIndex(e => e.ClientApplicationId);

                entity.HasIndex(e => e.SeverityName);

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.SeverityName)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasDefaultValueSql("(N'')");

                entity.HasOne(d => d.ClientApplication)
                    .WithMany(p => p.Entry)
                    .HasForeignKey(d => d.ClientApplicationId);

                entity.HasOne(d => d.SeverityNameNavigation)
                    .WithMany(p => p.Entry)
                    .HasForeignKey(d => d.SeverityName);
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
