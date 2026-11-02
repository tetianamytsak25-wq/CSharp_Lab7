using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using StudentRegistryApp.Events;

namespace StudentRegistryApp.Models
{
    public class Student : INotifyPropertyChanged
    {
        private List<int> marks = new List<int>();
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public double Mass { get; set; }
        public List<int> Marks
        {
            get { return marks; }
            set
            {
                SetMarks(value);
            }
        }

        [JsonIgnore]
        public string MarksText
        {
            get { return string.Join(", ", marks); }
        }

        [JsonIgnore]
        public double AverageMark
        {
            get
            {
                if (marks.Count == 0)
                {
                    return 0;
                }

                return marks.Average();
            }
        }

        public event EventHandler<MarksChangedEventArgs>? MarksChanged;
        public void SetMarks(List<int>? newMarks)
        {
            newMarks ??= new List<int>();

            if (marks.SequenceEqual(newMarks))
            {
                return;
            }

            double oldAverage = AverageMark;

            marks = new List<int>(newMarks);

            double newAverage = AverageMark;

            OnPropertyChanged(nameof(Marks));
            OnPropertyChanged(nameof(MarksText));
            OnPropertyChanged(nameof(AverageMark));

            OnMarksChanged(new MarksChangedEventArgs(Id, oldAverage, newAverage));
        }

        protected virtual void OnMarksChanged(MarksChangedEventArgs e)
        {
            MarksChanged?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}