using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CrossLayer.Models
{
    public class PerformanceAction
    {
        public PerformanceAction()
        {
            this.PerformanceSubActions = new List<PerformanceSubAction>();
            this.ActionStarted = DateTime.UtcNow;
        }

        [JsonPropertyName("performanceSubActions")]
        public IList<PerformanceSubAction> PerformanceSubActions { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("totalRequests")]
        public int TotalRequests { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("actionStarted")]
        public DateTime ActionStarted { get; set; }

        [JsonPropertyName("actionEnded")]
        public DateTime ActionEnded { get; set; }

        [JsonPropertyName("subActionsHaveAnyErrors")]
        public bool SubActionsHaveAnyErrors
        {
            get
            {
                if (this.PerformanceSubActions == null || this.PerformanceSubActions.Count() == 0)
                {
                    return false;
                }

                return this.PerformanceSubActions.Any(m => m.HasErrors == true);
            }
        }

        [JsonPropertyName("subActionsTotalTime")]
        public float SubActionsTotalTime
        {
            get
            {
                if (this.PerformanceSubActions == null || this.PerformanceSubActions.Count() == 0)
                {
                    return 0;
                }

                return this.PerformanceSubActions.Sum(m => m.TotalTime);
            }
        }
    }
}