using Microsoft.EntityFrameworkCore.Migrations;

namespace SchedulerAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobRevision",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevisionNumber = table.Column<int>(nullable: false),
                    RevisionSummary = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRevision", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuoteRevisions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevisionNumber = table.Column<int>(nullable: false),
                    RevisionSummary = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteRevisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRole = table.Column<string>(nullable: false),
                    AuthorizationLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteRevisionId = table.Column<int>(nullable: false),
                    JobRevisionId = table.Column<int>(nullable: false),
                    QuoteNumber = table.Column<string>(maxLength: 15, nullable: false),
                    JobNumber = table.Column<string>(maxLength: 15, nullable: true),
                    ProjectNumber = table.Column<string>(maxLength: 15, nullable: true),
                    IsAJob = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_JobRevision_JobRevisionId",
                        column: x => x.JobRevisionId,
                        principalTable: "JobRevision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_QuoteRevisions_QuoteRevisionId",
                        column: x => x.QuoteRevisionId,
                        principalTable: "QuoteRevisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 18, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobNumber",
                table: "Jobs",
                column: "JobNumber",
                unique: true,
                filter: "[JobNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobRevisionId",
                table: "Jobs",
                column: "JobRevisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_QuoteNumber",
                table: "Jobs",
                column: "QuoteNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_QuoteRevisionId",
                table: "Jobs",
                column: "QuoteRevisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "JobRevision");

            migrationBuilder.DropTable(
                name: "QuoteRevisions");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
