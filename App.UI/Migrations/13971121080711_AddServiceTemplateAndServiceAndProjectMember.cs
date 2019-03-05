using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class AddServiceTemplateAndServiceAndProjectMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                schema: "dbo",
                table: "EVFormItems");

            migrationBuilder.AddColumn<int>(
                name: "EVFormTemplateItemRef",
                schema: "dbo",
                table: "EVFormTemplateItems",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                schema: "dbo",
                table: "EVFormTemplateItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "EVFormItemRef",
                schema: "dbo",
                table: "EVFormItems",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                schema: "dbo",
                table: "EVFormItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ServiceTemplateTree",
                schema: "dbo",
                columns: table => new
                {
                    ServiceTemplateTreeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ReginalPowerCorpRef = table.Column<int>(nullable: true),
                    ServiceTemplateTreeRef = table.Column<int>(nullable: true),
                    ProjectTreeRef = table.Column<int>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    EvalType = table.Column<byte>(nullable: true),
                    LevelCode = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTemplateTree", x => x.ServiceTemplateTreeId);
                    table.ForeignKey(
                        name: "FK_ServiceTemplateTree_ProjectTrees_ProjectTreeRef",
                        column: x => x.ProjectTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ProjectTrees",
                        principalColumn: "ProjectTreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTemplateTree_ReginalPowerCorps_ReginalPowerCorpRef",
                        column: x => x.ReginalPowerCorpRef,
                        principalSchema: "dbo",
                        principalTable: "ReginalPowerCorps",
                        principalColumn: "ReginalPowerCorpId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTemplateTree_ServiceTemplateTree_ServiceTemplateTreeRef",
                        column: x => x.ServiceTemplateTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ServiceTemplateTree",
                        principalColumn: "ServiceTemplateTreeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTree",
                schema: "dbo",
                columns: table => new
                {
                    ServiceTreeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ReginalPowerCorpRef = table.Column<int>(nullable: true),
                    ProjectInfoRef = table.Column<int>(nullable: true),
                    ServiceTreeRef = table.Column<int>(nullable: true),
                    ProjectTreeRef = table.Column<int>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    LevelCode = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTree", x => x.ServiceTreeId);
                    table.ForeignKey(
                        name: "FK_ServiceTree_ProjectInfos_ProjectInfoRef",
                        column: x => x.ProjectInfoRef,
                        principalSchema: "dbo",
                        principalTable: "ProjectInfos",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTree_ProjectTrees_ProjectTreeRef",
                        column: x => x.ProjectTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ProjectTrees",
                        principalColumn: "ProjectTreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTree_ReginalPowerCorps_ReginalPowerCorpRef",
                        column: x => x.ReginalPowerCorpRef,
                        principalSchema: "dbo",
                        principalTable: "ReginalPowerCorps",
                        principalColumn: "ReginalPowerCorpId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTree_ServiceTree_ServiceTreeRef",
                        column: x => x.ServiceTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ServiceTree",
                        principalColumn: "ServiceTreeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMember",
                schema: "dbo",
                columns: table => new
                {
                    ProjectMemberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ReginalPowerCorpRef = table.Column<int>(nullable: true),
                    ServiceTreeRef = table.Column<int>(nullable: true),
                    PersoneRef = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMember", x => x.ProjectMemberID);
                    table.ForeignKey(
                        name: "FK_ProjectMember_Persons_PersoneRef",
                        column: x => x.PersoneRef,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMember_ReginalPowerCorps_ReginalPowerCorpRef",
                        column: x => x.ReginalPowerCorpRef,
                        principalSchema: "dbo",
                        principalTable: "ReginalPowerCorps",
                        principalColumn: "ReginalPowerCorpId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMember_ServiceTree_ServiceTreeRef",
                        column: x => x.ServiceTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ServiceTree",
                        principalColumn: "ServiceTreeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EVFormTemplateItems_EVFormTemplateItemRef",
                schema: "dbo",
                table: "EVFormTemplateItems",
                column: "EVFormTemplateItemRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVFormItems_EVFormItemRef",
                schema: "dbo",
                table: "EVFormItems",
                column: "EVFormItemRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_PersoneRef",
                schema: "dbo",
                table: "ProjectMember",
                column: "PersoneRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_ReginalPowerCorpRef",
                schema: "dbo",
                table: "ProjectMember",
                column: "ReginalPowerCorpRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_ServiceTreeRef",
                schema: "dbo",
                table: "ProjectMember",
                column: "ServiceTreeRef");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplateTree_ProjectTreeRef",
                schema: "dbo",
                table: "ServiceTemplateTree",
                column: "ProjectTreeRef");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplateTree_ReginalPowerCorpRef",
                schema: "dbo",
                table: "ServiceTemplateTree",
                column: "ReginalPowerCorpRef");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTemplateTree_ServiceTemplateTreeRef",
                schema: "dbo",
                table: "ServiceTemplateTree",
                column: "ServiceTemplateTreeRef");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTree_ProjectInfoRef",
                schema: "dbo",
                table: "ServiceTree",
                column: "ProjectInfoRef");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTree_ProjectTreeRef",
                schema: "dbo",
                table: "ServiceTree",
                column: "ProjectTreeRef");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTree_ReginalPowerCorpRef",
                schema: "dbo",
                table: "ServiceTree",
                column: "ReginalPowerCorpRef");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTree_ServiceTreeRef",
                schema: "dbo",
                table: "ServiceTree",
                column: "ServiceTreeRef");

            migrationBuilder.AddForeignKey(
                name: "FK_EVFormItems_EVFormItems_EVFormItemRef",
                schema: "dbo",
                table: "EVFormItems",
                column: "EVFormItemRef",
                principalSchema: "dbo",
                principalTable: "EVFormItems",
                principalColumn: "EVFormItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EVFormTemplateItems_EVFormTemplateItems_EVFormTemplateItemRef",
                schema: "dbo",
                table: "EVFormTemplateItems",
                column: "EVFormTemplateItemRef",
                principalSchema: "dbo",
                principalTable: "EVFormTemplateItems",
                principalColumn: "EVFormTemplateItemId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EVFormItems_EVFormItems_EVFormItemRef",
                schema: "dbo",
                table: "EVFormItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EVFormTemplateItems_EVFormTemplateItems_EVFormTemplateItemRef",
                schema: "dbo",
                table: "EVFormTemplateItems");

            migrationBuilder.DropTable(
                name: "ProjectMember",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ServiceTemplateTree",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ServiceTree",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_EVFormTemplateItems_EVFormTemplateItemRef",
                schema: "dbo",
                table: "EVFormTemplateItems");

            migrationBuilder.DropIndex(
                name: "IX_EVFormItems_EVFormItemRef",
                schema: "dbo",
                table: "EVFormItems");

            migrationBuilder.DropColumn(
                name: "EVFormTemplateItemRef",
                schema: "dbo",
                table: "EVFormTemplateItems");

            migrationBuilder.DropColumn(
                name: "Score",
                schema: "dbo",
                table: "EVFormTemplateItems");

            migrationBuilder.DropColumn(
                name: "EVFormItemRef",
                schema: "dbo",
                table: "EVFormItems");

            migrationBuilder.DropColumn(
                name: "Score",
                schema: "dbo",
                table: "EVFormItems");

            migrationBuilder.AddColumn<string>(
                name: "Result",
                schema: "dbo",
                table: "EVFormItems",
                nullable: true);
        }
    }
}
