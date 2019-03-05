using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationPeriods",
                schema: "dbo",
                columns: table => new
                {
                    PeriadId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: true),
                    ReginalPowerCorpRef = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationPeriods", x => x.PeriadId);
                    table.ForeignKey(
                        name: "FK_EvaluationPeriods_ReginalPowerCorps_ReginalPowerCorpRef",
                        column: x => x.ReginalPowerCorpRef,
                        principalSchema: "dbo",
                        principalTable: "ReginalPowerCorps",
                        principalColumn: "ReginalPowerCorpId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationProjects",
                schema: "dbo",
                columns: table => new
                {
                    EvaluationProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    ReginalPowerCorpRef = table.Column<int>(nullable: true),
                    ProjectTreeRef = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationProjects", x => x.EvaluationProjectId);
                    table.ForeignKey(
                        name: "FK_EvaluationProjects_ProjectTrees_ProjectTreeRef",
                        column: x => x.ProjectTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ProjectTrees",
                        principalColumn: "ProjectTreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationProjects_ReginalPowerCorps_ReginalPowerCorpRef",
                        column: x => x.ReginalPowerCorpRef,
                        principalSchema: "dbo",
                        principalTable: "ReginalPowerCorps",
                        principalColumn: "ReginalPowerCorpId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationPeriods_ReginalPowerCorpRef",
                schema: "dbo",
                table: "EvaluationPeriods",
                column: "ReginalPowerCorpRef");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationProjects_ProjectTreeRef",
                schema: "dbo",
                table: "EvaluationProjects",
                column: "ProjectTreeRef");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationProjects_ReginalPowerCorpRef",
                schema: "dbo",
                table: "EvaluationProjects",
                column: "ReginalPowerCorpRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationPeriods",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EvaluationProjects",
                schema: "dbo");
        }
    }
}
