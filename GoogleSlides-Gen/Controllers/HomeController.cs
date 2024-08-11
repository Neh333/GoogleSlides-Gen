using GoogleSlides_Gen.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.VisualBasic;



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

        public JsonResult FilterDates(bool excludeHolidays)
        {
            List<DateOnly> DatesInRange = new List<DateOnly>();
            
            /*go through each date range*/
            for (int i = 0; i < dateRanges.Count(); i++)
            {
                DateOnly startDate = dateRanges[i][0];
                DateOnly endDate = dateRanges[i][1];

                /*<= overload to keep on continue to dates after [i][0]*/
                for (var dt = dateRanges[i][0]; dt <= dateRanges[i][1]; dt = dt.AddDays(1)) 
                {
                    DatesInRange.Add(dt);
                }

                this.Mon_FriDatesInRange = DatesInRange.Where(d => d.DayOfWeek != DayOfWeek.Sunday ||
                     d.DayOfWeek != DayOfWeek.Saturday).ToList();

                if (excludeHolidays)
                {
                    this.Mon_FriDatesInRange = ExcludeHolidays(this.Mon_FriDatesInRange);
                }
            }
            return Json(Mon_FriDatesInRange);
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
