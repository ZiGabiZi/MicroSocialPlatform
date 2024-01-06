using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalDAW2.Migrations
{
    public partial class NuMerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserApplicationUser_AspNetUsers_FriendsId",
                table: "ApplicationUserApplicationUser");

            migrationBuilder.RenameColumn(
                name: "FriendsId",
                table: "ApplicationUserApplicationUser",
                newName: "FriendListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserApplicationUser_AspNetUsers_FriendListId",
                table: "ApplicationUserApplicationUser",
                column: "FriendListId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserApplicationUser_AspNetUsers_FriendListId",
                table: "ApplicationUserApplicationUser");

            migrationBuilder.RenameColumn(
                name: "FriendListId",
                table: "ApplicationUserApplicationUser",
                newName: "FriendsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserApplicationUser_AspNetUsers_FriendsId",
                table: "ApplicationUserApplicationUser",
                column: "FriendsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
