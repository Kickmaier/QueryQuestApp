using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static QueryQuest.Infrastructure.Data.CategoryStatisticsData;

namespace QueryQuest.Infrastructure.Data
{
    public class OpenTriviaDbApiConnection
    {
        private readonly BaseApiClient _baseApiClient;
        public OpenTriviaDbApiConnection(BaseApiClient baseApiClient)
        {
            _baseApiClient = baseApiClient;
        }
        public async Task <List<TriviaData.Result>>GetQuestionFromApiAsync(string amount, string difficulty, string categoryId)
        {

            string url = $"https://opentdb.com/api.php?amount={amount}";
            if (!string.IsNullOrEmpty(difficulty))
            {
                url += $"&difficulty={difficulty}";
            }
            if (!string.IsNullOrEmpty(categoryId))
            {
                url += $"&category={categoryId}";
            }
            url += "&type=multiple";
            var response = await _baseApiClient.GetAsync<TriviaData.Rootobject>(url);
            
            return response?.Results.ToList() ?? new List<TriviaData.Result>();
        }
        public async Task<CategoryCountData> GetCategoryCountFromApiAsync(int categoryId)
        {
            
            string url = $"https://opentdb.com/api_count.php?category={categoryId}";

            
            return await _baseApiClient.GetAsync<CategoryCountData>(url);
        }
    }
}
