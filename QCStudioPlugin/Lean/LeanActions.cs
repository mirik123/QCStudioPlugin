/*
* QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals,
* QuantConnect Visual Studio Plugin
*/

using QuantConnect.QCStudioPlugin.Forms;
using Newtonsoft.Json;
using QuantConnect.QCPlugin;
using QuantConnect.RestAPI;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json.Linq;
using QuantConnect.QCStudioPlugin.Properties;
using QuantConnect.RestAPI.Models;
using System.ComponentModel;

namespace QuantConnect.QCStudioPlugin.Actions
{
    public static class LeanActions
    {
        private static LeanProxy lean;
        private static object composer;
        private static string _oldPluginsPath;

        public static void ResetLeanAndComposer() 
        {
            lean = null;
            composer = null;
        }

        public static void UpdateLeanAndComposer(string pluginsPath)
        {
            if (string.IsNullOrEmpty(pluginsPath))
            {
                QCPluginUtilities.OutputCommandString("Lean engine path is empty.", QCPluginUtilities.Severity.Error);               
                composer = null;
                lean = null;
                return;
            }

            if (lean != null && composer != null && pluginsPath == _oldPluginsPath) return;

            _oldPluginsPath = pluginsPath;
            QCPluginUtilities.OutputCommandString("Started updating Lean engine and composer...", QCPluginUtilities.Severity.Info);

            AppDomain.CurrentDomain.SetData("APPBASE", pluginsPath);
            Environment.CurrentDirectory = pluginsPath; 

            QCPluginUtilities.OutputCommandString("Using ApplicationDomain: " + AppDomain.CurrentDomain.BaseDirectory, QCPluginUtilities.Severity.Info);

            lean = new LeanProxy();
            if (!string.IsNullOrEmpty(pluginsPath))
            {
                lean.LoadLibraries(pluginsPath);

                var objVal = QCPluginUtilities.dte.Properties["QuantConnect Client", "General"].Item("PluginDirectory").Value;
                lean.SetConfiguration("plugin-directory", (string)objVal);
            }
            composer = lean.CreateComposer();

            QCPluginUtilities.OutputCommandString("Finished updating Lean engine and composer...", QCPluginUtilities.Severity.Info);
        }

        public static Task<string> RunLocalBacktest(string algorithmPath, string fileName, string pluginsPath, string dataPath)
        {
            var statistics = new Dictionary<string, string>();
            var task = new TaskCompletionSource<string>();

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var timer = DateTime.Now;

            //lean.LoadLibraries(pluginsPath);
            UpdateLeanAndComposer(pluginsPath);
            if (lean == null || composer == null)
            {
                QCPluginUtilities.OutputCommandString("Failed to generate Lean proxy", QCPluginUtilities.Severity.Warning);
                task.SetException(new Exception("Failed to generate proxy"));
                return task.Task;
            }

            try
            {
                //Config.Set("environment", "");
                lean.SetConfiguration("environment", "");   //"backtesting-desktop"
                lean.SetConfiguration("data-folder", dataPath);
                lean.SetConfiguration("data-directory", dataPath);                
                lean.SetConfiguration("algorithm-location", algorithmPath);
                lean.SetConfiguration("algorithm-type-name", fileName);
                //lean.SetConfiguration("api-access-token", "");
                //lean.SetConfiguration("job-user-id", "0");

                var props = TypeDescriptor.GetProperties(typeof(OptionPageGrid)).Cast<PropertyDescriptor>().Where(x => x.Category == "Configuration");
                foreach (var prop in props)
                {
                    var objVal = QCPluginUtilities.dte.Properties["QuantConnect Client", "General"].Item(prop.Name).Value;
                    var strVal = objVal is bool ? ((bool)objVal).ToString().ToLower() : objVal.ToString();
                    lean.SetConfiguration(prop.DisplayName, strVal);
                }

                //Log.LogHandler = Composer.Instance.GetExportedValueByTypeName<ILogHandler>("QuantConnect.Logging.QueueLogHandler");
                lean.SetLogHandler(composer, (packet) =>
                {
                    var json = JObject.FromObject(packet);
                    var message = (json["sMessage"] ?? json["Message"] ?? JToken.FromObject("")).Value<string>();
                    var hstack = (json["sStackTrace"] ?? json["StackTrace"] ?? JToken.FromObject("")).Value<string>();
                    var msgtype = string.IsNullOrEmpty(hstack) ? QCPluginUtilities.Severity.Info : QCPluginUtilities.Severity.Error;

                    QCPluginUtilities.OutputCommandString(message + ", " + hstack, msgtype);
                });

                //var systemHandlers = LeanEngineSystemHandlers.FromConfiguration(Composer.Instance);
                var systemHandlers = lean.CreateLeanEngineSystemHandlers(composer);

                //var algorithmHandlers = LeanEngineAlgorithmHandlers.FromConfiguration(Composer.Instance);
                var algorithmHandlers = lean.CreateLeanEngineAlgorithmHandlers(composer);

                QCPluginUtilities.OutputCommandString("Running " + fileName + "...", QCPluginUtilities.Severity.Info);

                //var _messaging = systemHandlers.Notify;
                lean.AddMessagingEvents(systemHandlers, algorithmHandlers, (packet) =>
                {
                    var json = JObject.FromObject(packet);
                    var message = (json["sMessage"] ?? json["Message"] ?? JToken.FromObject("")).Value<string>();
                    var hstack = (json["sStackTrace"] ?? json["StackTrace"] ?? JToken.FromObject("")).Value<string>();
                    var msgtype = string.IsNullOrEmpty(hstack) ? QCPluginUtilities.Severity.Info : QCPluginUtilities.Severity.Error;

                    QCPluginUtilities.OutputCommandString(message + ", " + hstack, msgtype);
                }, (packet) =>
                {
                    var json = JObject.FromObject(packet);
                    if (json["oResults"] == null && json["Results"] == null)
                    {
                        task.SetException(new Exception("No Backend Result!"));
                        return;
                    }

                    var progress = (json["dProgress"] ?? json["Progress"] ?? JToken.FromObject("0")).Value<string>();
                    QCPluginUtilities.OutputCommandString("Backtest progress: " + progress, QCPluginUtilities.Severity.Info);

                    if (progress == "1")
                    {
                        //result.Progress = "100%";
                        var strjson = json.ToString(Formatting.None);
                        if (task.TrySetResult(strjson))
                        {                            
                            tokenSource.Cancel();
                            (systemHandlers as IDisposable).Dispose();
                            (algorithmHandlers as IDisposable).Dispose();
                        }                        
                    }
                });

                string _algorithmPath;
                var job = lean.GetNextJob(systemHandlers, out _algorithmPath);

                //var engine = new Lean.Engine.Engine(systemHandlers, algorithmHandlers, false);
                var engine = lean.CreateEngine(systemHandlers, algorithmHandlers, false);

                Task.Run(() =>
                {
                    try
                    {
                        //engine.Run(job, _algorithmPath); 
                        lean.RunEngine(engine, job, _algorithmPath);

                        QCPluginUtilities.OutputCommandString("Finished runnig backtest.", QCPluginUtilities.Severity.Info);
                    }
                    catch (Exception ex)
                    {
                        QCPluginUtilities.OutputCommandString(string.Format("{0} {1}", ex.ToString(), ex.StackTrace), QCPluginUtilities.Severity.Error);
                        task.SetException(ex);
                    }
                }, token);
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString(string.Format("{0} {1}", ex.ToString(), ex.StackTrace), QCPluginUtilities.Severity.Error);
                task.SetException(ex);
            }

            return task.Task;
        }

        public static JObject SerializeBacktestPacket(object packet)
        {
            var json = JObject.FromObject(packet, new JsonSerializer { 
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                TypeNameHandling = TypeNameHandling.All,
                //NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CustomResolver()                
            });

            json.Property("Results").Replace(new JProperty("results", json["Results"]));
            json.Property("PeriodStart").Replace(new JProperty("dtPeriodStart", json["PeriodStart"]));
            json.Property("PeriodFinish").Replace(new JProperty("dtPeriodFinish", json["PeriodFinish"]));
            json.Property("Progress").Replace(new JProperty("progress", json["Progress"]));
            json.Property("ProcessingTime").Replace(new JProperty("processingTime", json["ProcessingTime"]));
            

            /*var strjson = JsonConvert.SerializeObject(packet, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,                
                //NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                ContractResolver = new CustomResolver()
            });
            json = JObject.Parse(strjson);
            */

            return json;
        }

        /*public static Task<BacktestResultPacket> RunLocalBacktest(string algorithmPath, string fileName)
        {
            var statistics = new Dictionary<string, string>();
            var task = new TaskCompletionSource<BacktestResultPacket>();

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            try
            {
                Composer.Instance.Reset();

                // set the configuration up
                Config.Set("algorithm-type-name", fileName);
                Config.Set("live-mode", "false");
                Config.Set("environment", "");
                Config.Set("messaging-handler", "QuantConnect.Messaging.EventMessagingHandler");
                Config.Set("job-queue-handler", "QuantConnect.Queues.JobQueue");
                Config.Set("api-handler", "QuantConnect.Api.Api");
                Config.Set("result-handler", "QuantConnect.Lean.Engine.Results.BacktestingResultHandler");
                Config.Set("algorithm-language", "CSharp");
                Config.Set("algorithm-location", algorithmPath);

                Task enginetask = null;
                var algorithmHandlers = LeanEngineAlgorithmHandlers.FromConfiguration(Composer.Instance);
                var systemHandlers = LeanEngineSystemHandlers.FromConfiguration(Composer.Instance);
                
                QCPluginUtilities.OutputCommandString("{0}: Running " + fileName + "...", QCPluginUtilities.Severity.Info);

                var _messaging = (EventMessagingHandler)systemHandlers.Notify;
                _messaging.DebugEvent += (DebugPacket packet) => {
                    QCPluginUtilities.OutputCommandString(packet.Message, QCPluginUtilities.Severity.Info);
                };
                _messaging.LogEvent += (LogPacket packet) => {
                    QCPluginUtilities.OutputCommandString(packet.Message, QCPluginUtilities.Severity.Info);
                };
                _messaging.RuntimeErrorEvent += (RuntimeErrorPacket packet) => {
                    var hstack = !string.IsNullOrEmpty(packet.StackTrace) ? packet.StackTrace : string.Empty;
                    QCPluginUtilities.OutputCommandString(packet.Message + hstack, QCPluginUtilities.Severity.Error);
                };
                _messaging.HandledErrorEvent += (HandledErrorPacket packet) => {
                    var hstack = !string.IsNullOrEmpty(packet.StackTrace) ? packet.StackTrace : string.Empty;
                    QCPluginUtilities.OutputCommandString(packet.Message + hstack, QCPluginUtilities.Severity.Error);
                };
                _messaging.BacktestResultEvent += (BacktestResultPacket packet) => {
                    systemHandlers.Dispose();
                    algorithmHandlers.Dispose();
                    tokenSource.Cancel();
                    tokenSource.Dispose();

                    task.SetResult(packet);
                };

                string _algorithmPath;
                var job = systemHandlers.JobQueue.NextJob(out _algorithmPath);
                var engine = new Lean.Engine.Engine(systemHandlers, algorithmHandlers, false);
                enginetask = Task.Factory.StartNew(() => { engine.Run(job, algorithmPath); }, token);                            
            }
            catch (Exception ex)
            {
                QCPluginUtilities.OutputCommandString(string.Format("{0} {1}", ex.ToString(), ex.StackTrace), QCPluginUtilities.Severity.Error);
                task.SetException(ex);
            }

            return task.Task;
        }*/     
    }
}
