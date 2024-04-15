using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelGroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class mssql_migration_620 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "TripId",
                keyValue: 1,
                column: "TripReference",
                value: "240415172188a1bf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "TripId",
                keyValue: 1,
                column: "TripReference",
                value: "2404151716e46ad1");
        }
    }
}
