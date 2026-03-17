using QueryQuest.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace QueryQuest.Core.Models
{
    public class AnswerOption : ObservableObject
    {
        public string Text { get; set; }

    private AnswerStatus _status = AnswerStatus.Unanswered;
        public AnswerStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }
    }
}

