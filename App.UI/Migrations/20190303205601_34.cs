using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssigneTo",
                schema: "dbo",
                table: "TaskLists",
                newName: "SenderProjectMemberRef");

            migrationBuilder.AddColumn<int>(
                name: "ReciverProjectMemberRef",
                schema: "dbo",
                table: "TaskLists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleOrgsRef",
                schema: "dbo",
                table: "ProjectMember",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskLists_ReciverProjectMemberRef",
                schema: "dbo",
                table: "TaskLists",
                column: "ReciverProjectMemberRef");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLists_SenderProjectMemberRef",
                schema: "dbo",
                table: "TaskLists",
                column: "SenderProjectMemberRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_RoleOrgsRef",
                schema: "dbo",
                table: "ProjectMember",
                column: "RoleOrgsRef");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_RoleOrgs_RoleOrgsRef",
                schema: "dbo",
                table: "ProjectMember",
                column: "RoleOrgsRef",
                principalSchema: "dbo",
                principalTable: "RoleOrgs",
                principalColumn: "RoleOrgId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_ProjectMember_ReciverProjectMemberRef",
                schema: "dbo",
                table: "TaskLists",
                column: "ReciverProjectMemberRef",
                principalSchema: "dbo",
                principalTable: "ProjectMember",
                principalColumn: "ProjectMemberID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_ProjectMember_SenderProjectMemberRef",
                schema: "dbo",
                table: "TaskLists",
                column: "SenderProjectMemberRef",
                principalSchema: "dbo",
                principalTable: "ProjectMember",
                principalColumn: "ProjectMemberID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_RoleOrgs_RoleOrgsRef",
                schema: "dbo",
                table: "ProjectMember");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_ProjectMember_ReciverProjectMemberRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_ProjectMember_SenderProjectMemberRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_TaskLists_ReciverProjectMemberRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_TaskLists_SenderProjectMemberRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMember_RoleOrgsRef",
                schema: "dbo",
                table: "ProjectMember");

            migrationBuilder.DropColumn(
                name: "ReciverProjectMemberRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "RoleOrgsRef",
                schema: "dbo",
                table: "ProjectMember");

            migrationBuilder.RenameColumn(
                name: "SenderProjectMemberRef",
                schema: "dbo",
                table: "TaskLists",
                newName: "AssigneTo");
        }
    }
}
