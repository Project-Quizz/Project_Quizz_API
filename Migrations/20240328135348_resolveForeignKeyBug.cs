using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    /// <inheritdoc />
    public partial class resolveForeignKeyBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Single_Quiz_Attempts_Quiz_Question_Answers_GivenAnswerId",
                table: "Single_Quiz_Attempts");

            migrationBuilder.DropForeignKey(
                name: "FK_Single_Quiz_Attempts_Quiz_Questions_GivenAnswerId",
                table: "Single_Quiz_Attempts");

            migrationBuilder.CreateIndex(
                name: "IX_Single_Quiz_Attempts_AskedQuestionId",
                table: "Single_Quiz_Attempts",
                column: "AskedQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Single_Quiz_Attempts_Quiz_Question_Answers_GivenAnswerId",
                table: "Single_Quiz_Attempts",
                column: "GivenAnswerId",
                principalTable: "Quiz_Question_Answers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Single_Quiz_Attempts_Quiz_Questions_AskedQuestionId",
                table: "Single_Quiz_Attempts",
                column: "AskedQuestionId",
                principalTable: "Quiz_Questions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Single_Quiz_Attempts_Quiz_Question_Answers_GivenAnswerId",
                table: "Single_Quiz_Attempts");

            migrationBuilder.DropForeignKey(
                name: "FK_Single_Quiz_Attempts_Quiz_Questions_AskedQuestionId",
                table: "Single_Quiz_Attempts");

            migrationBuilder.DropIndex(
                name: "IX_Single_Quiz_Attempts_AskedQuestionId",
                table: "Single_Quiz_Attempts");

            migrationBuilder.AddForeignKey(
                name: "FK_Single_Quiz_Attempts_Quiz_Question_Answers_GivenAnswerId",
                table: "Single_Quiz_Attempts",
                column: "GivenAnswerId",
                principalTable: "Quiz_Question_Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Single_Quiz_Attempts_Quiz_Questions_GivenAnswerId",
                table: "Single_Quiz_Attempts",
                column: "GivenAnswerId",
                principalTable: "Quiz_Questions",
                principalColumn: "Id");
        }
    }
}
