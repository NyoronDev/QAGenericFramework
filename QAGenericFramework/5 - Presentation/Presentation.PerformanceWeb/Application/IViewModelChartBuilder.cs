using Presentation.PerformanceWeb.Models;
using System.Collections.Generic;

namespace Presentation.PerformanceWeb.Application
{
    public interface IViewModelChartBuilder
    {
        IEnumerable<PerformanceActionsViewModel> CreateKycChartViewModel();
    }
}