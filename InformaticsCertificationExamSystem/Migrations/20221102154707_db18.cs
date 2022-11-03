using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TheoryTests_TestScheduleId",
                table: "TheoryTests");

            migrationBuilder.CreateIndex(
                name: "IX_TheoryTests_TestScheduleId",
                table: "TheoryTests",
                column: "TestScheduleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TheoryTests_TestScheduleId",
                table: "TheoryTests");

            migrationBuilder.CreateIndex(
                name: "IX_TheoryTests_TestScheduleId",
                table: "TheoryTests",
                column: "TestScheduleId");
        }
    }
}
