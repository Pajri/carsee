using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSee.Migrations.ApplicationDb
{
    public partial class ChangeBuyerToSeller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyerPhoneNumber",
                table: "Car",
                newName: "SellerPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "BuyerName",
                table: "Car",
                newName: "SellerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellerPhoneNumber",
                table: "Car",
                newName: "BuyerPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "SellerName",
                table: "Car",
                newName: "BuyerName");
        }
    }
}
