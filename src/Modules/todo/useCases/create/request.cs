namespace backend_challenge.Modules.todo.useCases.create;

public class Request
{
    public string name { get; set; } = null!;
    public string description { get; set; } = string.Empty;
}