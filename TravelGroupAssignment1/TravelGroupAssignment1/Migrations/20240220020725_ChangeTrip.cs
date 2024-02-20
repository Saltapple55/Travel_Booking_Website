using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelGroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Flights_FlightId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Trips_TripId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Booking_FlightBookingId",
                table: "Passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_TripId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Booking");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "FlightBookings");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_FlightId",
                table: "FlightBookings",
                newName: "IX_FlightBookings_FlightId");

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "FlightBookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FlightClass",
                table: "FlightBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightBookings",
                table: "FlightBookings",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightBookings_Flights_FlightId",
                table: "FlightBookings",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_FlightBookings_FlightBookingId",
                table: "Passengers",
                column: "FlightBookingId",
                principalTable: "FlightBookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightBookings_Flights_FlightId",
                table: "FlightBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_FlightBookings_FlightBookingId",
                table: "Passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightBookings",
                table: "FlightBookings");

            migrationBuilder.RenameTable(
                name: "FlightBookings",
                newName: "Booking");

            migrationBuilder.RenameIndex(
                name: "IX_FlightBookings_FlightId",
                table: "Booking",
                newName: "IX_Booking_FlightId");

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "Booking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FlightClass",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Booking",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_TripId",
                table: "Booking",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Flights_FlightId",
                table: "Booking",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Trips_TripId",
                table: "Booking",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Booking_FlightBookingId",
                table: "Passengers",
                column: "FlightBookingId",
                principalTable: "Booking",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
