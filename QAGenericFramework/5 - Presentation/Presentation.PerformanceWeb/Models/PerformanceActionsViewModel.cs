using CrossLayer.Models.Performance;
using Presentation.PerformanceWeb.Models.ChartViewModels;
using System;
using System.Collections.Generic;

namespace Presentation.PerformanceWeb.Models
{
    public class PerformanceActionsViewModel
    {
        public IEnumerable<ActionViewModel> ActionViewModelList { get; set; }

        public IList<ChartPointReportViewModel> ChartPointTimeViewModelList { get; set; }

        public PerformanceActionType PerformanceActionType { get; set; }

        public DateTime StartedDate { get; set; }

        public DateTime FinishedDate { get; set; }
    }
}