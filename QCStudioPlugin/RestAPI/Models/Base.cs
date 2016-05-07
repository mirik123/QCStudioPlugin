/*
* QUANTCONNECT.COM - REST API
* C# Wrapper for Restful API for Managing QuantConnect Connection
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
/**********************************************************
* USING NAMESPACES
**********************************************************/
using System.Collections.Generic;

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
}
