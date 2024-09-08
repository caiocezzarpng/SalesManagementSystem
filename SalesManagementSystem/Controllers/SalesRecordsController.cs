using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Models;
using SalesManagementSystem.Services;
using SalesManagementSystem.Services.Factory;

namespace SalesWebMvc.Services
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;
        private readonly ReportFactory<SalesRecord> _reportFactory;

        public SalesRecordsController(SalesRecordService salesRecordService, ReportFactory<SalesRecord> reportFactory)
        {
            _salesRecordService = salesRecordService;
            _reportFactory = reportFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            //para ficar registrado na tela o intervalo das datas da busca, sem apagar depois que vier o resultado:
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            //Abaixo eu passo esses resultados para a View e coloco lá na view SimpleSeach
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            //para ficar registrado na tela o intervalo das datas da busca, sem apagar depois que vier o resultado:
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            //Abaixo eu passo esses resultados para a View e coloco lá na view SimpleSeach
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateSalesReport(DateTime? minDate, DateTime? maxDate)
        {
            minDate ??= new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            maxDate ??= DateTime.Now;

            var salesRecords = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            var report = _reportFactory.CreateReport();
            report.Generate(salesRecords);

            return View(nameof(Index));
        }
    }
}