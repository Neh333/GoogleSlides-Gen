using GoogleSlides_Gen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoogleSlides_Gen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /*
         * DateOnly array 1: {start date, end date}, DateOnly array ...: {start date, end date}
        */
        public List<DateOnly[]> dateRanges { get; set; } = new List<DateOnly[]>
        {
            new DateOnly[2]
        };

        public DateOnly[] SchoolYearHolidays2024 = {
           new DateOnly(2024, 11, 5), //election day 
           new DateOnly(2025, 1, 20), //MLK  Jr day 
           new DateOnly(2025, 2, 17), //presidents day 
           new DateOnly(2025, 4, 18), //good friday 
        };

        List<DateOnly> Mon_FriDatesInRange { get; set; } = new List<DateOnly>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public void CreateSlides(bool excludeHolidays)
        {
            List<DateOnly> datesInRange = new List<DateOnly>();
            
            /*go through each date range*/
            for (int i = 0; i < dateRanges.Count(); i++)
            {
                DateOnly startDate = dateRanges[i][0];
                DateOnly endDate = dateRanges[i][1];

                var query = datesInRange.Where(d => d.DayOfWeek != DayOfWeek.Sunday ||
                     d.DayOfWeek != DayOfWeek.Saturday).ToList();

                Mon_FriDatesInRange = query;

                if (excludeHolidays)
                {
                    Mon_FriDatesInRange = ExcludeHolidays(Mon_FriDatesInRange);
                }

                foreach (var dateRange in Mon_FriDatesInRange)
                {
                    //api calls to make slides for each M-F in date range 
                }
            }
        }

        public List<DateOnly> ExcludeHolidays(List<DateOnly> dates)
        {
            for (int i = 0; i < SchoolYearHolidays2024.Length; i++)
            { 
                foreach (var item in dates)
                {

                    if (item.Equals(SchoolYearHolidays2024[i]))
                    {
                       dates.Remove(item);
                    }
                }
            }
            return dates;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
