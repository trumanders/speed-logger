using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changenameBaseLogDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogDays_Logs_LogId",
                table: "LogDays");

            migrationBuilder.DropTable(
                name: "BaseLogDays");

            migrationBuilder.AlterColumn<int>(
                name: "LogId",
                table: "LogDays",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "BaseLogId",
                table: "LogDays",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcademicYearId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseLog_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogDays_BaseLogId",
                table: "LogDays",
                column: "BaseLogId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseLog_AcademicYearId",
                table: "BaseLog",
                column: "AcademicYearId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LogDays_BaseLog_BaseLogId",
                table: "LogDays",
                column: "BaseLogId",
                principalTable: "BaseLog",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogDays_Logs_LogId",
                table: "LogDays",
                column: "LogId",
                principalTable: "Logs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogDays_BaseLog_BaseLogId",
                table: "LogDays");

            migrationBuilder.DropForeignKey(
                name: "FK_LogDays_Logs_LogId",
                table: "LogDays");

            migrationBuilder.DropTable(
                name: "BaseLog");

            migrationBuilder.DropIndex(
                name: "IX_LogDays_BaseLogId",
                table: "LogDays");

            migrationBuilder.DropColumn(
                name: "BaseLogId",
                table: "LogDays");

            migrationBuilder.AlterColumn<int>(
                name: "LogId",
                table: "LogDays",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BaseLogDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AcademicYearId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseLogDays", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LogDays_Logs_LogId",
                table: "LogDays",
                column: "LogId",
                principalTable: "Logs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
