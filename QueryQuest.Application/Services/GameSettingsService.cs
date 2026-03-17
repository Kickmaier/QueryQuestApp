
using QueryQuest.Core.Models;
using QueryQuest.Application.Interfaces;

namespace QueryQuest.Application.Services
{
    public class GameSettingsService : ObservableObject, IGameSettingsService
    {
        public string UnknownCategory => "Okänd kategori";
        public string AmountError => "Ej standardvärde";
        public string DifficultyError => "Felaktig svårighetsgrad";

        private string _amount = "10";
        public string Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(AmountDisplay));
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DifficultyDisplay));
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CategoryIdDisplay));
                }
            }
        }

        public string AmountDisplay => Amount switch
        {
            "10" => "Kort",
            "20" => "Medel",
            "30" => "Långt",
            _ => AmountError
        };

        public string DifficultyDisplay => Difficulty switch
        {
            "" => "Blandat",
            "easy" => "Lätt",
            "medium" => "Medel",
            "hard" => "Svårt",
            _ => DifficultyError
        };

        public string CategoryIdDisplay => CategoryId switch
        {
            "" => "Blandat",
            "9" => "Allmänbildning",
            "11" => "Film & Bio",
            "12" => "Musik",
            "14" => "TV-serier",
            "15" => "Dataspel",
            "16" => "Brädspel",
            "17" => "Natur & Vetenskap",
            "18" => "Teknik & IT",
            "20" => "Mytologi",
            "21" => "Sport",
            "22" => "Geografi",
            "23" => "Historia",
            "26" => "Kändisar",
            "27" => "Djur",
            _ => UnknownCategory
        };
    }
}


