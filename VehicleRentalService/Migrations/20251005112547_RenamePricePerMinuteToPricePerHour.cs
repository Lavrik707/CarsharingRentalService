using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRentalService.Migrations
{
    /// <inheritdoc />
    public partial class RenamePricePerMinuteToPricePerHour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerMinute",   // Old Name
                table: "Vehicle",
                newName: "PricePerHour"); // new name
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerHour",
                table: "Vehicle",
                newName: "PricePerMinute");
        }
    }
}
