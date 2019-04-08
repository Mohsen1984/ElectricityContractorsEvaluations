using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_ServiceTree_ProjectInfoRef",
                schema: "dbo",
                table: "ProjectMember");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_ProjectInfos_ProjectInfoRef",
                schema: "dbo",
                table: "ProjectMember",
                column: "ProjectInfoRef",
                principalSchema: "dbo",
                principalTable: "ProjectInfos",
                principalColumn: "ProjectInfoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_ProjectInfos_ProjectInfoRef",
                schema: "dbo",
                table: "ProjectMember");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_ServiceTree_ProjectInfoRef",
                schema: "dbo",
                table: "ProjectMember",
                column: "ProjectInfoRef",
                principalSchema: "dbo",
                principalTable: "ServiceTree",
                principalColumn: "ServiceTreeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
