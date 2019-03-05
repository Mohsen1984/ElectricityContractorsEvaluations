using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationFactorTrees",
                schema: "dbo",
                columns: table => new
                {
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    EvaluationFactorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    EvaluationFactorRef = table.Column<int>(nullable: true),
                    EvaluationFactorType = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationFactorTrees", x => x.EvaluationFactorID);
                    table.ForeignKey(
                        name: "FK_EvaluationFactorTrees_EvaluationFactorTrees_EvaluationFactorRef",
                        column: x => x.EvaluationFactorRef,
                        principalSchema: "dbo",
                        principalTable: "EvaluationFactorTrees",
                        principalColumn: "EvaluationFactorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationFactorTrees_EvaluationFactorRef",
                schema: "dbo",
                table: "EvaluationFactorTrees",
                column: "EvaluationFactorRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationFactorTrees",
                schema: "dbo");
        }
    }
}
