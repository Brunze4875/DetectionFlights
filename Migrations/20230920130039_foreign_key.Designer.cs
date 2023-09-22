﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using test_app.DataAccess.DatabaseContext;

#nullable disable

namespace test_app.Migrations
{
    [DbContext(typeof(AirlineContext))]
    [Migration("20230920130039_foreign_key")]
    partial class foreign_key
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("test_app.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AirlineId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("RouteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("test_app.Models.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DepartureDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("OriginCityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("test_app.Models.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AgencyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DestinationCityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OriginCityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("test_app.Models.Flight", b =>
                {
                    b.HasOne("test_app.Models.Route", "Route")
                        .WithMany("Flights")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("test_app.Models.Route", b =>
                {
                    b.Navigation("Flights");
                });
#pragma warning restore 612, 618
        }
    }
}
