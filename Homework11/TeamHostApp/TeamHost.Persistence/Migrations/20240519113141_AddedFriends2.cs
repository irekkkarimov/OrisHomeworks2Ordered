using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamHost.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedFriends2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    FriendsWithId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MyFriendsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.FriendsWithId, x.MyFriendsId });
                    table.ForeignKey(
                        name: "FK_UserUser_AspNetUsers_FriendsWithId",
                        column: x => x.FriendsWithId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_AspNetUsers_MyFriendsId",
                        column: x => x.MyFriendsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_MyFriendsId",
                table: "UserUser",
                column: "MyFriendsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
