using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AdminAccounts_AdminId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Jobs_JobId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsEditHistory_AdminAccounts_EditorId",
                table: "JobsEditHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsEditHistory_Jobs_JobId",
                table: "JobsEditHistory");

            migrationBuilder.AlterColumn<Guid>(
                name: "JobId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AdminAccounts_AdminId",
                table: "Applications",
                column: "AdminId",
                principalTable: "AdminAccounts",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Jobs_JobId",
                table: "Applications",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_JobsEditHistory_AdminAccounts_EditorId",
                table: "JobsEditHistory",
                column: "EditorId",
                principalTable: "AdminAccounts",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_JobsEditHistory_Jobs_JobId",
                table: "JobsEditHistory",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AdminAccounts_AdminId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Jobs_JobId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsEditHistory_AdminAccounts_EditorId",
                table: "JobsEditHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsEditHistory_Jobs_JobId",
                table: "JobsEditHistory");

            migrationBuilder.AlterColumn<Guid>(
                name: "JobId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AdminAccounts_AdminId",
                table: "Applications",
                column: "AdminId",
                principalTable: "AdminAccounts",
                principalColumn: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Jobs_JobId",
                table: "Applications",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobsEditHistory_AdminAccounts_EditorId",
                table: "JobsEditHistory",
                column: "EditorId",
                principalTable: "AdminAccounts",
                principalColumn: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobsEditHistory_Jobs_JobId",
                table: "JobsEditHistory",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId");
        }
    }
}
