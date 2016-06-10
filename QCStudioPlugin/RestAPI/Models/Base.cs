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

    /// <summary>
    /// Base class for packet messaging system
    /// </summary>
    public class Packet
    {
        /// <summary>
        /// Packet type defined by a string enum
        /// </summary>
        [JsonProperty(PropertyName = "eType")]
        public PacketType Type = PacketType.None;

        /// <summary>
        /// User unique specific channel endpoint to send the packets
        /// </summary>
        [JsonProperty(PropertyName = "sChannel")]
        public string Channel = "";

        /// <summary>
        /// Initialize the base class and setup the packet type.
        /// </summary>
        /// <param name="type">PacketType for the class.</param>
        public Packet(PacketType type)
        {
            Channel = "";
            Type = type;
        }

        public Packet() { }
    }

    /// <summary>
    /// Classifications of internal packet system
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PacketType
    {
        /// Default, unset:
        None,

        /// Base type for backtest and live work
        AlgorithmNode,

        /// Autocomplete Work Packet
        AutocompleteWork,

        /// Result of the Autocomplete Job:
        AutocompleteResult,

        /// Controller->Backtest Node Packet:
        BacktestNode,

        /// Packet out of backtest node:
        BacktestResult,

        /// API-> Controller Work Packet:
        BacktestWork,

        /// Controller -> Live Node Packet:
        LiveNode,

        /// Live Node -> User Packet:
        LiveResult,

        /// API -> Controller Packet:
        LiveWork,

        /// Node -> User Algo Security Types
        SecurityTypes,

        /// Controller -> User Error in Backtest Settings:
        BacktestError,

        /// Nodes -> User Algorithm Status Packet:
        AlgorithmStatus,

        /// API -> Compiler Work Packet:
        BuildWork,

        /// Compiler -> User Build Success
        BuildSuccess,

        /// Compiler -> User, Compile Error
        BuildError,

        /// Node -> User Algorithm Runtime Error
        RuntimeError,

        /// Error is an internal handled error packet inside users algorithm
        HandledError,

        /// Nodes -> User Log Message
        Log,

        /// Nodes -> User Debug Message
        Debug,

        /// Nodes -> User, Order Update Event
        OrderEvent,

        /// Boolean true/false success
        Success,

        /// History live job packets
        History,

        /// Result from a command
        CommandResult,

        /// Hook from git hub
        GitHubHook
    }
}
