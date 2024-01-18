using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateColleague.Migrations
{
    /// <inheritdoc />
    public partial class AddDebugFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatedColleagues_Rooms_RoomId",
                table: "RatedColleagues");

            migrationBuilder.DropIndex(
                name: "IX_RatedColleagues_RoomId",
                table: "RatedColleagues");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RatedColleagues");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueSign",
                table: "Rooms",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "RatedColleagueId",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RatedColleagueId",
                table: "Rooms",
                column: "RatedColleagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RatedColleagues_RatedColleagueId",
                table: "Rooms",
                column: "RatedColleagueId",
                principalTable: "RatedColleagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RatedColleagues_RatedColleagueId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RatedColleagueId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RatedColleagueId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "UniqueSign",
                table: "Rooms",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "RatedColleagues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RatedColleagues_RoomId",
                table: "RatedColleagues",
                column: "RoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RatedColleagues_Rooms_RoomId",
                table: "RatedColleagues",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
