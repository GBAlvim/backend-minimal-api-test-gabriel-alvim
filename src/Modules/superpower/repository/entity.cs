namespace backend_challenge.Modules.superpower.repository;
using backend_challenge.Modules.hero.repository;

public class Superpower
{
    public Guid id { get; set; }
    public string name { get; set; } = null!;
    public List<Hero> Heroes { get; set; } = new();
}