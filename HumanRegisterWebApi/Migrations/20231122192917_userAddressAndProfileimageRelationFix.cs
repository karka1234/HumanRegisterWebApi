using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanRegisterWebApi.Migrations
{
    /// <inheritdoc />
    public partial class userAddressAndProfileimageRelationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_ProfileImage_ProfileImageId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_UserAddreses_UserAddressId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Users_UserId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_ProfileImageId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_UserAddressId",
                table: "UserInfos");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_ProfileImageId",
                table: "UserInfos",
                column: "ProfileImageId",
                unique: true,
                filter: "[ProfileImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserAddressId",
                table: "UserInfos",
                column: "UserAddressId",
                unique: true,
                filter: "[UserAddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_ProfileImage_ProfileImageId",
                table: "UserInfos",
                column: "ProfileImageId",
                principalTable: "ProfileImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_UserAddreses_UserAddressId",
                table: "UserInfos",
                column: "UserAddressId",
                principalTable: "UserAddreses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Users_UserId",
                table: "UserInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_ProfileImage_ProfileImageId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_UserAddreses_UserAddressId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Users_UserId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_ProfileImageId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_UserAddressId",
                table: "UserInfos");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_ProfileImageId",
                table: "UserInfos",
                column: "ProfileImageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserAddressId",
                table: "UserInfos",
                column: "UserAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_ProfileImage_ProfileImageId",
                table: "UserInfos",
                column: "ProfileImageId",
                principalTable: "ProfileImage",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_UserAddreses_UserAddressId",
                table: "UserInfos",
                column: "UserAddressId",
                principalTable: "UserAddreses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Users_UserId",
                table: "UserInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
