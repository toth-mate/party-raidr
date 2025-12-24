using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace PartyRaidR.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPlaceLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GpsLattitude",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "GpsLongitude",
                table: "Places");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Places",
                type: "point",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Places");

            migrationBuilder.AddColumn<string>(
                name: "GpsLattitude",
                table: "Places",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GpsLongitude",
                table: "Places",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
