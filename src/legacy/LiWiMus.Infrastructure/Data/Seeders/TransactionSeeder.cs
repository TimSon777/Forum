using LiWiMus.Core.Transactions;
using LiWiMus.Core.Users;
using LiWiMus.Core.Users.Enums;
using LiWiMus.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LiWiMus.Infrastructure.Data.Seeders;

// ReSharper disable once UnusedType.Global
public class TransactionSeeder : ISeeder
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationContext _applicationContext;

    public TransactionSeeder(UserManager<User> userManager, ApplicationContext applicationContext)
    {
        _userManager = userManager;
        _applicationContext = applicationContext;
    }
    
    public async Task SeedAsync(EnvironmentType environmentType)
    {
        const string userName = "MockUser_Trans";
        if (await _userManager.FindByNameAsync(userName) is not null)
        {
            return;
        }
        
        switch (environmentType)
        {
            case EnvironmentType.Development:
                var user = new User
                {
                    Id = 40000,
                    UserName = userName,
                    Gender = Gender.Male,
                    AvatarLocation = "Location",
                    Email = "mockEmail@mock.mock_Trans"
                };

                var result = await _userManager.CreateAsync(user, "Password");

                if (!result.Succeeded)
                {
                    throw new SystemException();
                }
                
                var transaction = new Transaction
                {
                    Id = 1000,
                    Amount = -100,
                    Description = "Description",
                    User = user
                };

                _applicationContext.Add(transaction);
                break;
            case EnvironmentType.Production:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
        }
    }

    public int Priority => 20;
}