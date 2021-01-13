using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class UpdatedM2M4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatch_Matches_MatchId",
                table: "TeamMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatch_Teams_TeamId",
                table: "TeamMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTournament_Teams_TeamId",
                table: "TeamTournament");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTournament_Tournaments_TournamentId",
                table: "TeamTournament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamTournament",
                table: "TeamTournament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMatch",
                table: "TeamMatch");

            migrationBuilder.RenameTable(
                name: "TeamTournament",
                newName: "TeamTournaments");

            migrationBuilder.RenameTable(
                name: "TeamMatch",
                newName: "TeamMatches");

            migrationBuilder.RenameIndex(
                name: "IX_TeamTournament_TournamentId",
                table: "TeamTournaments",
                newName: "IX_TeamTournaments_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMatch_MatchId",
                table: "TeamMatches",
                newName: "IX_TeamMatches_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamTournaments",
                table: "TeamTournaments",
                columns: new[] { "TeamId", "TournamentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMatches",
                table: "TeamMatches",
                columns: new[] { "TeamId", "MatchId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_Matches_MatchId",
                table: "TeamMatches",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_Teams_TeamId",
                table: "TeamMatches",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTournaments_Teams_TeamId",
                table: "TeamTournaments",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamTournaments_Tournaments_TournamentId",
                table: "TeamTournaments",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_Matches_MatchId",
                table: "TeamMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_Teams_TeamId",
                table: "TeamMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTournaments_Teams_TeamId",
                table: "TeamTournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamTournaments_Tournaments_TournamentId",
                table: "TeamTournaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamTournaments",
                table: "TeamTournaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMatches",
                table: "TeamMatches");

            migrationBuilder.RenameTable(
                name: "TeamTournaments",
                newName: "TeamTournament");

            migrationBuilder.RenameTable(
                name: "TeamMatches",
                newName: "TeamMatch");

            migrationBuilder.RenameIndex(
                name: "IX_TeamTournaments_TournamentId",
                table: "TeamTournament",
                newName: "IX_TeamTournament_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMatches_MatchId",
                table: "TeamMatch",
                newName: "IX_TeamMatch_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamTournament",
                table: "TeamTournament",
                columns: new[] { "TeamId", "TournamentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMatch",
                table: "TeamMatch",
                columns: new[] { "TeamId", "MatchId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatch_Matches_MatchId",
                table: "TeamMatch",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatch_Teams_TeamId",
                table: "TeamMatch",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
