namespace QueryQuest.Core.Interfaces
{
    public interface IScoreHandler
    {
        int CorrectAnswerCount { get; set; }
        int CurrentScore { get; set; }
        int HighestStreakCount { get; set; }
        int Streak { get; set; }

        void AddCorrectAnswer();
        void HandleWrongAnswer();
        void Reset();
    }
}