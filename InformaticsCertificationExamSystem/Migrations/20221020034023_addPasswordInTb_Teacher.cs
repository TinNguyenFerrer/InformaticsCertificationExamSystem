using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class addPasswordInTb_Teacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_TestSchedule_TestScheduleId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_TheoryTests_TheoryTestId",
                table: "Student");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Teacher",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "TheoryTestId",
                table: "Student",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TestScheduleId",
                table: "Student",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_TestSchedule_TestScheduleId",
                table: "Student",
                column: "TestScheduleId",
                principalTable: "TestSchedule",
                principalColumn: "TestScheduleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_TheoryTests_TheoryTestId",
                table: "Student",
                column: "TheoryTestId",
                principalTable: "TheoryTests",
                principalColumn: "TheoryTestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_TestSchedule_TestScheduleId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_TheoryTests_TheoryTestId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Teacher");

            migrationBuilder.AlterColumn<int>(
                name: "TheoryTestId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TestScheduleId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_TestSchedule_TestScheduleId",
                table: "Student",
                column: "TestScheduleId",
                principalTable: "TestSchedule",
                principalColumn: "TestScheduleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_TheoryTests_TheoryTestId",
                table: "Student",
                column: "TheoryTestId",
                principalTable: "TheoryTests",
                principalColumn: "TheoryTestID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
