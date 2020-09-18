using CrossLayer.Models.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CrossLayer.Models
{
    public class PerformanceActionList
    {
        public PerformanceActionList()
        {
            this.PerformanceActions = new List<PerformanceAction>();
        }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("type")]
        public PerformanceActionType PerformanceActionType { get; set; }

        [JsonPropertyName("filename")]
        public string Filename { get; set; }

        [JsonPropertyName("totalActions")]
        public int TotalActions { get; set; }

        [JsonPropertyName("average")]
        public float Average { get => this.PerformanceActions.Average(m => m.SubActionsTotalTime); }

        [JsonPropertyName("actionsHaveAnyErrors")]
        public bool ActionsHaveAnyErrors { get => this.PerformanceActions.Any(m => m.SubActionsHaveAnyErrors == true); }

        [JsonPropertyName("totalActionsWithErrors")]
        public int TotalActionsWithErrors { get => this.PerformanceActions.Count(m => m.SubActionsHaveAnyErrors == true); }

        [JsonPropertyName("actionsTotalTime")]
        public float ActionsTotalTime { get => this.PerformanceActions.Sum(m => m.SubActionsTotalTime); }

        [JsonPropertyName("executionTotalTime")]
        public float ExecutionTotalTime { get; set; }

        [JsonPropertyName("startedDate")]
        public DateTime StartedDate { get; set; }

        [JsonPropertyName("finishedDate")]
        public DateTime FinishedDate { get; set; }

        [JsonPropertyName("performanceActions")]
        public IList<PerformanceAction> PerformanceActions { get; set; }
    }
}