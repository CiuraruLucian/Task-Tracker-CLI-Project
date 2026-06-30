using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackerCLIProject.Models;
using TaskTrackerCLIProject.Services;
using TaskTrackerCLIProject.Storage;

namespace TaskTrackerCLIProject.CLI
{
    // Responsible for parsing command line arguments and routing to the correct service method
    // Handles all user-facing output and input validation at the CLI level
    public class CommandParser
    {
        private readonly TaskServices _services;

        // TaskServices is injected via constructor rather than created internally
        public CommandParser(TaskServices services)
        {
            _services = services;
        }

        // Entry point for all CLI commands — called from Program.cs with the raw args array
        public void Parser(string[] args)
        {
            // Guard against running the program with no arguments
            if (args.Length == 0)
            {
                Console.WriteLine("Error! Correct usage: task-cli <command> [arguments] ");
                return;
            }

            // Route to the correct operation based on the first positional argument
            if (args[0] == "add")
            {
                // Expects exactly one argument after the command: the task description
                if (args.Length == 2)
                {
                    var result = _services.AddTask(args[1]);
                    Console.WriteLine($"Sucessfully added the task with id: {result.Id}");
                    Console.WriteLine($"Id: {result.Id}");
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
                // Expects two arguments after the command: the task id and the new description
                if (args.Length == 3)
                {
                    // TryParse prevents a crash if the user passes a non-numeric id
                    if (int.TryParse(args[1], out int id))
                    {
                        var result = _services.UpdateTask(id, args[2]);
                        Console.WriteLine($"Sucessfully updated the task with id: {id}");
                        Console.WriteLine($"Id: {result.Id}");
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
                // Expects exactly one argument after the command: the task id to delete
                if (args.Length == 2)
                {
                    if (int.TryParse(args[1], out int id))
                    {
                        var result = _services.DeleteTask(id);
                        Console.WriteLine($"Sucessfully deleted the task with id: {id}");
                        Console.WriteLine($"Id: {result.Id}");
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
                // Expects exactly one argument after the command: the task id to mark
                if (args.Length == 2)
                {
                    if (int.TryParse(args[1], out int id))
                    {
                        var result = _services.MarkStatus("in-progress", id);
                        Console.WriteLine($"Sucessfully updated the status for the task with id: {id}");
                        Console.WriteLine($"Id: {result.Id}");
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
                // Expects exactly one argument after the command: the task id to mark
                if (args.Length == 2)
                {
                    if (int.TryParse(args[1], out int id))
                    {
                        var result = _services.MarkStatus("done", id);
                        Console.WriteLine($"Sucessfully updated the status for the task with id: {id}");
                        Console.WriteLine($"Id: {result.Id}");
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
                // No second argument means list all tasks with no filter applied
                if (args.Length == 1)
                {
                    var results = _services.ListTasks();
                    foreach (TaskItem result in results)
                    {
                        Console.WriteLine($"Id: {result.Id}");
                        Console.WriteLine($"Description: {result.Description}");
                        Console.WriteLine($"Status: {result.Status}");
                        Console.WriteLine($"Created at: {result.CreatedAt}");
                        Console.WriteLine($"Updated at: {result.UpdatedAt}");
                    }
                }
                else if (args.Length == 2)
                {
                    // Pass args[1] directly as the filter — validation is handled in TaskServices
                    var results = _services.ListTasks(args[1]);
                    foreach (TaskItem result in results)
                    {
                        Console.WriteLine($"Id: {result.Id}");
                        Console.WriteLine($"Description: {result.Description}");
                        Console.WriteLine($"Status: {result.Status}");
                        Console.WriteLine($"Created at: {result.CreatedAt}");
                        Console.WriteLine($"Updated at: {result.UpdatedAt}");
                    }
                }
            }
            else
            {
                // Catches any command that doesn't match the known list
                Console.WriteLine("Unknown command. Usage: task-cli <command> [arguments]");
            }
        }
    }
}