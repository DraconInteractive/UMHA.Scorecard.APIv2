using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScorecardAPI.Migrations
{
    /// <inheritdoc />
    public partial class EventsRefactorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EventType",
                table: "MatchEvents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "FighterOneReduction",
                table: "MatchEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FighterTwoReduction",
                table: "MatchEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Matches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FighterOneReduction",
                table: "MatchEvents");

            migrationBuilder.DropColumn(
                name: "FighterTwoReduction",
                table: "MatchEvents");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Matches");

            migrationBuilder.AlterColumn<string>(
                name: "EventType",
                table: "MatchEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
