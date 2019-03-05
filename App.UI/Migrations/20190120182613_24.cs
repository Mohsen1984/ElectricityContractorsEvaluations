using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeriadId",
                schema: "dbo",
                table: "EvaluationPeriods",
                newName: "PeriodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeriodId",
                schema: "dbo",
                table: "EvaluationPeriods",
                newName: "PeriadId");
        }
    }
}
