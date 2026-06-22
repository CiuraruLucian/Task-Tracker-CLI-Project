using System;
using System.Text.Json;
using TaskTrackerCLIProject.Models;
using System.Collections.Generic;
using System.IO;

namespace TaskTrackerCLIProject.Storage
{
    public class JsonStorage
    {
        private readonly string _filepath;

        public JsonStorage(string filePath)
        {
            _filepath = filePath;
        }

        public List<TaskItem> LoadTasks()
        {
            string json = File.ReadAllText(_filepath);
            List<TaskItem> tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            return tasks;
        }
        public void SaveTasks(List<TaskItem> tasks)
        {
            List<TaskItem> tasks = LoadTasks();

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(tasks, jsonOptions);

            File.WriteAllText(_filepath, json);
        }
    }
}
