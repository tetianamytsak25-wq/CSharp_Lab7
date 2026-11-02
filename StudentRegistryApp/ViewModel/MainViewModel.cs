using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StudentRegistryApp.Models;
using StudentRegistryApp.Services;

namespace StudentRegistryApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DateParserService parser = new DateParserService();
        private string inputText = "";
        private string message = "";
        public string InputText
        {
            get { return inputText; }
            set
            {
                inputText = value;
                OnPropertyChanged();
            }
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DateParseResult> Results { get; } = new ObservableCollection<DateParseResult>();
        public ICommand ParseCommand { get; }
        public MainViewModel()
        {
            ParseCommand = new RelayCommand(_ => ParseText());
        }
        private void ParseText()
        {
            Results.Clear();

            foreach (DateParseResult result in parser.ParseText(InputText))
            {
                Results.Add(result);
            }
            Message = $"Оброблено рядків: {Results.Count}";
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}