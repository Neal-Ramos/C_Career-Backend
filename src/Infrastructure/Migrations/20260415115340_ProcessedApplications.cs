using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProcessedApplications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthCodes_AdminAccounts_OwnerId",
                table: "AuthCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AdminAccounts_AdminId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsEditHistory_AdminAccounts_EditorId",
                table: "JobsEditHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsEditHistory_Jobs_JobId",
                table: "JobsEditHistory");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "AuthCodes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AdminId",
                table: "Applications",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AdminAccounts_AdminId",
                table: "Applications",
                column: "AdminId",
                principalTable: "AdminAccounts",
                principalColumn: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthCodes_AdminAccounts_OwnerId",
                table: "AuthCodes",
                column: "OwnerId",
                principalTable: "AdminAccounts",
                principalColumn: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AdminAccounts_AdminId",
                table: "Jobs",
                column: "AdminId",
                principalTable: "AdminAccounts",
                principalColumn: "AdminId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AdminAccounts_AdminId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthCodes_AdminAccounts_OwnerId",
                table: "AuthCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AdminAccounts_AdminId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsEditHistory_AdminAccounts_EditorId",
                table: "JobsEditHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsEditHistory_Jobs_JobId",
                table: "JobsEditHistory");

            migrationBuilder.DropIndex(
                name: "IX_Applications_AdminId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Applications");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "AuthCodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthCodes_AdminAccounts_OwnerId",
                table: "AuthCodes",
                column: "OwnerId",
                principalTable: "AdminAccounts",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AdminAccounts_AdminId",
                table: "Jobs",
                column: "AdminId",
                principalTable: "AdminAccounts",
                principalColumn: "AdminId",
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
