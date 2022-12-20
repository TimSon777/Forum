using LiWiMus.Core.Users;
using Microsoft.AspNetCore.Identity;

namespace LiWiMus.Infrastructure.Identity;

public class ApplicationUserValidator : UserValidator<User>
{
    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var result = await base.ValidateAsync(manager, user);
        var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

        switch (user.UserName.Length)
        {
            case > 20:
                errors.Add(new IdentityError
                {
                    Description = "The length of the username must not exceed 20 characters"
                });
                break;
            case < 3:
                errors.Add(new IdentityError
                {
                    Description = "The username must be at least 3 characters long"
                });
                break;
        }

        var russianChars = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();
        if (user.UserName.Any(c => russianChars.Contains(c)))
        {
            errors.Add(new IdentityError
            {
                Description = "Russian characters are prohibited"
            });
        }

        return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
    }
}