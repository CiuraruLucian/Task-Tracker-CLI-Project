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
        

        public TaskItem UpdateTask(int id, string description) 
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Error: The description cannot be empty.");
            }
            List<TaskItem> tasks = _storage.LoadTasks();
            try
            {
                    TaskItem? updatedTask = tasks.FirstOrDefault(task => task.Id == id);
                    if(updatedTask == null)
                    {
                        throw new KeyNotFoundException($"The task with the id: {id} was not found.");
                    }
                    updatedTask.Description = description;
                    updatedTask.UpdatedAt = DateTime.Now;
        
                    _storage.SaveTasks(tasks);

                    return updatedTask;

                
              
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

        public void DeleteTask() { }

        public void MarkStatus() { }

        public void ListTasks() { }

    }
}

