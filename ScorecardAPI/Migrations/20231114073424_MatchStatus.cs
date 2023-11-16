using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScorecardAPI.Migrations
{
    /// <inheritdoc />
    public partial class MatchStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Finished",
                table: "Matches",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Matches",
                newName: "Finished");
        }
    }
}
