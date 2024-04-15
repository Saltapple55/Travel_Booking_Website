using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelGroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class roomhotelcommentmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelComments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelComments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_HotelComments_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomComments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomComments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_RoomComments_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "TripId",
                keyValue: 1,
                column: "TripReference",
                value: "24041420148e7be4");

            migrationBuilder.CreateIndex(
                name: "IX_HotelComments_HotelId",
                table: "HotelComments",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomComments_RoomId",
                table: "RoomComments",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelComments");

            migrationBuilder.DropTable(
                name: "RoomComments");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "TripId",
                keyValue: 1,
                column: "TripReference",
                value: "2404141858706358");
        }
    }
}
