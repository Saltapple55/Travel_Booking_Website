using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelGroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class carcomments1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarComment_Cars_CarId",
                table: "CarComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarComment",
                table: "CarComment");

            migrationBuilder.RenameTable(
                name: "CarComment",
                newName: "CarComments");

            migrationBuilder.RenameIndex(
                name: "IX_CarComment_CarId",
                table: "CarComments",
                newName: "IX_CarComments_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarComments",
                table: "CarComments",
                column: "ProjectCommentId");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "TripId",
                keyValue: 1,
                column: "TripReference",
                value: "240414162941f56d");

            migrationBuilder.AddForeignKey(
                name: "FK_CarComments_Cars_CarId",
                table: "CarComments",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarComments_Cars_CarId",
                table: "CarComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarComments",
                table: "CarComments");

            migrationBuilder.RenameTable(
                name: "CarComments",
                newName: "CarComment");

            migrationBuilder.RenameIndex(
                name: "IX_CarComments_CarId",
                table: "CarComment",
                newName: "IX_CarComment_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarComment",
                table: "CarComment",
                column: "ProjectCommentId");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "TripId",
                keyValue: 1,
                column: "TripReference",
                value: "24041416273ccfb9");

            migrationBuilder.AddForeignKey(
                name: "FK_CarComment_Cars_CarId",
                table: "CarComment",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
