using backend_challenge.context;
using backend_challenge.Modules.hero.repository;
using Microsoft.EntityFrameworkCore; //    ToListAsync e AsNoTracking

namespace backend_challenge.Modules.hero.useCases.create;

public class HeroCreateUseCase
{
    private readonly IHero _heroData;
    private readonly AppDbContext _context;

    public HeroCreateUseCase(IHero heroData, AppDbContext context) 
    {
        _heroData = heroData;
        _context = context;
    }

    public async Task<Hero> exec(Hero entity)
    {
        return await _heroData.create(entity);
    }
}