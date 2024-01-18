using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateColleague.Migrations
{
    /// <inheritdoc />
    public partial class AddRatedColleagues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatedColleague_Rooms_RoomId",
                table: "RatedColleague");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RatedColleague",
                table: "RatedColleague");

            migrationBuilder.RenameTable(
                name: "RatedColleague",
                newName: "RatedColleagues");

            migrationBuilder.RenameIndex(
                name: "IX_RatedColleague_RoomId",
                table: "RatedColleagues",
                newName: "IX_RatedColleagues_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatedColleagues",
                table: "RatedColleagues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatedColleagues_Rooms_RoomId",
                table: "RatedColleagues",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatedColleagues_Rooms_RoomId",
                table: "RatedColleagues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RatedColleagues",
                table: "RatedColleagues");

            migrationBuilder.RenameTable(
                name: "RatedColleagues",
                newName: "RatedColleague");

            migrationBuilder.RenameIndex(
                name: "IX_RatedColleagues_RoomId",
                table: "RatedColleague",
                newName: "IX_RatedColleague_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatedColleague",
                table: "RatedColleague",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RatedColleague_Rooms_RoomId",
                table: "RatedColleague",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
