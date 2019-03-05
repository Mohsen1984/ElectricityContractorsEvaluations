using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "EducationalLevels",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    EducationalLevelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    State = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalLevels", x => x.EducationalLevelId);
                });

            migrationBuilder.CreateTable(
                name: "ExternalOrgTypes",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    ExternalOrgTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalOrgTypes", x => x.ExternalOrgTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ReginalPowerCorps",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    ReginalPowerCorpId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Manager = table.Column<string>(maxLength: 200, nullable: true),
                    ManagerMobile = table.Column<string>(maxLength: 200, nullable: true),
                    Tel = table.Column<string>(maxLength: 200, nullable: true),
                    Fax = table.Column<string>(maxLength: 200, nullable: true),
                    Mail = table.Column<string>(maxLength: 200, nullable: true),
                    MainAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    SubAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    RegisterCode = table.Column<string>(maxLength: 200, nullable: true),
                    State = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReginalPowerCorps", x => x.ReginalPowerCorpId);
                });

            migrationBuilder.CreateTable(
                name: "RoleOrgs",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    RoleOrgId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    State = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleOrgs", x => x.RoleOrgId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FistName = table.Column<string>(maxLength: 200, nullable: true),
                    LastName = table.Column<string>(maxLength: 200, nullable: true),
                    NationalID = table.Column<string>(maxLength: 10, nullable: true),
                    MobileNo = table.Column<string>(maxLength: 10, nullable: true),
                    Tel = table.Column<string>(maxLength: 10, nullable: true),
                    FatherName = table.Column<string>(maxLength: 100, nullable: true),
                    IDNumber = table.Column<int>(nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Address = table.Column<string>(maxLength: 1000, nullable: true),
                    EducationLevelRef = table.Column<int>(nullable: true),
                    State = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Persons_EducationalLevels_EducationLevelRef",
                        column: x => x.EducationLevelRef,
                        principalSchema: "dbo",
                        principalTable: "EducationalLevels",
                        principalColumn: "EducationalLevelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExternalOrganizations",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    ExternalOrganizationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalOrgTypeRef = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Manager = table.Column<string>(maxLength: 200, nullable: true),
                    ManagerMobile = table.Column<string>(maxLength: 200, nullable: true),
                    Tel = table.Column<string>(maxLength: 200, nullable: true),
                    Fax = table.Column<string>(maxLength: 200, nullable: true),
                    Mail = table.Column<string>(maxLength: 200, nullable: true),
                    MainAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    SubAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    RegisterCode = table.Column<string>(maxLength: 200, nullable: true),
                    State = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalOrganizations", x => x.ExternalOrganizationId);
                    table.ForeignKey(
                        name: "FK_ExternalOrganizations_ExternalOrgTypes_ExternalOrgTypeRef",
                        column: x => x.ExternalOrgTypeRef,
                        principalSchema: "dbo",
                        principalTable: "ExternalOrgTypes",
                        principalColumn: "ExternalOrgTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OUTrees",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    OUTreeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReginalPowerCorpRef = table.Column<int>(nullable: true),
                    OUTreeRef = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OUTrees", x => x.OUTreeId);
                    table.ForeignKey(
                        name: "FK_OUTrees_OUTrees_OUTreeRef",
                        column: x => x.OUTreeRef,
                        principalSchema: "dbo",
                        principalTable: "OUTrees",
                        principalColumn: "OUTreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OUTrees_ReginalPowerCorps_ReginalPowerCorpRef",
                        column: x => x.ReginalPowerCorpRef,
                        principalSchema: "dbo",
                        principalTable: "ReginalPowerCorps",
                        principalColumn: "ReginalPowerCorpId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTrees",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    ProjectTreeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReginalPowerCorpRef = table.Column<int>(nullable: true),
                    ProjectTreeRef = table.Column<int>(nullable: true),
                    LevelCode = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    IsTemplate = table.Column<bool>(nullable: true),
                    State = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTrees", x => x.ProjectTreeId);
                    table.ForeignKey(
                        name: "FK_ProjectTrees_ProjectTrees_ProjectTreeRef",
                        column: x => x.ProjectTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ProjectTrees",
                        principalColumn: "ProjectTreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectTrees_ReginalPowerCorps_ReginalPowerCorpRef",
                        column: x => x.ReginalPowerCorpRef,
                        principalSchema: "dbo",
                        principalTable: "ReginalPowerCorps",
                        principalColumn: "ReginalPowerCorpId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalOrganizations_ExternalOrgTypeRef",
                schema: "dbo",
                table: "ExternalOrganizations",
                column: "ExternalOrgTypeRef");

            migrationBuilder.CreateIndex(
                name: "IX_OUTrees_OUTreeRef",
                schema: "dbo",
                table: "OUTrees",
                column: "OUTreeRef");

            migrationBuilder.CreateIndex(
                name: "IX_OUTrees_ReginalPowerCorpRef",
                schema: "dbo",
                table: "OUTrees",
                column: "ReginalPowerCorpRef");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_EducationLevelRef",
                schema: "dbo",
                table: "Persons",
                column: "EducationLevelRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTrees_ProjectTreeRef",
                schema: "dbo",
                table: "ProjectTrees",
                column: "ProjectTreeRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTrees_ReginalPowerCorpRef",
                schema: "dbo",
                table: "ProjectTrees",
                column: "ReginalPowerCorpRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalOrganizations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OUTrees",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ProjectTrees",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoleOrgs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ExternalOrgTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EducationalLevels",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ReginalPowerCorps",
                schema: "dbo");
        }
    }
}
