public class Utils
{
  public DateOnly SkipWeekends(DateOnly d)
  {
    while (d.DayOfWeek is DayOfWeek.Saturday || d.DayOfWeek is DayOfWeek.Sunday)
    {
      d = d.AddDays(1);
    }

    return d;
  }

}
