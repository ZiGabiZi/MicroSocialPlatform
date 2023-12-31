using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalDAW2.Migrations
{
    public partial class SperCaEUltimaIn2023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGroup");

            migrationBuilder.CreateTable(
                name: "ApplicationUserGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGroups", x => new { x.Id, x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGroups_GroupId",
                table: "ApplicationUserGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGroups_UserId",
                table: "ApplicationUserGroups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGroups");

            migrationBuilder.CreateTable(
                name: "ApplicationUserGroup",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGroup", x => new { x.GroupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGroup_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGroup_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGroup_UsersId",
                table: "ApplicationUserGroup",
                column: "UsersId");
        }
    }
}
