using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigratorJob.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseIndexColumnToEscalationItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BaseIndex",
                table: "EscalationItems",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseIndex",
                table: "EscalationItems");
        }
    }
}
