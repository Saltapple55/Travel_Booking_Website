using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelGroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class carcomments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarComment",
                columns: table => new
                {
                    ProjectCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarComment", x => x.ProjectCommentId);
                    table.ForeignKey(
                        name: "FK_CarComment_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "TripId",
                keyValue: 1,
                column: "TripReference",
                value: "24041416273ccfb9");

            migrationBuilder.CreateIndex(
                name: "IX_CarComment_CarId",
                table: "CarComment",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarComment");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "TripId",
                keyValue: 1,
                column: "TripReference",
                value: "2404141526ba9d46");
        }
    }
}
