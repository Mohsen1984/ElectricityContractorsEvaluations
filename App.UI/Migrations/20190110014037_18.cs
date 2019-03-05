using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                schema: "dbo",
                table: "EvaluationFactorTrees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Test",
                schema: "dbo",
                table: "EvaluationFactorTrees",
                maxLength: 5,
                nullable: true);
        }
    }
}
