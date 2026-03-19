using QueryQuest.Core.Interfaces;
using QueryQuest.Core.Models;
using QueryQuest.Infrastructure.Data;

namespace QueryQuest.Infrastructure.Repositories
{
    public class OpenTriviaService : ITriviaService
    {
        private readonly OpenTriviaDbApiConnection _apiConnection;
        private readonly IHtmlService _htmlService;

        public OpenTriviaService(OpenTriviaDbApiConnection apiConnection, IHtmlService htmlService)
        {
            _apiConnection = apiConnection;
            _htmlService = htmlService;
        }
        public async Task<List<Question>> GetQuestionAsync(string amount, string difficulty, string categoryId)
        {
            var apiResults = await _apiConnection.GetQuestionFromApiAsync(amount, difficulty, categoryId);
            return apiResults.Select(r => new Question 
            {
            Text = _htmlService.Decode(r.QuestionText),
            CorrectAnswer = _htmlService.Decode(r.CorrectAnswer),
            IncorrectAnswers = r.IncorrectAnswers.Select(a => _htmlService.Decode(a)).ToList()
        }
            ).ToList();
        }
        public async Task<CategoryStatistics> GetCategoryStatsAsync(int categoryId)
        {
            var data = await _apiConnection.GetCategoryCountFromApiAsync(categoryId);

            return new CategoryStatistics
            {
                EasyCount = data.Counts.Easy,
                MediumCount = data.Counts.Medium,
                HardCount = data.Counts.Hard
            };
        }
    }
}
