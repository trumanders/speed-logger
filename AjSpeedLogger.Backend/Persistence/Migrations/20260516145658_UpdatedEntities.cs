using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogDays_BaseLogs_BaseLogId",
                table: "LogDays");

            migrationBuilder.DropForeignKey(
                name: "FK_LogDays_StudentLogs_StudentLogId",
                table: "LogDays");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_ApplicationUserId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "BaseLogs");

            migrationBuilder.DropTable(
                name: "StudentLogs");

            migrationBuilder.DropIndex(
                name: "IX_LogDays_BaseLogId",
                table: "LogDays");

            migrationBuilder.DropColumn(
                name: "LessonDayOfWeek",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LessonEndTime",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LessonStartTime",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BaseLogId",
                table: "LogDays");

            migrationBuilder.RenameColumn(
                name: "StudentLogId",
                table: "LogDays",
                newName: "AcademicYearId");

            migrationBuilder.RenameIndex(
                name: "IX_LogDays_StudentLogId",
                table: "LogDays",
                newName: "IX_LogDays_AcademicYearId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "LogId",
                table: "LogDays",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BaseLogDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    AcademicYearId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseLogDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    LessonStartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    LessonEndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    AcademicYearId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogDays_LogId",
                table: "LogDays",
                column: "LogId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_StudentId",
                table: "Logs",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogDays_AcademicYears_AcademicYearId",
                table: "LogDays",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogDays_Logs_LogId",
                table: "LogDays",
                column: "LogId",
                principalTable: "Logs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_ApplicationUserId",
                table: "Students",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogDays_AcademicYears_AcademicYearId",
                table: "LogDays");

            migrationBuilder.DropForeignKey(
                name: "FK_LogDays_Logs_LogId",
                table: "LogDays");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_ApplicationUserId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "BaseLogDays");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_LogDays_LogId",
                table: "LogDays");

            migrationBuilder.DropColumn(
                name: "LogId",
                table: "LogDays");

            migrationBuilder.RenameColumn(
                name: "AcademicYearId",
                table: "LogDays",
                newName: "StudentLogId");

            migrationBuilder.RenameIndex(
                name: "IX_LogDays_AcademicYearId",
                table: "LogDays",
                newName: "IX_LogDays_StudentLogId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonDayOfWeek",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "LessonEndTime",
                table: "Students",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "LessonStartTime",
                table: "Students",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "BaseLogId",
                table: "LogDays",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcademicYearId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseLogs_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcademicYearId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentLogs_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentLogs_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogDays_BaseLogId",
                table: "LogDays",
                column: "BaseLogId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseLogs_AcademicYearId",
                table: "BaseLogs",
                column: "AcademicYearId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentLogs_AcademicYearId",
                table: "StudentLogs",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLogs_StudentId",
                table: "StudentLogs",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogDays_BaseLogs_BaseLogId",
                table: "LogDays",
                column: "BaseLogId",
                principalTable: "BaseLogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogDays_StudentLogs_StudentLogId",
                table: "LogDays",
                column: "StudentLogId",
                principalTable: "StudentLogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_ApplicationUserId",
                table: "Students",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
