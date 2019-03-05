using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "State",
                schema: "dbo",
                table: "ExternalOrgTypes",
                nullable: true,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<byte>(
                name: "State",
                schema: "dbo",
                table: "ExternalOrganizations",
                nullable: true,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<byte>(
                name: "State",
                schema: "dbo",
                table: "ContractInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                schema: "dbo",
                table: "ContractInfos");

            migrationBuilder.AlterColumn<byte>(
                name: "State",
                schema: "dbo",
                table: "ExternalOrgTypes",
                nullable: false,
                oldClrType: typeof(byte),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "State",
                schema: "dbo",
                table: "ExternalOrganizations",
                nullable: false,
                oldClrType: typeof(byte),
                oldNullable: true);
        }
    }
}
