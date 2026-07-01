using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackerCLIProject.Models;
using TaskTrackerCLIProject.Storage;

namespace TaskTrackerCLIProject.Services
{
    // Contains all business logic for managing tasks
    // Depends on IStorage for persistence, keeping file system concerns separate
    public class TaskServices
    {
        private readonly IStorage _storage;

        // Storage is injected via constructor to allow easy swapping (e.g. for testing)
        public TaskServices(IStorage storage)
        {
            _storage = storage;
        }

        // Adds a new task with a default status of "todo"
        // Auto-increments the Id based on the highest existing Id in the list
        public TaskItem AddTask(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Error: The description cannot be empty.");
            }

            List<TaskItem> tasks = _storage.LoadTasks();

            try
            {
                bool hasTasks = tasks.Any();
                if (hasTasks)
                {
                    // Find the highest existing Id and increment by 1
                    int newId = tasks.Max(task => task.Id) + 1;
                    var newTask = new TaskItem
                    {
                        Id = newId,
                        Description = description,
                        Status = "todo",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    tasks.Add(newTask);
                    _storage.SaveTasks(tasks);

                    return newTask;
                }
                else
                {
                    // No existing tasks, so start Id from 1
                    var newTask = new TaskItem
                    {
                        Id = 1,
                        Description = description,
                        Status = "todo",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    tasks.Add(newTask);
                    _storage.SaveTasks(tasks);

                    return newTask;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Error: There are no elements in the list", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: An error has occured, the list wasn't saved", ex);
            }
        }

        // Updates the description of an existing task and refreshes its UpdatedAt timestamp
        public TaskItem UpdateTask(int id, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Error: The description cannot be empty.");
            }

            List<TaskItem> tasks = _storage.LoadTasks();

            try
            {
                // Find the task by Id, returns null if not found
                TaskItem? updatedTask = tasks.FirstOrDefault(task => task.Id == id);
                if (updatedTask == null)
                {
                    throw new KeyNotFoundException($"The task with the id: {id} was not found.");
                }

                // Modify the existing object in place — no need to re-add to the list
                updatedTask.Description = description;
                updatedTask.UpdatedAt = DateTime.Now;

                _storage.SaveTasks(tasks);

                return updatedTask;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"The task with the id: {id} was not found.",ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: An error has occured, the list wasn't saved", ex);
            }
        }

        // Removes a task from the list by Id and saves the updated list
        public TaskItem DeleteTask(int id)
        {
            List<TaskItem> tasks = _storage.LoadTasks();

            try
            {
                TaskItem? deletedTask = tasks.FirstOrDefault(task => task.Id == id);
                if (deletedTask == null)
                {
                    throw new KeyNotFoundException($"The task with the id: {id} was not found.");
                }

                tasks.Remove(deletedTask);
                _storage.SaveTasks(tasks);

                return deletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: An error has occured, the list wasn't saved", ex);
            }
        }

        // Updates the status of a task — valid values are todo, in-progress, and done
        // Also updates the UpdatedAt timestamp to reflect the change
        public TaskItem MarkStatus(string status, int id)
        {
            // Validate that the provided status is one of the accepted values
            string[] validStatuses = { "todo", "in-progress", "done" };
            if (!validStatuses.Contains(status))
            {
                throw new ArgumentException("Error: You entered an invalid status.");
            }

            List<TaskItem> tasks = _storage.LoadTasks();

            try
            {
                TaskItem? markedTask = tasks.FirstOrDefault(task => task.Id == id);
                if (markedTask == null)
                {
                    throw new KeyNotFoundException($"The task with the id: {id} was not found.");
                }

                markedTask.Status = status;
                markedTask.UpdatedAt = DateTime.Now;

                _storage.SaveTasks(tasks);

                return markedTask;
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("Error: The task wasn't founded.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: An error has occured, the list wasn't saved.", ex);
            }
        }

        // Returns all tasks, or filters by status if one is provided
        // Passing no argument returns the full unfiltered list
        public List<TaskItem> ListTasks(string? status = null)
        {
            string[] validStatuses = { "todo", "in-progress", "done" };
            List<TaskItem> tasks = _storage.LoadTasks();

            // No filter provided — return everything
            if (status == null)
            {
                return tasks;
            }
            else
            {
                // Validate the filter value before applying it
                var hasFilteredResults = validStatuses.Contains(status);
                if (!hasFilteredResults)
                {
                    throw new ArgumentException("Error: You entered an invalid status.");
                }

                var filteredResults = tasks.Where(task => task.Status == status);
                return filteredResults.ToList();
            }
        }
    }
}