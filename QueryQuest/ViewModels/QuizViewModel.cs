using QueryQuest.Application.Interfaces;
using QueryQuest.Core.Interfaces;
using QueryQuest.Core.Models;
using QueryQuest.ViewModels.Models;
using QueryQuest.Views;
using System.Diagnostics;
using System.Windows.Input;
using QueryQuest.Core.Enums;
using QueryQuest.Application.Services;


namespace QueryQuest.ViewModels
{
    public class QuizViewModel : ObservableObject
    {
        private readonly IGameSettingsService _gameSettings;
        public IGameEngine GameEngine { get; }
        public QuizUIState UI {  get; } 
        private IDispatcherTimer _timer;
        private double _totalTime = 100;
        private double _timeLeft;
        public Question CurrentQuestion => GameEngine.CurrentQuestion;
        public List<AnswerOption> CurrentAnswers => GameEngine.CurrentAnswerOptions;
        
        public ICommand AnswerSelectedCommand { get; }
        public ICommand PlayAgainCommand { get; }
        public ICommand GoToMainPageCommand { get; }
        public QuizViewModel (QuizUIState quizUIState, GameSettingsUI gameSettingsUI, IGameEngine gameEngine)
        {
            _gameSettings = GameSettingsService.GetGameSettingsService;
            GameEngine = gameEngine;
            UI = quizUIState;
            _timer = Dispatcher.GetForCurrentThread().CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += (s, e) => UpdateTimer();

            AnswerSelectedCommand = new Command<AnswerOptionUI>(OnAnswerSelected);
            PlayAgainCommand = new Command(async () => await ResetGame());
            GoToMainPageCommand = new Command(async () => await Shell.Current.GoToAsync($"//{nameof(MainPage)}"));
        }
        
        public event EventHandler TimeOutOccurred;
        
        public async Task LoadQuestionAsync()
        {
            UI.QuizAreaVisible = true;
            UI.GameOverVisible = false;
            string amount = _gameSettings.Amount;
            string difficulty = _gameSettings.Difficulty;
            string catregoruId = _gameSettings.CategoryId;
            try
            {
            await GameEngine.StartGameAsync(amount,difficulty,catregoruId);
                ShowNextQuestion();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fel vid laddning av frågor: {ex.Message} \n {ex.StackTrace}");


                string error = ex.Message switch
                {
                    var message when message.Contains("429") => "Api:et är upptaget vänta några sekunder",
                    "Inga frågor hittades"                   => "Inga frågor hittades",
                    "Fel på frågor, försök igen"             => "Fel på frågor, försök igen",
                    _                                        => "Kunde inte ladda frågor. Kontrollera din anslutning."
                };

                HandleGameOver("Hoppsan", error);
            }
        }
        public void ShowNextQuestion()
        {
            try
            {
                if(GameEngine.HasMoreQuestions)
                {
                    GameEngine.NextQuestion();
                    UI.Answers.Clear();
                    foreach (var answer in GameEngine.CurrentAnswerOptions)
                    {
                        UI.Answers.Add(new AnswerOptionUI { Text = answer.Text, Status = AnswerStatus.Unanswered });
                    }
                    OnPropertyChanged(nameof(CurrentQuestion));
                    OnPropertyChanged(nameof(CurrentAnswers));
                    UI.ProgressBarProgress = 0;
                    UI.QuestionCounterText = GameEngine.GetCurrenProgress();
                    _timeLeft = _totalTime;
                    _timer.Start();
                }
                else
                {
                    HandleGameOver();
                }
            }
            catch(Exception ex) 
            {
                HandleGameOver("Ett fel uppstod", "Spelet var tvunget att avbrytas.");
                Debug.WriteLine($"Fel i ShowNextQuestion: {ex.Message}");
            }
        }

        public async Task ResetGame()
        {
            CleanUp();

            await LoadQuestionAsync();
        }

        private void UpdateTimer()
        {
            if (UI.IsAnswerd) return;
            _timeLeft -= 1;
            double elapsedProgress = (_totalTime - _timeLeft) / _totalTime;
            UI.ProgressBarProgress = elapsedProgress;

            if (UI.ProgressBarProgress > 0.66) UI.TimerStatus = TimerState.Danger;

            else if (UI.ProgressBarProgress > 0.33) UI.TimerStatus = TimerState.Warning;

            else UI.TimerStatus = TimerState.Good;
            
            if (_timeLeft <= 0)
            {
                _timer.Stop();
                HandleTimeOut();
            }
        }
        private async void OnAnswerSelected(AnswerOptionUI selectedOption)
        {
            await ProsessAnswerAsync(selectedOption);
        }
        private async Task ProsessAnswerAsync(AnswerOptionUI selectedOption)
        {
            if (IsInvalid(selectedOption)) return;
            try
            {
                PrepareCheck();
                bool isCorrect = GameEngine.ProcessAnswer(new AnswerOption { Text = selectedOption.Text });
                selectedOption.Status = isCorrect ? AnswerStatus.Correct : AnswerStatus.Wrong;
                if (!isCorrect)
                {
                    ShowAnswer(selectedOption);
                }
                OnPropertyChanged(nameof(GameEngine));
                await Task.Delay(1000);
                
                PrepareForNextQuestion();
            }
            catch (Exception ex)
            {
                HandleGameOver("Ett fel uppstod", "Spelet var tvunget att avbrytas.");
                Debug.WriteLine($"Fel i OnAnswerSelected: {ex.Message}");
            }
        }
        private bool IsInvalid(AnswerOptionUI selectedOption) => (selectedOption == null || UI.IsAnswerd);
        private void PrepareCheck()
        {
            UI.IsAnswerd = true;
            _timer.Stop();
        }

        private void PrepareForNextQuestion()
        {
            UI.IsAnswerd = false;
            ShowNextQuestion();
        }
        private void ShowAnswer(AnswerOptionUI? selectedOption)
        {
            var correct = UI.Answers
                    .FirstOrDefault(a => a.Text == GameEngine.CurrentCorrectAnswer);
            if (correct != null) correct.Status = AnswerStatus.Correct;
        }
        private async void HandleTimeOut()
        {
            TimeOutOccurred?.Invoke(this, EventArgs.Empty);
            UI.IsAnswerd = true;
            ShowAnswer(null);
            await Task.Delay(1000);
            PrepareForNextQuestion();
        }

        private void HandleGameOver(string? header = null, string? body = null)
        {
            UI.StatusHeader = header ?? "Spelet är över";
            UI.StatusBody = body ?? string.Empty;
            if (header == null)
            {
                UI.StatusBody =
                    $"Antal rätt: {GameEngine.Score.CorrectAnswerCount} / {_gameSettings.Amount}" +
                    $"\nHögsta Streak: {GameEngine.Score.HighestStreakCount} / {_gameSettings.Amount}";
                UI.StatusScore =
                    $"Slutresultat: {GameEngine.Score.CurrentScore}";
            }
            else
            {
                UI.StatusScore = string.Empty;
            }
                UI.RetryButtonText = header != null ? "Försök igen" : "Spela igen";
            CleanUp();
            UI.QuizAreaVisible = false;
            UI.GameOverVisible = true;
        }
        public void CleanUp()
        { 
            _timer.Stop();
            GameEngine.ResetGame();
            UI.Reset();
            OnPropertyChanged(nameof(GameEngine)); 
        }
    }
}
