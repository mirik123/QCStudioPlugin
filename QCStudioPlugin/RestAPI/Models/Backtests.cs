/*
* QUANTCONNECT.COM - REST API
* C# Wrapper for Restful API for Managing QuantConnect Connection
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using QuantConnect.Util;
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

        public string UserID;
        public string AuthToken;

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
        public IDictionary<string, QCChart> Charts = new Dictionary<string, QCChart>();

        /// <summary>
        /// Order updates since the last backtest result packet was sent.
        /// </summary>
        public IDictionary<int, dynamic> Orders = new Dictionary<int, dynamic>();

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

    [JsonObject]
    public class QCChart
    {
        /// Name of the Chart:
        public string Name = "";

        /// Type of the Chart, Overlayed or Stacked.
        [Obsolete("ChartType is now obsolete. Please use Series indexes instead by setting index in the series constructor.")]
        public ChartType ChartType = ChartType.Overlay;

        /// List of Series Objects for this Chart:
        public Dictionary<string, Series> Series = new Dictionary<string, Series>();
    }

    /// <summary>
    /// Chart Series Object - Series data and properties for a chart:
    /// </summary>
    [JsonObject]
    public class Series
    {
        /// <summary>
        /// Name of the Series:
        /// </summary>
        public string Name = "";

        /// <summary>
        /// Axis for the chart series.
        /// </summary>
        public string Unit = "$";

        /// <summary>
        /// Index/position of the series on the chart.
        /// </summary>
        public int Index;

        /// <summary>
        ///  Values for the series plot:
        /// These values are assumed to be in ascending time order (first points earliest, last points latest)
        /// </summary>
        public List<ChartPoint> Values = new List<ChartPoint>();

        /// <summary>
        /// Chart type for the series:
        /// </summary>
        public SeriesType SeriesType = SeriesType.Line;

        /// <summary>
        /// Color the series 
        /// </summary>
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color Color = Color.Empty;

        /// <summary>
        /// Shape or symbol for the marker in a scatter plot
        /// </summary>
        public ScatterMarkerSymbol ScatterMarkerSymbol = ScatterMarkerSymbol.None;
    }

    [JsonObject]
    public struct ChartPoint
    {
        /// Time of this chart point: lower case for javascript encoding simplicty
        public long x;

        /// Value of this chart point:  lower case for javascript encoding simplicty
        public decimal y;
    }

    /// <summary>
    /// Available types of charts
    /// </summary>
    public enum SeriesType
    {
        /// Line Plot for Value Types
        Line,
        /// Scatter Plot for Chart Distinct Types
        Scatter,
        /// Charts
        Candle,
        /// Bar chart.
        Bar,
        /// Flag indicators
        Flag
    }

    /// <summary>
    /// Type of chart - should we draw the series as overlayed or stacked
    /// </summary>
    public enum ChartType
    {
        /// Overlayed stacked
        Overlay,
        /// Stacked series on top of each other.
        Stacked
    }

    /// <summary>
    /// Shape or symbol for the marker in a scatter plot
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ScatterMarkerSymbol
    {
        /// Circle symbol
        [EnumMember(Value = "none")]
        None,
        /// Circle symbol
        [EnumMember(Value = "circle")]
        Circle,
        /// Square symbol
        [EnumMember(Value = "square")]
        Square,
        /// Diamond symbol
        [EnumMember(Value = "diamond")]
        Diamond,
        /// Triangle symbol
        [EnumMember(Value = "triangle")]
        Triangle,
        /// Triangle-down symbol
        [EnumMember(Value = "triangle-down")]
        TriangleDown,
    }

        /// <summary>
    /// Backtest result packet: send backtest information to GUI for user consumption.
    /// </summary>
    public class BacktestResultPacket : Packet
    {
        public BacktestResultPacket() { }
        
        /// <summary>
        /// User Id placing this task
        /// </summary>
        [JsonProperty(PropertyName = "iUserID")]
        public int UserId = 0;

        /// <summary>
        /// Project Id of the this task.
        /// </summary>
        [JsonProperty(PropertyName = "iProjectID")]
        public int ProjectId = 0;

        /// <summary>
        /// User Session Id
        /// </summary>
        [JsonProperty(PropertyName = "sSessionID")]
        public string SessionId = "";

        /// <summary>
        /// BacktestId for this result packet
        /// </summary>
        [JsonProperty(PropertyName = "sBacktestID")]
        public string BacktestId = "";

        /// <summary>
        /// Compile Id for the algorithm which generated this result packet.
        /// </summary>
        [JsonProperty(PropertyName = "sCompileID")]
        public string CompileId = "";

        /// <summary>
        /// Start of the backtest period as defined in Initialize() method.
        /// </summary>
        [JsonProperty(PropertyName = "dtPeriodStart")]
        public DateTime PeriodStart = DateTime.Now;

        /// <summary>
        /// End of the backtest period as defined in the Initialize() method.
        /// </summary>
        [JsonProperty(PropertyName = "dtPeriodFinish")]
        public DateTime PeriodFinish = DateTime.Now;

        /// <summary>
        /// DateTime (EST) the user requested this backtest.
        /// </summary>
        [JsonProperty(PropertyName = "dtDateRequested")]
        public DateTime DateRequested = DateTime.Now;

        /// <summary>
        /// DateTime (EST) when the backtest was completed.
        /// </summary>
        [JsonProperty(PropertyName = "dtDateFinished")]
        public DateTime DateFinished = DateTime.Now;

        /// <summary>
        /// Progress of the backtest as a percentage from 0-1 based on the days lapsed from start-finish.
        /// </summary>
        [JsonProperty(PropertyName = "dProgress")]
        public decimal Progress = 0;

        /// <summary>
        /// Runmode for this backtest.
        /// </summary>
        /// <obsolete>The RunMode property has been made obsolete and all backtests will be run in series mode.</obsolete>
        [Obsolete("The RunMode property has been made obsolete and all backtests will be run in series mode.")]
        [JsonProperty(PropertyName = "eRunMode")]
        public RunMode RunMode = RunMode.Series;

        /// <summary>
        /// Name of this backtest.
        /// </summary>
        [JsonProperty(PropertyName = "sName")]
        public string Name = String.Empty;

        /// <summary>
        /// Result data object for this backtest
        /// </summary>
        [JsonProperty(PropertyName = "oResults")]
        public BacktestResult Results = new BacktestResult();

        /// <summary>
        /// Processing time of the algorithm (from moment the algorithm arrived on the algorithm node)
        /// </summary>
        [JsonProperty(PropertyName = "dProcessingTime")]
        public double ProcessingTime = 0;

        /// <summary>
        /// Estimated number of tradeable days in the backtest based on the start and end date or the backtest
        /// </summary>
        [JsonProperty(PropertyName = "iTradeableDates")]
        public int TradeableDates = 0;
    }

    /// <summary>
    /// Processing runmode of the backtest.
    /// </summary>
    /// <obsolete>The runmode enum is now obsolete and all tasks are run in series mode. This was done to ensure algorithms have memory of the day before.</obsolete>
    public enum RunMode
    {
        /// Automatically detect the runmode of the algorithm: series for minute data, parallel for second-tick
        Automatic,
        /// Series runmode for the algorithm
        Series,
        /// Parallel runmode for the algorithm
        Parallel
    }
}
