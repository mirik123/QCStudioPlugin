/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace QuantConnect
{
    /// <summary>
    /// Single Parent Chart Object for Custom Charting
    /// </summary>
    [JsonObject]
    public class Chart
    {
        /// Name of the Chart:
        public string Name = "";

        /// Type of the Chart, Overlayed or Stacked.
        [Obsolete("ChartType is now obsolete. Please use Series indexes instead by setting index in the series constructor.")]
        public ChartType ChartType = ChartType.Overlay;

        /// List of Series Objects for this Chart:
        public Dictionary<string, Series> Series = new Dictionary<string, Series>();

        /// <summary>
        /// Default constructor for chart:
        /// </summary>
        public Chart() { }   
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
        public Color Color = Color.Empty;

        /// <summary>
        /// Shape or symbol for the marker in a scatter plot
        /// </summary>
        public ScatterMarkerSymbol ScatterMarkerSymbol = ScatterMarkerSymbol.None;

        /// <summary>
        /// Default constructor for chart series
        /// </summary>
        public Series() { }      
    }


    /// <summary>
    /// Single Chart Point Value Type for QCAlgorithm.Plot();
    /// </summary>
    [JsonObject]
    public class ChartPoint
    {
        /// Time of this chart point: lower case for javascript encoding simplicty
        public long x;

        /// Value of this chart point:  lower case for javascript encoding simplicty
        public decimal y;

        public ChartPoint() { }
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
