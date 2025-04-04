namespace WsRest_UpWay.Models;

public class TimeUtils
{
    public static TimeSpan PrettyParse(string time)
    {
        if (time.EndsWith("d")) return TimeSpan.FromDays(double.Parse(time.Replace("d", "")));

        if (time.EndsWith("h")) return TimeSpan.FromHours(double.Parse(time.Replace("h", "")));

        if (time.EndsWith("m")) return TimeSpan.FromMinutes(double.Parse(time.Replace("m", "")));

        if (time.EndsWith("s")) return TimeSpan.FromSeconds(double.Parse(time.Replace("s", "")));

        return TimeSpan.Parse(time);
    }
}