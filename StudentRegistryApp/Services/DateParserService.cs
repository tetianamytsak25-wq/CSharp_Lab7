using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using StudentRegistryApp.Models;

namespace StudentRegistryApp.Services
{
    public class DateParserService
    {
        private readonly string[] monthNames =
        {
            "January", "February", "March", "April",
            "May", "June", "July", "August",
            "September", "October", "November", "December"
        };
        private readonly string[] monthAbbreviations =
        {
            "Jan", "Feb", "Mar", "Apr", "May", "Jun",
            "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
        };

        private readonly Regex dateRegex = new Regex(@"(?<![A-Za-z])(?:(?<month1>[A-Za-z]+)\s+(?<day1>\d{1,2})|(?<day2>\d{1,2})\s+(?<month2>[A-Za-z]+))(?![A-Za-z])", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        public List<DateParseResult> ParseText(string text)
        {
            List<DateParseResult> results = new List<DateParseResult>();

            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                Match match = dateRegex.Match(line);

                if (!match.Success)
                {
                    results.Add(new DateParseResult
                    {
                        OriginalText = line,
                        Status = "Дату не розпізнано"
                    });
                    continue;
                }

                string dayText;
                string monthText;

                if (match.Groups["day1"].Success)
                {
                    dayText = match.Groups["day1"].Value;
                    monthText = match.Groups["month1"].Value;
                }
                else
                {
                    dayText = match.Groups["day2"].Value;
                    monthText = match.Groups["month2"].Value;
                }

                int day = int.Parse(dayText);
                int month = FindMonth(monthText);

                if (month == 0)
                {
                    results.Add(new DateParseResult
                    {
                        OriginalText = line,
                        Day = day,
                        Month = monthText,
                        Status = "Невідомий місяць"
                    });
                    continue;
                }

                if (day < 1 || day > DateTime.DaysInMonth(2024, month))
                {
                    results.Add(new DateParseResult
                    {
                        OriginalText = line,
                        Day = day,
                        Month = monthNames[month - 1],
                        Status = "Некоректний день"
                    });
                    continue;
                }
                DateTime parsedDate = new DateTime(2024, month, day);

                results.Add(new DateParseResult
                {
                    OriginalText = line,
                    Day = day,
                    Month = monthNames[month - 1],
                    NormalizedDate = parsedDate.ToString("dd-MMM", CultureInfo.InvariantCulture),
                    ParsedDate = parsedDate,
                    Status = "Розпізнано"
                });
            }
            return results;
        }
        private int FindMonth(string monthText)
        {
            for (int i = 0; i < monthNames.Length; i++)
            {
                if (monthNames[i].StartsWith(monthText, StringComparison.OrdinalIgnoreCase))
                {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}