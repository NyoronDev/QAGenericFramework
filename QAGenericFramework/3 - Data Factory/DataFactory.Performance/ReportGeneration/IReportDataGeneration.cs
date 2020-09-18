using CrossLayer.Models;

namespace DataFactory.Performance.ReportGeneration
{
    public interface IReportDataGeneration
    {
        void GeneratePerformanceJsonReport(PerformanceActionList performanceActionList);
    }
}