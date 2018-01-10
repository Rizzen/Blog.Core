using System;

namespace Blog.Core.Utils
{
    /// <summary>Utility Class to Display Date & Time</summary>
    public class PostDateTime
    {
        public int Year { get; }
        
        public int Month { get; }
        
        public  int Day { get; }

        public PostDateTime(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public PostDateTime(DateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
        }

        public override string ToString()
        {
            return $"{Year}-{Month}-{Day}";
        }
    }
}