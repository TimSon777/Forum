using FluentValidation;

namespace Forum.API.Features.Messages.History;

public sealed class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(request => request.CountMessages)
            .GreaterThan(0);
    }
}