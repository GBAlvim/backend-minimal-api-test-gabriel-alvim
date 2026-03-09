using backend_challenge.context;
using backend_challenge.Modules.hero.repository;

namespace backend_challenge.Modules.hero.useCases.create;

public class HeroReadOneEndPoint : Endpoint<Request, Response, Mapper>
{
    public AppDbContext _dbContext { get; init; }

    public override void Configure()
    {
        Post("heroes");
        Summary(s =>
        {
            s.Summary = "Creates a new \"Hero\"";
            s.Description = "Register a \"Hero\" on the platform";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {   
            IHero heroRepository = new HeroData(_dbContext);

            var useCase = new HeroCreateUseCase(heroRepository, _dbContext);

            var newHero = Map.ToEntity(req);

            var createdHero = await useCase.exec(newHero);

            var responseHero = Map.FromEntity(createdHero);

            await SendAsync(responseHero);
        }
        catch (Exception e)
        {
            ThrowError(e.Message);
        }
    }
}