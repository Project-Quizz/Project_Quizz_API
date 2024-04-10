using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    /// <inheritdoc />
    public partial class addFeedbackTableForQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quiz_Question_Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz_Question_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quiz_Question_Feedbacks_Quiz_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Quiz_Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_Question_Feedbacks_QuestionId",
                table: "Quiz_Question_Feedbacks",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Multi_Given_Answer_Attempts");

            migrationBuilder.DropTable(
                name: "Quiz_Question_Feedbacks");

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
