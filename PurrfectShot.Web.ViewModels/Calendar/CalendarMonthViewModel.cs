namespace PurrfectShot.Web.ViewModels.Calendar
{
    public class CalendarMonthViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public string MonthName { get; set; } = null!;

        public string CoverImageUrl { get; set; } = null!;

        public int PhotoCount { get; set; }

    }
}
