using System;
using TaskTrackerCLIProject.Services;
using TaskTrackerCLIProject.Storage;

namespace TaskTrackerCLIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JsonStorage js = new JsonStorage("tasks.json");
            TaskServices ts = new TaskServices(js);
            var hardCodedTask = ts.AddTask("Buy Groceries");
            Console.WriteLine($"Id:{hardCodedTask.Id} ");
            Console.WriteLine($"Description: {hardCodedTask.Description} ");
            Console.WriteLine($"Status: {hardCodedTask.Status} ");
        }

    }
}