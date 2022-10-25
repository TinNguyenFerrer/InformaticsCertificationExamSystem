using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class updateDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSchedule_Supervisor_SupervisorId",
                table: "TestSchedule");

            migrationBuilder.DropIndex(
                name: "IX_TestSchedule_SupervisorId",
                table: "TestSchedule");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "TestSchedule");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "TestSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestSchedule_SupervisorId",
                table: "TestSchedule",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSchedule_Supervisor_SupervisorId",
                table: "TestSchedule",
                column: "SupervisorId",
                principalTable: "Supervisor",
                principalColumn: "SupervisorID");
        }
    }
}
