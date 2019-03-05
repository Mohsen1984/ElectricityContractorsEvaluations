using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactNo",
                schema: "dbo",
                table: "ContractInfos",
                newName: "ContractNo");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "dbo",
                table: "ContractInfos",
                newName: "ContractID");

            migrationBuilder.AddColumn<int>(
                name: "ReginalPowerCorpRef",
                schema: "dbo",
                table: "ContractInfos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectInfos",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    ProjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    ProjectNo = table.Column<string>(nullable: true),
                    ProjectTreeRef = table.Column<int>(nullable: true),
                    ReginalPowerCorpRef = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInfos", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_ProjectInfos_ProjectTrees_ProjectTreeRef",
                        column: x => x.ProjectTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ProjectTrees",
                        principalColumn: "ProjectTreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfos_ReginalPowerCorps_ReginalPowerCorpRef",
                        column: x => x.ReginalPowerCorpRef,
                        principalSchema: "dbo",
                        principalTable: "ReginalPowerCorps",
                        principalColumn: "ReginalPowerCorpId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractInfos_ReginalPowerCorpRef",
                schema: "dbo",
                table: "ContractInfos",
                column: "ReginalPowerCorpRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfos_ProjectTreeRef",
                schema: "dbo",
                table: "ProjectInfos",
                column: "ProjectTreeRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfos_ReginalPowerCorpRef",
                schema: "dbo",
                table: "ProjectInfos",
                column: "ReginalPowerCorpRef");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractInfos_ReginalPowerCorps_ReginalPowerCorpRef",
                schema: "dbo",
                table: "ContractInfos",
                column: "ReginalPowerCorpRef",
                principalSchema: "dbo",
                principalTable: "ReginalPowerCorps",
                principalColumn: "ReginalPowerCorpId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractInfos_ReginalPowerCorps_ReginalPowerCorpRef",
                schema: "dbo",
                table: "ContractInfos");

            migrationBuilder.DropTable(
                name: "ProjectInfos",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_ContractInfos_ReginalPowerCorpRef",
                schema: "dbo",
                table: "ContractInfos");

            migrationBuilder.DropColumn(
                name: "ReginalPowerCorpRef",
                schema: "dbo",
                table: "ContractInfos");

            migrationBuilder.RenameColumn(
                name: "ContractNo",
                schema: "dbo",
                table: "ContractInfos",
                newName: "ContactNo");

            migrationBuilder.RenameColumn(
                name: "ContractID",
                schema: "dbo",
                table: "ContractInfos",
                newName: "ContactID");
        }
    }
}
