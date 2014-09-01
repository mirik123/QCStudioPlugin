/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

/**********************************************************
* USING NAMESPACES
**********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using QuantConnect.RestAPI;
using QuantConnect.RestAPI.Models;

namespace QuantConnect.QCPlugin
{
    class Async
    {
        public static ConcurrentQueue<APIJob> Queue = new ConcurrentQueue<APIJob>();
        public static void Consumer()
        {
            while (true)
            {
                if (Queue.Count > 0)
                {
                    APIJob job;
                    if (Queue.TryDequeue(out job))
                    {
                        switch (job.Command)
                        {
                            case APICommand.Authenticate:
                                bool loggedIn = false;
                                if (job.Parameters.Length == 2)
                                {
                                    loggedIn = QuantConnectPlugin.API.Authenticate((string)job.Parameters[0], (string)job.Parameters[1]);
                                }
                                job.Callback(loggedIn, new List<string>());
                                break;

                            case APICommand.ProjectList:
                                var packet = QuantConnectPlugin.API.ProjectList();
                                List<Project> projectList = new List<Project>();
                                if (packet.Projects != null) projectList = packet.Projects;
                                job.Callback(projectList, packet.Errors);
                                break;

                            case APICommand.OpenProject:
                                var files = new PacketProjectFiles();
                                List<File> projectFiles = new List<File>();
                                if (job.Parameters.Length == 1)
                                {
                                    files = QuantConnectPlugin.API.ProjectFiles((int)job.Parameters[0]);
                                }
                                if (files.Files != null) projectFiles = files.Files;
                                job.Callback(projectFiles, files.Errors);
                                break;

                            case APICommand.CheckQCAlgoVersion:
                                bool latestversion = QuantConnectPlugin.API.CheckQCAlgorithmVersion();
                                job.Callback(latestversion, new List<string>());
                                break;

                            case APICommand.Compile:
                                QuantConnectPlugin.SaveToQC(false);
                                var compile = new PacketCompile();
                                if (job.Parameters.Length == 1)
                                {
                                    compile = QuantConnectPlugin.API.Compile((int)job.Parameters[0]);
                                }
                                job.Callback(compile, compile.Errors);
                                break;
                            case APICommand.UpdateTemplate:
                                job.Callback(true, new List<string>());
                                break;
                            case APICommand.BacktestResults:
                                var backtestResult = new PacketBacktestResult();
                                if (job.Parameters.Length == 1)
                                {
                                    backtestResult = QuantConnectPlugin.API.BacktestResults((string)job.Parameters[0]);
                                }
                                job.Callback(backtestResult, backtestResult.Errors);
                                break;
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }

        public static void Add(APIJob job)
        {
            Queue.Enqueue(job);
        }
    }

    class APIJob
    {
        public APIJob(APICommand command, Action<object, List<string>> callback, params object[] arguments)
        {
            this.Command = command;
            this.Callback = callback;
            this.Parameters = arguments;
        }

        public APIJob(APICommand command, Action<object, List<string>> action)
        {
            // TODO: Complete member initialization
            this.Command = command;
            this.Callback = action;
        }
        public APICommand Command;
        public object[] Parameters;
        public Action<object, List<string>> Callback;
    }

    public enum APICommand
    {
        Authenticate, 
        ProjectList, 
        OpenProject,
        CheckQCAlgoVersion,
        Compile,
        UpdateTemplate,
        BacktestResults
    }
}
