/*
* QUANTCONNECT.COM - REST API
* C# Wrapper for Restful API for Managing QuantConnect Connection
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public PacketBase() { }
    }

    public abstract class ChartControl: UserControl
    {
        public virtual void Initialize(params string[] args) { }
        public virtual void Run(PacketBacktestResult _results) { }
    }
}
