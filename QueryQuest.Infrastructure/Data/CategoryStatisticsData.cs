using System.Text.Json.Serialization;

namespace QueryQuest.Infrastructure.Data
{
    public class CategoryStatisticsData
    {
        public class CategoryCountData
        {
            [JsonPropertyName("category_id")]
            public int CategoryId { get; set; }

            [JsonPropertyName("category_question_count")]
            public QuestionCountDetails Counts { get; set; }
        }

        public class QuestionCountDetails
        {
            [JsonPropertyName("total_question_count")]
            public int Total { get; set; }

            [JsonPropertyName("total_easy_question_count")]
            public int Easy { get; set; }

            [JsonPropertyName("total_medium_question_count")]
            public int Medium { get; set; }

            [JsonPropertyName("total_hard_question_count")]
            public int Hard { get; set; }
        }
    }
}
