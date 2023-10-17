using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrition.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodDiarys_Users_UserId",
                table: "FoodDiarys");

            migrationBuilder.DropForeignKey(
                name: "FK_MealDetails_FoodDiarys_FoodDiaryId",
                table: "MealDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodDiarys",
                table: "FoodDiarys");

            migrationBuilder.RenameTable(
                name: "FoodDiarys",
                newName: "FoodDiaries");

            migrationBuilder.RenameIndex(
                name: "IX_FoodDiarys_UserId",
                table: "FoodDiaries",
                newName: "IX_FoodDiaries_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodDiaries",
                table: "FoodDiaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodDiaries_Users_UserId",
                table: "FoodDiaries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealDetails_FoodDiaries_FoodDiaryId",
                table: "MealDetails",
                column: "FoodDiaryId",
                principalTable: "FoodDiaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodDiaries_Users_UserId",
                table: "FoodDiaries");

            migrationBuilder.DropForeignKey(
                name: "FK_MealDetails_FoodDiaries_FoodDiaryId",
                table: "MealDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodDiaries",
                table: "FoodDiaries");

            migrationBuilder.RenameTable(
                name: "FoodDiaries",
                newName: "FoodDiarys");

            migrationBuilder.RenameIndex(
                name: "IX_FoodDiaries_UserId",
                table: "FoodDiarys",
                newName: "IX_FoodDiarys_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodDiarys",
                table: "FoodDiarys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodDiarys_Users_UserId",
                table: "FoodDiarys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealDetails_FoodDiarys_FoodDiaryId",
                table: "MealDetails",
                column: "FoodDiaryId",
                principalTable: "FoodDiarys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
