using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    /// <inheritdoc />
    public partial class resolveForeignKeyBugInMulit_Quiz_Attempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Multi_Quiz_Attempts_Quiz_Question_Answers_AskedQuestionId",
                table: "Multi_Quiz_Attempts");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Multi_Quiz_Attempts_Quiz_Question_Answers_GivenAnswerId",
                table: "Multi_Quiz_Attempts");

            migrationBuilder.DropIndex(
                name: "IX_Multi_Quiz_Attempts_GivenAnswerId",
                table: "Multi_Quiz_Attempts");

            migrationBuilder.AddForeignKey(
                name: "FK_Multi_Quiz_Attempts_Quiz_Question_Answers_AskedQuestionId",
                table: "Multi_Quiz_Attempts",
                column: "AskedQuestionId",
                principalTable: "Quiz_Question_Answers",
                principalColumn: "Id");
        }
    }
}
