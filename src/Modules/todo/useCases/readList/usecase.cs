using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.readList;

public class TodoReadListUseCase
{
    private readonly ITodo _todoData;

    public TodoReadListUseCase(ITodo todoData)
    {
        _todoData = todoData;
    }

    public async Task<List<TodoItem>> exec()
    {
        return await _todoData.readList();
    }
}