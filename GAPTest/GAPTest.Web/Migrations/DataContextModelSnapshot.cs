﻿// <auto-generated />
using System;
using GAPTest.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GAPTest.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GAPTest.Web.Data.Entities.CoveringType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("CoveringType");
                });

            modelBuilder.Entity("GAPTest.Web.Data.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("GAPTest.Web.Data.Entities.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CoveringPeriod");

                    b.Property<int?>("CoveringTypeId");

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PolicyName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("PolicyStartDate");

                    b.Property<double>("Price");

                    b.Property<int?>("RiskTypeId");

                    b.HasKey("Id");

                    b.HasIndex("CoveringTypeId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RiskTypeId");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("GAPTest.Web.Data.Entities.PolicyCustomer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("CoveringPercentage");

                    b.Property<int?>("CustomerId");

                    b.Property<int?>("PolicyId");

                    b.Property<bool>("State");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PolicyId");

                    b.ToTable("PolicyCustomer");
                });

            modelBuilder.Entity("GAPTest.Web.Data.Entities.RiskType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("RiskType");
                });

            modelBuilder.Entity("GAPTest.Web.Data.Entities.Policy", b =>
                {
                    b.HasOne("GAPTest.Web.Data.Entities.CoveringType", "CoveringType")
                        .WithMany("Policies")
                        .HasForeignKey("CoveringTypeId");

                    b.HasOne("GAPTest.Web.Data.Entities.Customer", "Customer")
                        .WithMany("Policies")
                        .HasForeignKey("CustomerId");

                    b.HasOne("GAPTest.Web.Data.Entities.RiskType", "RiskType")
                        .WithMany()
                        .HasForeignKey("RiskTypeId");
                });

            modelBuilder.Entity("GAPTest.Web.Data.Entities.PolicyCustomer", b =>
                {
                    b.HasOne("GAPTest.Web.Data.Entities.Customer", "Customer")
                        .WithMany("PolicyCustomers")
                        .HasForeignKey("CustomerId");

                    b.HasOne("GAPTest.Web.Data.Entities.Policy", "Policy")
                        .WithMany("PolicyCustomers")
                        .HasForeignKey("PolicyId");
                });
#pragma warning restore 612, 618
        }
    }
}
