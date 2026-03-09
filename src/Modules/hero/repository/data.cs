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
        var random = new Random();

        var colors = await _context.UniformColors!.ToListAsync();
        if (colors.Any())
        {
            var randomColor = colors[random.Next(colors.Count)];
            
            entity.UniformColorId = randomColor.id; 
            entity.UniformColor = randomColor;
        }

        var powers = await _context.SuperPowers!.ToListAsync();
        if (powers.Any())
        {
            var selected = powers.OrderBy(x => random.Next()).Take(random.Next(1, 4)).ToList();
            foreach (var p in selected)
            {
                entity.Superpowers.Add(p);
            }
        }

        _context.Heroes!.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<List<Hero>> readList(string? name = null, string? superpower = null)
    {
        var query = _context.Heroes!
            .Include(h => h.UniformColor)
            .Include(h => h.Superpowers)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(h => h.name.ToLower().Contains(name.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(superpower))
        {
            query = query.Where(h => h.Superpowers.Any(s => s.name.ToLower().Contains(superpower.ToLower())));
        }
        return await query.ToListAsync();
    }

    public async Task<Hero?> readOne(Guid id)
    {
        return await _context.Heroes!
            .Include(h => h.UniformColor)
            .Include(h => h.Superpowers)
            .FirstOrDefaultAsync(h => h.id == id);
    }

    public async Task<Hero> update(Hero entity)
    {
        var existingHero = await _context.Heroes!.FindAsync(entity.id);
        if (existingHero != null)
        {
            _context.Entry(existingHero).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
        return entity;
    }

    public async Task<int> delete(Guid id)
    {
        var hero = await _context.Heroes!.FindAsync(id);
        if (hero is null) return 0;

        _context.Heroes.Remove(hero);
        return await _context.SaveChangesAsync();
    }
}