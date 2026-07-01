using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackerCLIProject.Storage;
using TaskTrackerCLIProject.Models;

namespace TaskTrackerCLIProject.Tests
{
    public class FakeStorage : IStorage
    {
        private List<TaskItem> _testingTasks = new List<TaskItem>();
        public void SaveTasks(List<TaskItem> tasks)
        {
            _testingTasks = tasks;

        }

        public List<TaskItem> LoadTasks()
        {
            return _testingTasks;
        }
    }
}
