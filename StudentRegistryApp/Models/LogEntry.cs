using System;

namespace StudentRegistryApp.Models
{
    public class LogEntry
    {
        public DateTime Time { get; set; }
        public int StudentId { get; set; }
        public double OldAverage { get; set; }
        public double NewAverage { get; set; }
        public string Message { get; set; } = "";
    }
}