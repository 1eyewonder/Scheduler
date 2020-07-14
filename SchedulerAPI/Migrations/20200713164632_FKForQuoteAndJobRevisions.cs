using Microsoft.EntityFrameworkCore.Migrations;

namespace SchedulerAPI.Migrations
{
    public partial class FKForQuoteAndJobRevisions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "QuoteRevisions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "JobRevisions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRevisions_JobId",
                table: "QuoteRevisions",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRevisions_JobId",
                table: "JobRevisions",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRevisions_Jobs_JobId",
                table: "JobRevisions",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRevisions_Jobs_JobId",
                table: "QuoteRevisions",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRevisions_Jobs_JobId",
                table: "JobRevisions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRevisions_Jobs_JobId",
                table: "QuoteRevisions");

            migrationBuilder.DropIndex(
                name: "IX_QuoteRevisions_JobId",
                table: "QuoteRevisions");

            migrationBuilder.DropIndex(
                name: "IX_JobRevisions_JobId",
                table: "JobRevisions");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "QuoteRevisions");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobRevisions");
        }
    }
}
