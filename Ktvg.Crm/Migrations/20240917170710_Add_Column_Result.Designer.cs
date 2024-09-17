﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ktvg.Crm.Migrations
{
    [DbContext(typeof(KtvgCrmContext))]
    [Migration("20240917170710_Add_Column_Result")]
    partial class Add_Column_Result
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ktvg.Crm.Models.ContactHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ContactProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("ContactPurposeId")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("DeletedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RescheduleDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContactProjectId");

                    b.HasIndex("ContactPurposeId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeletedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("ContactHistories", "dbo");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.ContactProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeletedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DeletedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("ContactProjects", "dbo");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.ContactPurpose", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeletedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DeletedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("ContactPurposes", "dbo");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DeletedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceInstalled")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("HasZalo")
                        .HasColumnType("bit");

                    b.Property<string>("InstallationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsSendSms")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsSendZalo")
                        .HasColumnType("bit");

                    b.Property<int?>("LocateType")
                        .HasColumnType("int");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("PaymentAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DeletedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Customers", "dbo");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeletedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Sex")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DeletedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Employees", "dbo");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.LoginHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Device")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LoginHistories", "dbo");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.MessageLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<string>("Recipient")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestPayload")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponsePayload")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("MessageLogs", "dbo");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.ZaloOAuth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpireIn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ZaloOAuths", "dbo");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.ContactHistory", b =>
                {
                    b.HasOne("Ktvg.Crm.Models.ContactProject", "ContactProject")
                        .WithMany()
                        .HasForeignKey("ContactProjectId");

                    b.HasOne("Ktvg.Crm.Models.ContactPurpose", "ContactPurpose")
                        .WithMany()
                        .HasForeignKey("ContactPurposeId");

                    b.HasOne("Ktvg.Crm.Models.Employee", "CreatedByEmployee")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Ktvg.Crm.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Ktvg.Crm.Models.Employee", "DeletedByEmployee")
                        .WithMany()
                        .HasForeignKey("DeletedById");

                    b.HasOne("Ktvg.Crm.Models.Employee", "ModifiedByEmployee")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.Navigation("ContactProject");

                    b.Navigation("ContactPurpose");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("Customer");

                    b.Navigation("DeletedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.ContactProject", b =>
                {
                    b.HasOne("Ktvg.Crm.Models.Employee", "CreatedByEmployee")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Ktvg.Crm.Models.Employee", "DeletedByEmployee")
                        .WithMany()
                        .HasForeignKey("DeletedById");

                    b.HasOne("Ktvg.Crm.Models.Employee", "ModifiedByEmployee")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("DeletedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.ContactPurpose", b =>
                {
                    b.HasOne("Ktvg.Crm.Models.Employee", "CreatedByEmployee")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Ktvg.Crm.Models.Employee", "DeletedByEmployee")
                        .WithMany()
                        .HasForeignKey("DeletedById");

                    b.HasOne("Ktvg.Crm.Models.Employee", "ModifiedByEmployee")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("DeletedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.Customer", b =>
                {
                    b.HasOne("Ktvg.Crm.Models.Employee", "CreatedByEmployee")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Ktvg.Crm.Models.Employee", "DeletedByEmployee")
                        .WithMany()
                        .HasForeignKey("DeletedById");

                    b.HasOne("Ktvg.Crm.Models.Employee", "ModifiedByEmployee")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("DeletedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.Employee", b =>
                {
                    b.HasOne("Ktvg.Crm.Models.Employee", "CreatedByEmployee")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Ktvg.Crm.Models.Employee", "DeletedByEmployee")
                        .WithMany()
                        .HasForeignKey("DeletedById");

                    b.HasOne("Ktvg.Crm.Models.Employee", "ModifiedByEmployee")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("DeletedByEmployee");

                    b.Navigation("ModifiedByEmployee");
                });

            modelBuilder.Entity("Ktvg.Crm.Models.MessageLog", b =>
                {
                    b.HasOne("Ktvg.Crm.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}
