using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    /// <inheritdoc />
    public partial class implemenMultiQuizzesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Quiz_Categories_Quiz_Questions_QuestionsId",
            //    table: "Quiz_Categories");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Quiz_Categories_Single_Quizzes_SingleQuizzesId",
            //    table: "Quiz_Categories");

            //migrationBuilder.DropIndex(
            //    name: "IX_Quiz_Categories_QuestionsId",
            //    table: "Quiz_Categories");

            //migrationBuilder.DropIndex(
            //    name: "IX_Quiz_Categories_SingleQuizzesId",
            //    table: "Quiz_Categories");

            //migrationBuilder.DropColumn(
            //    name: "QuestionsId",
            //    table: "Quiz_Categories");

            //migrationBuilder.DropColumn(
            //    name: "SingleQuizzesId",
            //    table: "Quiz_Categories");

            migrationBuilder.CreateTable(
                name: "Multi_Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuizCompleted = table.Column<bool>(type: "bit", nullable: false),
                    QuizCategorieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multi_Quizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Multi_Quizzes_Quiz_Categories_QuizCategorieId",
                        column: x => x.QuizCategorieId,
                        principalTable: "Quiz_Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Multi_Quiz_Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MultiQuizId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multi_Quiz_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Multi_Quiz_Players_Multi_Quizzes_MultiQuizId",
                        column: x => x.MultiQuizId,
                        principalTable: "Multi_Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Multi_Quiz_Attempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MultiQuizId = table.Column<int>(type: "int", nullable: false),
                    MultiQuizPlayerId = table.Column<int>(type: "int", nullable: false),
                    AskedQuestionId = table.Column<int>(type: "int", nullable: false),
                    GivenAnswerId = table.Column<int>(type: "int", nullable: true),
                    AnswerDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multi_Quiz_Attempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Multi_Quiz_Attempts_Multi_Quiz_Players_MultiQuizPlayerId",
                        column: x => x.MultiQuizPlayerId,
                        principalTable: "Multi_Quiz_Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Multi_Quiz_Attempts_Multi_Quizzes_MultiQuizId",
                        column: x => x.MultiQuizId,
                        principalTable: "Multi_Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Multi_Quiz_Attempts_Quiz_Question_Answers_AskedQuestionId",
                        column: x => x.AskedQuestionId,
                        principalTable: "Quiz_Question_Answers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Multi_Quiz_Attempts_Quiz_Questions_AskedQuestionId",
                        column: x => x.AskedQuestionId,
                        principalTable: "Quiz_Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Multi_Quiz_Attempts_AskedQuestionId",
                table: "Multi_Quiz_Attempts",
                column: "AskedQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Multi_Quiz_Attempts_MultiQuizId",
                table: "Multi_Quiz_Attempts",
                column: "MultiQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Multi_Quiz_Attempts_MultiQuizPlayerId",
                table: "Multi_Quiz_Attempts",
                column: "MultiQuizPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Multi_Quiz_Players_MultiQuizId",
                table: "Multi_Quiz_Players",
                column: "MultiQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Multi_Quizzes_QuizCategorieId",
                table: "Multi_Quizzes",
                column: "QuizCategorieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Multi_Quiz_Attempts");

            migrationBuilder.DropTable(
                name: "Multi_Quiz_Players");

            migrationBuilder.DropTable(
                name: "Multi_Quizzes");

            //migrationBuilder.AddColumn<int>(
            //    name: "QuestionsId",
            //    table: "Quiz_Categories",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "SingleQuizzesId",
            //    table: "Quiz_Categories",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Quiz_Categories_QuestionsId",
            //    table: "Quiz_Categories",
            //    column: "QuestionsId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Quiz_Categories_SingleQuizzesId",
            //    table: "Quiz_Categories",
            //    column: "SingleQuizzesId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Quiz_Categories_Quiz_Questions_QuestionsId",
            //    table: "Quiz_Categories",
            //    column: "QuestionsId",
            //    principalTable: "Quiz_Questions",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Quiz_Categories_Single_Quizzes_SingleQuizzesId",
            //    table: "Quiz_Categories",
            //    column: "SingleQuizzesId",
            //    principalTable: "Single_Quizzes",
            //    principalColumn: "Id");
        }
    }
}
