using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class modify_DB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSchedule_Examination_ExaminationId",
                table: "TestSchedule");

            migrationBuilder.DropIndex(
                name: "IX_TestSchedule_ExaminationId",
                table: "TestSchedule");

            migrationBuilder.DropColumn(
                name: "ExaminationId",
                table: "TestSchedule");

            migrationBuilder.AddColumn<int>(
                name: "ExaminationId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Student_ExaminationId",
                table: "Student",
                column: "ExaminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Examination_ExaminationId",
                table: "Student",
                column: "ExaminationId",
                principalTable: "Examination",
                principalColumn: "ExaminationID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Examination_ExaminationId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_ExaminationId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ExaminationId",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "ExaminationId",
                table: "TestSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestSchedule_ExaminationId",
                table: "TestSchedule",
                column: "ExaminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSchedule_Examination_ExaminationId",
                table: "TestSchedule",
                column: "ExaminationId",
                principalTable: "Examination",
                principalColumn: "ExaminationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
