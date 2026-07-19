using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace PartyRaidR.Backend.Migrations
{
    /// <inheritdoc />
    public partial class FixSrid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Update the SRID of existing geometries.
            migrationBuilder.Sql("UPDATE `Places` SET `Location` = ST_SRID(`Location`, 4326) WHERE ST_SRID(`Location`) != 4326;");

            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "Places",
                type: "point srid 4326",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "point");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "Places",
                type: "point",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "point srid 4326");
        }
    }
}
