using FluentValidation;
using Forum.Infrastructure.MessageHandlers.Data;

namespace Forum.Infrastructure.MessageHandlers.Validators;

public class GetMessageHubItemValidator : AbstractValidator<GetMessageHubItem>
{
    public GetMessageHubItemValidator()
    {
        RuleFor(x => x.IPv4)
            .GreaterThan(0);

        RuleFor(x => x.Text)
            .Length(1, 500);
    }
}