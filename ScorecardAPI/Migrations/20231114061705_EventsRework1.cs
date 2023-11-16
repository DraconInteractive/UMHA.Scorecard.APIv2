using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScorecardAPI.Migrations
{
    /// <inheritdoc />
    public partial class EventsRework1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApplyHealthReduction",
                table: "MatchEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Matches",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyHealthReduction",
                table: "MatchEvents");

            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Matches");
        }
    }
}
