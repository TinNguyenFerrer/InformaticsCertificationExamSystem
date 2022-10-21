using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class modify_Exammination7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Examination_ExaminationCode",
                table: "Examination");

            migrationBuilder.DropColumn(
                name: "ExaminationCode",
                table: "Examination");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExaminationCode",
                table: "Examination",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Examination_ExaminationCode",
                table: "Examination",
                column: "ExaminationCode",
                unique: true);
        }
    }
}
