using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class UpdatedM2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchTeam");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "Matches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeamMatch",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMatch", x => new { x.TeamId, x.MatchId });
                    table.ForeignKey(
                        name: "FK_TeamMatch_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMatch_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinnerId",
                table: "Matches",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMatch_MatchId",
                table: "TeamMatch",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Teams_WinnerId",
                table: "Matches",
                column: "WinnerId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Teams_WinnerId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "TeamMatch");

            migrationBuilder.DropIndex(
                name: "IX_Matches_WinnerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "Winner",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MatchTeam",
                columns: table => new
                {
                    MatchesId = table.Column<int>(type: "int", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeam", x => new { x.MatchesId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_MatchTeam_Matches_MatchesId",
                        column: x => x.MatchesId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeam_TeamsId",
                table: "MatchTeam",
                column: "TeamsId");
        }
    }
}
