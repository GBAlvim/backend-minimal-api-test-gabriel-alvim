namespace backend_challenge.Modules.todo.useCases.update;

public class Request
{
    public Guid id { get; set; } //FastEndpoints injeta da URL
    public string name { get; set; } = null!;
    public string description { get; set; } = string.Empty;
}