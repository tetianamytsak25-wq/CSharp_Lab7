using System;

namespace StudentRegistryApp.Models
{
    public class DateParseResult
    {
        public string OriginalText { get; set; } = "";
        public int? Day { get; set; }
        public string Month { get; set; } = "";
        public string NormalizedDate { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime? ParsedDate { get; set; }
    }
}