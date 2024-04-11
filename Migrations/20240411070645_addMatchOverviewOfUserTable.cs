using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Quizz_API.Migrations
{
    /// <inheritdoc />
    public partial class addMatchOverviewOfUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quiz_Match_Overview_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    TotalPointsSingle = table.Column<int>(type: "int", nullable: false),
                    TotalSingleGamesCount = table.Column<int>(type: "int", nullable: false),
                    SingleGoldCount = table.Column<int>(type: "int", nullable: false),
                    SingleSilverCount = table.Column<int>(type: "int", nullable: false),
                    SingleBronzeCount = table.Column<int>(type: "int", nullable: false),
                    TotalPointsMulti = table.Column<int>(type: "int", nullable: false),
                    TotalMultiGamesCount = table.Column<int>(type: "int", nullable: false),
                    MultiGoldCount = table.Column<int>(type: "int", nullable: false),
                    MultiSilverCount = table.Column<int>(type: "int", nullable: false),
                    MultiBronzeCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz_Match_Overview_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quiz_Match_Overview_Users");
        }
    }
}
