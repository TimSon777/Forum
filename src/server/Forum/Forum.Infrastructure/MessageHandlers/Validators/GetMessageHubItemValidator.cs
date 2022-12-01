using FluentValidation;
using Forum.Infrastructure.MessageHandlers.Data;

namespace Forum.Infrastructure.MessageHandlers.Validators;

public class GetMessageHubItemValidator : AbstractValidator<GetMessageHubItem>
{
    public GetMessageHubItemValidator()
    {
        RuleFor(item => item.IpAddress)
            .GreaterThan(0);

        RuleFor(item => item)
            .Must(item => item.Text.Length is >= 1 and <= 500 || item.FileKey is not null);
    }
}