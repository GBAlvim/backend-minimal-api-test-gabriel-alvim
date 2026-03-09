using backend_challenge.context;
using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.delete;

public class TodoDeleteEndpoint : Endpoint<Request>
{
    public AppDbContext _dbContext { get; set; } = null!;

    public override void Configure()
    {
        Delete("todos/{id}");
        Summary(s =>
        {
            s.Summary = "Delete a task";
            s.Description = "Permanently removes a task from the system.";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        ITodo todoRepository = new TodoData(_dbContext);
        var useCase = new TodoDeleteUseCase(todoRepository);

        var success = await useCase.exec(req.id);

        if (!success)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendNoContentAsync(ct); 
    }
}