using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreU_WepApi.Migrations
{
    public partial class UpdatedCompanyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonalPhoneNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RFC",
                table: "Company",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalPhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "RFC",
                table: "Company");
        }
    }
}
