using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanRegisterWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addProfileImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "UserInfos");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileImageId",
                table: "UserInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProfileImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_ProfileImageId",
                table: "UserInfos",
                column: "ProfileImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_ProfileImage_ProfileImageId",
                table: "UserInfos",
                column: "ProfileImageId",
                principalTable: "ProfileImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_ProfileImage_ProfileImageId",
                table: "UserInfos");

            migrationBuilder.DropTable(
                name: "ProfileImage");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_ProfileImageId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "UserInfos");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "UserInfos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
