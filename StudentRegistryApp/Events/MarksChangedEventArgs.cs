using System;

namespace StudentRegistryApp.Events
{
    public class MarksChangedEventArgs : EventArgs
    {
        public int StudentId { get; }
        public double OldAverage { get; }
        public double NewAverage { get; }
        public DateTime EventTime { get; }
        public MarksChangedEventArgs(int studentId,
            double oldAverage, double newAverage)
        {
            StudentId = studentId;
            OldAverage = oldAverage;
            NewAverage = newAverage;
            EventTime = DateTime.Now;
        }
    }
}