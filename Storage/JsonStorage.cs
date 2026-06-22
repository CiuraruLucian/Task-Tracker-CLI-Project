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

            }catch(Exception ex)
            {
                throw new Exception("Error: An error has occured while loading the tasks!", ex);
            }
            

        }
        public void SaveTasks(List<TaskItem> tasks)
        {

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
