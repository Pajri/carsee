using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSee.Migrations.ApplicationDb
{
    public partial class ChangeUIDtoUUID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "Car",
                newName: "UUID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UUID",
                table: "Car",
                newName: "Uid");
        }
    }
}
