using CrossLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.Json;

namespace DataFactory.Performance.ReportGeneration
{
    public class ReportDataGeneration : IReportDataGeneration
    {
        private readonly IConfigurationRoot configurationRoot;

        // Paths
        private string testOutputFolder => configurationRoot.GetSection("AppConfiguration")["TestOutputFolder"];

        private string testPerformanceFolder => configurationRoot.GetSection("AppConfiguration")["PerformanceFileFolder"];
        private string testPerformancePath => $"{testOutputFolder}{Path.DirectorySeparatorChar}{this.testPerformanceFolder}";

        public ReportDataGeneration(IConfigurationRoot configurationRoot)
        {
            this.configurationRoot = configurationRoot ?? throw new ArgumentNullException(nameof(configurationRoot));
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