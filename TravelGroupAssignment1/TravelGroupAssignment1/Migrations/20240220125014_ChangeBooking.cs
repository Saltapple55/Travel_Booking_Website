using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelGroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_FlightBookings_FlightBookingId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_FlightBookingId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "FlightBookingId",
                table: "Passengers");

            migrationBuilder.AddColumn<int>(
                name: "FlightBookingBookingId",
                table: "Passengers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_FlightBookingBookingId",
                table: "Passengers",
                column: "FlightBookingBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_FlightBookings_FlightBookingBookingId",
                table: "Passengers",
                column: "FlightBookingBookingId",
                principalTable: "FlightBookings",
                principalColumn: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_FlightBookings_FlightBookingBookingId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_FlightBookingBookingId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "FlightBookingBookingId",
                table: "Passengers");

            migrationBuilder.AddColumn<int>(
                name: "FlightBookingId",
                table: "Passengers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_FlightBookingId",
                table: "Passengers",
                column: "FlightBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_FlightBookings_FlightBookingId",
                table: "Passengers",
                column: "FlightBookingId",
                principalTable: "FlightBookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
