using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class updateDB1 : Migration
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
                    StarTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MinimumTheoreticalScore = table.Column<float>(type: "real", nullable: false),
                    MinimumPracticeScore = table.Column<float>(type: "real", nullable: false),
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
                    Location = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
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
                name: "Supervisor",
                columns: table => new
                {
                    SupervisorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisor", x => x.SupervisorID);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdentifierCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
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
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExaminationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheoryTests", x => x.TheoryTestID);
                    table.ForeignKey(
                        name: "FK_TheoryTests_Examination_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examination",
                        principalColumn: "ExaminationID",
                        onDelete: ReferentialAction.Cascade);
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
                    ExaminationId = table.Column<int>(type: "int", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSchedule", x => x.TestScheduleID);
                    table.ForeignKey(
                        name: "FK_TestSchedule_Supervisor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisor",
                        principalColumn: "SupervisorID");
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
                name: "SupervisorTeacher",
                columns: table => new
                {
                    SupervisorsId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorTeacher", x => new { x.SupervisorsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_SupervisorTeacher_Supervisor_SupervisorsId",
                        column: x => x.SupervisorsId,
                        principalTable: "Supervisor",
                        principalColumn: "SupervisorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupervisorTeacher_Teacher_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teacher",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationRoom_TestSchedule",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminationRoomId = table.Column<int>(type: "int", nullable: false),
                    TestScheduleId = table.Column<int>(type: "int", nullable: false),
                    SupervisorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationRoom_TestSchedule", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExaminationRoom_TestSchedule_ExaminationRoom_ExaminationRoomId",
                        column: x => x.ExaminationRoomId,
                        principalTable: "ExaminationRoom",
                        principalColumn: "ExaminationRoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationRoom_TestSchedule_Supervisor_SupervisorID",
                        column: x => x.SupervisorID,
                        principalTable: "Supervisor",
                        principalColumn: "SupervisorID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ExaminationRoom_TestSchedule_TestSchedule_TestScheduleId",
                        column: x => x.TestScheduleId,
                        principalTable: "TestSchedule",
                        principalColumn: "TestScheduleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IdentifierCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NumberOfCheats = table.Column<int>(type: "int", nullable: false),
                    ExaminationId = table.Column<int>(type: "int", nullable: false),
                    TheoryTestId = table.Column<int>(type: "int", nullable: true),
                    StudentTypeId = table.Column<int>(type: "int", nullable: true),
                    TestScheduleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_Examination_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examination",
                        principalColumn: "ExaminationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_StudentType_StudentTypeId",
                        column: x => x.StudentTypeId,
                        principalTable: "StudentType",
                        principalColumn: "StudentTypeID");
                    table.ForeignKey(
                        name: "FK_Student_TestSchedule_TestScheduleId",
                        column: x => x.TestScheduleId,
                        principalTable: "TestSchedule",
                        principalColumn: "TestScheduleID");
                    table.ForeignKey(
                        name: "FK_Student_TheoryTests_TheoryTestId",
                        column: x => x.TheoryTestId,
                        principalTable: "TheoryTests",
                        principalColumn: "TheoryTestID");
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
                name: "IX_ExaminationRoom_Name",
                table: "ExaminationRoom",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRoom_TestSchedule_ExaminationRoomId",
                table: "ExaminationRoom_TestSchedule",
                column: "ExaminationRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRoom_TestSchedule_SupervisorID",
                table: "ExaminationRoom_TestSchedule",
                column: "SupervisorID",
                unique: true,
                filter: "[SupervisorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationRoom_TestSchedule_TestScheduleId",
                table: "ExaminationRoom_TestSchedule",
                column: "TestScheduleId");

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
                name: "IX_Student_ExaminationId",
                table: "Student",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_IdentifierCode",
                table: "Student",
                column: "IdentifierCode",
                unique: true,
                filter: "[IdentifierCode] IS NOT NULL");

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
                name: "IX_SupervisorTeacher_TeachersId",
                table: "SupervisorTeacher",
                column: "TeachersId");

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
                name: "IX_TestSchedule_SupervisorId",
                table: "TestSchedule",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_TheoryTests_ExaminationId",
                table: "TheoryTests",
                column: "ExaminationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationRoom_TestSchedule");

            migrationBuilder.DropTable(
                name: "FileSubmitted");

            migrationBuilder.DropTable(
                name: "FinalResults");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "SupervisorTeacher");

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
                name: "Supervisor");

            migrationBuilder.DropTable(
                name: "Examination");
        }
    }
}
