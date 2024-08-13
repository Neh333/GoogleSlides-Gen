namespace GoogleSlides_Gen.Models;
public class HomeViewModel
{
    private static readonly List<DateOnly[]> list = new List<DateOnly[]>
    {
        new DateOnly[2]
    };
    public List<DateOnly[]> dateRanges = list; 

    /*public bool excludeMon, excludeTue, excludeWed, excludeThu, excludeFri, excludeSat, excludeSun;
*/
    public bool excludeHolidays;
   
}


