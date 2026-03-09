using backend_challenge.context;
using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.create;

public class TodoCreateEndpoint : Endpoint<Request, Response, Mapper>
{
    public AppDbContext _dbContext { get; init; } = null!;

    public override void Configure()
    {
        Post("todos");
        Summary(s =>
        {
            s.Summary = "Creates a new Todo task";
            s.Description = "Register a new task on the platform.";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            ITodo todoRepository = new TodoData(_dbContext);
            var useCase = new TodoCreateUseCase(todoRepository);

            var newTodo = Map.ToEntity(req);
            var createdTodo = await useCase.exec(newTodo);
            var responseTodo = Map.FromEntity(createdTodo);

            await SendAsync(responseTodo, cancellation: ct);
        }
        catch (Exception e)
        {
            ThrowError(e.Message);
        }
    }
}