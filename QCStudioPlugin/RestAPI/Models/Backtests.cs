/*
* QUANTCONNECT.COM - REST API
* C# Wrapper for Restful API for Managing QuantConnect Connection
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
/**********************************************************
* USING NAMESPACES
**********************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace QuantConnect.RestAPI.Models
{
    /// <summary>
    /// Compiler Packet:
    /// </summary>
    public class PacketBacktest : PacketBase
    {
        [JsonProperty(PropertyName = "backtestId")]
        public string BacktestId;

        public PacketBacktest()
        { }
    }


    /// <summary>
    /// Result Class for Backtest:
    /// </summary>
    public class PacketBacktestResult : PacketBase
    {
        [JsonProperty(PropertyName = "progress")]
        public string Progress = "";

        [JsonProperty(PropertyName = "processingTime")]
        public double ProcessingTime = 0;

        [JsonProperty(PropertyName = "results")]
        public BacktestResult Results;

        // <summary>
        /// Start of the backtest period as defined in Initialize() method.
        /// </summary>
        [JsonProperty(PropertyName = "dtPeriodStart")]
        public DateTime PeriodStart = DateTime.Now;

        /// <summary>
        /// End of the backtest period as defined in the Initialize() method.
        /// </summary>
        [JsonProperty(PropertyName = "dtPeriodFinish")]
        public DateTime PeriodFinish = DateTime.Now;

        public PacketBacktestResult()
        { }
    }

    public class BacktestResult
    {
        /// <summary>
        /// Chart updates in this backtest since the last backtest result packet was sent.
        /// </summary>
        public IDictionary<string, object> Charts = new Dictionary<string, object>();

        /// <summary>
        /// Order updates since the last backtest result packet was sent.
        /// </summary>
        public IDictionary<int, object> Orders = new Dictionary<int, object>();

        /// <summary>
        /// Profit and loss results from closed trades.
        /// </summary>
        public IDictionary<DateTime, decimal> ProfitLoss = new Dictionary<DateTime, decimal>();

        /// <summary>
        /// Statistics information for the backtest.
        /// </summary>
        /// <remarks>The statistics are only generated on the last result packet of the backtest.</remarks>
        public IDictionary<string, string> Statistics = new Dictionary<string, string>();

        /// <summary>
        /// The runtime / dynamic statistics generated while a backtest is running.
        /// </summary>
        public IDictionary<string, string> RuntimeStatistics = new Dictionary<string, string>();

        /// <summary>
        /// Rolling window detailed statistics.
        /// </summary>
        public Dictionary<string, object> RollingWindow = new Dictionary<string, object>();

        /// <summary>
        /// Rolling window detailed statistics.
        /// </summary>
        public object TotalPerformance = null;
    }


    /// <summary>
    /// List of Backtest Results for this project:
    /// </summary>
    public class PacketBacktestList : PacketBase
    {
        [JsonProperty(PropertyName = "results")]
        public List<BacktestSummary> Summary;
    }

    /// <summary>
    /// Summary of a Backtest (no result data):
    /// </summary>
    public class BacktestSummary
    {
        [JsonProperty(PropertyName = "backtestId")]
        public string BacktestId { get; set; }

        [JsonIgnore]
        public string BacktestName { get { return Name; } set { Name = value; } }

        [JsonProperty(PropertyName = "name")]
        public string Name;

        [JsonProperty(PropertyName = "progress")]
        public double Progress { get; set; }

        [JsonProperty(PropertyName = "processingTime")]
        public double ProcessingTime { get; set; }

        [JsonProperty(PropertyName = "requested")]
        public DateTime Requested { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty(PropertyName = "sparkline")]
        public string SparkLine { get; set; }
    }
}
