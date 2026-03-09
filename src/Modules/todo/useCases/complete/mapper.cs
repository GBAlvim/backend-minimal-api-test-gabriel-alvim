using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.complete;

public class Mapper : ResponseMapper<Response, TodoItem>
{
    public override Response FromEntity(TodoItem e) => new Response
    {
        id = e.id,
        name = e.name,
        description = e.description,
        status = e.status.ToString(),
        creationDate = e.creationDate,
        completionDate = e.completionDate
    };
}