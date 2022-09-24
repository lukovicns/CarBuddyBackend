using Microsoft.EntityFrameworkCore.Migrations;

namespace CarBuddy.Persistence.Migrations
{
    public partial class AddRatingAndCommentTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rating_user_RecipientId",
                table: "rating");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ratingComment",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "rating",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "rating",
                newName: "authorId");

            migrationBuilder.RenameIndex(
                name: "IX_rating_RecipientId",
                table: "rating",
                newName: "IX_rating_authorId");

            migrationBuilder.AddForeignKey(
                name: "FK_rating_user_authorId",
                table: "rating",
                column: "authorId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rating_user_authorId",
                table: "rating");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ratingComment",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "rating",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "authorId",
                table: "rating",
                newName: "RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_rating_authorId",
                table: "rating",
                newName: "IX_rating_RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_rating_user_RecipientId",
                table: "rating",
                column: "RecipientId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
