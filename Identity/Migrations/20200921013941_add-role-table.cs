using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Migrations
{
    public partial class addroletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "RoleName" },
                values: new object[] { 1, "Admin Role", "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "RoleName" },
                values: new object[] { 2, "Employee Role", "Employee" });

            migrationBuilder.CreateIndex(
                name: "IX_UserData_RoleId",
                table: "UserData",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_Roles_RoleId",
                table: "UserData",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserData_Roles_RoleId",
                table: "UserData");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_UserData_RoleId",
                table: "UserData");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserData");
        }
    }
}
