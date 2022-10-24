using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class modify_student1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student_IdentifierCode",
                table: "Student");

            migrationBuilder.AlterColumn<string>(
                name: "IdentifierCode",
                table: "Student",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Student_IdentifierCode",
                table: "Student",
                column: "IdentifierCode",
                unique: true,
                filter: "[IdentifierCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student_IdentifierCode",
                table: "Student");

            migrationBuilder.AlterColumn<string>(
                name: "IdentifierCode",
                table: "Student",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_IdentifierCode",
                table: "Student",
                column: "IdentifierCode",
                unique: true);
        }
    }
}
