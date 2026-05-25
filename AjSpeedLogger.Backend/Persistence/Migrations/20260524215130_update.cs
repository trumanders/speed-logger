using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogDays_AcademicYears_AcademicYearId",
                table: "LogDays");

            migrationBuilder.DropIndex(
                name: "IX_LogDays_AcademicYearId",
                table: "LogDays");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "LogDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcademicYearId",
                table: "LogDays",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogDays_AcademicYearId",
                table: "LogDays",
                column: "AcademicYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogDays_AcademicYears_AcademicYearId",
                table: "LogDays",
                column: "AcademicYearId",
                principalTable: "AcademicYears",
                principalColumn: "Id");
        }
    }
}
