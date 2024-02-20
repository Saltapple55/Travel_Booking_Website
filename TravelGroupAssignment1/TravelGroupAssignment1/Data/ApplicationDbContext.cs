using Microsoft.EntityFrameworkCore;
using TravelGroupAssignment1.Models;
namespace TravelGroupAssignment1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<CarRentalCompany> CarRentalCompanies { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarBooking> CarBookings { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<Room_RoomBooking> Room_RoomBookings { get; set; }

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
                .HasMany(r => r.Room_RoomBooking)
                .WithOne(h => h.Room)
                .HasForeignKey(r => r.RoomId);

            modelBuilder.Entity<RoomBooking>()
                .HasMany(r => r.Room_RoomBooking)
                .WithOne(h => h.RoomBooking)
                .HasForeignKey(r => r.BookingId);

            modelBuilder.Entity<Room_RoomBooking>()
                .HasKey(rb => new { rb.BookingId, rb.RoomId });

            /*            modelBuilder.Entity<Room>()
                            .HasMany(r => r.Bookings)
                            .WithMany(rb => rb.Room_RoomBooking)
                            .UsingEntity(j => j.ToTable("Room_RoomBookings"));*/
        }
    }
}
