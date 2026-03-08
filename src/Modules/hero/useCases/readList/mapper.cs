using backend_challenge.Modules.hero.repository;

namespace backend_challenge.Modules.hero.useCases.readList;

public class Mapper : ResponseMapper<Response, Hero>
{
    public override Response FromEntity(Hero e)
    {
        return new Response
        {
            id = e.id,
            name = e.name,
            description = e.description,
            image = e.image,
            uniformColor = e.UniformColor?.name,
            superpowers = e.Superpowers.Select(s => s.name).ToList()
        };
    }
}