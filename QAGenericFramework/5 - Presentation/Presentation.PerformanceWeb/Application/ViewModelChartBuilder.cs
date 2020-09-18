using CrossLayer.Models;
using Presentation.PerformanceWeb.Models;
using Presentation.PerformanceWeb.Models.ChartViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Presentation.PerformanceWeb.Application
{
    public class ViewModelChartBuilder : IViewModelChartBuilder
    {
        private const string ResourcesFolder = "Resources";

        public IEnumerable<PerformanceActionsViewModel> CreateKycChartViewModel()
        {
            var kycPerformanceViewModelList = new List<PerformanceActionsViewModel>();

            var listPerformanceActionList = GetPerformanceActionList();

            // For each file
            foreach (var performanceActionListFile in listPerformanceActionList)
            {
                var kycPerformanceActionViewModelList = new List<ActionViewModel>();

                // For each amount of users
                var performanceActionsList = performanceActionListFile.PerformanceActions.GroupBy(m => m.Id).Select(n => n.ToList());

                foreach (var performanceActionList in performanceActionsList)
                {
                    var chartPointReportViewModelList = new List<ChartPointReportViewModel>();
                    var subActionsErrorViewModelList = new List<SubActionsErrorViewModel>();

                    // Sub actions dictionary with type and time
                    // Sub actions type 1 --- Average
                    // Sub actions type 2 --- Average
                    var performanceSubActionDictionary = performanceActionList.SelectMany(p => p.PerformanceSubActions)
                                                                              .GroupBy(m => m.Type)
                                                                              .ToDictionary(m => m.Key, m => m.Average(p => p.TotalTime) / 1000);

                    var totalErrors = 0;
                    foreach (var performanceActions in performanceActionList)
                    {
                        // Sub action errors
                        if (performanceActions.SubActionsHaveAnyErrors)
                        {
                            foreach (var subAction in performanceActions.PerformanceSubActions)
                            {
                                if (subAction.HasErrors)
                                {
                                    var subActionError = new SubActionsErrorViewModel { SubAction = subAction.Type, ErrorMessage = subAction.Error };
                                    subActionsErrorViewModelList.Add(subActionError);
                                    totalErrors++;

                                    // We only need the first error of all sub actions.
                                    break;
                                }
                            }
                        }

                        // Char points
                        var chartPoint = new ChartPointReportViewModel { Quantity = performanceActions.SubActionsTotalTime / 1000, DimensionOne = "1" }; // Seconds
                        chartPointReportViewModelList.Add(chartPoint);
                    }

                    var kycPerformanceViewModel = new ActionViewModel
                    {
                        ChartPointReportViewModelList = chartPointReportViewModelList,
                        SubActionsErrorViewModelList = subActionsErrorViewModelList,
                        SubActionsTimeDictionary = performanceSubActionDictionary,
                        Title = $"{performanceActionListFile.Title} - Progressive {performanceActionList.First().TotalRequests}",
                        TotalRequests = performanceActionListFile.TotalActions,
                        TotalUsers = performanceActionList.First().TotalRequests,
                        AnyErrors = performanceActionList.Any(m => m.SubActionsHaveAnyErrors),
                        TotalActionsWithErrors = totalErrors,
                        ExecutionTime = (performanceActionList.Last().ActionEnded - performanceActionList.First().ActionStarted).TotalMinutes, // Minutes
                        ActionsAverage = performanceActionList.Average(m => m.SubActionsTotalTime) / 1000, // Seconds
                        ActionsAverageWithoutErrors = performanceActionList.Where(m => m.SubActionsHaveAnyErrors == false).Average(m => m.SubActionsTotalTime) / 1000, // Seconds
                        RequestPerSecond = performanceActionListFile.TotalActions / (float)(performanceActionList.Last().ActionEnded - performanceActionList.First().ActionStarted).TotalSeconds, // Seconds
                        Maximum = performanceActionList.Max(m => m.SubActionsTotalTime) / 1000, // Seconds
                        Minimum = performanceActionList.Min(m => m.SubActionsTotalTime) / 1000, // Seconds
                        MinimumWithoutErrors = performanceActionList.Where(m => m.SubActionsHaveAnyErrors == false).Min(m => m.SubActionsTotalTime) / 1000, // Seconds
                        PercentageOfErrors = ((float)totalErrors / (float)performanceActionListFile.TotalActions) * 100
                    };

                    kycPerformanceActionViewModelList.Add(kycPerformanceViewModel);
                }

                // Create time chart
                var chartPointTimeReportViewModelList = new List<ChartPointReportViewModel>();
                var totalTimePerformed = Convert.ToInt32((performanceActionListFile.FinishedDate - performanceActionListFile.StartedDate).TotalSeconds);
                var totalColumns = Convert.ToInt32(totalTimePerformed / 30); // One column for each 30 seconds

                var startTime = performanceActionListFile.StartedDate;
                var columnTime = 0;
                for (int i = 1; i <= (totalColumns + 1); i++)
                {
                    columnTime += 30;

                    var amountOfRequest = performanceActionListFile.PerformanceActions.Count(m => m.ActionEnded >= startTime.AddSeconds(30 * (i - 1)) && m.ActionEnded <= startTime.AddSeconds(30 * i));
                    var amountOfErrors = performanceActionListFile.PerformanceActions.Count(m => m.ActionEnded >= startTime.AddSeconds(30 * (i - 1)) && m.ActionEnded <= startTime.AddSeconds(30 * i) && m.SubActionsHaveAnyErrors == true);

                    var columnTimeMinutes = Convert.ToDecimal(columnTime) / 60;
                    var chartPoint = new ChartPointReportViewModel { Quantity = amountOfRequest, ErrorQuantity = amountOfErrors, DimensionOne = columnTimeMinutes.ToString() };
                    chartPointTimeReportViewModelList.Add(chartPoint);
                }

                var performanceViewModel = new PerformanceActionsViewModel
                {
                    ActionViewModelList = kycPerformanceActionViewModelList,
                    ChartPointTimeViewModelList = chartPointTimeReportViewModelList,
                    PerformanceActionType = performanceActionListFile.PerformanceActionType,
                    StartedDate = performanceActionListFile.StartedDate,
                    FinishedDate = performanceActionListFile.FinishedDate
                };
                kycPerformanceViewModelList.Add(performanceViewModel);
            }

            return kycPerformanceViewModelList;
        }

        private IEnumerable<PerformanceActionList> GetPerformanceActionList()
        {
            var directoryInfo = new DirectoryInfo(ResourcesFolder);
            var performanceResultFiles = directoryInfo.GetFiles("*.json");

            var listPerformanceActionList = new List<PerformanceActionList>();
            foreach (var performanceResultFile in performanceResultFiles)
            {
                var fileLines = File.ReadAllLines(performanceResultFile.FullName);
                var performanceActionList = JsonSerializer.Deserialize<PerformanceActionList>(string.Join(Environment.NewLine, fileLines));

                listPerformanceActionList.Add(performanceActionList);
            }

            return listPerformanceActionList;
        }
    }
}