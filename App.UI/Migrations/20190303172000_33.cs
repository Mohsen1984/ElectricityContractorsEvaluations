using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.UI.Migrations
{
    public partial class _33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                schema: "dbo",
                table: "TaskLists",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DoDate",
                schema: "dbo",
                table: "TaskLists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvaluationPeriodRef",
                schema: "dbo",
                table: "TaskLists",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                schema: "dbo",
                table: "TaskLists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectInfoRef",
                schema: "dbo",
                table: "TaskLists",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WatchDate",
                schema: "dbo",
                table: "TaskLists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskLists_EvaluationPeriodRef",
                schema: "dbo",
                table: "TaskLists",
                column: "EvaluationPeriodRef");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLists_ProjectInfoRef",
                schema: "dbo",
                table: "TaskLists",
                column: "ProjectInfoRef");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_EvaluationPeriods_EvaluationPeriodRef",
                schema: "dbo",
                table: "TaskLists",
                column: "EvaluationPeriodRef",
                principalSchema: "dbo",
                principalTable: "EvaluationPeriods",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_ProjectInfos_ProjectInfoRef",
                schema: "dbo",
                table: "TaskLists",
                column: "ProjectInfoRef",
                principalSchema: "dbo",
                principalTable: "ProjectInfos",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_EvaluationPeriods_EvaluationPeriodRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_ProjectInfos_ProjectInfoRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_TaskLists_EvaluationPeriodRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropIndex(
                name: "IX_TaskLists_ProjectInfoRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "DoDate",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "EvaluationPeriodRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "InsertDate",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "ProjectInfoRef",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "WatchDate",
                schema: "dbo",
                table: "TaskLists");

            migrationBuilder.AlterColumn<string>(
                name: "DueDate",
                schema: "dbo",
                table: "TaskLists",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
