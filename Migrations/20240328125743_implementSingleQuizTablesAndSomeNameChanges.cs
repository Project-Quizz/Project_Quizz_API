using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    /// <inheritdoc />
    public partial class implementSingleQuizTablesAndSomeNameChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions");

            migrationBuilder.RenameTable(
                name: "QuizQuestions",
                newName: "Quiz_Questions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quiz_Questions",
                table: "Quiz_Questions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Quiz_Question_Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrectAnswer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz_Question_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quiz_Question_Answers_Quiz_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Quiz_Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Single_Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Single_Quizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Single_Quiz_Attempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SingleQuizId = table.Column<int>(type: "int", nullable: false),
                    AskedQuestionId = table.Column<int>(type: "int", nullable: false),
                    GivenAnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Single_Quiz_Attempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Single_Quiz_Attempts_Quiz_Question_Answers_GivenAnswerId",
                        column: x => x.GivenAnswerId,
                        principalTable: "Quiz_Question_Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Single_Quiz_Attempts_Quiz_Questions_GivenAnswerId",
                        column: x => x.GivenAnswerId,
                        principalTable: "Quiz_Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Single_Quiz_Attempts_Single_Quizzes_SingleQuizId",
                        column: x => x.SingleQuizId,
                        principalTable: "Single_Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_Question_Answers_QuestionId",
                table: "Quiz_Question_Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Single_Quiz_Attempts_GivenAnswerId",
                table: "Single_Quiz_Attempts",
                column: "GivenAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Single_Quiz_Attempts_SingleQuizId",
                table: "Single_Quiz_Attempts",
                column: "SingleQuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Single_Quiz_Attempts");

            migrationBuilder.DropTable(
                name: "Quiz_Question_Answers");

            migrationBuilder.DropTable(
                name: "Single_Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quiz_Questions",
                table: "Quiz_Questions");

            migrationBuilder.RenameTable(
                name: "Quiz_Questions",
                newName: "QuizQuestions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "QuizAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrectAnswer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAnswers_QuizQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswers_QuestionId",
                table: "QuizAnswers",
                column: "QuestionId");
        }
    }
}
