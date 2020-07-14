using Microsoft.EntityFrameworkCore.Migrations;

namespace SchedulerAPI.Migrations
{
    public partial class CorrectedJobRevisions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobRevision_JobRevisionId",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobRevision",
                table: "JobRevision");

            migrationBuilder.RenameTable(
                name: "JobRevision",
                newName: "JobRevisions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobRevisions",
                table: "JobRevisions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobRevisions_JobRevisionId",
                table: "Jobs",
                column: "JobRevisionId",
                principalTable: "JobRevisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobRevisions_JobRevisionId",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobRevisions",
                table: "JobRevisions");

            migrationBuilder.RenameTable(
                name: "JobRevisions",
                newName: "JobRevision");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobRevision",
                table: "JobRevision",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobRevision_JobRevisionId",
                table: "Jobs",
                column: "JobRevisionId",
                principalTable: "JobRevision",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
