using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.create;

public class Mapper : Mapper<Request, Response, TodoItem>
{
    public override TodoItem ToEntity(Request r) => new TodoItem
    {
        name = r.name,
        description = r.description
    };

    public override Response FromEntity(TodoItem e) => new Response
    {
        id = e.id,
        name = e.name,
        description = e.description,
        status = e.status.ToString(), // "Pending", "InProgress", etc.
        creationDate = e.creationDate,
        completionDate = e.completionDate
    };
}