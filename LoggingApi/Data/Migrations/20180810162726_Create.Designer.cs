﻿// <auto-generated />
using System;
using Meyer.Logging.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Meyer.Logging.Data.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20180810162726_Create")]
    partial class Create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Meyer.Logging.Data.Application", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.HasKey("Name");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Meyer.Logging.Data.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationName")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("EnvironmentName")
                        .IsRequired();

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("TypeName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationName");

                    b.HasIndex("EnvironmentName");

                    b.HasIndex("TypeName");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Meyer.Logging.Data.EventType", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.HasKey("Name");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("Meyer.Logging.Data.OperatingEnvironment", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.HasKey("Name");

                    b.ToTable("Environments");
                });

            modelBuilder.Entity("Meyer.Logging.Data.Event", b =>
                {
                    b.HasOne("Meyer.Logging.Data.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationName")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meyer.Logging.Data.OperatingEnvironment", "Environment")
                        .WithMany()
                        .HasForeignKey("EnvironmentName")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Meyer.Logging.Data.EventType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeName")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}