﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelGroupAssignment1.Data;

#nullable disable

namespace TravelGroupAssignment1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240219005405_RoomBookingManyToMany-Modification")]
    partial class RoomBookingManyToManyModification
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TravelGroupAssignment1.Models.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarId"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<bool>("HasAirConditioning")
                        .HasColumnType("bit");

                    b.Property<bool>("HasUnlimitedMileage")
                        .HasColumnType("bit");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPassengers")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerDay")
                        .HasColumnType("float");

                    b.Property<string>("Transmission")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.CarBooking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<string>("BookingReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("CarId");

                    b.ToTable("CarBookings");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.CarRentalCompany", b =>
                {
                    b.Property<int>("CarRentalCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarRentalCompanyId"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("CarRentalCompanyId");

                    b.ToTable("CarRentalCompanies");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Hotel", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelId"));

                    b.Property<string>("Amenities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HotelId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<string>("Amenities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BedDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerNight")
                        .HasColumnType("float");

                    b.Property<int?>("RoomSize")
                        .HasColumnType("int");

                    b.HasKey("RoomId");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.RoomBooking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<string>("BookingReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CheckInDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOutDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.ToTable("RoomBookings");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Room_RoomBooking", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("BookingId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("Room_RoomBookings");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Car", b =>
                {
                    b.HasOne("TravelGroupAssignment1.Models.CarRentalCompany", "Company")
                        .WithMany("Cars")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.CarBooking", b =>
                {
                    b.HasOne("TravelGroupAssignment1.Models.Car", "Car")
                        .WithMany("Bookings")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Room", b =>
                {
                    b.HasOne("TravelGroupAssignment1.Models.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Room_RoomBooking", b =>
                {
                    b.HasOne("TravelGroupAssignment1.Models.RoomBooking", "RoomBooking")
                        .WithMany("Room_RoomBooking")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelGroupAssignment1.Models.Room", "Room")
                        .WithMany("Room_RoomBooking")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("RoomBooking");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Car", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.CarRentalCompany", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Room", b =>
                {
                    b.Navigation("Room_RoomBooking");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.RoomBooking", b =>
                {
                    b.Navigation("Room_RoomBooking");
                });
#pragma warning restore 612, 618
        }
    }
}
