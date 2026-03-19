using QueryQuest.Core.Models;

namespace QueryQuest.Core.Interfaces
{
    public interface IQuestionService
    {
        void PrepareQuestion(IEnumerable<Question> questions, int expected);
        bool SetNextQuestion();
        bool CheckAnswer(AnswerOption selected);
        void Reset();

        Question CurrentQuestion { get; }
        string CurrentCorrectAnswer { get; }
        string GetCurrentText();
        bool HasMoreQuestions {  get; }
    }
}
