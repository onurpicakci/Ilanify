using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ilanify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Added_DataTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "CategoryAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataType",
                table: "CategoryAttributes");
        }
    }
}
