using FluentValidation;
using Forum.API.Features.History.Requests;

namespace Forum.API.Features.History.Validators;

public sealed class GetMessagesRequestValidator : AbstractValidator<GetMessagesRequest>
{
    public GetMessagesRequestValidator()
    {
        RuleFor(request => request.CountMessages)
            .GreaterThan(0);
    }
}