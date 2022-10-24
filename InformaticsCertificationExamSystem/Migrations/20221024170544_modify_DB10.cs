using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class modify_DB10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TestSchedule_Name",
                table: "TestSchedule");

            migrationBuilder.AddColumn<int>(
                name: "ExaminationId",
                table: "TestSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExaminationId",
                table: "TestSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_TestSchedule_Name",
                table: "TestSchedule",
                column: "Name",
                unique: true);
        }
    }
}
