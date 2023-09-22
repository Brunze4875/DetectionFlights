using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test_app.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnRoutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinationCityId",
                table: "Routes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationCityId",
                table: "Routes");
        }
    }
}
