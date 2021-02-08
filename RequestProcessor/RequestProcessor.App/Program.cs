using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Menu;
using RequestProcessor.App.Services;

namespace RequestProcessor.App
{
    /// <summary>
    /// Entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <returns>Returns exit code.</returns>
        private static async Task<int> Main()
        {
            Console.WriteLine("Task 1. Student of PM academy Zinchenko Bohdan ");
            var link = "options.json";
            try
            {
                var httpClient = new HttpClient();
                var logger = new Logger();
                var reqOptions = new OptionsSource(link);
                var responseHandler = new ResponseHandler();
                var requestHandler = new RequestHandler(httpClient);
                var reqPerf = new RequestPerformer(requestHandler, responseHandler, logger);
                var mainMenu = new MainMenu(reqPerf, reqOptions, logger);
                Console.WriteLine("Start work with file");
                return await mainMenu.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Critical unhandled exception");
                Console.WriteLine(ex);
                return -1;
            }
        }
    }
}
