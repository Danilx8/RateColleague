using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateColleague.Migrations
{
    /// <inheritdoc />
    public partial class FixDataRecursionByRemovingUserRoomRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_InitiatorId",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_InitiatorId",
                table: "Rooms",
                column: "InitiatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_InitiatorId",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_InitiatorId",
                table: "Rooms",
                column: "InitiatorId",
                unique: true);
        }
    }
}
