using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FFF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Jobs_ApplicationId",
                table: "JobApplications");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Jobs_CreatorId",
                table: "Jobs");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CreatorId",
                table: "Jobs",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Jobs_JobId",
                table: "JobApplications",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Jobs_JobId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CreatorId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Jobs_CreatorId",
                table: "Jobs",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Jobs_ApplicationId",
                table: "JobApplications",
                column: "ApplicationId",
                principalTable: "Jobs",
                principalColumn: "CreatorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
