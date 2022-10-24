using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class modify_DB9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationRoomTestSchedule");

            migrationBuilder.AddColumn<int>(
                name: "ExaminationRoomId",
                table: "TestSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestSchedule_ExaminationRoomId",
                table: "TestSchedule",
                column: "ExaminationRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSchedule_ExaminationRoom_ExaminationRoomId",
                table: "TestSchedule",
                column: "ExaminationRoomId",
                principalTable: "ExaminationRoom",
                principalColumn: "ExaminationRoomID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSchedule_ExaminationRoom_ExaminationRoomId",
                table: "TestSchedule");

            migrationBuilder.DropIndex(
                name: "IX_TestSchedule_ExaminationRoomId",
                table: "TestSchedule");

            migrationBuilder.DropColumn(
                name: "ExaminationRoomId",
                table: "TestSchedule");

            migrationBuilder.CreateTable(
                name: "ExaminationRoomTestSchedule",
                columns: table => new
                {
                    ExaminationRoomsId = table.Column<int>(type: "int", nullable: false),
                    TestScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationRoomTestSchedule", x => new { x.ExaminationRoomsId, x.TestScheduleId });
                    table.ForeignKey(
                        name: "FK_ExaminationRoomTestSchedule_ExaminationRoom_ExaminationRoomsId",
                        column: x => x.ExaminationRoomsId,
                        principalTable: "ExaminationRoom",
                        principalColumn: "ExaminationRoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationRoomTestSchedule_TestSchedule_TestScheduleId",
                        column: x => x.TestScheduleId,
                        principalTable: "TestSchedule",
                        principalColumn: "TestScheduleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRoomTestSchedule_TestScheduleId",
                table: "ExaminationRoomTestSchedule",
                column: "TestScheduleId");
        }
    }
}
