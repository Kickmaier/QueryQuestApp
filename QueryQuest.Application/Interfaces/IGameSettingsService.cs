namespace QueryQuest.Application.Interfaces
{
    public interface IGameSettingsService
    {
        string Amount { get; set; }
        string Difficulty { get; set; }
        string CategoryId { get; set; }

        string? GetAmountDisplay(string value);
        string? GetDifficultyDisplay(string value);
        string? GetCategoryIdDisplay(string value);

        string AmountDisplay { get; }
        string DifficultyDisplay { get; }
        string CategoryIdDisplay { get; }
    }
}
