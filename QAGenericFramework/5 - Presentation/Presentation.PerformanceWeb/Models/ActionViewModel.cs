using Presentation.PerformanceWeb.Models.ChartViewModels;
using System.Collections.Generic;

namespace Presentation.PerformanceWeb.Models
{
    public class ActionViewModel
    {
        public IList<ChartPointReportViewModel> ChartPointReportViewModelList { get; set; }

        public IList<SubActionsErrorViewModel> SubActionsErrorViewModelList { get; set; }

        public IDictionary<string, float> SubActionsTimeDictionary { get; set; }

        public string Title { get; set; }

        public int TotalRequests { get; set; }

        public int TotalUsers { get; set; }

        public bool AnyErrors { get; set; }

        public int TotalActionsWithErrors { get; set; }

        public float PercentageOfErrors { get; set; }

        public double ExecutionTime { get; set; }

        public float ActionsAverage { get; set; }

        public float ActionsAverageWithoutErrors { get; set; }

        public float RequestPerSecond { get; set; }

        public float Maximum { get; set; }

        public float Minimum { get; set; }

        public float MinimumWithoutErrors { get; set; }

        public string ImageUrl { get; set; }

        public string ImageName { get; set; }
    }
}