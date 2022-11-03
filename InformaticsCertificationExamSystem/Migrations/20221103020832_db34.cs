using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted");

            migrationBuilder.AddColumn<int>(
                name: "FileSubmittedId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastSubmissionTime",
                table: "FileSubmitted",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Student_FileSubmittedId",
                table: "Student",
                column: "FileSubmittedId");

            migrationBuilder.CreateIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_FileSubmitted_FileSubmittedId",
                table: "Student",
                column: "FileSubmittedId",
                principalTable: "FileSubmitted",
                principalColumn: "FileSubmittedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_FileSubmitted_FileSubmittedId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_FileSubmittedId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted");

            migrationBuilder.DropColumn(
                name: "FileSubmittedId",
                table: "Student");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastSubmissionTime",
                table: "FileSubmitted",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileSubmitted_StudentId",
                table: "FileSubmitted",
                column: "StudentId",
                unique: true);
        }
    }
}
