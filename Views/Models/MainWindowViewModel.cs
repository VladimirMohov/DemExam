using DemExam.DataApp.DBModels;
using System.ComponentModel;

namespace DemExam.Views.Models
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private User _currentUser;
        private string _opacity;
        private string _zindex;

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnPropertyChanged(nameof(CurrentUser));
                }
            }
        }

        public string Opacity
        {
            get => _opacity;
            set
            {
                if (_opacity != value)
                {
                    _opacity = value;
                    OnPropertyChanged(nameof(Opacity));
                }
            }
        }

        public string ZIndex
        {
            get => _zindex;
            set
            {
                if (_zindex != value)
                {
                    _zindex = value;
                    OnPropertyChanged(nameof(Opacity));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
