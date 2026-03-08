namespace backend_challenge.Modules.uniformColor.repository;
using backend_challenge.Modules.hero.repository;

public class UniformColor
{
    public Guid id { get; set; }
    public string name { get; set; } = null!;
    public List<Hero> Heroes { get; set; } = new();
}