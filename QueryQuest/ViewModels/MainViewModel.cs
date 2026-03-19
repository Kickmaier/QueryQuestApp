using System.Windows.Input;
using QueryQuest.Application.Interfaces;
using QueryQuest.Application.Services;
using QueryQuest.ViewModels.Models;
using QueryQuest.Views;

namespace QueryQuest.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IGameSettingsService _gameSettings;
        public ICommand StartGameCommand { get; }
        public ICommand ToggleMenuCommand { get; }
        public ICommand SetAmountCommand { get; }
        public ICommand SetDifficultyCommand {  get; }
        public ICommand SetCategoryCommand { get; }
        public MainUIState UI { get; }
        public MainViewModel(MainUIState mainUIState, GameSettingsUI gameSettingsUI)
        {
            _gameSettings = GameSettingsService.GetGameSettingsService;
            UI = mainUIState;
            UI.Settings = gameSettingsUI;
            StartGameCommand = new Command(async () => await StartGame());
            ToggleMenuCommand = new Command(ToggleMenu);
            SetAmountCommand = new Command<string>((val) =>
            {
                UI.Settings.Amount = val;
                UI.IsAmountVisible = false;
                UpdateAll();
            });
            SetDifficultyCommand = new Command<string>((val) =>
            {
                UI.Settings.Difficulty = val;
                UI.IsDifficultyVisible = false;
                UpdateAll();
            });
            SetCategoryCommand = new Command<string>((val) =>
            {
                UI.Settings.CategoryId = val;
                UI.IsCategoryVisible = false;
                UpdateAll();
            });
        }

        public string AmountLabelText => $"Längd: {UI.Settings.AmountDisplay}";
        public string DifficultyLabelText => $"Svårighetsgrad: {UI.Settings.DifficultyDisplay}";
        public string CategoryLabelText => $"Kategori: {UI.Settings.CategoryDisplay}";
        public bool IsAmountError => _gameSettings.GetAmountDisplay(UI.Settings.Amount) == null;
        public bool IsDifficultyError => _gameSettings.GetDifficultyDisplay(UI.Settings.Difficulty) == null;
        public bool IsCategoryError => _gameSettings.GetCategoryIdDisplay(UI.Settings.CategoryId) == null;
        public bool CanStartGame => !IsAmountError && !IsDifficultyError && !IsCategoryError;

        public void ToggleMenu() => UI.IsMenuVisible = !UI.IsMenuVisible;
        public void UpdateAll()
        {
            OnPropertyChanged(string.Empty);
        }
        public async Task StartGame()
        {
            UI.Settings.SyncToService();
            await Shell.Current.GoToAsync($"//{nameof(QuizPage)}");
        }
    }

}
