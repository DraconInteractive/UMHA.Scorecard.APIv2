using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScorecardAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPools : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Pool",
                table: "Matches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pool",
                table: "Matches");
        }
    }
}
