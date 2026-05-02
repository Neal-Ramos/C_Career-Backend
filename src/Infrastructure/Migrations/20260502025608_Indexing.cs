using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Indexing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_JobsEditHistory_EditId",
                table: "JobsEditHistory");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Pending");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AdminAccounts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_OwnerId",
                table: "RefreshTokens",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobsEditHistory_EditId",
                table: "JobsEditHistory",
                column: "EditId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Title",
                table: "Jobs",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Email",
                table: "Applications",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Status",
                table: "Applications",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AdminAccounts_Email",
                table: "AdminAccounts",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AdminAccounts_Password",
                table: "AdminAccounts",
                column: "Password");

            migrationBuilder.CreateIndex(
                name: "IX_AdminAccounts_UserName",
                table: "AdminAccounts",
                column: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_OwnerId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_JobsEditHistory_EditId",
                table: "JobsEditHistory");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_Title",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Applications_Email",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_Status",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_AdminAccounts_Email",
                table: "AdminAccounts");

            migrationBuilder.DropIndex(
                name: "IX_AdminAccounts_Password",
                table: "AdminAccounts");

            migrationBuilder.DropIndex(
                name: "IX_AdminAccounts_UserName",
                table: "AdminAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AdminAccounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token");

            migrationBuilder.CreateIndex(
                name: "IX_JobsEditHistory_EditId",
                table: "JobsEditHistory",
                column: "EditId");
        }
    }
}
