using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class modify_DB6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Examination_ExaminationId",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "ExaminationId",
                table: "Student",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Examination_ExaminationId",
                table: "Student",
                column: "ExaminationId",
                principalTable: "Examination",
                principalColumn: "ExaminationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Examination_ExaminationId",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "ExaminationId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Examination_ExaminationId",
                table: "Student",
                column: "ExaminationId",
                principalTable: "Examination",
                principalColumn: "ExaminationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
