using QueryQuest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
