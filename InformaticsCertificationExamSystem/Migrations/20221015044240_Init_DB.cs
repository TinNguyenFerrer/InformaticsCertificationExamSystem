using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class Init_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Examination",
                columns: table => new
                {
                    ExaminationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExaminationCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StarTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MinimumTheoreticalScore = table.Column<float>(type: "real", nullable: false),
                    MinimumPracticeScore = table.Column<float>(type: "real", nullable: false),
                    ReviewTime = table.Column<bool>(type: "bit", nullable: false),
                    GradingDeadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examination", x => x.ExaminationID);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationRoom",
                columns: table => new
                {
                    ExaminationRoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationRoom", x => x.ExaminationRoomID);
                });

            migrationBuilder.CreateTable(
                name: "StudentType",
                columns: table => new
                {
                    StudentTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentType", x => x.StudentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdentifierCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.TeacherID);
                });

            migrationBuilder.CreateTable(
                name: "TheoryTests",
                columns: table => new
                {
                    TheoryTestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheoryTests", x => x.TheoryTestID);
                });

            migrationBuilder.CreateTable(
                name: "TestSchedule",
                columns: table => new
                {
                    TestScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StarTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExaminationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSchedule", x => x.TestScheduleID);
                    table.ForeignKey(
                        name: "FK_TestSchedule_Examination_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examination",
                        principalColumn: "ExaminationID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supervision = table.Column<bool>(type: "bit", nullable: false),
                    Marker = table.Column<bool>(type: "bit", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    PermissionOfTeacherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                    table.ForeignKey(
                        name: "FK_Permission_Teacher_PermissionOfTeacherID",
                        column: x => x.PermissionOfTeacherID,
                        principalTable: "Teacher",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IdentifierCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NumberOfCheats = table.Column<int>(type: "int", nullable: false),
                    TheoryTestId = table.Column<int>(type: "int", nullable: false),
                    StudentTypeId = table.Column<int>(type: "int", nullable: false),
                    TestScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_StudentType_StudentTypeId",
                        column: x => x.StudentTypeId,
                        principalTable: "StudentType",
                        principalColumn: "StudentTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_TestSchedule_TestScheduleId",
                        column: x => x.TestScheduleId,
                        principalTable: "TestSchedule",
                        principalColumn: "TestScheduleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_TheoryTests_TheoryTestId",
                        column: x => x.TheoryTestId,
                        principalTable: "TheoryTests",
                        principalColumn: "TheoryTestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileSubmitted",
                columns: table => new
                {
                    FileSubmittedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    FileWord = table.Column<bool>(type: "bit", nullable: false),
                    FileExcel = table.Column<bool>(type: "bit", nullable: false),
                    FilePowerPoint = table.Column<bool>(type: "bit", nullable: false),
                    LastSubmissionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileOfStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSubmitted", x => x.FileSubmittedID);
                    table.ForeignKey(
                        name: "FK_FileSubmitted_Student_FileOfStudentId",
                        column: x => x.FileOfStudentId,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinalResults",
                columns: table => new
                {
                    InconsistentMarkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<float>(type: "real", nullable: false),
                    Excel = table.Column<float>(type: "real", nullable: false),
                    PowerPoint = table.Column<float>(type: "real", nullable: false),
                    Practice = table.Column<float>(type: "real", nullable: false),
                    FinalMark = table.Column<float>(type: "real", nullable: false),
                    ResultOfStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalResults", x => x.InconsistentMarkID);
                    table.ForeignKey(
                        name: "FK_FinalResults_Student_ResultOfStudentId",
                        column: x => x.ResultOfStudentId,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InconsistentMark",
                columns: table => new
                {
                    InconsistentMarkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<float>(type: "real", nullable: false),
                    Excel = table.Column<float>(type: "real", nullable: false),
                    PowerPoint = table.Column<float>(type: "real", nullable: false),
                    MarkOfStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InconsistentMark", x => x.InconsistentMarkID);
                    table.ForeignKey(
                        name: "FK_InconsistentMark_Student_MarkOfStudentId",
                        column: x => x.MarkOfStudentId,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher_InconsistentMarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    InconsistentMarkID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher_InconsistentMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teacher_InconsistentMarks_InconsistentMark_InconsistentMarkID",
                        column: x => x.InconsistentMarkID,
                        principalTable: "InconsistentMark",
                        principalColumn: "InconsistentMarkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teacher_InconsistentMarks_Teacher_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teacher",
                        principalColumn: "TeacherID",
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

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRoom_Name",
                table: "ExaminationRoom",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileSubmitted_Code",
                table: "FileSubmitted",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileSubmitted_FileOfStudentId",
                table: "FileSubmitted",
                column: "FileOfStudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinalResults_ResultOfStudentId",
                table: "FinalResults",
                column: "ResultOfStudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InconsistentMark_MarkOfStudentId",
                table: "InconsistentMark",
                column: "MarkOfStudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permission_PermissionOfTeacherID",
                table: "Permission",
                column: "PermissionOfTeacherID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_IdentifierCode",
                table: "Student",
                column: "IdentifierCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentTypeId",
                table: "Student",
                column: "StudentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_TestScheduleId",
                table: "Student",
                column: "TestScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_TheoryTestId",
                table: "Student",
                column: "TheoryTestId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_IdentifierCode",
                table: "Teacher",
                column: "IdentifierCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_InconsistentMarks_InconsistentMarkID",
                table: "Teacher_InconsistentMarks",
                column: "InconsistentMarkID");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_InconsistentMarks_TeacherID",
                table: "Teacher_InconsistentMarks",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_TestSchedule_ExaminationId",
                table: "TestSchedule",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSchedule_Name",
                table: "TestSchedule",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examination_ExaminationRooms");

            migrationBuilder.DropTable(
                name: "FileSubmitted");

            migrationBuilder.DropTable(
                name: "FinalResults");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Teacher_InconsistentMarks");

            migrationBuilder.DropTable(
                name: "ExaminationRoom");

            migrationBuilder.DropTable(
                name: "InconsistentMark");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "StudentType");

            migrationBuilder.DropTable(
                name: "TestSchedule");

            migrationBuilder.DropTable(
                name: "TheoryTests");

            migrationBuilder.DropTable(
                name: "Examination");
        }
    }
}
