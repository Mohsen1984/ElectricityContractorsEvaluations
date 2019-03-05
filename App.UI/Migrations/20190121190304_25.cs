using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodRef",
                schema: "dbo",
                table: "EvaluationProjects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationProjects_PeriodRef",
                schema: "dbo",
                table: "EvaluationProjects",
                column: "PeriodRef");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationProjects_EvaluationPeriods_PeriodRef",
                schema: "dbo",
                table: "EvaluationProjects",
                column: "PeriodRef",
                principalSchema: "dbo",
                principalTable: "EvaluationPeriods",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationProjects_EvaluationPeriods_PeriodRef",
                schema: "dbo",
                table: "EvaluationProjects");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationProjects_PeriodRef",
                schema: "dbo",
                table: "EvaluationProjects");

            migrationBuilder.DropColumn(
                name: "PeriodRef",
                schema: "dbo",
                table: "EvaluationProjects");
        }
    }
}
