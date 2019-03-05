using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvalatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplateItems");

            migrationBuilder.AddColumn<int>(
                name: "EvaluationPeriodRef",
                schema: "dbo",
                table: "EVFormTemplates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EVFormTemplates_EvaluationPeriodRef",
                schema: "dbo",
                table: "EVFormTemplates",
                column: "EvaluationPeriodRef");

            migrationBuilder.AddForeignKey(
                name: "FK_EVFormTemplates_EvaluationPeriods_EvaluationPeriodRef",
                schema: "dbo",
                table: "EVFormTemplates",
                column: "EvaluationPeriodRef",
                principalSchema: "dbo",
                principalTable: "EvaluationPeriods",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EVFormTemplates_EvaluationPeriods_EvaluationPeriodRef",
                schema: "dbo",
                table: "EVFormTemplates");

            migrationBuilder.DropIndex(
                name: "IX_EVFormTemplates_EvaluationPeriodRef",
                schema: "dbo",
                table: "EVFormTemplates");

            migrationBuilder.DropColumn(
                name: "EvaluationPeriodRef",
                schema: "dbo",
                table: "EVFormTemplates");

            migrationBuilder.AddColumn<int>(
                name: "EvalatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplateItems",
                nullable: true);
        }
    }
}
