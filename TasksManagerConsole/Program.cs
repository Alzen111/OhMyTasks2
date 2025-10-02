using TasksManagerConsole;

Console.WriteLine("Welcome to the Task Manager Demo!");
var taskManager = new TaskManager();

while (true)
{
    ShowMenu();
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddTask(taskManager);
            break;
        case "2":
            ListTasks(taskManager);
            break;
        case "3":
            MarkTaskComplete(taskManager);
            break;
        case "4":
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

static void ShowMenu()
{
    Console.WriteLine("\n" + new string('=', 25));
    Console.WriteLine("Please select an option:");
    Console.WriteLine("1. Add new task");
    Console.WriteLine("2. List all tasks");
    Console.WriteLine("3. Mark task as complete");
    Console.WriteLine("4. Exit");
    Console.WriteLine(new string('=', 25));
}

static void AddTask(TaskManager manager)
{
    Console.Write("Enter task description: ");
    var description = Console.ReadLine();
    manager.AddTask(description!);
    Console.WriteLine("\nTask added successfully!");
}

static void ListTasks(TaskManager manager)
{
    var tasks = manager.GetTasks();
    if (!tasks.Any())
    {
        Console.WriteLine("No tasks found.");
        return;
    }

    for (int i = 0; i < tasks.Count; i++)
    {
        Console.WriteLine($"{i + 1}. [{(tasks[i].IsCompleted ? "X" : " ")}] {tasks[i].Description}");
    }
}

static void MarkTaskComplete(TaskManager manager)
{
    ListTasks(manager);
    Console.Write("Enter task number to mark as complete: ");
    if (int.TryParse(Console.ReadLine(), out int taskNumber))
    {
        manager.MarkTaskComplete(taskNumber - 1);
        Console.WriteLine("\nTask marked as complete!");
    }
    else
    {
        Console.WriteLine("Invalid task number.");
    }
}
