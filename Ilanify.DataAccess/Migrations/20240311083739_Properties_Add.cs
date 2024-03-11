using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ilanify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Properties_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessType",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasBalcony",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasElevator",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasGarage",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasGarden",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasKitchenFacilities",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasMeetingRooms",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasParking",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasParkingSpace",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HasSecurity",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "HeatingType",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "IsFurnished",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "IsInRegulation",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "NumberOfBathrooms",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "NumberOfFloors",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "NumberOfRooms",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "YearBuilt",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "ZoningStatus",
                table: "RealEstates");

            migrationBuilder.CreateTable(
                name: "CategoryAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryAttributes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttributeValues",
                columns: table => new
                {
                    AttributeValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateId = table.Column<int>(type: "int", nullable: false),
                    CategoryAttributeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValues", x => x.AttributeValueId);
                    table.ForeignKey(
                        name: "FK_AttributeValues_CategoryAttributes_CategoryAttributeId",
                        column: x => x.CategoryAttributeId,
                        principalTable: "CategoryAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttributeValues_RealEstates_RealEstateId",
                        column: x => x.RealEstateId,
                        principalTable: "RealEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_CategoryAttributeId",
                table: "AttributeValues",
                column: "CategoryAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_RealEstateId",
                table: "AttributeValues",
                column: "RealEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAttributes_CategoryId",
                table: "CategoryAttributes",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeValues");

            migrationBuilder.DropTable(
                name: "CategoryAttributes");

            migrationBuilder.AddColumn<string>(
                name: "BusinessType",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "RealEstates",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HasBalcony",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasElevator",
                table: "RealEstates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasGarage",
                table: "RealEstates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasGarden",
                table: "RealEstates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasKitchenFacilities",
                table: "RealEstates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasMeetingRooms",
                table: "RealEstates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasParking",
                table: "RealEstates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasParkingSpace",
                table: "RealEstates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasSecurity",
                table: "RealEstates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeatingType",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFurnished",
                table: "RealEstates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInRegulation",
                table: "RealEstates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBathrooms",
                table: "RealEstates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfFloors",
                table: "RealEstates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfRooms",
                table: "RealEstates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YearBuilt",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZoningStatus",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
