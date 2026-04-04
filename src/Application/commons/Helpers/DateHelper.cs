
namespace Application.commons.Helpers
{
    public class DateHelper
    {
        public static DateTime GetPHTime()
        {
            var phTimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, phTimeZone);
        }
    }
}