using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackerCLIProject.Models;
using TaskTrackerCLIProject.Storage;

namespace TaskTrackerCLIProject.Services
{
    public class TaskServices
    {
        private readonly JsonStorage _storage;

        public TaskServices(JsonStorage storage)
        {
            _storage = storage;
        }

        public TaskItem AddTask(string description) 
        {
            List<TaskItem> tasks = _storage.LoadTasks();
            try
            {
                bool hasTasks = tasks.Any();
                if (hasTasks)
                {
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
            }catch (InvalidOperationException ex)
            {
                throw new Exception("Error: There are no elements in the list", ex);
            }catch (Exception ex)
            {
                throw new Exception("Error: An error has occured, the list wasn't saved", ex);
            }
        }

        public void UpdateTask() { }

        public void DeleteTask() { }

        public void MarkStatus() { }

        public void ListTasks() { }

    }
}

