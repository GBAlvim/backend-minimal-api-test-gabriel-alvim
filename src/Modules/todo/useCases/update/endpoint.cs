using backend_challenge.context;
using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.update;

public class TodoUpdateEndpoint : Endpoint<Request, Response, Mapper>
{
    public AppDbContext _dbContext { get; set; } = null!;

    public override void Configure()
    {
        Put("todos/{id}");
        Summary(s =>
        {
            s.Summary = "Update a task";
            s.Description = "Updates the name and description of a specific task.";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        ITodo todoRepository = new TodoData(_dbContext);
        var useCase = new TodoUpdateUseCase(todoRepository);

        var updatedTodo = await useCase.exec(req);

        // Prevenção segue padrão challenge 2
        if (updatedTodo is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = Map.FromEntity(updatedTodo);
        await SendAsync(response, cancellation: ct);
    }
}