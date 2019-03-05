using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "EVFormTemplates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "EVForms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                schema: "dbo",
                table: "EVFormTemplates");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "dbo",
                table: "EVForms");
        }
    }
}
