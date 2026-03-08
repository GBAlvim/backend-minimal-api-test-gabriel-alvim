using backend_challenge.context;
using backend_challenge.Modules.hero.repository;

namespace backend_challenge.Modules.hero.useCases.update;

public class HeroUpdateEndPoint : Endpoint<Request, Response, Mapper>
{
    public AppDbContext _dbContext { get; init; } = null!;

    public override void Configure()
    {
        Put("heroes");
        Summary(s => {
            s.Summary = "Update the hero";
            s.Description = "Update the whole object with all the provided parameters.";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            IHero heroRepository = new HeroData(_dbContext);

            var existingHero = await heroRepository.readOne(req.id);
            
            if (existingHero is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var useCase = new HeroUpdateUseCase(heroRepository);
            var updateData = Map.ToEntity(req);
            
            updateData.id = req.id; 

            var updatedHero = await useCase.exec(updateData);
            var response = Map.FromEntity(updatedHero);

            await SendAsync(response, cancellation: ct);
        }
        catch (System.Exception e)
        {
            ThrowError(e.Message);
        }
    }
}