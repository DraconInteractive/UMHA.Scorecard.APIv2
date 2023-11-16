using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScorecardAPI.Migrations
{
    /// <inheritdoc />
    public partial class MatchStatus2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EventType",
                table: "MatchEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PenaltyEvent_Reason",
                table: "MatchEvents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "MatchEvents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MatchEvents",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PenaltyEvent_Reason",
                table: "MatchEvents");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "MatchEvents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MatchEvents");

            migrationBuilder.AlterColumn<string>(
                name: "EventType",
                table: "MatchEvents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
