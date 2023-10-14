using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workouts.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExercisesDiaries_UserId",
                table: "ExercisesDiaries");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesDiaries_UserId",
                table: "ExercisesDiaries",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExercisesDiaries_UserId",
                table: "ExercisesDiaries");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesDiaries_UserId",
                table: "ExercisesDiaries",
                column: "UserId");
        }
    }
}
