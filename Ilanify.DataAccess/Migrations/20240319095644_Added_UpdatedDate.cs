using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ilanify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Added_UpdatedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "RealEstates",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "RealEstates");
        }
    }
}
