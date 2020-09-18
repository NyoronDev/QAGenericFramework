using System.Diagnostics;
using System.Text.Json.Serialization;

namespace CrossLayer.Models
{
    public class PerformanceSubAction
    {
        private readonly Stopwatch stopwatch;

        public PerformanceSubAction()
        {
            this.stopwatch = Stopwatch.StartNew();
        }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("totalTime")]
        public float TotalTime { get; set; }

        [JsonPropertyName("hasErrors")]
        public bool HasErrors { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        public void StopWatch()
        {
            this.stopwatch.Stop();
            this.TotalTime = this.stopwatch.ElapsedMilliseconds;
        }
    }
}