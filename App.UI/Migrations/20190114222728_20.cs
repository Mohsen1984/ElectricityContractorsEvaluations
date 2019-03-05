using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskLists",
                schema: "dbo",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<byte>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 80, nullable: true),
                    Created = table.Column<string>(maxLength: 10, nullable: true),
                    ModifyBy = table.Column<string>(maxLength: 80, nullable: true),
                    Modified = table.Column<string>(maxLength: 10, nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    WfInstanceID = table.Column<Guid>(nullable: true),
                    FormLink = table.Column<string>(maxLength: 100, nullable: true),
                    DuDate = table.Column<DateTime>(nullable: true),
                    AssigneTo = table.Column<int>(nullable: true),
                    Priority = table.Column<byte>(nullable: true),
                    OutCome = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLists", x => x.TaskId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskLists",
                schema: "dbo");
        }
    }
}
