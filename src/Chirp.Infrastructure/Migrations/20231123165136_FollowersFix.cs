﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chirp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FollowersFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowedId",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_FollowedId",
                table: "Follows");

            migrationBuilder.AddColumn<string>(
                name: "FollowingId",
                table: "Follows",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowingId",
                table: "Follows",
                column: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowingId",
                table: "Follows",
                column: "FollowingId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowingId",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_FollowingId",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "FollowingId",
                table: "Follows");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowedId",
                table: "Follows",
                column: "FollowedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowedId",
                table: "Follows",
                column: "FollowedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
