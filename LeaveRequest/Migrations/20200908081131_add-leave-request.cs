using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveRequest.Migrations
{
    public partial class addleaverequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "leaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    LeaveDates = table.Column<DateTime>(nullable: false),
                    LeaveTime = table.Column<string>(nullable: true),
                    LeaveTypeId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    MemberName = table.Column<string>(nullable: true),
                    MemberEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leaveRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "leaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leaveTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "leaveRequests");

            migrationBuilder.DropTable(
                name: "leaveTypes");
        }
    }
}
