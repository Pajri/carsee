using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSee.Migrations.ApplicationDb
{
    public partial class AddBuyerData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerName",
                table: "Car",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerPhoneNumber",
                table: "Car",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerName",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "BuyerPhoneNumber",
                table: "Car");
        }
    }
}
