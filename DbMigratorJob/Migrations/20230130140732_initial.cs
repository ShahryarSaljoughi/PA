using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbMigratorJob.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subfields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Field = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subfields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeBoxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SolarYear = table.Column<int>(type: "INTEGER", nullable: false),
                    ThreeMonthNo = table.Column<int>(type: "INTEGER", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeBoxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EscalationItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PreviousPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    SubfieldId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalationItems_Subfields_SubfieldId",
                        column: x => x.SubfieldId,
                        principalTable: "Subfields",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PAIndexes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    TimeBoxId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubfieldId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAIndexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PAIndexes_Subfields_SubfieldId",
                        column: x => x.SubfieldId,
                        principalTable: "Subfields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PAIndexes_TimeBoxes_TimeBoxId",
                        column: x => x.TimeBoxId,
                        principalTable: "TimeBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EscalationItemRows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EscalationItemId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalationItemRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalationItemRows_EscalationItems_EscalationItemId",
                        column: x => x.EscalationItemId,
                        principalTable: "EscalationItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EscalationItemRows_EscalationItemId",
                table: "EscalationItemRows",
                column: "EscalationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalationItems_SubfieldId",
                table: "EscalationItems",
                column: "SubfieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PAIndexes_SubfieldId",
                table: "PAIndexes",
                column: "SubfieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PAIndexes_TimeBoxId",
                table: "PAIndexes",
                column: "TimeBoxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EscalationItemRows");

            migrationBuilder.DropTable(
                name: "PAIndexes");

            migrationBuilder.DropTable(
                name: "EscalationItems");

            migrationBuilder.DropTable(
                name: "TimeBoxes");

            migrationBuilder.DropTable(
                name: "Subfields");
        }
    }
}
