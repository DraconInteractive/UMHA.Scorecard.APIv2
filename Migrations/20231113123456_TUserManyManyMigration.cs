using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScorecardAPI.Migrations
{
    /// <inheritdoc />
    public partial class TUserManyManyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentUser",
                columns: table => new
                {
                    FightersUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TournamentsTournamentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentUser", x => new { x.FightersUserId, x.TournamentsTournamentId });
                    table.ForeignKey(
                        name: "FK_TournamentUser_Tournaments_TournamentsTournamentId",
                        column: x => x.TournamentsTournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentUser_Users_FightersUserId",
                        column: x => x.FightersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentUser_TournamentsTournamentId",
                table: "TournamentUser",
                column: "TournamentsTournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentUser");
        }
    }
}
