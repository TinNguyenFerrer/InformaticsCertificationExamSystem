using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db_Update_FinalResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InconsistentMarkID",
                table: "FinalResults",
                newName: "FinalResultID");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "FinalResults",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "FinalResults");

            migrationBuilder.RenameColumn(
                name: "FinalResultID",
                table: "FinalResults",
                newName: "InconsistentMarkID");
        }
    }
}
