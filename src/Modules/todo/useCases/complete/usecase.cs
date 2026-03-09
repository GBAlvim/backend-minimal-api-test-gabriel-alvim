using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.complete;

public class TodoCompleteUseCase
{
    private readonly ITodo _todoData;

    public TodoCompleteUseCase(ITodo todoData)
    {
        _todoData = todoData;
    }

    public async Task<TodoItem?> exec(Guid id)
    {
        var todo = await _todoData.readOne(id);
        if (todo is null) return null;

        // Regra do Desafio: muda  status e seta a data de conclusão
        todo.status = TodoStatus.Completed;
        todo.completionDate = DateTime.UtcNow;

        return await _todoData.update(todo);
    }
}