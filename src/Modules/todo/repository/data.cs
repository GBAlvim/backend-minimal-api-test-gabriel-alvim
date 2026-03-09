using backend_challenge.context;
using Microsoft.EntityFrameworkCore;

namespace backend_challenge.Modules.todo.repository;

public class TodoData : ITodo
{
    private readonly AppDbContext _context;

    public TodoData(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<TodoItem> create(TodoItem entity)
    {
        _context.Todos.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TodoItem>> readList()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<TodoItem?> readOne(Guid id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task<TodoItem> update(TodoItem entity)
    {
        _context.Todos.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<int> delete(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo is null) return 0;

        _context.Todos.Remove(todo);
        return await _context.SaveChangesAsync();
    }
}