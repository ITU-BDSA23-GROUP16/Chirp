using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chirp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAuthorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheeps_AspNetUsers_AuthorId",
                table: "Cheeps");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_AuthorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Cheeps",
                newName: "Message");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Cheeps",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Cheeps_AspNetUsers_AuthorId",
                table: "Cheeps",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheeps_AspNetUsers_AuthorId",
                table: "Cheeps");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Cheeps",
                newName: "Text");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Cheeps",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_AuthorId",
                table: "AspNetUsers",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cheeps_AspNetUsers_AuthorId",
                table: "Cheeps",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
