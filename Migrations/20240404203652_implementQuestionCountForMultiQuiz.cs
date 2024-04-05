using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    /// <inheritdoc />
    public partial class implementQuestionCountForMultiQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionCount",
                table: "Multi_Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionCount",
                table: "Multi_Quizzes");
        }
    }
}
