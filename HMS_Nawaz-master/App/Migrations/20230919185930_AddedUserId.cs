using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AddedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Doctor_Dr_ID",
                table: "Patient");

            migrationBuilder.AlterColumn<long>(
                name: "Dr_ID",
                table: "Patient",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Doctor_Dr_ID",
                table: "Patient",
                column: "Dr_ID",
                principalTable: "Doctor",
                principalColumn: "Dr_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Doctor_Dr_ID",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Patient");

            migrationBuilder.AlterColumn<long>(
                name: "Dr_ID",
                table: "Patient",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Doctor_Dr_ID",
                table: "Patient",
                column: "Dr_ID",
                principalTable: "Doctor",
                principalColumn: "Dr_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
