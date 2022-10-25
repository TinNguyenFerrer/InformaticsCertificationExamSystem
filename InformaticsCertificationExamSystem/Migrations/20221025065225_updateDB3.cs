using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class updateDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExaminationRoom_TestScheduleId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_ExaminationRoom_TestScheduleId",
                table: "Student",
                column: "ExaminationRoom_TestScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_ExaminationRoom_TestSchedule_ExaminationRoom_TestScheduleId",
                table: "Student",
                column: "ExaminationRoom_TestScheduleId",
                principalTable: "ExaminationRoom_TestSchedule",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_ExaminationRoom_TestSchedule_ExaminationRoom_TestScheduleId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_ExaminationRoom_TestScheduleId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ExaminationRoom_TestScheduleId",
                table: "Student");
        }
    }
}
