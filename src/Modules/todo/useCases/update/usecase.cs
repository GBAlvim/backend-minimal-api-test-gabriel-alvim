using backend_challenge.Modules.todo.repository;

namespace backend_challenge.Modules.todo.useCases.update;

public class TodoUpdateUseCase
{
    private readonly ITodo _todoData;

    public TodoUpdateUseCase(ITodo todoData)
    {
        _todoData = todoData;
    }

    public async Task<TodoItem?> exec(Request req)
    {
        //Busca no banco osdados imutáveis (creationDate, status)
        var existingTodo = await _todoData.readOne(req.id);
        
        if (existingTodo is null) return null;

        // Atualiza apenas o que é permitido
        existingTodo.name = req.name;
        existingTodo.description = req.description;

        //Salva/retorna
        return await _todoData.update(existingTodo);
    }
}