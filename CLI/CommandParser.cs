using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackerCLIProject.Services;
using TaskTrackerCLIProject.Storage;

namespace TaskTrackerCLIProject.CLI
{
    public class CommandParser
    {
        private readonly  TaskServices _services;

        public CommandParser(TaskServices services)
        {
            _services = services;
        }
        
        public void Parser(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Error! Correct usage: task-cli <command> [arguments] ");
                return;
            }

            if (args[0] == "add")
            {
                if (args.Length == 2)
                {
                    var result = _services.AddTask(args[1]);
                    Console.WriteLine($"Id: {result.Id} ");
                    Console.WriteLine($"Description: {result.Description}");
                    Console.WriteLine($"Status: {result.Status}");
                }
                else
                {
                    Console.WriteLine("Error! You need to add a description");
                    return;
                }

            }
            else if (args[0] == "update")
            {
                if(args.Length == 3)
                {
                    int id = int.Parse(args[1]);
                    Console.WriteLine($"Parsed id: {id}");
                    var result = _services.UpdateTask(id, args[2]);
                    Console.WriteLine($"Id: {result.Id} ");
                    Console.WriteLine($"Description: {result.Description}");
                    Console.WriteLine($"Status: {result.Status}");
                }
                else
                {
                    Console.WriteLine("Error! You need to add a description");
                    return;
                }
            }
        }
    }
}
