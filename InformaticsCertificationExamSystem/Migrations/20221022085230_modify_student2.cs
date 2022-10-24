using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class modify_student2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_StudentType_StudentTypeId",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "StudentTypeId",
                table: "Student",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_StudentType_StudentTypeId",
                table: "Student",
                column: "StudentTypeId",
                principalTable: "StudentType",
                principalColumn: "StudentTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_StudentType_StudentTypeId",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "StudentTypeId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_StudentType_StudentTypeId",
                table: "Student",
                column: "StudentTypeId",
                principalTable: "StudentType",
                principalColumn: "StudentTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
