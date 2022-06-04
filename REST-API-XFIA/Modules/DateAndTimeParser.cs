namespace REST_API_XFIA.Modules
{
    public class DateAndTimeParser
    {
        public static DateTime parseDate(string toParse)
        {
            return DateTime.Parse(DateTime.Parse(toParse).ToString("yyyy-MM-dd"));
        }

        public static TimeSpan parseTime(string toParse)
        {
            return DateTime.Parse(toParse).TimeOfDay;
        }
    }
}
