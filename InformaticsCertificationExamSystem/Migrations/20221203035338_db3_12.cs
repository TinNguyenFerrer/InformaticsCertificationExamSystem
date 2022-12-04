using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db3_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student_Email",
                table: "Student");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Student_Email",
                table: "Student",
                column: "Email",
                unique: true);
        }
    }
}
