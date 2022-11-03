using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinalResults_Student_ResultOfStudentId",
                table: "FinalResults");

            migrationBuilder.DropIndex(
                name: "IX_FinalResults_ResultOfStudentId",
                table: "FinalResults");

            migrationBuilder.DropColumn(
                name: "ResultOfStudentId",
                table: "FinalResults");

            migrationBuilder.AddColumn<int>(
                name: "FinalResultId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_FinalResultId",
                table: "Student",
                column: "FinalResultId",
                unique: true,
                filter: "[FinalResultId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_FinalResults_FinalResultId",
                table: "Student",
                column: "FinalResultId",
                principalTable: "FinalResults",
                principalColumn: "InconsistentMarkID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_FinalResults_FinalResultId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_FinalResultId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "FinalResultId",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "ResultOfStudentId",
                table: "FinalResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FinalResults_ResultOfStudentId",
                table: "FinalResults",
                column: "ResultOfStudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FinalResults_Student_ResultOfStudentId",
                table: "FinalResults",
                column: "ResultOfStudentId",
                principalTable: "Student",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
