using backend_challenge.Modules.todo.repository;
using backend_challenge.Modules.todo.useCases.update;
using UnitTests.Helpers;

namespace backend_challenge.unitTests;

public class TodoUpdateUseCaseTests
{
    [Fact]
    public async Task Exec_ShouldReturnUpdatedTodo_WhenTaskExists()
    {
        // Arrange
        var testTodo = new TodoItem
        {
            id = Guid.NewGuid(),
            name = "Old Task",
            description = "Old Desc",
            status = TodoStatus.Pending,
            creationDate = DateTime.UtcNow
        };

        await using var _dbContext = new MockDb().CreateDbContext();
        await _dbContext.Todos.AddAsync(testTodo);
        await _dbContext.SaveChangesAsync();

        ITodo todoRepository = new TodoData(_dbContext);
        var sut = new TodoUpdateUseCase(todoRepository);

        var request = new Request
        {
            id = testTodo.id,
            name = "New Task Name",
            description = "New Desc"
        };

        // Act
        var result = await sut.exec(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Task Name", result.name);
        Assert.Equal("New Desc", result.description);
        Assert.Equal(TodoStatus.Pending, result.status); // O Status não deve ter sido alterado!
    }

    [Fact]
    public async Task Exec_ShouldReturnNull_WhenTaskDoesNotExist()
    {
        // Arrange
        await using var _dbContext = new MockDb().CreateDbContext();
        ITodo todoRepository = new TodoData(_dbContext);
        var sut = new TodoUpdateUseCase(todoRepository);

        var request = new Request
        {
            id = Guid.NewGuid(), // ID Fantasma
            name = "Ghost Task"
        };

        // Act
        var result = await sut.exec(request);

        // Assert
        Assert.Null(result); // caso de Uso deve barrar e retornar nulo
    }
}