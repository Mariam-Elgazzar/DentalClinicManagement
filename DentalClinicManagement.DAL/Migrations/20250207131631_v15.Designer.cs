﻿// <auto-generated />
using System;
using DentalClinicManagement.DAL.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DentalClinicManagement.DAL.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20250207131631_v15")]
    partial class v15
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.Dentist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialist")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Dentists");
                });

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.DentistManagement", b =>
                {
                    b.Property<int>("DId")
                        .HasColumnType("int");

                    b.Property<int>("RId")
                        .HasColumnType("int");

                    b.HasKey("DId", "RId");

                    b.HasIndex("RId");

                    b.ToTable("DentistManagements");
                });

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Allergies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DOB")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.PatientManagement", b =>
                {
                    b.Property<int>("PId")
                        .HasColumnType("int");

                    b.Property<int>("RId")
                        .HasColumnType("int");

                    b.HasKey("PId", "RId");

                    b.HasIndex("RId");

                    b.ToTable("PatientManagements");
                });

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.Prescription", b =>
                {
                    b.Property<int>("PId")
                        .HasColumnType("int");

                    b.Property<int>("TId")
                        .HasColumnType("int");

                    b.Property<int>("DId")
                        .HasColumnType("int");

                    b.HasKey("PId", "TId", "DId");

                    b.HasIndex("DId");

                    b.HasIndex("TId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.Session", b =>
                {
                    b.Property<int>("PId")
                        .HasColumnType("int");

                    b.Property<int>("RId")
                        .HasColumnType("int");

                    b.Property<int>("DId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("PId", "RId", "DId", "dateTime");

                    b.HasIndex("DId");

                    b.HasIndex("RId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.Treatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("DentalClinicManagement.PL.Receptionist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Receptionists");
                });

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.DentistManagement", b =>
                {
                    b.HasOne("DentalClinicManagement.DAL.Models.Dentist", "dentist")
                        .WithMany()
                        .HasForeignKey("DId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DentalClinicManagement.PL.Receptionist", "receptionist")
                        .WithMany()
                        .HasForeignKey("RId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dentist");

                    b.Navigation("receptionist");
                });

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.PatientManagement", b =>
                {
                    b.HasOne("DentalClinicManagement.DAL.Models.Patient", "patient")
                        .WithMany()
                        .HasForeignKey("PId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DentalClinicManagement.PL.Receptionist", "receptionist")
                        .WithMany()
                        .HasForeignKey("RId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("patient");

                    b.Navigation("receptionist");
                });

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.Prescription", b =>
                {
                    b.HasOne("DentalClinicManagement.DAL.Models.Dentist", "dentist")
                        .WithMany()
                        .HasForeignKey("DId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DentalClinicManagement.DAL.Models.Patient", "patient")
                        .WithMany()
                        .HasForeignKey("PId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DentalClinicManagement.DAL.Models.Treatment", "treatment")
                        .WithMany()
                        .HasForeignKey("TId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dentist");

                    b.Navigation("patient");

                    b.Navigation("treatment");
                });

            modelBuilder.Entity("DentalClinicManagement.DAL.Models.Session", b =>
                {
                    b.HasOne("DentalClinicManagement.DAL.Models.Dentist", "dentist")
                        .WithMany()
                        .HasForeignKey("DId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DentalClinicManagement.DAL.Models.Patient", "patient")
                        .WithMany()
                        .HasForeignKey("PId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DentalClinicManagement.PL.Receptionist", "receptionist")
                        .WithMany()
                        .HasForeignKey("RId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dentist");

                    b.Navigation("patient");

                    b.Navigation("receptionist");
                });
#pragma warning restore 612, 618
        }
    }
}
