using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigratorJob.Migrations
{
    /// <inheritdoc />
    public partial class TypoInNameFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscalationItems_Escalation_EscallationId",
                table: "EscalationItems");

            migrationBuilder.DropColumn(
                name: "EscallaionId",
                table: "EscalationItems");

            migrationBuilder.RenameColumn(
                name: "EscallationId",
                table: "EscalationItems",
                newName: "EscalationId");

            migrationBuilder.RenameIndex(
                name: "IX_EscalationItems_EscallationId",
                table: "EscalationItems",
                newName: "IX_EscalationItems_EscalationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EscalationItems_Escalation_EscalationId",
                table: "EscalationItems",
                column: "EscalationId",
                principalTable: "Escalation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscalationItems_Escalation_EscalationId",
                table: "EscalationItems");

            migrationBuilder.RenameColumn(
                name: "EscalationId",
                table: "EscalationItems",
                newName: "EscallationId");

            migrationBuilder.RenameIndex(
                name: "IX_EscalationItems_EscalationId",
                table: "EscalationItems",
                newName: "IX_EscalationItems_EscallationId");

            migrationBuilder.AddColumn<Guid>(
                name: "EscallaionId",
                table: "EscalationItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_EscalationItems_Escalation_EscallationId",
                table: "EscalationItems",
                column: "EscallationId",
                principalTable: "Escalation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
