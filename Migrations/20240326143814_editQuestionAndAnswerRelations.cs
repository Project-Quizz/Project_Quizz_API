using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    /// <inheritdoc />
    public partial class editQuestionAndAnswerRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizAnswers_QuizQuestions_Quiz_QuestionId",
                table: "QuizAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuizAnswers_Quiz_QuestionId",
                table: "QuizAnswers");

            migrationBuilder.DropColumn(
                name: "Quiz_QuestionId",
                table: "QuizAnswers");

            migrationBuilder.DropColumn(
                name: "Quiz_QuestionId1",
                table: "QuizAnswers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quiz_QuestionId",
                table: "QuizAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quiz_QuestionId1",
                table: "QuizAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswers_Quiz_QuestionId",
                table: "QuizAnswers",
                column: "Quiz_QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAnswers_QuizQuestions_Quiz_QuestionId",
                table: "QuizAnswers",
                column: "Quiz_QuestionId",
                principalTable: "QuizQuestions",
                principalColumn: "Id");
        }
    }
}
