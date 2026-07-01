using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackerCLIProject.Models;

namespace TaskTrackerCLIProject.Storage
{
    public interface IStorage
    {
        List<TaskItem> LoadTasks();

        void SaveTasks(List<TaskItem> tasks);
    }
}
