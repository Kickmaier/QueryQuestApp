using QueryQuest.Core.Enums;
using System.Collections.ObjectModel;

namespace QueryQuest.ViewModels.Models
{

    public class QuizUIState : ObservableObject
    {
        public ObservableCollection<AnswerOptionUI> Answers { get; } = new();
        public bool IsScoreVisible => _statusHeader == null;

        private bool _quizAreaVisible = true;
        public bool QuizAreaVisible
        {
            get { return _quizAreaVisible; }
            set { _quizAreaVisible = value; OnPropertyChanged(); }
        }

        private bool _gameOverVisible = false;
        public bool GameOverVisible
        {
            get { return _gameOverVisible; }
            set { _gameOverVisible = value; OnPropertyChanged(); }
        }

        private string _statusHeader;
        public string StatusHeader
        {
            get => _statusHeader;
            set { _statusHeader = value; OnPropertyChanged(); }
        }

        private string _statusBody;
        public string StatusBody
        {
            get => _statusBody;
            set { _statusBody = value; OnPropertyChanged(); }
        }

        private string _statusScore;
        public string StatusScore
        {
            get => _statusScore;
            set { _statusScore = value; OnPropertyChanged(); }
        }

        private string _retryButtonText;
        public string RetryButtonText
        {
            get => _retryButtonText;
            set { _retryButtonText = value; OnPropertyChanged(); }
        }

        private string _questionCounterText;
        public string QuestionCounterText
        {
            get => _questionCounterText;
            set { _questionCounterText = value; OnPropertyChanged(); }
        }

        private double _progressBarProgress = 1.0;
        public double ProgressBarProgress
        {
            get => _progressBarProgress;
            set { _progressBarProgress = value; OnPropertyChanged(); }
        }

        private TimerState _timerStatus = TimerState.Good;
        public TimerState TimerStatus
        {
            get => _timerStatus;
            set
            {
                if (_timerStatus != value)
                {
                    _timerStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isAnswerd;
        public bool IsAnswerd
        {
            get => _isAnswerd;
            set { _isAnswerd = value; OnPropertyChanged(); }
        }

        public void Reset()
        {
            Answers.Clear();
            IsAnswerd = false;
            ProgressBarProgress = 0;
            TimerStatus = TimerState.Good;
        }
    }
    public class AnswerOptionUI : ObservableObject
    {
        public string Text { get; init; }

        private AnswerStatus _status = AnswerStatus.Unanswered;
        public AnswerStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }
    }

}
