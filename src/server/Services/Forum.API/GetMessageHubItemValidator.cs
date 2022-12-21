using FluentValidation;
using Forum.API.Data;

namespace Forum.API;

public class GetMessageHubItemValidator : AbstractValidator<GetMessageHubItem>
{
    public GetMessageHubItemValidator()
    {
        RuleFor(item => item)
            .Must(item => item.Text.Length is >= 1 and <= 500 || item.FileKey is not null);
    }
}