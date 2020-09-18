using CrossLayer.Models.Performance;
using Microsoft.AspNetCore.Mvc;
using Presentation.PerformanceWeb.Application;
using Presentation.PerformanceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.PerformanceWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewModelChartBuilder viewModelChartBuilder;
        private readonly IEnumerable<PerformanceActionsViewModel> actionViewModels;

        public HomeController(IViewModelChartBuilder viewModelChartBuilder)
        {
            this.viewModelChartBuilder = viewModelChartBuilder ?? throw new ArgumentNullException(nameof(viewModelChartBuilder));
            this.actionViewModels = this.viewModelChartBuilder.CreateKycChartViewModel();
        }

        public IActionResult ExamplePerformance()
        {
            var performanceViewModel = new PerformanceViewModel
            {
                Title = "Example Rest API",
                PerformanceActionsViewModel = this.actionViewModels.Where(m => m.PerformanceActionType == PerformanceActionType.ExamplePerformance).First()
            };

            return View(performanceViewModel);
        }
    }
}