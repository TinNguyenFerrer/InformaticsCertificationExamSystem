using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InformaticsCertificationExamSystem.Migrations
{
    public partial class db25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Permission_PermissionId",
                table: "Teacher");

            migrationBuilder.AlterColumn<int>(
                name: "PermissionId",
                table: "Teacher",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Permission_PermissionId",
                table: "Teacher",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "PermissionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Permission_PermissionId",
                table: "Teacher");

            migrationBuilder.AlterColumn<int>(
                name: "PermissionId",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Permission_PermissionId",
                table: "Teacher",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "PermissionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
