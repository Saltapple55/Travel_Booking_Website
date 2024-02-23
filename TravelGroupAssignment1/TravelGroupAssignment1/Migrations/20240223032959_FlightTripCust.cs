using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelGroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class FlightTripCust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MaxPassenger = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                });

            migrationBuilder.CreateTable(
                name: "FlightBookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    FlightClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    BookingReference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightBookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_FlightBookings_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightBookingBookingId = table.Column<int>(type: "int", nullable: true),
                    FlightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerId);
                    table.ForeignKey(
                        name: "FK_Passengers_FlightBookings_FlightBookingBookingId",
                        column: x => x.FlightBookingBookingId,
                        principalTable: "FlightBookings",
                        principalColumn: "BookingId");
                    table.ForeignKey(
                        name: "FK_Passengers_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId");
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 1, "Comp2319@gmail.com", "Guest", "User", "password", "username" });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "TripId", "CustomerId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_FlightId",
                table: "FlightBookings",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_FlightBookingBookingId",
                table: "Passengers",
                column: "FlightBookingBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_FlightId",
                table: "Passengers",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "FlightBookings");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
