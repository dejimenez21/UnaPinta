﻿// <auto-generated />
using System;
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Migrations
{
    [DbContext(typeof(UnaPintaDBContext))]
    partial class UnaPintaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Api.Entities.BloodComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("BloodComponents");
                });

            modelBuilder.Entity("Api.Entities.BloodType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.ToTable("BloodTypes");
                });

            modelBuilder.Entity("Api.Entities.Condition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Decription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Conditions");
                });

            modelBuilder.Entity("Api.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<int?>("BloodComponentId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("RequesterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BloodComponentId");

                    b.HasIndex("RequesterId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Api.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BloodTypeId")
                        .HasColumnType("int");

                    b.Property<bool?>("CanDonate")
                        .HasColumnType("bit");

                    b.Property<bool?>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Handle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.Property<int?>("UserTypeId")
                        .HasColumnType("int");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BloodTypeId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Api.Entities.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("Api.Entities.WaitList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("AvailableAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ConditionId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConditionId");

                    b.HasIndex("UserId");

                    b.ToTable("WaitLists");
                });

            modelBuilder.Entity("Api.Entities.Request", b =>
                {
                    b.HasOne("Api.Entities.BloodComponent", "BloodComponentNav")
                        .WithMany("Requests")
                        .HasForeignKey("BloodComponentId");

                    b.HasOne("Api.Entities.User", "RequesterNav")
                        .WithMany("Requests")
                        .HasForeignKey("RequesterId");

                    b.Navigation("BloodComponentNav");

                    b.Navigation("RequesterNav");
                });

            modelBuilder.Entity("Api.Entities.User", b =>
                {
                    b.HasOne("Api.Entities.BloodType", "BloodTypeNav")
                        .WithMany("Users")
                        .HasForeignKey("BloodTypeId");

                    b.HasOne("Api.Entities.UserType", "UserTypeNav")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeId");

                    b.Navigation("BloodTypeNav");

                    b.Navigation("UserTypeNav");
                });

            modelBuilder.Entity("Api.Entities.WaitList", b =>
                {
                    b.HasOne("Api.Entities.Condition", "ConditionNav")
                        .WithMany("WaitLists")
                        .HasForeignKey("ConditionId");

                    b.HasOne("Api.Entities.User", "UserNav")
                        .WithMany("WaitLists")
                        .HasForeignKey("UserId");

                    b.Navigation("ConditionNav");

                    b.Navigation("UserNav");
                });

            modelBuilder.Entity("Api.Entities.BloodComponent", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("Api.Entities.BloodType", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Api.Entities.Condition", b =>
                {
                    b.Navigation("WaitLists");
                });

            modelBuilder.Entity("Api.Entities.User", b =>
                {
                    b.Navigation("Requests");

                    b.Navigation("WaitLists");
                });

            modelBuilder.Entity("Api.Entities.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
