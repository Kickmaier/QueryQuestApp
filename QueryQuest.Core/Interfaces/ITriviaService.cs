using QueryQuest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QueryQuest.Core.Interfaces
{
    public interface ITriviaService
    {
        Task <List<Question>>GetQuestionAsync(string ammount, string difficulty, string categoryId);
        Task<CategoryStatistics> GetCategoryStatsAsync(int categoryId);

    }
}
