﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Models;
namespace TravelGroupAssignment1.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<CarRentalCompany> CarRentalCompanies { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarBooking> CarBookings { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<FlightBooking> FlightBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hotel>()
                .HasMany(r => r.Rooms)
                .WithOne(h => h.Hotel)
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<CarRentalCompany>()
                .HasMany(c => c.Cars)
                .WithOne(cr => cr.Company)
                .HasForeignKey(c => c.CompanyId);

            modelBuilder.Entity<Car>()
                .HasMany(c => c.Bookings)
                .WithOne(cb => cb.Car)
                .HasForeignKey(c => c.CarId);

            modelBuilder.Entity<Room>()
                .HasMany(rb => rb.RoomBookings)
                .WithOne(r => r.Room)
                .HasForeignKey(rb => rb.RoomId);

            modelBuilder.Entity<Customer>().HasData(
               new Customer { CustomerId = 1, Username = "username", Password = "password", FirstName = "Guest", LastName = "User", Email = "Comp2319@gmail.com" }

               );

            modelBuilder.Entity<Trip>().HasData(
                new Trip { TripId = 1, CustomerId = 1 }
                 );
        }
    }
}
