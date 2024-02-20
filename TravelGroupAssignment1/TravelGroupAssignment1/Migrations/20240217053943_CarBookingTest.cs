using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelGroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class CarBookingTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TripFKId",
                table: "RoomBookings",
                newName: "TripId");

            migrationBuilder.RenameColumn(
                name: "TripFKId",
                table: "CarBookings",
                newName: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "RoomBookings",
                newName: "TripFKId");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "CarBookings",
                newName: "TripFKId");
        }
    }
}
