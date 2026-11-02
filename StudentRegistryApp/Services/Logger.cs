using System.Collections.ObjectModel;
using StudentRegistryApp.Events;
using StudentRegistryApp.Models;

namespace StudentRegistryApp.Services
{
    public class Logger
    {
        public ObservableCollection<LogEntry> Entries { get; } = new ObservableCollection<LogEntry>();

        public void OnMarksChanged(object? sender, MarksChangedEventArgs e)
        {
            LogEntry entry = new LogEntry
            {
                Time = e.EventTime,
                StudentId = e.StudentId,
                OldAverage = e.OldAverage,
                NewAverage = e.NewAverage,
                Message = "Оцінки студента змінено"
            };
            Entries.Add(entry);
        }
    }
}