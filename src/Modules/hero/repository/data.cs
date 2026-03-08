using backend_challenge.context;
using Microsoft.EntityFrameworkCore;

namespace backend_challenge.Modules.hero.repository;

public class HeroData : IHero
{
    private readonly AppDbContext _context;

    public HeroData(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<Hero> create(Hero entity)
    {
        _context.Heroes.Add(entity);

        var ret = await _context.SaveChangesAsync();

        Console.WriteLine(ret);

        return entity;
    }

    public async Task<List<Hero>> readList(string? name = null, string? superpower = null)
    {
        var query = _context.Heroes
            .Include(h => h.UniformColor)
            .Include(h => h.Superpowers)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(h => h.name.Contains(name));
        }
        if (!string.IsNullOrWhiteSpace(superpower))
        {
            query = query.Where(h => h.Superpowers.Any(s => s.name.Contains(superpower)));
        }
        return await query.ToListAsync();
    }


    public async Task<Hero?> readOne(Guid id)
    {
        var hero = await _context.Heroes.FindAsync(id);

        return hero;
    }

    public async Task<Hero> update(Hero entity)
    {
        _context.Heroes.Update(entity);

        _ = await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<int> delete(Guid id)
    {
        var hero = await _context.Heroes.FindAsync(id);

        if (hero is null) return 0;

        _context.Heroes.Remove(hero);

        return await _context.SaveChangesAsync();
    }
}