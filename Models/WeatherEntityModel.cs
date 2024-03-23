using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatherMoscowApp.Models
{
    public class WeatherEntityModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; } // Дата

        [Column(TypeName = "time(7)")]
        public TimeSpan Time { get; set; } // Время

        public string? Temperature { get; set; } // Температура в градусах Цельсия

        public string? RelativeHumidity { get; set; } // Относительная влажность в процентах

        public string? DewPoint { get; set; } // Точка росы в градусах Цельсия

        public string? AtmosphericPressure { get; set; } // Атмосферное давление в мм рт. ст.

        public string? WindDirection { get; set; } // Направление ветра

        public string? WindSpeed { get; set; } // Скорость ветра в м/с

        public string? Cloudiness { get; set; } // Облачность в процентах

        public string? H { get; set; } // Нижняя граница облачности

        public string? Visibility { get; set; } // Видимость в км

        public string? WeatherPhenomena { get; set; } // Погодные явления

    }
}
