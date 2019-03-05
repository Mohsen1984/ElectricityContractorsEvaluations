using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ProjectInfos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfos_ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ProjectInfos",
                column: "ServiceTemplateTreeRef");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInfos_ServiceTemplateTree_ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ProjectInfos",
                column: "ServiceTemplateTreeRef",
                principalSchema: "dbo",
                principalTable: "ServiceTemplateTree",
                principalColumn: "ServiceTemplateTreeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectInfos_ServiceTemplateTree_ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropIndex(
                name: "IX_ProjectInfos_ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropColumn(
                name: "ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ProjectInfos");
        }
    }
}
