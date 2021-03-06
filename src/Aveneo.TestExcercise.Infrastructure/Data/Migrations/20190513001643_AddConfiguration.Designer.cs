﻿// <auto-generated />
using System;
using Aveneo.TestExcercise.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aveneo.TestExcercise.Infrastructure.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190513001643_AddConfiguration")]
    partial class AddConfiguration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aveneo.TestExcercise.ApplicationCore.Entities.Configuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("Aveneo.TestExcercise.ApplicationCore.Entities.DataObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(19,4)");

                    b.HasKey("Id");

                    b.ToTable("DataObjects");
                });

            modelBuilder.Entity("Aveneo.TestExcercise.ApplicationCore.Entities.DataObjectFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DataObjectId");

                    b.Property<int>("FeatureId");

                    b.HasKey("Id");

                    b.ToTable("DateObjectFeatures");
                });

            modelBuilder.Entity("Aveneo.TestExcercise.ApplicationCore.Entities.DataObjectGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DataObjectId");

                    b.Property<Guid>("FileName");

                    b.Property<int>("Sequence");

                    b.HasKey("Id");

                    b.ToTable("DataObjectGalleries");
                });

            modelBuilder.Entity("Aveneo.TestExcercise.ApplicationCore.Entities.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IconName");

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("Aveneo.TestExcercise.ApplicationCore.Entities.DataObject", b =>
                {
                    b.OwnsOne("Aveneo.TestExcercise.ApplicationCore.Entities.Geography", "Location", b1 =>
                        {
                            b1.Property<int>("DataObjectId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Latitude")
                                .HasColumnType("decimal(19,4)");

                            b1.Property<decimal>("Longitude")
                                .HasColumnType("decimal(19,4)");

                            b1.HasKey("DataObjectId");

                            b1.ToTable("DataObjects");

                            b1.HasOne("Aveneo.TestExcercise.ApplicationCore.Entities.DataObject")
                                .WithOne("Location")
                                .HasForeignKey("Aveneo.TestExcercise.ApplicationCore.Entities.Geography", "DataObjectId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
