using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class ModifyPersonInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tel",
                schema: "dbo",
                table: "Persons",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNo",
                schema: "dbo",
                table: "Persons",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tel",
                schema: "dbo",
                table: "Persons",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNo",
                schema: "dbo",
                table: "Persons",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 11,
                oldNullable: true);
        }
    }
}
