using System;
using System.Text.Json;
using TaskTrackerCLIProject.Models;
using System.Collections.Generic;
using System.IO;

namespace TaskTrackerCLIProject.Storage
{
    public interface IStorage
    {
        List<TaskItem> LoadTasks();

        void SaveTasks(List<TaskItem> tasks);
    }
    
    // Handles all reading and writing of tasks to the JSON file on disk
    public class JsonStorage : IStorage
    {
        private readonly string _filepath;

        // Accepts the file path so storage location can be decided by the caller
        public JsonStorage(string filePath)
        {
            _filepath = filePath;
        }

        // Loads all tasks from the JSON file
        // Returns an empty list if the file does not exist or is empty
        public List<TaskItem> LoadTasks()
        {
            if (!File.Exists(_filepath))
            {
                return new List<TaskItem>();
            }

            string json = File.ReadAllText(_filepath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<TaskItem>();
            }

            try
            {
                List<TaskItem> tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
                return tasks;
            }
            catch (JsonException ex)
            {
                throw new Exception("Error: the file you are trying to read is corrupted!", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: An error has occured while loading the tasks!", ex);
            }
        }

        // Serializes the full task list and overwrites the JSON file
        // WriteIndented is set to true to keep the file human-readable
        public void SaveTasks(List<TaskItem> tasks)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(tasks, jsonOptions);

            File.WriteAllText(_filepath, json);
        }
    }
}