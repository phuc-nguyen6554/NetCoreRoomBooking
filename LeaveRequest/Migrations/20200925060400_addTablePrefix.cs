using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveRequest.Migrations
{
    public partial class addTablePrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_leaveTypes",
                table: "leaveTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leaveRequests",
                table: "leaveRequests");

            migrationBuilder.RenameTable(
                name: "leaveTypes",
                newName: "LR_LeaveTypes");

            migrationBuilder.RenameTable(
                name: "leaveRequests",
                newName: "LR_LeaveRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LR_LeaveTypes",
                table: "LR_LeaveTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LR_LeaveRequests",
                table: "LR_LeaveRequests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LR_LeaveRequests_LeaveTypeId",
                table: "LR_LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LR_LeaveRequests_LR_LeaveTypes_LeaveTypeId",
                table: "LR_LeaveRequests",
                column: "LeaveTypeId",
                principalTable: "LR_LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LR_LeaveRequests_LR_LeaveTypes_LeaveTypeId",
                table: "LR_LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LR_LeaveTypes",
                table: "LR_LeaveTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LR_LeaveRequests",
                table: "LR_LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LR_LeaveRequests_LeaveTypeId",
                table: "LR_LeaveRequests");

            migrationBuilder.RenameTable(
                name: "LR_LeaveTypes",
                newName: "leaveTypes");

            migrationBuilder.RenameTable(
                name: "LR_LeaveRequests",
                newName: "leaveRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leaveTypes",
                table: "leaveTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leaveRequests",
                table: "leaveRequests",
                column: "Id");
        }
    }
}
