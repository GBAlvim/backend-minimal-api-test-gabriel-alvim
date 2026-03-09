namespace backend_challenge.Modules.todo.repository;

public interface ITodo
{
    Task<TodoItem> create(TodoItem entity);
    Task<List<TodoItem>> readList();
    Task<TodoItem?> readOne(Guid id);
    Task<TodoItem> update(TodoItem entity);
    Task<int> delete(Guid id);
}