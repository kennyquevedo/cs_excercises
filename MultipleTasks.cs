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
    //Represent an indivudal.
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

    //Constructor.
    public MultipleTasks()
    {
        //Request an option. 1--> Single task, 2 --> multiple tasks.
        Console.Write("Please enter an option [1/SingleTask] - [2/MultiTask]: ");

        var key = Console.ReadLine();
        if (key == "1")
            ProcessIndividuals();
        else if (key == "2")
            ProcessMultiIndividuals();
    }

    //Loop through individual list.
    public void ProcessIndividuals()
    {
        //Timer to measure the process.
        Stopwatch watch = new Stopwatch();
        watch.Start();

        var list = GetIndividuals();
        foreach (var item in list)
        {
            Thread.Sleep(500);
            ClearCurrentConsoleLine();
            Console.Write("\r{0}", item.ToString());
        }

        //Show some data of the process.
        watch.Stop();
        Console.WriteLine();
        Console.WriteLine($"Time:{(watch.ElapsedMilliseconds).ToString()}");
    }

    //Create sublist from a main list and create a task for each sublist.
    public void ProcessMultiIndividuals()
    {
        //Timer to measure the process.
        Stopwatch watch = new Stopwatch();
        watch.Start();

        //Get the individual list and split into 3 sublist.
        var list = GetIndividuals().Select((x, i) => new { Index = i, Value = x })
                    .GroupBy(x => x.Index / 3)
                                .Select(x => x.Select(v => v.Value).ToList())
                                            .ToList();

        //List of tasks to add a task per sublist.
        var taskList = new List<Task>();

        //loop through list
        foreach (var itemList in list)
        {
            //Create a task for each sublist
            var task = new Task(() =>
            {
                //Loop individual sublist and sleep during 500ms. Show a dot to indicate the process.
                foreach (var item in itemList)
                {
                    Thread.Sleep(500);
                    ClearCurrentConsoleLine();
                    Console.Write(".");
                }
            });

            //Add current tast to list.
            taskList.Add(task);

            //start current task.
            task.Start();
        }

        //wait until complete all tasks.
        Task.WaitAll(taskList.ToArray());
        watch.Stop();

        //Write some data to verify the process.
        Console.WriteLine();
        Console.WriteLine($"Tasks:{taskList.Count}");
        Console.WriteLine($"Multi-Time:{(watch.ElapsedMilliseconds).ToString()}");
    }

    //Get a task with the individual list.
    public List<Individual> GetIndividuals()
    {
        Task<List<Individual>> task = ProcessRepositories();
        task.Wait();

        return task.Result;
    }


    //Just get an individual list.
    private static async Task<List<Individual>> ProcessRepositories()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();

        var streamTask = client.GetStreamAsync("https://my.api.mockaroo.com/random_full_name?key=6c3463c0");
        var repositories = await JsonSerializer.DeserializeAsync<List<Individual>>(await streamTask);

        return repositories;
    }

    //Just clean the current console line.
    public static void ClearCurrentConsoleLine()
    {
        int currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }
}