using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddedDrId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DrId",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrId",
                table: "Doctor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "DrId",
                table: "Doctor");
        }
    }
}
