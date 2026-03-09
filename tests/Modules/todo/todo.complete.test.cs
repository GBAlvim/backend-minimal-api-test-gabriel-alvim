using backend_challenge.Modules.todo.repository;
using backend_challenge.Modules.todo.useCases.complete;
using UnitTests.Helpers;

namespace backend_challenge.unitTests;

public class TodoCompleteUseCaseTests
{
    [Fact]
    public async Task Exec_ShouldMarkAsCompletedAndSetDate_WhenTaskExists()
    {
        // Arrange
        var testTodo = new TodoItem
        {
            id = Guid.NewGuid(),
            name = "Finish the Test",
            status = TodoStatus.Pending,
            creationDate = DateTime.UtcNow
        };

        await using var _dbContext = new MockDb().CreateDbContext();
        await _dbContext.Todos.AddAsync(testTodo);
        await _dbContext.SaveChangesAsync();

        ITodo todoRepository = new TodoData(_dbContext);
        var sut = new TodoCompleteUseCase(todoRepository);

        // Act
        var result = await sut.exec(testTodo.id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(TodoStatus.Completed, result.status); // Mudou para Completed!
        Assert.NotNull(result.completionDate); // Preencheu a data de conclusão!
    }
}