﻿namespace Project_Quizz_API.Models.DTOs
{
    public class QuizMatchOverviewUserDto
    {
        public string UserId { get; set; }
        public int TotalPoints { get; set; }

        public int TotalPointsSingle { get; set; }
        public int TotalSingleGamesCount { get; set; }
        public int SingleGoldCount { get; set; }
        public int SingleSilverCount { get; set; }
        public int SingleBronzeCount { get; set; }

        public int TotalPointsMulti { get; set; }
        public int TotalMultiGamesCount { get; set; }
        public int MultiGoldCount { get; set; }
        public int MultiSilverCount { get; set; }
        public int MultiBronzeCount { get; set; }
    }

    public class HighscoreDataDto
    {
        public string UserId { get; set; }
        public int TotalGames { get; set; }
        public int TotalSingleGames { get; set; }
        public int TotalMultiGames { get; set; }
        public int TotalPointWorth { get; set; }
        public int TotalPointWorthSingle { get; set;}
        public int TotalPointWorthMulti { get; set; }
    }
}