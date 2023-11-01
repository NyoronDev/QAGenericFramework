using CrossLayer.Configuration;
using CrossLayer.Models;
using System.IO;
using System.Text.Json;

namespace DataFactory.Performance.ReportGeneration
{
    public class ReportDataGeneration : IReportDataGeneration
    {
        // Paths
        private readonly string testPerformancePath;

        public ReportDataGeneration(AppSettings appSettings)
        {
            var testOutputFolder = appSettings.AppConfiguration.TestOutputFolder;
            var testPerformanceFolder = appSettings.AppConfiguration.PerformanceFileFolder;

            testPerformancePath = $"{testOutputFolder}{Path.DirectorySeparatorChar}{testPerformanceFolder}";
        }

        public void GeneratePerformanceJsonReport(PerformanceActionList performanceActionList)
        {
            using (var file = File.CreateText($"{this.testPerformancePath}{Path.DirectorySeparatorChar}{performanceActionList.Filename}.json"))
            {
                var stringJson = JsonSerializer.Serialize(performanceActionList);
                file.Write(stringJson);
            }
        }
    }
}