﻿using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Services;

namespace SalesWebMvc.Services
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
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

    }
}