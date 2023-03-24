using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigratorJob.Migrations
{
    /// <inheritdoc />
    public partial class AddIsInterimToTimeBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInterim",
                table: "TimeBoxes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInterim",
                table: "TimeBoxes");
        }
    }
}
