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
    /// Common base packet for interfacing with the QC API: All packets return with Success and Errors Array:
    /// </summary>
    public class PacketBase
    {
        /// <summary>
        /// Successful Request
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success;

        /// <summary>
        /// Errors string array
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public List<string> Errors;

        /// <summary>
        /// Your IP
        /// </summary>
        [JsonProperty(PropertyName = "ip")]
        public string Ip;

        public string rawData;

        public PacketBase() { }
    }
    
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
