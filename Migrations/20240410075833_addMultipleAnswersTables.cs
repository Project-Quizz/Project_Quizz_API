using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    /// <inheritdoc />
    public partial class addMultipleAnswersTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Multi_Quiz_Attempts_Quiz_Question_Answers_GivenAnswerId",
                table: "Multi_Quiz_Attempts");

            migrationBuilder.DropForeignKey(
                name: "FK_Single_Quiz_Attempts_Quiz_Question_Answers_GivenAnswerId",
                table: "Single_Quiz_Attempts");

            migrationBuilder.DropIndex(
                name: "IX_Single_Quiz_Attempts_GivenAnswerId",
                table: "Single_Quiz_Attempts");

            migrationBuilder.DropIndex(
                name: "IX_Multi_Quiz_Attempts_GivenAnswerId",
                table: "Multi_Quiz_Attempts");

            migrationBuilder.DropColumn(
                name: "GivenAnswerId",
                table: "Single_Quiz_Attempts");

            migrationBuilder.DropColumn(
                name: "GivenAnswerId",
                table: "Multi_Quiz_Attempts");

            migrationBuilder.CreateTable(
                name: "Multi_Given_Answer_Attempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MultiQuizAttemptId = table.Column<int>(type: "int", nullable: false),
                    QuizQuestionAnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multi_Given_Answer_Attempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Multi_Given_Answer_Attempts_Multi_Quiz_Attempts_MultiQuizAttemptId",
                        column: x => x.MultiQuizAttemptId,
                        principalTable: "Multi_Quiz_Attempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Multi_Given_Answer_Attempts_Quiz_Question_Answers_QuizQuestionAnswerId",
                        column: x => x.QuizQuestionAnswerId,
                        principalTable: "Quiz_Question_Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Single_Given_Answer_Attepmts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SingleQuizAttemptId = table.Column<int>(type: "int", nullable: false),
                    QuizQuestionAnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Single_Given_Answer_Attepmts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Single_Given_Answer_Attepmts_Quiz_Question_Answers_QuizQuestionAnswerId",
                        column: x => x.QuizQuestionAnswerId,
                        principalTable: "Quiz_Question_Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Single_Given_Answer_Attepmts_Single_Quiz_Attempts_SingleQuizAttemptId",
                        column: x => x.SingleQuizAttemptId,
                        principalTable: "Single_Quiz_Attempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Multi_Given_Answer_Attempts_MultiQuizAttemptId",
                table: "Multi_Given_Answer_Attempts",
                column: "MultiQuizAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_Multi_Given_Answer_Attempts_QuizQuestionAnswerId",
                table: "Multi_Given_Answer_Attempts",
                column: "QuizQuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Single_Given_Answer_Attepmts_QuizQuestionAnswerId",
                table: "Single_Given_Answer_Attepmts",
                column: "QuizQuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Single_Given_Answer_Attepmts_SingleQuizAttemptId",
                table: "Single_Given_Answer_Attepmts",
                column: "SingleQuizAttemptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Multi_Given_Answer_Attempts");

            migrationBuilder.DropTable(
                name: "Single_Given_Answer_Attepmts");

            migrationBuilder.AddColumn<int>(
                name: "GivenAnswerId",
                table: "Single_Quiz_Attempts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GivenAnswerId",
                table: "Multi_Quiz_Attempts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Single_Quiz_Attempts_GivenAnswerId",
                table: "Single_Quiz_Attempts",
                column: "GivenAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Multi_Quiz_Attempts_GivenAnswerId",
                table: "Multi_Quiz_Attempts",
                column: "GivenAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Multi_Quiz_Attempts_Quiz_Question_Answers_GivenAnswerId",
                table: "Multi_Quiz_Attempts",
                column: "GivenAnswerId",
                principalTable: "Quiz_Question_Answers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Single_Quiz_Attempts_Quiz_Question_Answers_GivenAnswerId",
                table: "Single_Quiz_Attempts",
                column: "GivenAnswerId",
                principalTable: "Quiz_Question_Answers",
                principalColumn: "Id");
        }
    }
}
