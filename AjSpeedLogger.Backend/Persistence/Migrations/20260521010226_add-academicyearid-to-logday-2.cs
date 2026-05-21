using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
	/// <inheritdoc />
	public partial class addacademicyearidtologday2 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			// 1. Lägg till den nya kolumnen (nullable)
			migrationBuilder.AddColumn<int>(
				name: "AcademicYearId",
				table: "LogDays",
				type: "integer",
				nullable: true);

			// 2. Skapa ett index för prestanda (valfritt men rekommenderat för FKs)
			migrationBuilder.CreateIndex(
				name: "IX_LogDays_AcademicYearId",
				table: "LogDays",
				column: "AcademicYearId");

			// 3. Skapa relationen (Foreign Key) till AcademicYears-tabellen
			migrationBuilder.AddForeignKey(
				name: "FK_LogDays_AcademicYears_AcademicYearId",
				table: "LogDays",
				column: "AcademicYearId",
				principalTable: "AcademicYears", // Kontrollera att din tabell heter exakt så här (plural)
				principalColumn: "Id",
				onDelete: ReferentialAction.SetNull); // Om ett läsår raderas blir ID:t null på loggdagarna istället för att radera dem
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
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
	}
}