using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigratorJob.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDescriptionFromPaIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PAIndexes");

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "TimeBoxes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "TimeBoxes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PAIndexes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
