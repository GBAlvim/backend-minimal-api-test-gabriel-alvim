using backend_challenge.context;
using backend_challenge.Modules.hero.repository;

namespace backend_challenge.Modules.hero.useCases.readList;

public class HeroReadListEndPoint : Endpoint<Request, List<Response>, Mapper>
{
    public AppDbContext _dbContext { get; set; } = null!;

    public override void Configure()
    {
        Get("heroes");
        Summary(s =>
        {
            s.Summary = "List heroes";
            s.Description = "Gets the list of heroes.";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            IHero heroRepository = new HeroData(_dbContext);
            
            var useCase = new HeroReadListUseCase(heroRepository);
            var heroes = await useCase.exec(req);
            var response = heroes.Select(h => Map.FromEntity(h)).ToList();
          
            await SendAsync(response, cancellation: ct);
        }
        catch (Exception e)
        {
            ThrowError(e.Message);
        }
    }
    
}