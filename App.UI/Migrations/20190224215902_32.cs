using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ServiceTree",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTree_ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ServiceTree",
                column: "ServiceTemplateTreeRef");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTree_ServiceTemplateTree_ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ServiceTree",
                column: "ServiceTemplateTreeRef",
                principalSchema: "dbo",
                principalTable: "ServiceTemplateTree",
                principalColumn: "ServiceTemplateTreeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTree_ServiceTemplateTree_ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ServiceTree");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTree_ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ServiceTree");

            migrationBuilder.DropColumn(
                name: "ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ServiceTree");
        }
    }
}
