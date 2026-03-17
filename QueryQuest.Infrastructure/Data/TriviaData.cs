using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace QueryQuest.Infrastructure.Data
{
    public class TriviaData
    {

        public class Rootobject
        {
            [JsonPropertyName("response_code")]
            public int ResponseCode { get; set; }
            [JsonPropertyName("results")]
            public List<Result> Results { get; set; }
        }

        public class Result
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }
            [JsonPropertyName("difficulty")]
            public string Difficulty { get; set; }
            [JsonPropertyName("category")]
            public string Category { get; set; }
            [JsonPropertyName("question")]
            public string QuestionText { get; set; }
            [JsonPropertyName("correct_answer")]
            public string? CorrectAnswer { get; set; }
            [JsonPropertyName("incorrect_answers")]
            public List<string>? IncorrectAnswers { get; set; }
        }

    }
}
