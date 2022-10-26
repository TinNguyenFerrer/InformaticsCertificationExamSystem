using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class DbUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheoryTests_Examination_ExaminationId",
                table: "TheoryTests");

            migrationBuilder.RenameColumn(
                name: "ExaminationId",
                table: "TheoryTests",
                newName: "TestScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_TheoryTests_ExaminationId",
                table: "TheoryTests",
                newName: "IX_TheoryTests_TestScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSchedule_ExaminationId",
                table: "TestSchedule",
                column: "ExaminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSchedule_Examination_ExaminationId",
                table: "TestSchedule",
                column: "ExaminationId",
                principalTable: "Examination",
                principalColumn: "ExaminationID");

            migrationBuilder.AddForeignKey(
                name: "FK_TheoryTests_TestSchedule_TestScheduleId",
                table: "TheoryTests",
                column: "TestScheduleId",
                principalTable: "TestSchedule",
                principalColumn: "TestScheduleID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSchedule_Examination_ExaminationId",
                table: "TestSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TheoryTests_TestSchedule_TestScheduleId",
                table: "TheoryTests");

            migrationBuilder.DropIndex(
                name: "IX_TestSchedule_ExaminationId",
                table: "TestSchedule");

            migrationBuilder.RenameColumn(
                name: "TestScheduleId",
                table: "TheoryTests",
                newName: "ExaminationId");

            migrationBuilder.RenameIndex(
                name: "IX_TheoryTests_TestScheduleId",
                table: "TheoryTests",
                newName: "IX_TheoryTests_ExaminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TheoryTests_Examination_ExaminationId",
                table: "TheoryTests",
                column: "ExaminationId",
                principalTable: "Examination",
                principalColumn: "ExaminationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
