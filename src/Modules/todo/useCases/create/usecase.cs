using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.create;

public class TodoCreateUseCase
{
    private readonly ITodo _todoData;

    public TodoCreateUseCase(ITodo todoData)
    {
        _todoData = todoData;
    }

    public async Task<TodoItem> exec(TodoItem entity)
    {
        entity.status = TodoStatus.Pending;
        entity.creationDate = DateTime.UtcNow;
        
        return await _todoData.create(entity);
    }
}