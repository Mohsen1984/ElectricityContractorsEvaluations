using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "dbo",
                table: "ProjectInfos",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "EvaluationFactorID",
                schema: "dbo",
                table: "EvaluationFactorTrees",
                newName: "EvaluationFactorId");

            migrationBuilder.RenameColumn(
                name: "ContractID",
                schema: "dbo",
                table: "ContractInfos",
                newName: "ContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectId",
                schema: "dbo",
                table: "ProjectInfos",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "EvaluationFactorId",
                schema: "dbo",
                table: "EvaluationFactorTrees",
                newName: "EvaluationFactorID");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                schema: "dbo",
                table: "ContractInfos",
                newName: "ContractID");
        }
    }
}
