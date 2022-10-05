using Chat.Infrastructure.MessageHandlers.Data;
using FluentValidation;

namespace Chat.Infrastructure.MessageHandlers.Validators;

public class GetMessageHubItemValidator : AbstractValidator<GetMessageHubItem>
{
    public GetMessageHubItemValidator()
    {
        RuleFor(x => x.Port)
            .InclusiveBetween(0, 65535);

        RuleFor(x => x.IPv4)
            .GreaterThan(0);

        RuleFor(x => x.Text)
            .Length(1, 500);
    }
}