using Microsoft.EntityFrameworkCore.Migrations;

namespace SchedulerAPI.Migrations
{
    public partial class DroppedRevisionForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRevisions_Jobs_JobId",
                table: "JobRevisions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRevisions_Jobs_JobId",
                table: "QuoteRevisions");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "QuoteRevisions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "JobRevisions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRevisions_Jobs_JobId",
                table: "JobRevisions",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteRevisions_Jobs_JobId",
                table: "QuoteRevisions",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRevisions_Jobs_JobId",
                table: "JobRevisions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRevisions_Jobs_JobId",
                table: "QuoteRevisions");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "QuoteRevisions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "JobRevisions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
    }
}
