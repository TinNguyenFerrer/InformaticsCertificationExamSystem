using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class updateDB4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_ExaminationRoom_TestSchedule_ExaminationRoom_TestScheduleId",
                table: "Student");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_ExaminationRoom_TestSchedule_ExaminationRoom_TestScheduleId",
                table: "Student",
                column: "ExaminationRoom_TestScheduleId",
                principalTable: "ExaminationRoom_TestSchedule",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_ExaminationRoom_TestSchedule_ExaminationRoom_TestScheduleId",
                table: "Student");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_ExaminationRoom_TestSchedule_ExaminationRoom_TestScheduleId",
                table: "Student",
                column: "ExaminationRoom_TestScheduleId",
                principalTable: "ExaminationRoom_TestSchedule",
                principalColumn: "ID");
        }
    }
}
