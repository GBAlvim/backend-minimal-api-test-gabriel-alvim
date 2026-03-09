namespace backend_challenge.Modules.todo.repository;

public enum TodoStatus
{
    Pending,
    InProgress,
    Completed
}

public class TodoItem
{
    public Guid id { get; set; }
    public string name { get; set; } = null!;
    public string description { get; set; } = string.Empty;
    public TodoStatus status { get; set; }
    public DateTime creationDate { get; set; }
    public DateTime? completionDate { get; set; }
}