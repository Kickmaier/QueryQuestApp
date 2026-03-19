using QueryQuest.Core.Models;

namespace QueryQuest.Core.Interfaces
{
    public interface IGameEngine
    {
        Question CurrentQuestion { get; }
        List<AnswerOption> CurrentAnswerOptions { get; }
        bool HasMoreQuestions { get; }
        string CurrentCorrectAnswer { get; }
        IScoreHandler Score { get; }

        bool ProcessAnswer(AnswerOption answer);
        Task<bool> StartGameAsync(string amount, string difficulty, string categoryId);
        string GetCurrenProgress();
        void NextQuestion();
        void ResetGame();
    }
}
