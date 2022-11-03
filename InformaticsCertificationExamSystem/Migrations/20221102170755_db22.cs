using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_Teacher_PermissionOfTeacherID",
                table: "Permission");

            migrationBuilder.DropIndex(
                name: "IX_Permission_PermissionOfTeacherID",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "Marker",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "PermissionOfTeacherID",
                table: "Permission");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_PermissionId",
                table: "Teacher",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Permission_PermissionId",
                table: "Teacher",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "PermissionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Permission_PermissionId",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_PermissionId",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "Teacher");

            migrationBuilder.AddColumn<bool>(
                name: "Marker",
                table: "Permission",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PermissionOfTeacherID",
                table: "Permission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Permission_PermissionOfTeacherID",
                table: "Permission",
                column: "PermissionOfTeacherID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_Teacher_PermissionOfTeacherID",
                table: "Permission",
                column: "PermissionOfTeacherID",
                principalTable: "Teacher",
                principalColumn: "TeacherID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
