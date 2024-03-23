using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NPOI.SS.Formula.Functions;
using System.Globalization;
using WatherMoscowApp.Models;

namespace WatherMoscowApp.Controllers
{
    
    public class DatabaseController(WatherDbContext dbContext) : Controller
    {

        public IActionResult Index(int month = 13, int year = 0, int courentPage = 1)
                {
            
            List<string> x = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList();
            ViewBag.ListOfMonths = x;

            ViewBag.DistinctMonth = dbContext.WatherEntities
                .Select(w => w.Date.Month)
                .Distinct()
                .OrderBy(w => w)
                .ToList();

            ViewBag.DistinctMonth.Add(13);

            ViewBag.DistinctYears = dbContext.WatherEntities
                .Select(w => 
                    w.Date.Year)
                .Distinct()
                .OrderBy(w => w)
                .ToList();
            ViewBag.DistinctYears.Add(0);

            

            var query = dbContext.WatherEntities;
            IQueryable<WeatherEntityModel>? res = null;

            if (month != 13)
            {
                res = query.Where(w => w.Date.Month == month);
            }

            if (year != 0)
            {
                res = (res != null ? res : query).Where(w => w.Date.Year == year);
            }

            int i = 1;
            var query1 = (res != null ? res : query)
                .AsEnumerable()
                .Select((e, index) => new { Entity = e, RowNum = index + 1 });

            ViewBag.Pages = Enumerable.Range(1, query1.Count()/10 + (query1.Count()%10 > 0?1:0));

            var list = query1.Where(x => x.RowNum > (courentPage - 1) * 10 && x.RowNum <= courentPage * 10)//Сделал иммено так, потому что у меня MSSQL Server 2008. Fetch и Offset неподдерживается
                .Select(x => x.Entity)
                .ToList();


            return View(new WeatherViewModel
            {
                Month = month,
                Year = year,
                CourentPage = courentPage,
                Entities = list
            });
        }

    }
}
