using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigratorJob.Migrations
{
    /// <inheritdoc />
    public partial class EscalationNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ContractStartDateTime",
                table: "Escalation",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contractor",
                table: "Escalation",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Employer",
                table: "Escalation",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentStatementFinal",
                table: "Escalation",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProjectTitle",
                table: "Escalation",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractStartDateTime",
                table: "Escalation");

            migrationBuilder.DropColumn(
                name: "Contractor",
                table: "Escalation");

            migrationBuilder.DropColumn(
                name: "Employer",
                table: "Escalation");

            migrationBuilder.DropColumn(
                name: "IsCurrentStatementFinal",
                table: "Escalation");

            migrationBuilder.DropColumn(
                name: "ProjectTitle",
                table: "Escalation");
        }
    }
}
