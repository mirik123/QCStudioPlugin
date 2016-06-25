/*
* QUANTCONNECT.COM - REST API
* C# Wrapper for Restful API for Managing QuantConnect Connection
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

/**********************************************************
* USING NAMESPACES
**********************************************************/
using System;
using System.Collections.Generic;

namespace QuantConnect.RestAPI.Models
{
    /// <summary>
    /// Types of log messages from the compiler:
    /// </summary>
    public enum CompilerLogType
    { 
        /// Build success notification
        BuildSuccess,

        /// Build error notification
        BuildError,

        /// Debug message
        Debug
    }

    /// <summary>
    /// Compiler Log Entry
    /// </summary>
    public class CompileLog
    {
        [JsonProperty(PropertyName = "type")]
        public CompilerLogType Type;

        [JsonProperty(PropertyName = "entry")]
        public string Entry;

        [JsonProperty(PropertyName = "time")]
        public DateTime Time;

        public CompileLog()
        { }
    }

    /// <summary>
    /// Compiler Packet:
    /// </summary>
    public class PacketCompile : PacketBase
    {
        [JsonProperty(PropertyName = "compileId")]
        public string CompileId;

        /// <summary>
        /// List of compiler messages
        /// </summary>
        [JsonProperty(PropertyName = "log")]
        public List<CompileLog> Logs;

        public PacketCompile()
        { }
    }
}
