using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class UpdatedM2M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamTournament_Teams_TeamsId",
                table: "TeamTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTournament_Tournaments_TournamentsId",
                table: "TeamTournament");

            migrationBuilder.RenameColumn(
                name: "TournamentsId",
                table: "TeamTournament",
                newName: "TournamentId");

            migrationBuilder.RenameColumn(
                name: "TeamsId",
                table: "TeamTournament",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamTournament_TournamentsId",
                table: "TeamTournament",
                newName: "IX_TeamTournament_TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTournament_Teams_TeamId",
                table: "TeamTournament",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTournament_Tournaments_TournamentId",
                table: "TeamTournament",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamTournament_Teams_TeamId",
                table: "TeamTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTournament_Tournaments_TournamentId",
                table: "TeamTournament");

            migrationBuilder.RenameColumn(
                name: "TournamentId",
                table: "TeamTournament",
                newName: "TournamentsId");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TeamTournament",
                newName: "TeamsId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamTournament_TournamentId",
                table: "TeamTournament",
                newName: "IX_TeamTournament_TournamentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTournament_Teams_TeamsId",
                table: "TeamTournament",
                column: "TeamsId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTournament_Tournaments_TournamentsId",
                table: "TeamTournament",
                column: "TournamentsId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
