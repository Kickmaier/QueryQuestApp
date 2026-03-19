using QueryQuest.ViewModels;
using QueryQuest.ViewModels.Models;

namespace QueryQuest.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _mainViewModel;
    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        _mainViewModel = mainViewModel;
        BindingContext = _mainViewModel;

        _mainViewModel.UI.PropertyChanged += async (s, e) =>
        {
            if (e.PropertyName == nameof(MainUIState.IsMenuVisible)) await AnimateMenu(_mainViewModel.UI.IsMenuVisible);
        };
    }

    private void ToggleAmountSelection(object sender, EventArgs e) => _mainViewModel.UI.IsAmountVisible = !_mainViewModel.UI.IsAmountVisible;
    private void ToggleDifficultySelection(object sender, EventArgs e) => _mainViewModel.UI.IsDifficultyVisible = !_mainViewModel.UI.IsDifficultyVisible;
    private void ToggleCategorySelection(object sender, EventArgs e) => _mainViewModel.UI.IsCategoryVisible = !_mainViewModel.UI.IsCategoryVisible;

    private async Task AnimateMenu(bool isOpen)
    {     
        if (isOpen)
        {
            MenuOverlay.InputTransparent = false;
            await Task.WhenAll(
                SideMenu.TranslateTo(0, 0, 1000, Easing.CubicOut),
                MainView.TranslateTo(280, 0, 1000, Easing.CubicOut),
                MenuOverlay.FadeTo(0.7, 300)
            );
        }
        else
        {
            MenuOverlay.InputTransparent = true;
            await Task.WhenAll(
                SideMenu.TranslateTo(-280, 0, 300, Easing.CubicIn),
                MainView.TranslateTo(0,0,300, Easing.CubicIn),
                MenuOverlay.FadeTo(0, 300)
            );
        }
    }
}


