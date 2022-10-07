using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient // Note: actual namespace depends on the project name.
{
    class Calendar
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("season_week")]
        public int SeasonWeek { get; set; }

        [JsonProperty("weekday")]
        public string Weekday { get; set; }

    }
    internal class Program
    {
        // this statis acts as middleman
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }
        private static async Task ProcessRepositories() 
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter today, tomorrow, or yesterday. Press Enter without specifying the time to quit the program. ");

                    var time = Console.ReadLine();

                    if (string.IsNullOrEmpty(time)) 
                    {
                        break;
                    }
                    var result = await client.GetAsync("http://calapi.inadiutorium.cz/api/v0/en/calendars/general-en/" + time.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var calendar = JsonConvert.DeserializeObject<Calendar>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Date: " + calendar.Date);
                    Console.WriteLine("Season: " + calendar.Season);
                    Console.WriteLine("Season Week: " + calendar.SeasonWeek);
                    Console.WriteLine("Weekday: " + calendar.Weekday);
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error. Please enter from the choices above!");
                }
            }
        }
    }
}