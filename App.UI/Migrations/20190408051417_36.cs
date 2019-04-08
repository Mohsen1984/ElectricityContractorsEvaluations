using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectInfoRef",
                schema: "dbo",
                table: "ProjectMember",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_ProjectInfoRef",
                schema: "dbo",
                table: "ProjectMember",
                column: "ProjectInfoRef");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_ServiceTree_ProjectInfoRef",
                schema: "dbo",
                table: "ProjectMember");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMember_ProjectInfoRef",
                schema: "dbo",
                table: "ProjectMember");

            migrationBuilder.DropColumn(
                name: "ProjectInfoRef",
                schema: "dbo",
                table: "ProjectMember");
        }
    }
}
