﻿// <auto-generated />
using System;
using DbMigratorJob;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbMigratorJob.Migrations
{
    [DbContext(typeof(MigratorDbContext))]
    [Migration("20230211173919_RemoveDescriptionFromPaIndex")]
    partial class RemoveDescriptionFromPaIndex
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("DataModel.Model.EscalationItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PreviousPrice")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SubfieldId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SubfieldId");

                    b.ToTable("EscalationItems");
                });

            modelBuilder.Entity("DataModel.Model.EscalationItemRow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EscalationItemId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EscalationItemId");

                    b.ToTable("EscalationItemRows");
                });

            modelBuilder.Entity("DataModel.Model.PAIndex", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubfieldId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TimeBoxId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("SubfieldId");

                    b.HasIndex("TimeBoxId");

                    b.ToTable("PAIndexes");
                });

            modelBuilder.Entity("DataModel.Model.Subfield", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Subfields");
                });

            modelBuilder.Entity("DataModel.Model.TimeBox", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<string>("Month")
                        .HasColumnType("TEXT");

                    b.Property<int>("SolarYear")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<int>("ThreeMonthNo")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("TimeBoxes");
                });

            modelBuilder.Entity("DataModel.Model.EscalationItem", b =>
                {
                    b.HasOne("DataModel.Model.Subfield", "Subfield")
                        .WithMany()
                        .HasForeignKey("SubfieldId");

                    b.Navigation("Subfield");
                });

            modelBuilder.Entity("DataModel.Model.EscalationItemRow", b =>
                {
                    b.HasOne("DataModel.Model.EscalationItem", null)
                        .WithMany("Rows")
                        .HasForeignKey("EscalationItemId");
                });

            modelBuilder.Entity("DataModel.Model.PAIndex", b =>
                {
                    b.HasOne("DataModel.Model.Subfield", "Subfield")
                        .WithMany()
                        .HasForeignKey("SubfieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModel.Model.TimeBox", "TimeBox")
                        .WithMany("PAIndexes")
                        .HasForeignKey("TimeBoxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subfield");

                    b.Navigation("TimeBox");
                });

            modelBuilder.Entity("DataModel.Model.EscalationItem", b =>
                {
                    b.Navigation("Rows");
                });

            modelBuilder.Entity("DataModel.Model.TimeBox", b =>
                {
                    b.Navigation("PAIndexes");
                });
#pragma warning restore 612, 618
        }
    }
}
