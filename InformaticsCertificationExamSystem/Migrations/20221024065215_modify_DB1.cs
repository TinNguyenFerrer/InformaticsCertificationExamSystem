using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class modify_DB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examination_ExaminationRooms");

            migrationBuilder.AddColumn<int>(
                name: "ExaminationRoomId",
                table: "TestSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestSchedule_ExaminationRoomId",
                table: "TestSchedule",
                column: "ExaminationRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSchedule_ExaminationRoom_ExaminationRoomId",
                table: "TestSchedule",
                column: "ExaminationRoomId",
                principalTable: "ExaminationRoom",
                principalColumn: "ExaminationRoomID",
                onDelete: ReferentialAction.Cascade);
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
                name: "Examination_ExaminationRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminationID = table.Column<int>(type: "int", nullable: false),
                    ExaminationRoomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examination_ExaminationRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examination_ExaminationRooms_Examination_ExaminationID",
                        column: x => x.ExaminationID,
                        principalTable: "Examination",
                        principalColumn: "ExaminationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Examination_ExaminationRooms_ExaminationRoom_ExaminationRoomID",
                        column: x => x.ExaminationRoomID,
                        principalTable: "ExaminationRoom",
                        principalColumn: "ExaminationRoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examination_ExaminationRooms_ExaminationID",
                table: "Examination_ExaminationRooms",
                column: "ExaminationID");

            migrationBuilder.CreateIndex(
                name: "IX_Examination_ExaminationRooms_ExaminationRoomID",
                table: "Examination_ExaminationRooms",
                column: "ExaminationRoomID");
        }
    }
}
