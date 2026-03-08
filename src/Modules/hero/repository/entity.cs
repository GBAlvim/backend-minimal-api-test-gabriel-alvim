namespace backend_challenge.Modules.hero.repository;
using backend_challenge.Modules.uniformColor.repository;
using backend_challenge.Modules.superpower.repository;

public class Hero
{
    public Guid id { get; set; }
    public string name { get; set; } = null!;
    public string description { get; set; } = null!;
    public string? image { get; set; }
    public Guid? UniformColorId { get; set; }
    public UniformColor? UniformColor { get; set; }
    public List<Superpower> Superpowers { get; set; } = new();
}