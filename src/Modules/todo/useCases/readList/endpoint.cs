using backend_challenge.context;
using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.readList;

public class TodoReadListEndpoint : EndpointWithoutRequest<List<Response>, Mapper>
{
    public AppDbContext _dbContext { get; set; } = null!;

    public override void Configure()
    {
        Get("todos");
        Summary(s =>
        {
            s.Summary = "List all tasks";
            s.Description = "Gets the full list of todo tasks.";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            ITodo todoRepository = new TodoData(_dbContext);
            var useCase = new TodoReadListUseCase(todoRepository);
            
            var todos = await useCase.exec();
            
            // Transformamos a lista de Entidades na lista de DTOs
            var response = todos.Select(t => Map.FromEntity(t)).ToList();
          
            await SendAsync(response, cancellation: ct);
        }
        catch (Exception e)
        {
            ThrowError(e.Message);
        }
    }
}