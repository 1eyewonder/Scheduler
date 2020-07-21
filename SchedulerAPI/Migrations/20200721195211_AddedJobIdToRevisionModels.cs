using Microsoft.EntityFrameworkCore.Migrations;

namespace SchedulerAPI.Migrations
{
    public partial class AddedJobIdToRevisionModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRevisions_Jobs_JobId",
                table: "JobRevisions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteRevisions_Jobs_JobId",
                table: "QuoteRevisions");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_QuoteNumber",
                table: "Jobs");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "QuoteRevisions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "JobRevisions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);

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
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "QuoteRevisions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "JobRevisions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_QuoteNumber",
                table: "Jobs",
                column: "QuoteNumber",
                unique: true);

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
    }
}
