using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ilanify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "House_SquareMeters",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Workplace_SquareMeters",
                table: "RealEstates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "House_SquareMeters",
                table: "RealEstates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Workplace_SquareMeters",
                table: "RealEstates",
                type: "int",
                nullable: true);
        }
    }
}
