using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarSee.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    ProductionYear = table.Column<int>(type: "integer", nullable: false),
                    Condition = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Mileage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");
        }
    }
}
