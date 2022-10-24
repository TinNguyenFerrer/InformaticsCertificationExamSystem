using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class modify_DB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestSchedule_TheoryTests");

            migrationBuilder.AddColumn<int>(
                name: "ExaminationId",
                table: "TheoryTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TheoryTests_ExaminationId",
                table: "TheoryTests",
                column: "ExaminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TheoryTests_Examination_ExaminationId",
                table: "TheoryTests",
                column: "ExaminationId",
                principalTable: "Examination",
                principalColumn: "ExaminationID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheoryTests_Examination_ExaminationId",
                table: "TheoryTests");

            migrationBuilder.DropIndex(
                name: "IX_TheoryTests_ExaminationId",
                table: "TheoryTests");

            migrationBuilder.DropColumn(
                name: "ExaminationId",
                table: "TheoryTests");

            migrationBuilder.CreateTable(
                name: "TestSchedule_TheoryTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestScheduleId = table.Column<int>(type: "int", nullable: false),
                    TheoryTestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSchedule_TheoryTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSchedule_TheoryTests_TestSchedule_TestScheduleId",
                        column: x => x.TestScheduleId,
                        principalTable: "TestSchedule",
                        principalColumn: "TestScheduleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestSchedule_TheoryTests_TheoryTests_TheoryTestId",
                        column: x => x.TheoryTestId,
                        principalTable: "TheoryTests",
                        principalColumn: "TheoryTestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestSchedule_TheoryTests_TestScheduleId",
                table: "TestSchedule_TheoryTests",
                column: "TestScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSchedule_TheoryTests_TheoryTestId",
                table: "TestSchedule_TheoryTests",
                column: "TheoryTestId");
        }
    }
}
