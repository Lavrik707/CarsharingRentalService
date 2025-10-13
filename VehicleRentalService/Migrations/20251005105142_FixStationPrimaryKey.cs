using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleRentalService.Migrations
{
    /// <inheritdoc />
    public partial class FixStationPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StationId",
                table: "Vehicle",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_StationId",
                table: "Vehicle",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Stations_StationId",
                table: "Vehicle",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Stations_StationId",
                table: "Vehicle");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_StationId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "Vehicle");
        }
    }
}
