using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanRegisterWebApi.Migrations
{
    /// <inheritdoc />
    public partial class fixrelations : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_ProfileImageId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_UserAddressId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "UserAddressId",
                table: "UserInfos");

            migrationBuilder.AddColumn<Guid>(
                name: "UserInfoId",
                table: "UserAddreses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserInfoId",
                table: "ProfileImage",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddreses_UserInfoId",
                table: "UserAddreses",
                column: "UserInfoId",
                unique: true,
                filter: "[UserInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImage_UserInfoId",
                table: "ProfileImage",
                column: "UserInfoId",
                unique: true,
                filter: "[UserInfoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileImage_UserInfos_UserInfoId",
                table: "ProfileImage",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddreses_UserInfos_UserInfoId",
                table: "UserAddreses",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileImage_UserInfos_UserInfoId",
                table: "ProfileImage");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddreses_UserInfos_UserInfoId",
                table: "UserAddreses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddreses_UserInfoId",
                table: "UserAddreses");

            migrationBuilder.DropIndex(
                name: "IX_ProfileImage_UserInfoId",
                table: "ProfileImage");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "UserAddreses");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "ProfileImage");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileImageId",
                table: "UserInfos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserAddressId",
                table: "UserInfos",
                type: "uniqueidentifier",
                nullable: true);

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
        }
    }
}
