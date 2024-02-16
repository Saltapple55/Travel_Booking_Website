using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Models;
namespace TravelGroupAssignment1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
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
                .HasForeignKey(h => h.HotelId);
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Username="username", Password="password", FirstName = "Guest", LastName="User", Email = "Comp2319@gmail.com" }

                );
            /*modelBuilder.Entity<Booking>()
            .HasOne(b => b.CarBooking)
            .WithMany(cb => cb.Bookings)
            .HasForeignKey(b => b.TripId); // Assuming TripId is the foreign key for CarBookings relationship

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.FlightBooking)
                .WithMany(fb => fb.Bookings)
                .HasForeignKey(b => b.TripId); */
            modelBuilder.Entity<Trip>().HasData(
                new Trip { TripId = 1, CustomerId = 1}
                 );
        }
    }
}
