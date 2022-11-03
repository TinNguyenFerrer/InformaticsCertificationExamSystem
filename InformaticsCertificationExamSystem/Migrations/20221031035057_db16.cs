using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileSubmitted_Student_FileOfStudentId",
                table: "FileSubmitted");

            migrationBuilder.RenameColumn(
                name: "FileOfStudentId",
                table: "FileSubmitted",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_FileSubmitted_FileOfStudentId",
                table: "FileSubmitted",
                newName: "IX_FileSubmitted_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileSubmitted_Student_StudentId",
                table: "FileSubmitted",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileSubmitted_Student_StudentId",
                table: "FileSubmitted");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "FileSubmitted",
                newName: "FileOfStudentId");

            migrationBuilder.RenameIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted",
                newName: "IX_FileSubmitted_FileOfStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileSubmitted_Student_FileOfStudentId",
                table: "FileSubmitted",
                column: "FileOfStudentId",
                principalTable: "Student",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
