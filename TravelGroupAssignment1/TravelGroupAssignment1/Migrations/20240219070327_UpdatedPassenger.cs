using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelGroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPassenger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Passengers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "passportNo",
                table: "Passengers",
                newName: "PassportNo");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Passengers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Passengers",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Passengers",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "PassportNo",
                table: "Passengers",
                newName: "passportNo");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Passengers",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Passengers",
                newName: "firstName");
        }
    }
}
