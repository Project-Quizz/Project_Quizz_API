using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    /// <inheritdoc />
    public partial class implementCategorieDbTableWithRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizCategorieId",
                table: "Single_Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizCategorieId",
                table: "Quiz_Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Quiz_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Single_Quizzes_QuizCategorieId",
                table: "Single_Quizzes",
                column: "QuizCategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_Questions_QuizCategorieId",
                table: "Quiz_Questions",
                column: "QuizCategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Questions_Quiz_Categories_QuizCategorieId",
                table: "Quiz_Questions",
                column: "QuizCategorieId",
                principalTable: "Quiz_Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Single_Quizzes_Quiz_Categories_QuizCategorieId",
                table: "Single_Quizzes",
                column: "QuizCategorieId",
                principalTable: "Quiz_Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_Questions_Quiz_Categories_QuizCategorieId",
                table: "Quiz_Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Single_Quizzes_Quiz_Categories_QuizCategorieId",
                table: "Single_Quizzes");

            migrationBuilder.DropTable(
                name: "Quiz_Categories");

            migrationBuilder.DropIndex(
                name: "IX_Single_Quizzes_QuizCategorieId",
                table: "Single_Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quiz_Questions_QuizCategorieId",
                table: "Quiz_Questions");

            migrationBuilder.DropColumn(
                name: "QuizCategorieId",
                table: "Single_Quizzes");

            migrationBuilder.DropColumn(
                name: "QuizCategorieId",
                table: "Quiz_Questions");
        }
    }
}
