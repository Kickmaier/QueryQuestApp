namespace QueryQuest.Core.Models
{
    public class Question
    {
        public string Text { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> IncorrectAnswers { get; set; }

        public List<AnswerOption> AllAnswerOptions { get; set; } = new();
    }
}
