﻿// <auto-generated />
using System;
using Blood_Link.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Blood_Link.Migrations
{
    [DbContext(typeof(BloodLinkDbContext))]
    [Migration("20241002005130_AddAppointmentSetup")]
    partial class AddAppointmentSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Blood_Link.Models.AppointmentSetup", b =>
                {
                    b.Property<int>("AppointmentSetupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentSetupId"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NurseId")
                        .HasColumnType("int");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("AppointmentSetupId");

                    b.HasIndex("NurseId");

                    b.ToTable("AppointmentSetups");
                });

            modelBuilder.Entity("Blood_Link.Models.Client", b =>
                {
                    b.Property<int>("clientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("clientId"), 1L, 1);

                    b.Property<int>("KapilaNaHospital")
                        .HasColumnType("int");

                    b.Property<int?>("personId")
                        .HasColumnType("int");

                    b.HasKey("clientId");

                    b.HasIndex("personId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Blood_Link.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"), 1L, 1);

                    b.Property<int>("personId")
                        .HasColumnType("int");

                    b.HasKey("DoctorId");

                    b.HasIndex("personId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Blood_Link.Models.Nurse", b =>
                {
                    b.Property<int>("NurseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NurseId"), 1L, 1);

                    b.Property<int>("personId")
                        .HasColumnType("int");

                    b.HasKey("NurseId");

                    b.HasIndex("personId");

                    b.ToTable("Nurses");
                });

            modelBuilder.Entity("Blood_Link.Models.Person", b =>
                {
                    b.Property<int>("personId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("personId"), 1L, 1);

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BloodType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WalletAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("personId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Blood_Link.Models.AppointmentSetup", b =>
                {
                    b.HasOne("Blood_Link.Models.Nurse", "Nurse")
                        .WithMany()
                        .HasForeignKey("NurseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nurse");
                });

            modelBuilder.Entity("Blood_Link.Models.Client", b =>
                {
                    b.HasOne("Blood_Link.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("personId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Blood_Link.Models.Doctor", b =>
                {
                    b.HasOne("Blood_Link.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("personId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Blood_Link.Models.Nurse", b =>
                {
                    b.HasOne("Blood_Link.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("personId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });
#pragma warning restore 612, 618
        }
    }
}
