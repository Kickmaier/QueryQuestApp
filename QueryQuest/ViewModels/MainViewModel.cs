using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QueryQuest.Application.Interfaces;
using QueryQuest.Core.Interfaces;
using QueryQuest.Core.Models;
using QueryQuest.ViewModels.Models;
using QueryQuest.Views;

namespace QueryQuest.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IGameSettingsService _gameSettings;
        public ICommand StartGameCommand { get; }
        public MainUIState UI { get; }
        public MainViewModel(IGameSettingsService gameSettings, MainUIState mainUIState, GameSettingsUI gameSettingsUI)
        {
            _gameSettings = gameSettings;
            UI = mainUIState;
            UI.Settings = gameSettingsUI;
            StartGameCommand = new Command(async () => await StartGame());
        }


        public string Amount => UI.Settings.Amount;
        public string Difficulty => UI.Settings.Difficulty;
        public string CategoryId => UI.Settings.CategoryId;

        public string AmountLabelText => $"Längd: {UI.Settings.AmountDisplay}";
        public string DifficultyLabelText => $"Svårighetsgrad: {UI.Settings.DifficultyDisplay}";
        public string CategoryLabelText => $"Kategori: {UI.Settings.CategoryDisplay}";
        public bool IsAmountError => _gameSettings.GetAmountDisplay(UI.Settings.Amount) == null;
        public bool IsDifficultyError => _gameSettings.GetDifficultyDisplay(UI.Settings.Difficulty) == null;
        public bool IsCategoryError => _gameSettings.GetCategoryIdDisplay(UI.Settings.CategoryId) == null;
        public bool CanStartGame => !IsAmountError && !IsDifficultyError && !IsCategoryError;

        public void SetAmount(string value) { UI.Settings.Amount = value; UI.IsAmountVisible = false; OnPropertyChanged(nameof(AmountLabelText)); }
        
        public void SetDifficulty(string value) { UI.Settings.Difficulty = value; UI.IsDifficultyVisible = false; OnPropertyChanged(nameof(DifficultyLabelText)); }
        
        public void SetCategory(string value) { UI.Settings.CategoryId = value; UI.IsCategoryVisible = false; OnPropertyChanged(nameof(CategoryLabelText)); }

        public void ToggleMenu() => UI.IsMenuVisible = !UI.IsMenuVisible;
        public void UppdateAll()
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
