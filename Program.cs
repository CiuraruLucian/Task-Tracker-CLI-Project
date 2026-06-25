using System;
using TaskTrackerCLIProject.CLI;
using TaskTrackerCLIProject.Services;
using TaskTrackerCLIProject.Storage;

namespace TaskTrackerCLIProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            JsonStorage js = new JsonStorage("tasks.json");
            TaskServices ts = new TaskServices(js);
            CommandParser cParser = new CommandParser(ts);
            cParser.Parser(args);
        }

    }
}