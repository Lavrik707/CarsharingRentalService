using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRentalService.Migrations
{
    /// <inheritdoc />
    public partial class StationsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Stations",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Stations",
                newName: "Adress");
        }
    }
}
