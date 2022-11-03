using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileSubmitted_Student_StudentId",
                table: "FileSubmitted");

            migrationBuilder.DropIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "FileSubmitted",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted",
                column: "StudentId",
                unique: true);

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

            migrationBuilder.DropIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "FileSubmitted",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FileSubmitted_Student_StudentId",
                table: "FileSubmitted",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentID");
        }
    }
}
