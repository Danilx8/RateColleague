using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateColleague.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ClosePollTime",
                table: "Rooms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InitiatorId",
                table: "Rooms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UniqueSign",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_InitiatorId",
                table: "Rooms",
                column: "InitiatorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_UniqueSign",
                table: "Rooms",
                column: "UniqueSign",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_InitiatorId",
                table: "Rooms",
                column: "InitiatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_InitiatorId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_InitiatorId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_UniqueSign",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ClosePollTime",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "InitiatorId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "UniqueSign",
                table: "Rooms");
        }
    }
}
