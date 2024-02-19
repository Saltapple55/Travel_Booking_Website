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
    [Migration("20240219070327_UpdatedPassenger")]
    partial class UpdatedPassenger
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TravelGroupAssignment1.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<string>("BookingReference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("TripId");

                    b.ToTable("Booking");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Booking");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Email = "Comp2319@gmail.com",
                            FirstName = "Guest",
                            LastName = "User",
                            Password = "password",
                            Username = "username"
                        });
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightId"));

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ArrivalTime")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DepartTime")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPassenger")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FlightId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Hotel", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelId"));

                    b.Property<string>("Amentities")
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

            modelBuilder.Entity("TravelGroupAssignment1.Models.Passenger", b =>
                {
                    b.Property<int>("PassengerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PassengerId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FlightBookingBookingId")
                        .HasColumnType("int");

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PassengerId");

                    b.HasIndex("FlightBookingBookingId");

                    b.HasIndex("FlightId");

                    b.ToTable("Passengers");
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

            modelBuilder.Entity("TravelGroupAssignment1.Models.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TripId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("TripId");

                    b.ToTable("Trips");

                    b.HasData(
                        new
                        {
                            TripId = 1,
                            CustomerId = 1
                        });
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.FlightBooking", b =>
                {
                    b.HasBaseType("TravelGroupAssignment1.Models.Booking");

                    b.Property<string>("FlightClass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.HasIndex("FlightId");

                    b.HasDiscriminator().HasValue("FlightBooking");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Booking", b =>
                {
                    b.HasOne("TravelGroupAssignment1.Models.Trip", null)
                        .WithMany("Bookings")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Passenger", b =>
                {
                    b.HasOne("TravelGroupAssignment1.Models.FlightBooking", null)
                        .WithMany("Passengers")
                        .HasForeignKey("FlightBookingBookingId");

                    b.HasOne("TravelGroupAssignment1.Models.Flight", null)
                        .WithMany("PassengerList")
                        .HasForeignKey("FlightId");
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

            modelBuilder.Entity("TravelGroupAssignment1.Models.FlightBooking", b =>
                {
                    b.HasOne("TravelGroupAssignment1.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Flight", b =>
                {
                    b.Navigation("PassengerList");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.Trip", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("TravelGroupAssignment1.Models.FlightBooking", b =>
                {
                    b.Navigation("Passengers");
                });
#pragma warning restore 612, 618
        }
    }
}
