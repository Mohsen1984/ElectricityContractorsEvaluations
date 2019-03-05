using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Test",
                schema: "dbo",
                table: "EvaluationFactorTrees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                schema: "dbo",
                table: "EvaluationFactorTrees");
        }
    }
}
