using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditJobHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AdminAccounts_CreatorId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobsEditHistory_AdminAccounts_EditedById",
                table: "JobsEditHistory");

            migrationBuilder.DropIndex(
                name: "IX_JobsEditHistory_EditedById",
                table: "JobsEditHistory");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CreatorId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "EditedById",
                table: "JobsEditHistory");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Jobs");

            migrationBuilder.AlterColumn<Guid>(
                name: "EditorId",
                table: "JobsEditHistory",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "EditSummary",
                table: "JobsEditHistory",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EditId",
                table: "JobsEditHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "JobsEditHistory",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobsEditHistory_EditId",
                table: "JobsEditHistory",
                column: "EditId");

            migrationBuilder.CreateIndex(
                name: "IX_JobsEditHistory_EditorId",
                table: "JobsEditHistory",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobsEditHistory_JobId",
                table: "JobsEditHistory",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_AdminId",
                table: "Jobs",
                column: "AdminId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_JobsEditHistory_EditId",
                table: "JobsEditHistory");

            migrationBuilder.DropIndex(
                name: "IX_JobsEditHistory_EditorId",
                table: "JobsEditHistory");

            migrationBuilder.DropIndex(
                name: "IX_JobsEditHistory_JobId",
                table: "JobsEditHistory");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_AdminId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobsEditHistory");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Jobs");

            migrationBuilder.AlterColumn<Guid>(
                name: "EditorId",
                table: "JobsEditHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EditSummary",
                table: "JobsEditHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<Guid>(
                name: "EditId",
                table: "JobsEditHistory",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<int>(
                name: "EditedById",
                table: "JobsEditHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Jobs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JobsEditHistory_EditedById",
                table: "JobsEditHistory",
                column: "EditedById");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CreatorId",
                table: "Jobs",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AdminAccounts_CreatorId",
                table: "Jobs",
                column: "CreatorId",
                principalTable: "AdminAccounts",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobsEditHistory_AdminAccounts_EditedById",
                table: "JobsEditHistory",
                column: "EditedById",
                principalTable: "AdminAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
