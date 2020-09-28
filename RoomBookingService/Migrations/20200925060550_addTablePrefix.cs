using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomBookingService.Migrations
{
    public partial class addTablePrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_rooms_RoomID",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rooms",
                table: "rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookings",
                table: "bookings");

            migrationBuilder.RenameTable(
                name: "rooms",
                newName: "BR_Rooms");

            migrationBuilder.RenameTable(
                name: "bookings",
                newName: "BR_Bookings");

            migrationBuilder.RenameColumn(
                name: "RoomID",
                table: "BR_Bookings",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_RoomID",
                table: "BR_Bookings",
                newName: "IX_BR_Bookings_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BR_Rooms",
                table: "BR_Rooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BR_Bookings",
                table: "BR_Bookings",
                column: "Id");

            migrationBuilder.InsertData(
                table: "BR_Rooms",
                columns: new[] { "Id", "RoomName" },
                values: new object[] { 1, "Meeting Room" });

            migrationBuilder.AddForeignKey(
                name: "FK_BR_Bookings_BR_Rooms_RoomId",
                table: "BR_Bookings",
                column: "RoomId",
                principalTable: "BR_Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BR_Bookings_BR_Rooms_RoomId",
                table: "BR_Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BR_Rooms",
                table: "BR_Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BR_Bookings",
                table: "BR_Bookings");

            migrationBuilder.DeleteData(
                table: "BR_Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "BR_Rooms",
                newName: "rooms");

            migrationBuilder.RenameTable(
                name: "BR_Bookings",
                newName: "bookings");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "bookings",
                newName: "RoomID");

            migrationBuilder.RenameIndex(
                name: "IX_BR_Bookings_RoomId",
                table: "bookings",
                newName: "IX_bookings_RoomID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rooms",
                table: "rooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookings",
                table: "bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_rooms_RoomID",
                table: "bookings",
                column: "RoomID",
                principalTable: "rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
