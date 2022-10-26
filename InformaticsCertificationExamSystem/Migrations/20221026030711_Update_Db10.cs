using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class Update_Db10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "blocked",
                table: "TheoryTests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "blocked",
                table: "TheoryTests");
        }
    }
}
