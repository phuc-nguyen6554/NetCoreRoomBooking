using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Migrations
{
    public partial class addTablePrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserData_Roles_RoleId",
                table: "UserData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserData",
                table: "UserData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "UserData",
                newName: "ID_Users");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "ID_Roles");

            migrationBuilder.RenameIndex(
                name: "IX_UserData_RoleId",
                table: "ID_Users",
                newName: "IX_ID_Users_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ID_Users",
                table: "ID_Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ID_Roles",
                table: "ID_Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ID_Users_ID_Roles_RoleId",
                table: "ID_Users",
                column: "RoleId",
                principalTable: "ID_Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ID_Users_ID_Roles_RoleId",
                table: "ID_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ID_Users",
                table: "ID_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ID_Roles",
                table: "ID_Roles");

            migrationBuilder.RenameTable(
                name: "ID_Users",
                newName: "UserData");

            migrationBuilder.RenameTable(
                name: "ID_Roles",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_ID_Users_RoleId",
                table: "UserData",
                newName: "IX_UserData_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserData",
                table: "UserData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_Roles_RoleId",
                table: "UserData",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
