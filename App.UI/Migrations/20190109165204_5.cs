using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractInfos",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    ContactID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    ContactNo = table.Column<string>(nullable: true),
                    ProjectTreeRef = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInfos", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_ContractInfos_ProjectTrees_ProjectTreeRef",
                        column: x => x.ProjectTreeRef,
                        principalSchema: "dbo",
                        principalTable: "ProjectTrees",
                        principalColumn: "ProjectTreeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractInfos_ProjectTreeRef",
                schema: "dbo",
                table: "ContractInfos",
                column: "ProjectTreeRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractInfos",
                schema: "dbo");
        }
    }
}
