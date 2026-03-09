using FastEndpoints;
using FluentValidation;

namespace backend_challenge.Modules.todo.useCases.create;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.name)
            .NotEmpty()
            .WithMessage("The task name is required.");
    }
}