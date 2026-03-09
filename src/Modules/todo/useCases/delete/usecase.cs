using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.delete;

public class TodoDeleteUseCase
{
    private readonly ITodo _todoData;

    public TodoDeleteUseCase(ITodo todoData)
    {
        _todoData = todoData;
    }

    public async Task<bool> exec(Guid id)
    {
        var linesAffected = await _todoData.delete(id);
        return linesAffected > 0;
    }
}