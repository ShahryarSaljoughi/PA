using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigratorJob.Migrations
{
    /// <inheritdoc />
    public partial class EscalationObjectsRevised : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscalationItemRows_EscalationItems_EscalationItemId",
                table: "EscalationItemRows");

            migrationBuilder.AddColumn<Guid>(
                name: "EscallaionId",
                table: "EscalationItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EscallationId",
                table: "EscalationItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "EscalationItemId",
                table: "EscalationItemRows",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "EscalationCoefficient",
                table: "EscalationItemRows",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "EscalationPrice",
                table: "EscalationItemRows",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkingTimeBoxId",
                table: "EscalationItemRows",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WorkingTimeBoxIndex",
                table: "EscalationItemRows",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Escalation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Coefficient = table.Column<double>(type: "REAL", nullable: false),
                    BaseTimeBoxId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PreviousStatementTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CurrentStatementTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsCalculated = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escalation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escalation_TimeBoxes_BaseTimeBoxId",
                        column: x => x.BaseTimeBoxId,
                        principalTable: "TimeBoxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EscalationItems_EscallationId",
                table: "EscalationItems",
                column: "EscallationId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalationItemRows_WorkingTimeBoxId",
                table: "EscalationItemRows",
                column: "WorkingTimeBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Escalation_BaseTimeBoxId",
                table: "Escalation",
                column: "BaseTimeBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_EscalationItemRows_EscalationItems_EscalationItemId",
                table: "EscalationItemRows",
                column: "EscalationItemId",
                principalTable: "EscalationItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EscalationItemRows_TimeBoxes_WorkingTimeBoxId",
                table: "EscalationItemRows",
                column: "WorkingTimeBoxId",
                principalTable: "TimeBoxes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EscalationItems_Escalation_EscallationId",
                table: "EscalationItems",
                column: "EscallationId",
                principalTable: "Escalation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscalationItemRows_EscalationItems_EscalationItemId",
                table: "EscalationItemRows");

            migrationBuilder.DropForeignKey(
                name: "FK_EscalationItemRows_TimeBoxes_WorkingTimeBoxId",
                table: "EscalationItemRows");

            migrationBuilder.DropForeignKey(
                name: "FK_EscalationItems_Escalation_EscallationId",
                table: "EscalationItems");

            migrationBuilder.DropTable(
                name: "Escalation");

            migrationBuilder.DropIndex(
                name: "IX_EscalationItems_EscallationId",
                table: "EscalationItems");

            migrationBuilder.DropIndex(
                name: "IX_EscalationItemRows_WorkingTimeBoxId",
                table: "EscalationItemRows");

            migrationBuilder.DropColumn(
                name: "EscallaionId",
                table: "EscalationItems");

            migrationBuilder.DropColumn(
                name: "EscallationId",
                table: "EscalationItems");

            migrationBuilder.DropColumn(
                name: "EscalationCoefficient",
                table: "EscalationItemRows");

            migrationBuilder.DropColumn(
                name: "EscalationPrice",
                table: "EscalationItemRows");

            migrationBuilder.DropColumn(
                name: "WorkingTimeBoxId",
                table: "EscalationItemRows");

            migrationBuilder.DropColumn(
                name: "WorkingTimeBoxIndex",
                table: "EscalationItemRows");

            migrationBuilder.AlterColumn<Guid>(
                name: "EscalationItemId",
                table: "EscalationItemRows",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_EscalationItemRows_EscalationItems_EscalationItemId",
                table: "EscalationItemRows",
                column: "EscalationItemId",
                principalTable: "EscalationItems",
                principalColumn: "Id");
        }
    }
}
