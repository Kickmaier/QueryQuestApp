using QueryQuest.Core.Interfaces;

namespace QueryQuest.Core.Models
{
    public class GameEngine : IGameEngine
    {
        private readonly IScoreHandler _scoreHandler;
        private readonly IQuestionService _questionManager;
        private readonly ITriviaService _triviaService;

        public IScoreHandler Score => _scoreHandler;
        public bool HasMoreQuestions => _questionManager.HasMoreQuestions;
        public string CurrentCorrectAnswer => _questionManager.CurrentCorrectAnswer;
        public Question CurrentQuestion => _questionManager.CurrentQuestion;
        public List<AnswerOption> CurrentAnswerOptions => _questionManager.CurrentQuestion?.AllAnswerOptions;
        public GameEngine(IScoreHandler scoreHandler, IQuestionService questionManager, ITriviaService triviaService )
        {
            _scoreHandler = scoreHandler;
            _questionManager = questionManager;
            _triviaService = triviaService;
        }

        public async Task<bool> StartGameAsync(string amount, string difficulty, string categoryId)
        {
            var getQuestions = await _triviaService.GetQuestionAsync(amount, difficulty, categoryId);
            if (getQuestions != null && getQuestions.Count > 0)
            {
                _questionManager.PrepareQuestion(getQuestions, (int.Parse(amount)));
                return true;
            }
            return false;
        }
        public bool ProcessAnswer(AnswerOption answer)
        {
            bool isCorrect = _questionManager.CheckAnswer(answer);

            if (isCorrect) _scoreHandler.AddCorrectAnswer();

            else _scoreHandler.HandleWrongAnswer();
        
            return isCorrect;
        }
        public void NextQuestion() => _questionManager.SetNextQuestion();
        public string GetCurrenProgress()
        {
            return _questionManager.GetCurrentText();
        }
        public void ResetGame()
        {
            _scoreHandler.Reset();
            _questionManager?.Reset();
        }
        
    }
}
