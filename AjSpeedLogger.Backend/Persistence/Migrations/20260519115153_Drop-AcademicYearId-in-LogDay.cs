using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DropAcademicYearIdinLogDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropForeignKey(
	name: "FK_LogDays_AcademicYears_AcademicYearId",
	table: "LogDays");

			migrationBuilder.DropColumn(
				name: "AcademicYearId",
				table: "LogDays");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
