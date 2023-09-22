using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test_app.Migrations
{
    /// <inheritdoc />
    public partial class indexi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Flights_RouteId",
                table: "Flights",
                newName: "IX_RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_RouteId",
                table: "Flights",
                newName: "IX_Flights_RouteId");
        }
    }
}
