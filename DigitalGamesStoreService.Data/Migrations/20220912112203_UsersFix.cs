using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalGamesStoreService.Data.Migrations
{
    public partial class UsersFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Profiles_UserPublicProfileId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserPublicProfileId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UserPublicProfileId",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles");

            migrationBuilder.AddColumn<int>(
                name: "UserPublicProfileId",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserPublicProfileId",
                table: "User",
                column: "UserPublicProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Profiles_UserPublicProfileId",
                table: "User",
                column: "UserPublicProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
