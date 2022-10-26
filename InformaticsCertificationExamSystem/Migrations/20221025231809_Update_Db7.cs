using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class Update_Db7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamCode",
                table: "TheoryTests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExamCode",
                table: "TheoryTests",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }
    }
}
