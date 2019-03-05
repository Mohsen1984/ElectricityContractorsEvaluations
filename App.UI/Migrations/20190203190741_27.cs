using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EVForms_Persons_EvalatorPersoneRef",
                schema: "dbo",
                table: "EVForms");

            migrationBuilder.DropForeignKey(
                name: "FK_EVFormTemplates_RoleOrgs_EvalatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplates");

            migrationBuilder.RenameColumn(
                name: "EvalatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplates",
                newName: "EvaluatorRoleRef");

            migrationBuilder.RenameIndex(
                name: "IX_EVFormTemplates_EvalatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplates",
                newName: "IX_EVFormTemplates_EvaluatorRoleRef");

            migrationBuilder.RenameColumn(
                name: "EvalatorPersoneRef",
                schema: "dbo",
                table: "EVForms",
                newName: "EvaluatorPersoneRef");

            migrationBuilder.RenameIndex(
                name: "IX_EVForms_EvalatorPersoneRef",
                schema: "dbo",
                table: "EVForms",
                newName: "IX_EVForms_EvaluatorPersoneRef");

            migrationBuilder.AddForeignKey(
                name: "FK_EVForms_Persons_EvaluatorPersoneRef",
                schema: "dbo",
                table: "EVForms",
                column: "EvaluatorPersoneRef",
                principalSchema: "dbo",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EVFormTemplates_RoleOrgs_EvaluatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplates",
                column: "EvaluatorRoleRef",
                principalSchema: "dbo",
                principalTable: "RoleOrgs",
                principalColumn: "RoleOrgId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EVForms_Persons_EvaluatorPersoneRef",
                schema: "dbo",
                table: "EVForms");

            migrationBuilder.DropForeignKey(
                name: "FK_EVFormTemplates_RoleOrgs_EvaluatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplates");

            migrationBuilder.RenameColumn(
                name: "EvaluatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplates",
                newName: "EvalatorRoleRef");

            migrationBuilder.RenameIndex(
                name: "IX_EVFormTemplates_EvaluatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplates",
                newName: "IX_EVFormTemplates_EvalatorRoleRef");

            migrationBuilder.RenameColumn(
                name: "EvaluatorPersoneRef",
                schema: "dbo",
                table: "EVForms",
                newName: "EvalatorPersoneRef");

            migrationBuilder.RenameIndex(
                name: "IX_EVForms_EvaluatorPersoneRef",
                schema: "dbo",
                table: "EVForms",
                newName: "IX_EVForms_EvalatorPersoneRef");

            migrationBuilder.AddForeignKey(
                name: "FK_EVForms_Persons_EvalatorPersoneRef",
                schema: "dbo",
                table: "EVForms",
                column: "EvalatorPersoneRef",
                principalSchema: "dbo",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EVFormTemplates_RoleOrgs_EvalatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplates",
                column: "EvalatorRoleRef",
                principalSchema: "dbo",
                principalTable: "RoleOrgs",
                principalColumn: "RoleOrgId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
