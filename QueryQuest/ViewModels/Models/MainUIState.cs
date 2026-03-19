namespace QueryQuest.ViewModels.Models
{
    public class MainUIState : ObservableObject 
    {
        private bool _isMenuVisible;
        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            set { _isMenuVisible = value; OnPropertyChanged(); }
        }
        private bool _isAmountVisible;
        public bool IsAmountVisible
        {
            get => _isAmountVisible;
            set { if (value) CloseAll(); 
                _isAmountVisible = value; 
                OnPropertyChanged(); }
        }
        private bool _isDifficultyVisible;
        public bool IsDifficultyVisible
        {
            get => _isDifficultyVisible;
            set { if (value) CloseAll();
                _isDifficultyVisible = value; 
                OnPropertyChanged(); }
        }
        private bool _isCategoryVisible;
        public bool IsCategoryVisible
        {
            get => _isCategoryVisible;
            set { if (value) CloseAll(); 
                _isCategoryVisible = value; 
                OnPropertyChanged(); }
        }
        private void CloseAll()
        {
            _isAmountVisible = _isDifficultyVisible = _isCategoryVisible = false; 
            OnPropertyChanged(string.Empty);
        }
        private GameSettingsUI _settings;
        public GameSettingsUI Settings
        {
            get => _settings;
            set { _settings = value; OnPropertyChanged(); }
        }
    }
}
