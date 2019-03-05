using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EVForms_RoleOrgs_EvalatorRoleRef",
                schema: "dbo",
                table: "EVForms");

            migrationBuilder.RenameColumn(
                name: "EvalatorRoleRef",
                schema: "dbo",
                table: "EVForms",
                newName: "EvaluatorRoleRef");

            migrationBuilder.RenameIndex(
                name: "IX_EVForms_EvalatorRoleRef",
                schema: "dbo",
                table: "EVForms",
                newName: "IX_EVForms_EvaluatorRoleRef");

            migrationBuilder.AddForeignKey(
                name: "FK_EVForms_RoleOrgs_EvaluatorRoleRef",
                schema: "dbo",
                table: "EVForms",
                column: "EvaluatorRoleRef",
                principalSchema: "dbo",
                principalTable: "RoleOrgs",
                principalColumn: "RoleOrgId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EVForms_RoleOrgs_EvaluatorRoleRef",
                schema: "dbo",
                table: "EVForms");

            migrationBuilder.RenameColumn(
                name: "EvaluatorRoleRef",
                schema: "dbo",
                table: "EVForms",
                newName: "EvalatorRoleRef");

            migrationBuilder.RenameIndex(
                name: "IX_EVForms_EvaluatorRoleRef",
                schema: "dbo",
                table: "EVForms",
                newName: "IX_EVForms_EvalatorRoleRef");

            migrationBuilder.AddForeignKey(
                name: "FK_EVForms_RoleOrgs_EvalatorRoleRef",
                schema: "dbo",
                table: "EVForms",
                column: "EvalatorRoleRef",
                principalSchema: "dbo",
                principalTable: "RoleOrgs",
                principalColumn: "RoleOrgId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
