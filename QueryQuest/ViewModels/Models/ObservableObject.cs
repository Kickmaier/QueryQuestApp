using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QueryQuest.ViewModels.Models
{
    public class ObservableObject : INotifyPropertyChanged  
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
