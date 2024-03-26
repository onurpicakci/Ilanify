using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ilanify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Neighborhood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Neighboord",
                table: "Locations",
                newName: "Neighborhood");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Neighborhood",
                table: "Locations",
                newName: "Neighboord");
        }
    }
}
