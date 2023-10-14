using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workouts.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseType_ExerciseTypeId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "ExerciseType");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_ExerciseTypeId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerciseTypeId",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseType",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExerciseType",
                table: "Exercises");

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseTypeId",
                table: "Exercises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ExerciseType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ExerciseTypeId",
                table: "Exercises",
                column: "ExerciseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseType_ExerciseTypeId",
                table: "Exercises",
                column: "ExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
