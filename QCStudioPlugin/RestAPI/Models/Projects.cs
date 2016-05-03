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
    /// Projects Packet for Returning a list of projects
    /// </summary>
    public class PacketProject : PacketBase
    {
        /// <summary>
        /// List of QuantConnect Projects
        /// </summary>
        [JsonProperty(PropertyName = "projects")]
        public List<Project> Projects;

        public PacketProject()
        { }
    }

    /// <summary>
    /// Response for creating a new project
    /// </summary>
    public class PacketCreateProject : PacketBase
    {
        [JsonProperty(PropertyName = "projectId")]
        public int ProjectId;

        public PacketCreateProject()
        { }
    }

    /// <summary>
    /// Single QuantConnect Project
    /// </summary>
    public class Project
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name;
        [JsonProperty(PropertyName = "modified")]
        public DateTime Modified { get; set; }

        public Project() 
        { }
    }


    /// <summary>
    /// Get a list of all the code files in this project:
    /// </summary>
    public class PacketProjectFiles : PacketBase
    {
        [JsonProperty(PropertyName = "files")]
        public List<QCFile> Files;

        public PacketProjectFiles()
        { }
    }

    /// <summary>
    /// QuantConnect File:
    /// </summary>
    public class QCFile
    {
        [JsonProperty(PropertyName = "name")]
        public string Name;
        [JsonProperty(PropertyName = "code")]
        public string Code;

        public QCFile()
        { }

        /// <summary>
        /// Ease of use constructor:
        /// </summary>
        public QCFile(string name, string contents)
        {
            this.Name = name;
            this.Code = contents;
        }
    }
}
