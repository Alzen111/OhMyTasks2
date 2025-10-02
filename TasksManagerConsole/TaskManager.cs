namespace TasksManagerConsole
{
    public class TaskManager
    {
        private List<TaskItem> _tasks = new();

        public void AddTask(string description)
        {
            _tasks.Add(new TaskItem(description));
        }

        public List<TaskItem> GetTasks()
        {
            return _tasks;
        }

        public void MarkTaskComplete(int index)
        {
            if (index >= 0 && index < _tasks.Count)
            {
                _tasks[index].IsCompleted = true;
            }
        }
    }
    public class TaskItem
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem(string description)
        {
            Description = description;
            IsCompleted = false;
        }
    }
}