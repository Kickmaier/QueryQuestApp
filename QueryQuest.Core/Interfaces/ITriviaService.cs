using QueryQuest.Core.Models;

namespace QueryQuest.Core.Interfaces
{
    public interface ITriviaService
    {
        Task <List<Question>>GetQuestionAsync(string ammount, string difficulty, string categoryId);
        Task<CategoryStatistics> GetCategoryStatsAsync(int categoryId);

    }
}
