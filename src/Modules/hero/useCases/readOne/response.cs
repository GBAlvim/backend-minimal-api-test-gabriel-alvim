using backend_challenge.Modules.hero.repository;

namespace backend_challenge.Modules.hero.useCases.readOne;

public class Response
{
    public Guid id { get; set; }
    public string name { get; set; } = null!;
    public string description { get; set; } = null!;
    public string? image { get; set; }
    public string? uniformColor { get; set; }
    public List<string> superpowers { get; set; } = new();
}
