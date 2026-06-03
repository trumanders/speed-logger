using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateToResolveBaseLogDaysBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseLogDay_AcademicYears_AcademicYearId",
                table: "BaseLogDay");

            migrationBuilder.DropForeignKey(
                name: "FK_LogDays_BaseLogDay_BaseLogDayId",
                table: "LogDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseLogDay",
                table: "BaseLogDay");

            migrationBuilder.RenameTable(
                name: "BaseLogDay",
                newName: "BaseLogDays");

            migrationBuilder.RenameIndex(
                name: "IX_BaseLogDay_AcademicYearId",
                table: "BaseLogDays",
                newName: "IX_BaseLogDays_AcademicYearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseLogDays",
                table: "BaseLogDays",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseLogDays_AcademicYears_AcademicYearId",
                table: "BaseLogDays",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogDays_BaseLogDays_BaseLogDayId",
                table: "LogDays",
                column: "BaseLogDayId",
                principalTable: "BaseLogDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseLogDays_AcademicYears_AcademicYearId",
                table: "BaseLogDays");

            migrationBuilder.DropForeignKey(
                name: "FK_LogDays_BaseLogDays_BaseLogDayId",
                table: "LogDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseLogDays",
                table: "BaseLogDays");

            migrationBuilder.RenameTable(
                name: "BaseLogDays",
                newName: "BaseLogDay");

            migrationBuilder.RenameIndex(
                name: "IX_BaseLogDays_AcademicYearId",
                table: "BaseLogDay",
                newName: "IX_BaseLogDay_AcademicYearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseLogDay",
                table: "BaseLogDay",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseLogDay_AcademicYears_AcademicYearId",
                table: "BaseLogDay",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogDays_BaseLogDay_BaseLogDayId",
                table: "LogDays",
                column: "BaseLogDayId",
                principalTable: "BaseLogDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
