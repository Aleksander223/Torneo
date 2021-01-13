using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class Final2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchOrder",
                table: "Tournaments");

            migrationBuilder.AddColumn<int>(
                name: "TournamentOrder",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentOrder",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "MatchOrder",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
