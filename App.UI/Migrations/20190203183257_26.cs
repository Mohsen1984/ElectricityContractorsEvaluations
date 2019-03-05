using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActualFinishDate",
                schema: "dbo",
                table: "ProjectInfos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStartDate",
                schema: "dbo",
                table: "ProjectInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumnetCotrollerRef",
                schema: "dbo",
                table: "ProjectInfos",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedAmount",
                schema: "dbo",
                table: "ProjectInfos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlanedFinishDate",
                schema: "dbo",
                table: "ProjectInfos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlanedStartDate",
                schema: "dbo",
                table: "ProjectInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectControllerRef",
                schema: "dbo",
                table: "ProjectInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectManagerRef",
                schema: "dbo",
                table: "ProjectInfos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExternalOrganizationRef",
                schema: "dbo",
                table: "ContractInfos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EVFormTemplates",
                schema: "dbo",
                columns: table => new
                {
                    EVFormTemplateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    EvalatorRoleRef = table.Column<int>(nullable: true),
                    EvaluatedRoleRef = table.Column<int>(nullable: true),
                    ReginalPowerCorpRef = table.Column<int>(nullable: true),
                    ProjectTreeRef = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVFormTemplates", x => x.EVFormTemplateId);
                    table.ForeignKey(
                        name: "FK_EVFormTemplates_RoleOrgs_EvalatorRoleRef",
                        column: x => x.EvalatorRoleRef,
                        principalSchema: "dbo",
                        principalTable: "RoleOrgs",
                        principalColumn: "RoleOrgId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVFormTemplates_RoleOrgs_EvaluatedRoleRef",
                        column: x => x.EvaluatedRoleRef,
                        principalSchema: "dbo",
                        principalTable: "RoleOrgs",
                        principalColumn: "RoleOrgId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVFormTemplates_ProjectTrees_ProjectTreeRef",
                        column: x => x.ProjectTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ProjectTrees",
                        principalColumn: "ProjectTreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVFormTemplates_ReginalPowerCorps_ReginalPowerCorpRef",
                        column: x => x.ReginalPowerCorpRef,
                        principalSchema: "dbo",
                        principalTable: "ReginalPowerCorps",
                        principalColumn: "ReginalPowerCorpId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EVForms",
                schema: "dbo",
                columns: table => new
                {
                    EVFormId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    EVFormInstanceID = table.Column<Guid>(nullable: true),
                    EVFormTemplateRef = table.Column<int>(nullable: true),
                    EvaluationPeriodRef = table.Column<int>(nullable: true),
                    EvalatorRoleRef = table.Column<int>(nullable: true),
                    EvaluatedRoleRef = table.Column<int>(nullable: true),
                    EvalatorPersoneRef = table.Column<int>(nullable: true),
                    EvaluatedPersoneRef = table.Column<int>(nullable: true),
                    ReginalPowerCorpRef = table.Column<int>(nullable: true),
                    ProjectTreeRef = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVForms", x => x.EVFormId);
                    table.ForeignKey(
                        name: "FK_EVForms_EVFormTemplates_EVFormTemplateRef",
                        column: x => x.EVFormTemplateRef,
                        principalSchema: "dbo",
                        principalTable: "EVFormTemplates",
                        principalColumn: "EVFormTemplateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVForms_Persons_EvalatorPersoneRef",
                        column: x => x.EvalatorPersoneRef,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVForms_RoleOrgs_EvalatorRoleRef",
                        column: x => x.EvalatorRoleRef,
                        principalSchema: "dbo",
                        principalTable: "RoleOrgs",
                        principalColumn: "RoleOrgId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVForms_Persons_EvaluatedPersoneRef",
                        column: x => x.EvaluatedPersoneRef,
                        principalSchema: "dbo",
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVForms_RoleOrgs_EvaluatedRoleRef",
                        column: x => x.EvaluatedRoleRef,
                        principalSchema: "dbo",
                        principalTable: "RoleOrgs",
                        principalColumn: "RoleOrgId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVForms_EvaluationPeriods_EvaluationPeriodRef",
                        column: x => x.EvaluationPeriodRef,
                        principalSchema: "dbo",
                        principalTable: "EvaluationPeriods",
                        principalColumn: "PeriodId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVForms_ProjectTrees_ProjectTreeRef",
                        column: x => x.ProjectTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ProjectTrees",
                        principalColumn: "ProjectTreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVForms_ReginalPowerCorps_ReginalPowerCorpRef",
                        column: x => x.ReginalPowerCorpRef,
                        principalSchema: "dbo",
                        principalTable: "ReginalPowerCorps",
                        principalColumn: "ReginalPowerCorpId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EVFormTemplateItems",
                schema: "dbo",
                columns: table => new
                {
                    EVFormTemplateItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    EvalatorRoleRef = table.Column<int>(nullable: true),
                    EVFormTemplateRef = table.Column<int>(nullable: true),
                    EvaluationFactorRef = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVFormTemplateItems", x => x.EVFormTemplateItemId);
                    table.ForeignKey(
                        name: "FK_EVFormTemplateItems_EVFormTemplates_EVFormTemplateRef",
                        column: x => x.EVFormTemplateRef,
                        principalSchema: "dbo",
                        principalTable: "EVFormTemplates",
                        principalColumn: "EVFormTemplateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVFormTemplateItems_EvaluationFactorTrees_EvaluationFactorRef",
                        column: x => x.EvaluationFactorRef,
                        principalSchema: "dbo",
                        principalTable: "EvaluationFactorTrees",
                        principalColumn: "EvaluationFactorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EVFormItems",
                schema: "dbo",
                columns: table => new
                {
                    EVFormItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    EVFormInstanceID = table.Column<Guid>(nullable: true),
                    EVFormRef = table.Column<int>(nullable: true),
                    EvaluationFactorRef = table.Column<int>(nullable: true),
                    Result = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVFormItems", x => x.EVFormItemId);
                    table.ForeignKey(
                        name: "FK_EVFormItems_EVForms_EVFormRef",
                        column: x => x.EVFormRef,
                        principalSchema: "dbo",
                        principalTable: "EVForms",
                        principalColumn: "EVFormId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVFormItems_EvaluationFactorTrees_EvaluationFactorRef",
                        column: x => x.EvaluationFactorRef,
                        principalSchema: "dbo",
                        principalTable: "EvaluationFactorTrees",
                        principalColumn: "EvaluationFactorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfos_DocumnetCotrollerRef",
                schema: "dbo",
                table: "ProjectInfos",
                column: "DocumnetCotrollerRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfos_ProjectControllerRef",
                schema: "dbo",
                table: "ProjectInfos",
                column: "ProjectControllerRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfos_ProjectManagerRef",
                schema: "dbo",
                table: "ProjectInfos",
                column: "ProjectManagerRef");

            migrationBuilder.CreateIndex(
                name: "IX_ContractInfos_ExternalOrganizationRef",
                schema: "dbo",
                table: "ContractInfos",
                column: "ExternalOrganizationRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVFormItems_EVFormRef",
                schema: "dbo",
                table: "EVFormItems",
                column: "EVFormRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVFormItems_EvaluationFactorRef",
                schema: "dbo",
                table: "EVFormItems",
                column: "EvaluationFactorRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVForms_EVFormTemplateRef",
                schema: "dbo",
                table: "EVForms",
                column: "EVFormTemplateRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVForms_EvalatorPersoneRef",
                schema: "dbo",
                table: "EVForms",
                column: "EvalatorPersoneRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVForms_EvalatorRoleRef",
                schema: "dbo",
                table: "EVForms",
                column: "EvalatorRoleRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVForms_EvaluatedPersoneRef",
                schema: "dbo",
                table: "EVForms",
                column: "EvaluatedPersoneRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVForms_EvaluatedRoleRef",
                schema: "dbo",
                table: "EVForms",
                column: "EvaluatedRoleRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVForms_EvaluationPeriodRef",
                schema: "dbo",
                table: "EVForms",
                column: "EvaluationPeriodRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVForms_ProjectTreeRef",
                schema: "dbo",
                table: "EVForms",
                column: "ProjectTreeRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVForms_ReginalPowerCorpRef",
                schema: "dbo",
                table: "EVForms",
                column: "ReginalPowerCorpRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVFormTemplateItems_EVFormTemplateRef",
                schema: "dbo",
                table: "EVFormTemplateItems",
                column: "EVFormTemplateRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVFormTemplateItems_EvaluationFactorRef",
                schema: "dbo",
                table: "EVFormTemplateItems",
                column: "EvaluationFactorRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVFormTemplates_EvalatorRoleRef",
                schema: "dbo",
                table: "EVFormTemplates",
                column: "EvalatorRoleRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVFormTemplates_EvaluatedRoleRef",
                schema: "dbo",
                table: "EVFormTemplates",
                column: "EvaluatedRoleRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVFormTemplates_ProjectTreeRef",
                schema: "dbo",
                table: "EVFormTemplates",
                column: "ProjectTreeRef");

            migrationBuilder.CreateIndex(
                name: "IX_EVFormTemplates_ReginalPowerCorpRef",
                schema: "dbo",
                table: "EVFormTemplates",
                column: "ReginalPowerCorpRef");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractInfos_ExternalOrganizations_ExternalOrganizationRef",
                schema: "dbo",
                table: "ContractInfos",
                column: "ExternalOrganizationRef",
                principalSchema: "dbo",
                principalTable: "ExternalOrganizations",
                principalColumn: "ExternalOrganizationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInfos_Persons_DocumnetCotrollerRef",
                schema: "dbo",
                table: "ProjectInfos",
                column: "DocumnetCotrollerRef",
                principalSchema: "dbo",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInfos_Persons_ProjectControllerRef",
                schema: "dbo",
                table: "ProjectInfos",
                column: "ProjectControllerRef",
                principalSchema: "dbo",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInfos_Persons_ProjectManagerRef",
                schema: "dbo",
                table: "ProjectInfos",
                column: "ProjectManagerRef",
                principalSchema: "dbo",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractInfos_ExternalOrganizations_ExternalOrganizationRef",
                schema: "dbo",
                table: "ContractInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectInfos_Persons_DocumnetCotrollerRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectInfos_Persons_ProjectControllerRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectInfos_Persons_ProjectManagerRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropTable(
                name: "EVFormItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EVFormTemplateItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EVForms",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EVFormTemplates",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_ProjectInfos_DocumnetCotrollerRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropIndex(
                name: "IX_ProjectInfos_ProjectControllerRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropIndex(
                name: "IX_ProjectInfos_ProjectManagerRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropIndex(
                name: "IX_ContractInfos_ExternalOrganizationRef",
                schema: "dbo",
                table: "ContractInfos");

            migrationBuilder.DropColumn(
                name: "ActualFinishDate",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropColumn(
                name: "ActualStartDate",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropColumn(
                name: "DocumnetCotrollerRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropColumn(
                name: "EstimatedAmount",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropColumn(
                name: "PlanedFinishDate",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropColumn(
                name: "PlanedStartDate",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropColumn(
                name: "ProjectControllerRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropColumn(
                name: "ProjectManagerRef",
                schema: "dbo",
                table: "ProjectInfos");

            migrationBuilder.DropColumn(
                name: "ExternalOrganizationRef",
                schema: "dbo",
                table: "ContractInfos");
        }
    }
}
