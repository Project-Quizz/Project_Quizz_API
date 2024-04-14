namespace Project_Quizz_API.Models
{
    /// <summary>
    /// Overview of the user. It contains the id of the user, the user id, the total points, the total points of single games, the total single games count, 
    /// the single gold count, the single silver count, the single bronze count, the total points of multi games, the total multi games count, the multi gold count, 
    /// the multi silver count and the multi bronze count.
    /// </summary>
    public class Quiz_Match_Overview_User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TotalPoints { get; set; } = 0;

        public int TotalPointsSingle { get; set; } = 0;
        public int TotalSingleGamesCount { get; set; } = 0;
        public int SingleGoldCount { get; set; } = 0;
        public int SingleSilverCount { get; set; } = 0;
        public int SingleBronzeCount { get; set; } = 0;

        public int TotalPointsMulti { get; set; } = 0;
        public int TotalMultiGamesCount { get; set; } = 0;
        public int MultiGoldCount { get; set; } = 0;
        public int MultiSilverCount { get; set; } = 0;
        public int MultiBronzeCount { get; set; } = 0;
    }
}
