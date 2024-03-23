using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;
using WatherMoscowApp.Models;

namespace WatherMoscowApp.Controllers
{
    public class UploadController(WatherDbContext dbContext) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("UploadExcelFiles")]
        public async Task<IActionResult> UploadExcelFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return View(new UploadResponceModel { Response = "Ошибка загрузки файлов"});
            }
            int i = 0;
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    int x = await UploadExcel(file);
                    if (x != 0)
                    {
                        i++;
                    }
                }
            }

            return View(new UploadResponceModel { Response = "Файлы загружены.Незагруженные файлы с ошибкой: " + i});
        }


        async Task<int> UploadExcel(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    return 1;
                }

                using (var stream = file.OpenReadStream())
                {
                    var workbook = new XSSFWorkbook(stream); // Загружаем книгу Excel из потока
                                                             // Получаем первый лист книги
                    ArrayList x = new ArrayList();

                    for (int i = 0; i < workbook.NumberOfSheets; i++)
                    {
                        var sheet = workbook.GetSheetAt(i);

                        // Проходим по строкам в листе
                        for (int rowIndex = 4; rowIndex <= sheet.LastRowNum; rowIndex++)
                        {
                            var row = sheet.GetRow(rowIndex);
                            if (row != null)
                            {
                                x.Clear();

                                // Проходим по ячейкам в строке
                                for (int cellIndex = 0; cellIndex <= 11; cellIndex++)
                                {

                                    var cell = row.GetCell(cellIndex);
                                    if (cell == null)
                                    {
                                        x.Add("");
                                    }
                                    else
                                    {
                                        // Если ячейка содержит текстовое значение
                                        string value = cell.ToString().Trim().Replace(',', '.');
                                        x.Add(value);
                                        // Далее можно выполнить необходимые операции с текстовым значением
                                    }

                                }

                                WeatherEntityModel w = new WeatherEntityModel
                                {
                                    Date = DateTime.Parse(x[0].ToString()),
                                    Time = TimeSpan.Parse(x[1].ToString()),
                                    Temperature = x[2] as string,
                                    RelativeHumidity = x[3] as string,
                                    DewPoint = x[4] as string,
                                    AtmosphericPressure = x[5] as string,
                                    WindDirection = x[6] as string,
                                    WindSpeed = x[7] as string,
                                    Cloudiness = x[8] as string,
                                    H = x[9] as string,
                                    Visibility = x[10] as string,
                                    WeatherPhenomena = x[11] as string
                                };

                                dbContext.WatherEntities.Add(w);

                            }
                        }
                    }
                }

                dbContext.SaveChanges();

                return 0;
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //    return StatusCode(500, $"An error occurred: {ex.Message}");
                //}
            } catch { 
                return 1; 
            }
        }
    }
}
