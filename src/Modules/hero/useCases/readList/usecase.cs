using backend_challenge.context;
using backend_challenge.Modules.hero.repository;

namespace backend_challenge.Modules.hero.useCases.readList;

public class HeroReadListUseCase
{
    private readonly IHero _heroData;

    public HeroReadListUseCase(IHero heroData)
    {
        _heroData = heroData;
    }

    public async Task<List<Hero>> exec(Request req)
    {
        return await _heroData.readList(req.name, req.superpower);
    }
}