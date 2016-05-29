

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuantConnect.QCStudioPlugin.Lean.interfaces
{
    public class LeanRefactoring
    {
        private Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

        public LeanRefactoring()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("Resolving..." + args.Name);
            var result = assemblies.Values.First(x => x.FullName == args.Name);

            return result;
        }

        public void LoadLibraries(string pluginsPath)
        {
            assemblies.Clear();
            var currfolder = AppDomain.CurrentDomain.BaseDirectory;

            foreach (var dllpath in Directory.GetFiles(pluginsPath, "*.dll"))
            {
                var assembly = Assembly.LoadFrom(dllpath);
                var fileName = Path.GetFileNameWithoutExtension(dllpath).Replace("QuantConnect.", "");
                assemblies[fileName] = assembly;
            }          
        }

        public void SetConfiguration(string key, string value) {
            //Config.Set("live-mode", "false");
            var type = assemblies["Configuration"].GetType("QuantConnect.Configuration.Config");
            type.InvokeMember("Set", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[] { key, value });
        }
        
        public object CreateComposer() {
            var inst = assemblies["Common"].CreateInstance("QuantConnect.Util.Composer");

            return inst;
        }

        public object CreateEngine(object systemHandlers, object algorithmHandlers, bool liveMode)
        {
            var type = assemblies["Lean.Engine"].GetType("QuantConnect.Lean.Engine.Engine");
            //var inst = type.GetConstructors()[0].Invoke(new object[] { systemHandlers, algorithmHandlers, liveMode });
            var inst = Activator.CreateInstance(type, systemHandlers, algorithmHandlers, liveMode);

            //var inst = assemblies["Lean.Engine"].CreateInstance("QuantConnect.Lean.Engine.Engine", false, BindingFlags.CreateInstance|BindingFlags.Public, 
            //    null, new object[] { systemHandlers, algorithmHandlers, liveMode }, null, null);

            return inst;
        }

        public void RunEngine(object engine, object job, string algorithmPath)
        {
            var type = assemblies["Lean.Engine"].GetType("QuantConnect.Lean.Engine.Engine");
            type.InvokeMember("Run", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, engine, new object[] { job, algorithmPath });
        }

        public object CreateLeanEngineAlgorithmHandlers(object composer)
        {
            //LeanEngineAlgorithmHandlers.FromConfiguration
            var type = assemblies["Lean.Engine"].GetType("QuantConnect.Lean.Engine.LeanEngineAlgorithmHandlers");
            var inst = type.InvokeMember("FromConfiguration", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[] { composer });

            return inst;
        }

        public object CreateLeanEngineSystemHandlers(object composer)
        {
            //LeanEngineSystemHandlers.FromConfiguration
            var type = assemblies["Lean.Engine"].GetType("QuantConnect.Lean.Engine.LeanEngineSystemHandlers");
            var inst = type.InvokeMember("FromConfiguration", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[] { composer });

            //initialize
            type.InvokeMember("Initialize", BindingFlags.Instance|BindingFlags.Public | BindingFlags.InvokeMethod, null, inst, new object[] { });

            return inst;
        }

        public object GetNextJob(object systemHandlers, out string algorithmPath)
        {
            //var job = systemHandlers.JobQueue.NextJob(out _algorithmPath);
            var type = assemblies["Lean.Engine"].GetType("QuantConnect.Lean.Engine.LeanEngineSystemHandlers");
            //var jobqueue = type.InvokeMember("JobQueue", BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.Public, null, systemHandlers, null);
            var jobqueue = type.GetProperty("JobQueue").GetValue(systemHandlers);

            var jobparams = new object[] { "" };
            type = assemblies["Common"].GetType("QuantConnect.Interfaces.IJobQueueHandler");
            var job = type.InvokeMember("NextJob", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public, null, jobqueue, jobparams);
            algorithmPath = (string)jobparams[0];

            return job;
        }

        public void AddMessagingEvents(object systemHandlers, object algorithmHandlers, Action<object> messagingLogEvent, Action<object> backtestResultEvent)
        {
            var type = assemblies["Lean.Engine"].GetType("QuantConnect.Lean.Engine.LeanEngineSystemHandlers");
            var notify = type.GetProperty("Notify").GetValue(systemHandlers);

            type = assemblies["Messaging"].GetType("QuantConnect.Messaging.EventMessagingHandler");
            AddEventHandler(type.GetEvent("DebugEvent"), notify, messagingLogEvent);
            AddEventHandler(type.GetEvent("LogEvent"), notify, messagingLogEvent);
            AddEventHandler(type.GetEvent("RuntimeErrorEvent"), notify, messagingLogEvent);
            AddEventHandler(type.GetEvent("HandledErrorEvent"), notify, messagingLogEvent);
            AddEventHandler(type.GetEvent("BacktestResultEvent"), notify, backtestResultEvent);
        }

        public void SetLogHandler(object composer, Action<object> messagingLogEvent) 
        {
            //Log.LogHandler = Composer.Instance.GetExportedValueByTypeName<ILogHandler>("QuantConnect.Logging.QueueLogHandler");
            var type = assemblies["Common"].GetType("QuantConnect.Util.Composer");
            var generictype = assemblies["Logging"].GetType("QuantConnect.Logging.ILogHandler");
            var method = type.GetMethod("GetExportedValueByTypeName").MakeGenericMethod(generictype);
            var inst = method.Invoke(composer, new object[] { "QuantConnect.Logging.QueueLogHandler" });

            type = assemblies["Logging"].GetType("QuantConnect.Logging.Log");
            type.GetProperty("LogHandler").SetValue(null, inst);

            type = assemblies["Logging"].GetType("QuantConnect.Logging.QueueLogHandler");
            AddEventHandler(type.GetEvent("LogEvent"), inst, messagingLogEvent);
        }

        public void AddEventHandler(EventInfo eventInfo, object item, Action<object> action)
        {
            var parameters = eventInfo.EventHandlerType
              .GetMethod("Invoke")
              .GetParameters()
              .Select(parameter => Expression.Parameter(parameter.ParameterType))
              .ToArray();

            var invoke = action.GetType().GetMethod("Invoke");

            var handler = Expression.Lambda(
                eventInfo.EventHandlerType,
                Expression.Call(Expression.Constant(action), invoke, parameters[0]),
                parameters
              )
              .Compile();

            eventInfo.AddEventHandler(item, handler);
        }
    }
}
