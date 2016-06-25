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

namespace QCInterfaces
{
    /// <summary>
    /// Result Class for Backtest:
    /// </summary>
    public class BacktestResultPacket : PacketBase
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

        public BacktestResultPacket()
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

        /// <summary>
        /// Rolling window detailed statistics.
        /// </summary>
        public Dictionary<string, object> RollingWindow = new Dictionary<string, object>();

        /// <summary>
        /// Rolling window detailed statistics.
        /// </summary>
        public object TotalPerformance = null;
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
}
