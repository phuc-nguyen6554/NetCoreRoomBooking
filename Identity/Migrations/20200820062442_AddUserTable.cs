using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Migrations
{
    public partial class AddUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9cf6996-e324-42a3-a826-fb37ed686bf6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f954cb0b-bb77-4edb-b4c9-69f1083a9b4c");

            migrationBuilder.CreateTable(
                name: "UserData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "553e4e66-58b0-46c5-b5b9-a8ad1d0e8a93", "c6b9d6f2-d903-4f69-9919-df8e60082554", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "537bf02f-d40d-4e7d-a537-450c4ef330d3", "97798090-922a-46e0-ae1c-4d125aa96d72", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserData");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "537bf02f-d40d-4e7d-a537-450c4ef330d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "553e4e66-58b0-46c5-b5b9-a8ad1d0e8a93");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f954cb0b-bb77-4edb-b4c9-69f1083a9b4c", "66986273-707a-4c31-9888-dcfa9ea99673", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9cf6996-e324-42a3-a826-fb37ed686bf6", "18180cc8-ec2e-4c9a-b6d2-4b5331a2ff84", "Admin", "ADMIN" });
        }
    }
}
