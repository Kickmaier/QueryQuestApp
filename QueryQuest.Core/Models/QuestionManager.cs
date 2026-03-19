using QueryQuest.Core.Interfaces;

namespace QueryQuest.Core.Models
{
    public class QuestionManager : IQuestionService
    {
        private int _currentQuestionIndex = 0;
        public int CurrentQuestionIndex
        {
            get => _currentQuestionIndex;
            private set => _currentQuestionIndex = value; 
        }

        private string _currentCorrectAnswer = "";
        public string CurrentCorrectAnswer
        {
            get => _currentCorrectAnswer;
            private set => _currentCorrectAnswer = value;
        }
        public List<Question> Questions { get; set; } = new();
        private Question _currentQuestion;
        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set => _currentQuestion = value;
        }
        private bool IsValid(Question question)
        {
            if(string.IsNullOrWhiteSpace(question.Text)) return  false;
            if(string.IsNullOrEmpty(question.CorrectAnswer)) return false;
            if(question.Text.Length < 5) return false;
            if(question.IncorrectAnswers.Any(string.IsNullOrWhiteSpace)) return false;
            if(question.IncorrectAnswers.Count != 1 && question.IncorrectAnswers.Count != 3) return false;
            return true;
        }
        public string GetCurrentText() => $"Fråga: {_currentQuestionIndex} / {Questions.Count}";
        public bool SetNextQuestion()
        {
            if (!HasMoreQuestions) return false;
            
            CurrentQuestion = Questions[_currentQuestionIndex];
            CurrentCorrectAnswer = CurrentQuestion.CorrectAnswer;
            CurrentQuestionIndex++;
            return true;
        }
        public void Reset()
        {
            _currentQuestionIndex = 0;
            CurrentQuestion = null;
            Questions.Clear();
        }
        public void PrepareQuestion(IEnumerable<Question> questions, int expected)
        {
            var cleanedQuestion = questions.Where(q => IsValid(q)).ToList();
            Questions.Clear();
            if (cleanedQuestion.Count < expected)
            {
                throw new Exception("Fel på frågor, försök igen");
            }

            foreach (var q in cleanedQuestion)
            {
                var allAnswers = q.IncorrectAnswers.Append(q.CorrectAnswer)
                    .OrderBy(a => Guid.NewGuid()).ToList();

                q.AllAnswerOptions = allAnswers
                        .Select(a => new AnswerOption { Text = a })
                        .ToList();
                Questions.Add(q);
            }
            _currentQuestionIndex = 0;
        }
        public bool CheckAnswer(AnswerOption selected)
        {
            if (selected == null) return false;
            return selected.Text == CurrentCorrectAnswer;
        }
        public bool HasMoreQuestions => CurrentQuestionIndex < Questions.Count;
    }

}
