using Microsoft.EntityFrameworkCore;
using WatherMoscowApp.Models;

namespace WatherMoscowApp
{
    public class WatherDbContext : DbContext
    {
        public DbSet<WeatherEntityModel> WatherEntities { get; set; } = null!;
        public WatherDbContext(DbContextOptions<WatherDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<WeatherEntityModel>().HasData(
            //        new WeatherEntityModel { Id = 1, AtmosphericPressure = "1", DewPoint = "1", H = "1", RelativeHumidity = "1", Temperature = "1", WindDirection = "C", WindSpeed = "1" },
            //        new WeatherEntityModel { Id = 2, AtmosphericPressure = "2", DewPoint = "1", H = "1", RelativeHumidity = "1", Temperature = "1", WindDirection = "C", WindSpeed = "1" },
            //        new WeatherEntityModel { Id = 3, AtmosphericPressure = "3", DewPoint = "1", H = "1", RelativeHumidity = "1", Temperature = "1", WindDirection = "C", WindSpeed = "1" }
            //);
        }
    }
}