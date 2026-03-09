namespace backend_challenge.Modules.todo.useCases.create;

public class Response
{
    public Guid id { get; set; }
    public string name { get; set; } = null!;
    public string description { get; set; } = string.Empty;
    public string status { get; set; } = null!; // enum em STr pro front
    public DateTime creationDate { get; set; }
    public DateTime? completionDate { get; set; }
}