using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class Update_Db9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "TheoryTests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "TheoryTests",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
