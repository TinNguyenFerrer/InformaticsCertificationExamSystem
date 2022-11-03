using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileSubmitted_Student_StudentId",
                table: "FileSubmitted");

            migrationBuilder.DropIndex(
                name: "IX_Student_FileSubmittedId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "FileSubmitted");

            migrationBuilder.CreateIndex(
                name: "IX_Student_FileSubmittedId",
                table: "Student",
                column: "FileSubmittedId",
                unique: true,
                filter: "[FileSubmittedId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student_FileSubmittedId",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "FileSubmitted",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Student_FileSubmittedId",
                table: "Student",
                column: "FileSubmittedId");

            migrationBuilder.CreateIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileSubmitted_Student_StudentId",
                table: "FileSubmitted",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
