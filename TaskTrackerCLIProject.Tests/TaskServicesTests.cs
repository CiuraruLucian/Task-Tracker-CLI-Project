using TaskTrackerCLIProject.Models;
using TaskTrackerCLIProject.Services;

namespace TaskTrackerCLIProject.Tests
{
    public class TaskServicesTests
    {
        [Fact]
        public void AddTask_ValidDescription_ReturnsTaskWithCorrectId()
        {
            FakeStorage fs = new FakeStorage();
            TaskServices ts = new TaskServices(fs);

            var result = ts.AddTask("Description for testing!");

            Assert.Equal(expected : 1, actual: result.Id  );
            
            
        }

        [Fact]

        public void AddTask_EmptyDescription_ThrowsArgumentException()
        {
            FakeStorage fs = new FakeStorage();
            TaskServices ts = new TaskServices(fs);

            Assert.Throws<ArgumentException>(() => ts.AddTask(""));

        }

        [Fact]

        public void AddTask_CallAddTaskTwice_SecondTaskid2()
        {
            FakeStorage fs = new FakeStorage();
            TaskServices ts = new TaskServices(fs);

            var result1 = ts.AddTask("First Task for testing!");

            var result2 = ts.AddTask("Second Task for testing!");


            Assert.Equal(expected: 2, actual: result2.Id);
        }

        [Fact]

        public void UpdateTask_InvalidId_ThrowsKeyNotFoundException()
        {
            FakeStorage fs = new FakeStorage();
            TaskServices ts = new TaskServices(fs);

            Assert.Throws<KeyNotFoundException>(() => ts.UpdateTask(999, "Invalid Id for testing"));

        }

        [Fact]

        public void DeleteTask_ValidId_TaskRemovedFromList()
        {
            FakeStorage fs = new FakeStorage();
            TaskServices ts = new TaskServices(fs);

            var resultAdd = ts.AddTask("First Task for testing!");
            var resultDelete = ts.DeleteTask(1);
            fs.LoadTasks();
            

            Assert.Equal(expected: 1, actual: resultDelete.Id);
        }

        [Fact]

        public void ListTasks_NoFilter_ReturnsAllTasks()
        {
            FakeStorage fs = new FakeStorage();
            TaskServices ts = new TaskServices(fs);

            var resultAdd1 = ts.AddTask("First Task for testing!");
            var resultAdd2 = ts.AddTask("Second Task for testing!");
            var resultAdd3 = ts.AddTask("Third Task for testing!");
            var filteredResults = ts.ListTasks();

            Assert.Equal(expected: 3, actual: filteredResults.Count);


        }
    }
}
