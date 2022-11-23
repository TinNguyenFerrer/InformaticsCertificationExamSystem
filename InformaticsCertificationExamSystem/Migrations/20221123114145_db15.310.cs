using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db15310 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InconsistentMarkID",
                table: "FinalResults",
                newName: "InconsistentMarkID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InconsistentMarkID",
                table: "FinalResults",
                newName: "InconsistentMarkID");
        }
    }
}
