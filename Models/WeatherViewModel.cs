using Microsoft.VisualBasic;

namespace WatherMoscowApp.Models
{
    public class WeatherViewModel
    {
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int CourentPage { get; set; }
        public List<WeatherEntityModel> Entities { get; set; }
    }
}
