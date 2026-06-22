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

        public void AddTask() { }

        public void UpdateTask() { }

        public void DeleteTask() { }

        public void MarkStatus() { }

        public void ListTasks() { }

    }
}

