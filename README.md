# Task Tracker CLI

A command line interface application built in C# that allows you to create, manage, and track your tasks. Tasks are persisted locally in a JSON file, meaning your data is saved between sessions without requiring a database or internet connection.

This project was built as part of my second year studies at Solent University to practice working with the file system, handling user input, parsing command line arguments, and structuring a C# application using separation of concerns and dependency injection.

---

## Project Repository

GitHub: https://github.com/CiuraruLucian/Task-Tracker-CLI-Project.git

---

## Technologies Used

- C# / .NET 10.0
- System.Text.Json — for serializing and deserializing tasks to and from a JSON file
- xUnit — for unit testing the business logic layer
- Visual Studio 2022

---

## Requirements

- .NET 10.0 SDK or later
- Windows, macOS, or Linux

---

## How to Run

Clone the repository:

```
git clone <your-repo-url-here>
cd Task-Tracker-CLI-Project
```

Build the project:

```
dotnet build
```

Navigate to the output folder:

```
cd TaskTrackerCLIProject/bin/Debug/net10.0
```

Run commands using the executable:

```
TaskTrackerCLIProject.exe <command> [arguments]
```

On macOS or Linux:

```
./TaskTrackerCLIProject <command> [arguments]
```

---

## Usage

### Adding a task

```
TaskTrackerCLIProject.exe add "Buy groceries"
```

Output:
```
Successfully added the task with id: 1
Id: 1
Description: Buy groceries
Status: todo
```

### Updating a task

```
TaskTrackerCLIProject.exe update 1 "Buy groceries and cook dinner"
```

### Deleting a task

```
TaskTrackerCLIProject.exe delete 1
```

### Marking a task as in progress

```
TaskTrackerCLIProject.exe mark-in-progress 1
```

### Marking a task as done

```
TaskTrackerCLIProject.exe mark-done 1
```

### Listing all tasks

```
TaskTrackerCLIProject.exe list
```

### Listing tasks by status

```
TaskTrackerCLIProject.exe list done
TaskTrackerCLIProject.exe list todo
TaskTrackerCLIProject.exe list in-progress
```

---

## Task Properties

Each task stored in the JSON file has the following properties:

- Id - a unique identifier, auto-incremented
- Description - a short description of the task
- Status - the current status of the task (todo, in-progress, done)
- CreatedAt - the date and time the task was created
- UpdatedAt - the date and time the task was last updated

---

## Project Structure

```
Task-Tracker-CLI-Project/
    TaskTrackerCLIProject/
        CLI/
            CommandParser.cs        -- Parses command line arguments and routes to the correct service method
        Models/
            TaskItem.cs             -- Defines the TaskItem data structure
        Services/
            TaskServices.cs         -- Contains the business logic for all task operations
        Storage/
            IStorage.cs             -- Interface for storage, enabling dependency injection and testability
            JsonStorage.cs          -- Handles reading and writing tasks to the JSON file
        Program.cs                  -- Entry point, wires dependencies together
    TaskTrackerCLIProject.Tests/
        FakeStorage.cs              -- In-memory storage implementation used in unit tests
        TaskServicesTests.cs        -- xUnit tests for the TaskServices business logic layer
```

---

## Running the Tests

Open the solution in Visual Studio and navigate to Test → Run All Tests, or use the Test Explorer window. All tests are written using xUnit and test the business logic layer in isolation using a fake in-memory storage implementation.

---

## Error Handling

The application handles the following error cases gracefully:

- Running the program with no arguments prints a usage message
- Passing a non-numeric value where an id is expected prints a clear error message
- Referencing an id that does not exist prints a clear error message
- Passing an empty description prints a validation error
- Passing an invalid status value prints a validation error
- Unknown commands print a usage hint instead of crashing

---

## Author

Gheorghe Lucian Ciuraru — Southampton Solent University, BSc Computer Science, Year 2

---

## Notes

Tasks are saved to a file called tasks.json in the same directory as the executable. This file is created automatically on the first run. Do not manually edit this file while the program is running, as changes may be overwritten.
