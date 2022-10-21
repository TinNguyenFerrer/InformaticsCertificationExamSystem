using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class modify_Exammination2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Examination_ExaminationCode",
                table: "Examination",
                column: "ExaminationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Examination_ExaminationCode",
                table: "Examination",
                column: "ExaminationCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Examination_ExaminationCode",
                table: "Examination");

            migrationBuilder.DropIndex(
                name: "IX_Examination_ExaminationCode",
                table: "Examination");
        }
    }
}
