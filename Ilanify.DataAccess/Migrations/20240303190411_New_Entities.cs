using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ilanify.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class New_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Floor",
                table: "RealEstates");

            migrationBuilder.AlterColumn<int>(
                name: "SquareMeters",
                table: "RealEstates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfRooms",
                table: "RealEstates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsFurnished",
                table: "RealEstates",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "HasGarage",
                table: "RealEstates",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "HasElevator",
                table: "RealEstates",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "HasBalcony",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

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

            migrationBuilder.AddColumn<int>(
                name: "House_SquareMeters",
                table: "RealEstates",
                type: "int",
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
                name: "Workplace_SquareMeters",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessType",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Discriminator",
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
                name: "House_SquareMeters",
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
                name: "Workplace_SquareMeters",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "YearBuilt",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "ZoningStatus",
                table: "RealEstates");

            migrationBuilder.AlterColumn<int>(
                name: "SquareMeters",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfRooms",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFurnished",
                table: "RealEstates",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasGarage",
                table: "RealEstates",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasElevator",
                table: "RealEstates",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasBalcony",
                table: "RealEstates",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
