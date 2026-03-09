using backend_challenge.context;
using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.complete;

public class TodoCompleteEndpoint : Endpoint<Request, Response, Mapper>
{
    public AppDbContext _dbContext { get; set; } = null!;

    public override void Configure()
    {
        Patch("todos/{id}/complete");
        Summary(s =>
        {
            s.Summary = "Mark task as completed";
            s.Description = "Changes the task status to Completed and sets the completion date.";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        ITodo todoRepository = new TodoData(_dbContext);
        var useCase = new TodoCompleteUseCase(todoRepository);

        var completedTodo = await useCase.exec(req.id);

        if (completedTodo is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = Map.FromEntity(completedTodo);
        await SendAsync(response, cancellation: ct);
    }
}