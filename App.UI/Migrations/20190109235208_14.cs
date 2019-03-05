using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EvaluationFactorType",
                schema: "dbo",
                table: "EvaluationFactorTrees",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EvaluationFactorType",
                schema: "dbo",
                table: "EvaluationFactorTrees",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
