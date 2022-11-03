using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileSubmitted_Code",
                table: "FileSubmitted");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "FileSubmitted",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.CreateIndex(
                name: "IX_FileSubmitted_Code",
                table: "FileSubmitted",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileSubmitted_Code",
                table: "FileSubmitted");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "FileSubmitted",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileSubmitted_Code",
                table: "FileSubmitted",
                column: "Code",
                unique: true);
        }
    }
}
