using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class UpdatedM2M3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Users_AdminId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_AdminId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Tournaments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Tournaments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_AdminId",
                table: "Tournaments",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Users_AdminId",
                table: "Tournaments",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
