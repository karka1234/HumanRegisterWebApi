using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanRegisterWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addNullsToUserInfo : Migration
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

            migrationBuilder.AlterColumn<Guid>(
                name: "UserAddressId",
                table: "UserInfos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileImageId",
                table: "UserInfos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "UserAddressId",
                table: "UserInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfileImageId",
                table: "UserInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
        }
    }
}
