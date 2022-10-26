using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class Update_Db8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TheoryTests",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TheoryTests");
        }
    }
}
