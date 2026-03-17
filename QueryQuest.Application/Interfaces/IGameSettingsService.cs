using QueryQuest.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QueryQuest.Application.Interfaces
{
    public interface IGameSettingsService : INotifyPropertyChanged
    {
        string UnknownCategory { get; }
        string AmountError { get; }
        string DifficultyError { get; }
        string Amount { get; set; }
        string Difficulty { get; set; }
        string CategoryId { get; set; }

        string AmountDisplay { get; }
        string DifficultyDisplay { get; }
        string CategoryIdDisplay { get; }
    }
}
