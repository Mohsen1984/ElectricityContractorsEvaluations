using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectId",
                schema: "dbo",
                table: "ProjectInfos",
                newName: "ProjectInfoId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                schema: "dbo",
                table: "EVFormItems",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "Result",
                schema: "dbo",
                table: "EVFormItems",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WeightFactor",
                schema: "dbo",
                table: "EVFormItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                schema: "dbo",
                table: "EVFormItems");

            migrationBuilder.DropColumn(
                name: "WeightFactor",
                schema: "dbo",
                table: "EVFormItems");

            migrationBuilder.RenameColumn(
                name: "ProjectInfoId",
                schema: "dbo",
                table: "ProjectInfos",
                newName: "ProjectId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                schema: "dbo",
                table: "EVFormItems",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
