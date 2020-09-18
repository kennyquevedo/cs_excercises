using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

public class MultipleTasks
{
    public class Individual
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Email:{Email}, Full Name:{FirstName} {LastName}";
        }
    }

    public MultipleTasks()
    {
        Console.Write("Please enter an option [1/One Task] - [2/MultiTask]: ");
        var key = Console.ReadLine();
        if (key == "1")
            ProcessIndividuals();
        else if (key == "2")
            ProcessMultiIndividuals();
    }

    public void ProcessIndividuals()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        var list = GetIndividuals();
        foreach (var item in list)
        {
            Thread.Sleep(500);
            ClearCurrentConsoleLine();
            Console.Write("\r{0}", item.ToString());
        }

        watch.Stop();
        Console.WriteLine();
        Console.WriteLine($"Time:{(watch.ElapsedMilliseconds).ToString()}");
    }

    public void ProcessMultiIndividuals()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        var list = GetIndividuals().Select((x, i) => new { Index = i, Value = x })
                    .GroupBy(x => x.Index / 3)
                                .Select(x => x.Select(v => v.Value).ToList())
                                            .ToList();

        var taskList = new List<Task>();

        foreach (var itemList in list)
        {
            var task = new Task(() =>
            {
                foreach (var item in itemList)
                {
                    Thread.Sleep(500);
                    ClearCurrentConsoleLine();
                    Console.Write(".");
                }
            });
            taskList.Add(task);
            task.Start();
        }

        Console.WriteLine($"Tasks:{taskList.Count}");
        Task.WaitAll(taskList.ToArray());
        watch.Stop();

        Console.WriteLine();
        Console.WriteLine($"Multi-Time:{(watch.ElapsedMilliseconds).ToString()}");
    }

    public List<Individual> GetIndividuals()
    {
        Task<List<Individual>> task = ProcessRepositories();
        task.Wait();

        return task.Result;
    }

    private static async Task<List<Individual>> ProcessRepositories()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();

        var streamTask = client.GetStreamAsync("https://my.api.mockaroo.com/random_full_name?key=6c3463c0");
        var repositories = await JsonSerializer.DeserializeAsync<List<Individual>>(await streamTask);

        return repositories;
    }

    public static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }
}