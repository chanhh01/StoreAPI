using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Driver;
using System.Collections.Generic;
using StoreModelandRepo;
using System.Diagnostics;
using System.Linq;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "App.config", Watch = true)]
namespace TestCrudConsoleSingleConnection
{
    class Program1
    {
        private string Database_Name = "newTest";
        private string Store_Collection_Name = "TestAPI";
        public IMongoCollection<Store> _store;
        private List<int> time = new List<int>();
        private static readonly ILog log =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Beginning operations...");
            Program1 p = new Program1();
            await p.fireLoop();
        }

        public async Task fireLoop()
        {
            time.Clear();
            Console.WriteLine("Enter the amount of loop.");
            string loopselection = Console.ReadLine();
            int j = int.Parse(loopselection);
            List<Task> task = new List<Task>();
            MongoClient mongoClient = new MongoClient("mongodb://chan:chh428493@testchanvm.eastasia.cloudapp.azure.com?maxPoolSize=500");
            var db = mongoClient.GetDatabase(Database_Name);
            _store = db.GetCollection<Store>(Store_Collection_Name);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            log.Info(DateTime.Now);
            for(int i = 0; i < j; i++)
            {
                task.Add(Demo());
            }
            await Task.WhenAll(task);
            stopwatch.Stop();
            log.Info("==============================END================================");
            log.Info("The number of attempts: " + time.Count());
            log.Info("The minimum time taken: " + time.Min());
            log.Info("The average time taken: " + time.Average());
            log.Info("The maximum time taken: " + time.Max());
            log.Info("Time taken for operation: " + stopwatch.ElapsedMilliseconds);
            Console.WriteLine("");
            Console.WriteLine("Continue to loop? '1' for yes, '0' to close");
            string loopcontinue = Console.ReadLine();
            if (loopcontinue == "1")
            {
                await this.fireLoop();
            }
            else if (loopcontinue == "0")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }

        public async Task Demo()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Store> store = await GetStore();
            store.ToJson();
            stopwatch.Stop();
            log.Info(stopwatch.ElapsedMilliseconds + "ms");
            int i = (int)stopwatch.ElapsedMilliseconds;
            time.Add(i);
        }

        public async Task<List<Store>> GetStore()
        {
            List<Store> store = (await _store.FindAsync(store => true)).ToList();
            return store;
        }
    }
}

