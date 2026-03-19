

using QueryQuest.Application.Interfaces;

namespace QueryQuest.Application.Services
{
    public class GameSettingsService : IGameSettingsService
    {
        private string _amount = "10";
        public string Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                }
            }
        }

        private string _difficulty = "";
        public string Difficulty
        {
            get => _difficulty;
            set
            {
                if (_difficulty != value)
                {
                    _difficulty = value;
                }
            }
        }

        private string _categoryId = "";
        public string CategoryId
        {
            get => _categoryId;
            set
            {
                if (_categoryId != value)
                {
                    _categoryId = value;
                }
            }
        }

        public string? GetAmountDisplay(string value) => value switch
        {
            "10" => "Kort",
            "20" => "Medel",
            "30" => "Långt",
            _ => null
        };

        public string? GetDifficultyDisplay(string value) => value switch
        {
            "" => "Blandat",
            "easy" => "Lätt",
            "medium" => "Medel",
            "hard" => "Svårt",
            _ => null
        };

        public string? GetCategoryIdDisplay(string value) => value switch
        {
            "" => "Blandat",
            "9" => "Allmänbildning",
            //"11" => "Film & Bio",
            "12" => "Musik",
            //"14" => "TV-serier",
            //"15" => "Dataspel",
            //"16" => "Brädspel",
            //"17" => "Natur & Vetenskap",
            //"18" => "Teknik & IT",
            //"20" => "Mytologi",
            //"21" => "Sport",
            //"22" => "Geografi",
            "23" => "Historia",
            //"26" => "Kändisar",
            "27" => "Djur",
            _ => null
        };
        public string AmountDisplay => GetAmountDisplay(Amount);
        public string DifficultyDisplay => GetDifficultyDisplay(Difficulty);
        public string CategoryIdDisplay => GetCategoryIdDisplay(CategoryId);
    }
}


