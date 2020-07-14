using Microsoft.EntityFrameworkCore.Migrations;

namespace SchedulerAPI.Migrations
{
    public partial class RemovedFKFromJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobRevisions_JobRevisionId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_QuoteRevisions_QuoteRevisionId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobRevisionId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_QuoteRevisionId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobRevisionId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "QuoteRevisionId",
                table: "Jobs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobRevisionId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteRevisionId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobRevisionId",
                table: "Jobs",
                column: "JobRevisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_QuoteRevisionId",
                table: "Jobs",
                column: "QuoteRevisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobRevisions_JobRevisionId",
                table: "Jobs",
                column: "JobRevisionId",
                principalTable: "JobRevisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_QuoteRevisions_QuoteRevisionId",
                table: "Jobs",
                column: "QuoteRevisionId",
                principalTable: "QuoteRevisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
