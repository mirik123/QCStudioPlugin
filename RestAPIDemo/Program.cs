/*
* QUANTCONNECT.COM - REST API
* C# Demonstration Test Application
*/

using QuantConnect.RestAPI;
using QuantConnect.RestAPI.Models;
/**********************************************************
* USING NAMESPACES
**********************************************************/
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QuantConnect.RestAPIDemo
{
    /// <summary>
    /// Our Test Application
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args)
            .GetAwaiter()
            .GetResult();
        }
        
        static async Task MainAsync(string[] args)
        {
            try
            {
                // Init the new API: email, password.
                API api = new API();

                await api.Authenticate("demo@quantconnect.com", "demo123");

                Console.WriteLine("==========================================");
                Console.WriteLine("Test 1: Create Project: ");
                PacketCreateProject projectCreated = await api.ProjectCreate("New Test Project");
                Console.WriteLine("New Project Id: " + projectCreated.ProjectId);
                int newProjectId = projectCreated.ProjectId;

                Console.WriteLine("==========================================");
                Console.WriteLine("Test 2: List Projects: ");
                PacketProject projects = await api.ProjectList();
                foreach (Project project in projects.Projects)
                {
                    Console.WriteLine("Name: " + project.Name + " Id: " + project.Id + " Date: " + project.Modified.ToLongTimeString());
                }

                Console.WriteLine("==========================================");
                Console.WriteLine("Test 3: Update Our New Project: (Set new project as basic template strategy)");
                List<QCFile> files = new List<QCFile>();
                files.Add(new QCFile("Main.cs", System.IO.File.ReadAllText("demo.cs")));
                PacketBase updateSuccess = await api.ProjectUpdate(newProjectId, files);
                Console.WriteLine("Updated project: " + updateSuccess.Success);

                Console.WriteLine("==========================================");
                Console.WriteLine("Test 4: List Project Contents:");
                var projectFiles = await api.ProjectFiles(newProjectId);
                foreach (QCFile file in projectFiles.Files)
                {
                    Console.WriteLine("File Name: " + file.Name + " Contents: " + file.Code);
                }

                Console.WriteLine("==========================================");
                Console.WriteLine("Test 5: Compile Project:");
                PacketCompile compileResult = await api.Compile(newProjectId);
                Console.WriteLine("CompileId: " + compileResult.CompileId);
                foreach (CompileLog entry in compileResult.Logs)
                {
                    Console.WriteLine("Compile Result : Time: " + entry.Time + " Type: " + entry.Type.ToString() + " Entry: " + entry.Entry);
                }

                Console.WriteLine("==========================================");
                Console.WriteLine("Test 6: Backtest Compiled Project:");
                PacketBacktest backtestResult = await api.Backtest(newProjectId, compileResult.CompileId, "New Random Name!");
                Console.WriteLine("Backtest Id: " + backtestResult.BacktestId);

                Console.WriteLine("==========================================");
                Console.WriteLine("Test 7: Reading Backtest Results:");
                PacketBacktestResult readResult = await api.BacktestResults(backtestResult.BacktestId);
                Thread.Sleep(3000);
                foreach (Chart chart in readResult.Results.Charts.Values)
                {
                    Console.WriteLine("Result Chart Name: " + chart.Name);
                }

                Console.WriteLine("==========================================");
                Console.WriteLine("Test 8: Reading Backtest List:");
                PacketBacktestList backtestList = await api.BacktestList(newProjectId);
                foreach (var summary in backtestList.Summary)
                {
                    Console.WriteLine("Backtest: " + summary.BacktestId + " Requested: " + summary.Requested.ToShortDateString());

                    Console.WriteLine("==========================================");
                    Console.WriteLine("Test 9: Delete Backtest:");
                    PacketBase deleteBacktest = await api.BacktestDelete(summary.BacktestId);
                    if (deleteBacktest.Success)
                    {
                        Console.WriteLine("Deleted: " + summary.BacktestId);
                    }
                }

                Console.WriteLine("==========================================");
                Console.WriteLine("Test 8: Delete Project: ");
                PacketBase deleteSuccess = await api.ProjectDelete(newProjectId);
                Console.WriteLine("Deleted project: " + deleteSuccess.Success.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("==========================================");
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }
    }
}
