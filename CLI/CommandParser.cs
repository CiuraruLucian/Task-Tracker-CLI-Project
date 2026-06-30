using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackerCLIProject.Models;
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
                    Console.WriteLine($"Sucessfully added the task with id: {result.Id}");
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
                    if(int.TryParse(args[1], out int id))
                    {
                        var result = _services.UpdateTask(id, args[2]);
                        Console.WriteLine($"Sucessfully updated the task with id: {id}");
                        Console.WriteLine($"Id: {result.Id} ");
                        Console.WriteLine($"Description: {result.Description}");
                        Console.WriteLine($"Status: {result.Status}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Id must be a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Error! You need to add a description");
                    return;
                }
            }
            else if (args[0] == "delete")
            {
                if(args.Length == 2)
                {
                    if (int.TryParse(args[1], out int id))
                    {
                        var result = _services.UpdateTask(id, args[2]);
                        Console.WriteLine($"Sucessfully deleted the task with id: {id}");
                        Console.WriteLine($"Id: {result.Id} ");
                        Console.WriteLine($"Description: {result.Description}");
                        Console.WriteLine($"Status: {result.Status}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Id must be a valid number.");
                    }
                }
            }
            else if (args[0] == "mark-in-progress")
            {
                if(args.Length == 2)
                {
                    if (int.TryParse(args[1], out int id))
                    {
                        var result = _services.UpdateTask(id, args[2]);
                        Console.WriteLine($"Sucessfully updated the status for the task with id: {id}");
                        Console.WriteLine($"Id: {result.Id} ");
                        Console.WriteLine($"Description: {result.Description}");
                        Console.WriteLine($"Status: {result.Status}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Id must be a valid number.");
                    }
                    
                }
            }
            else if (args[0] == "mark-done")
            {
                if(args.Length == 2)
                {
                    if (int.TryParse(args[1], out int id))
                    {
                        var result = _services.UpdateTask(id, args[2]);
                        Console.WriteLine($"Sucessfully updated the status for the task with id: {id}");
                        Console.WriteLine($"Id: {result.Id} ");
                        Console.WriteLine($"Description: {result.Description}");
                        Console.WriteLine($"Status: {result.Status}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Id must be a valid number.");
                    }
                }
            }
            else if (args[0] == "list")
            {
                if(args.Length == 1)
                {
                    var results = _services.ListTasks();
                    foreach (TaskItem result in results)
                    {
                        Console.WriteLine($"Id: {result.Id} ");
                        Console.WriteLine($"Description: {result.Description}");
                        Console.WriteLine($"Status: {result.Status}");
                        Console.WriteLine($"Created at: {result.CreatedAt}");
                        Console.WriteLine($"Updated at: {result.UpdatedAt}");
                    }
                }else if(args.Length == 2)
                {
                    var results = _services.ListTasks(args[1]);
                    foreach (TaskItem result in results)
                    {
                        Console.WriteLine($"Id: {result.Id} ");
                        Console.WriteLine($"Description: {result.Description}");
                        Console.WriteLine($"Status: {result.Status}");
                        Console.WriteLine($"Created at: {result.CreatedAt}");
                        Console.WriteLine($"Updated at: {result.UpdatedAt}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Unknown command. Usage: task-cli <command> [arguments]");
            }
        }
    }
}
