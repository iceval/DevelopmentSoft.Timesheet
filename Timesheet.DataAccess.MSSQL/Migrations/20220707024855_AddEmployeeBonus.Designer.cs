﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Timesheet.DataAccess.MSSQL;

namespace Timesheet.DataAccess.MSSQL.Migrations
{
    [DbContext(typeof(TimesheetContext))]
    [Migration("20220707024855_AddEmployeeBonus")]
    partial class AddEmployeeBonus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Timesheet.DataAccess.MSSQL.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Bonus")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bonus = 20000m,
                            LastName = "Иванов",
                            Position = 0,
                            Salary = 200000m
                        },
                        new
                        {
                            Id = 2,
                            LastName = "Сидоров",
                            Position = 2,
                            Salary = 120000m
                        },
                        new
                        {
                            Id = 3,
                            LastName = "Петров",
                            Position = 1,
                            Salary = 1000m
                        });
                });

            modelBuilder.Entity("Timesheet.DataAccess.MSSQL.Entities.TimeLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkingHours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("TimeLogs");
                });

            modelBuilder.Entity("Timesheet.DataAccess.MSSQL.Entities.TimeLog", b =>
                {
                    b.HasOne("Timesheet.DataAccess.MSSQL.Entities.Employee", null)
                        .WithMany("timeLogs")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("Timesheet.DataAccess.MSSQL.Entities.Employee", b =>
                {
                    b.Navigation("timeLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
