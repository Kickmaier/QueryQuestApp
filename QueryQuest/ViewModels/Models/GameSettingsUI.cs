using QueryQuest.Application.Interfaces;
using QueryQuest.Core.Interfaces;
using QueryQuest.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace QueryQuest.ViewModels.Models
{
    public class GameSettingsUI : ObservableObject
    {
        private readonly IGameSettingsService _gameSettings;
        private readonly ITriviaService _triviaService;

        public GameSettingsUI(IGameSettingsService gameSettingsService, ITriviaService triviaService)
        {
            _gameSettings = gameSettingsService;
            _amount = _gameSettings.Amount;
            _difficulty = _gameSettings.Difficulty;
            _categoryId = _gameSettings.CategoryId;
            _triviaService = triviaService;
            _ = UpdateDifficultyAvailabilityAsync();
        }
        public string AmountDisplay => _gameSettings.GetAmountDisplay(Amount) ?? "Felaktig mängd";
        public string DifficultyDisplay => _gameSettings.GetDifficultyDisplay(Difficulty) ?? "Felaktig svårighetsgrad";
        public string CategoryDisplay => _gameSettings.GetCategoryIdDisplay(CategoryId) ?? "Felaktig kategori";
        
        private string _amount;
        public string Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnSettingsChanged();
                    OnPropertyChanged(nameof(AmountDisplay));
                }
            }
        }
        private string _difficulty;
        public string Difficulty
        {
            get => _difficulty;
            set
            {
                if (_difficulty != value)
                {
                    _difficulty = value;
                    OnSettingsChanged();
                    OnPropertyChanged(nameof(DifficultyDisplay));
                }
            }
        }
        private string _categoryId;
        public string CategoryId
        {
            get => _categoryId;
            set
            {
                if (_categoryId != value)
                {
                    _categoryId = value;
                    OnSettingsChanged();
                    Amount = "10";
                    Difficulty = "";
                    OnPropertyChanged(string.Empty);
                }
            }
        }
        
        private bool _canSelectEasy = true;
        public bool CanSelectEasy
        {
            get => _canSelectEasy;
            set { _canSelectEasy = value; OnPropertyChanged(); }
        }

        private bool _canSelectMedium = true;
        public bool CanSelectMedium
        {
            get => _canSelectMedium;
            set { _canSelectMedium = value; OnPropertyChanged();  }
        }

        private bool _canSelectHard = true;
        public bool CanSelectHard
        {
            get => _canSelectHard;
            set { _canSelectHard = value; OnPropertyChanged(); }
        }

        public async void OnSettingsChanged()
        {
            await UpdateDifficultyAvailabilityAsync();
        }
        private async Task UpdateDifficultyAvailabilityAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(CategoryId))
                {
                    CanSelectEasy = CanSelectMedium = CanSelectHard = true;
                    return;
                }

                int catId = int.Parse(CategoryId);
                var response = await _triviaService.GetCategoryStatsAsync(catId);

                int required = int.Parse(Amount);

                CanSelectEasy = response.EasyCount >= required;
                CanSelectMedium = response.MediumCount >= required;
                CanSelectHard = response.HardCount >= required;
            }
            catch (Exception ex)
            {
                CanSelectEasy = CanSelectMedium = CanSelectHard = true;
                System.Diagnostics.Debug.WriteLine($"Statistik-fel: {ex.Message}");
            }
        }
        public void SyncToService()
        {
            _gameSettings.Amount = Amount;
            _gameSettings.Difficulty = Difficulty;
            _gameSettings.CategoryId = CategoryId;
        }
    }
}
