using LiWiMus.Core.Roles;
using LiWiMus.Core.Roles.Interfaces;
using LiWiMus.Core.Settings;
using LiWiMus.Core.Users;
using LiWiMus.Core.Users.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LiWiMus.Infrastructure.Data.Seeders;

// ReSharper disable once UnusedType.Global
public class UserSeeder : ISeeder
{
    private readonly UserManager<User> _userManager;
    private readonly IRoleManager _roleManager;
    private readonly AdminSettings _adminSettings;
    private readonly ApplicationContext _applicationContext;

    public UserSeeder(UserManager<User> userManager, IOptions<AdminSettings> adminSettingsOptions,
        IRoleManager roleManager, ApplicationContext applicationContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _applicationContext = applicationContext;
        _adminSettings = adminSettingsOptions.Value;
    }

    private async Task<User> CreateUserAndThrowWhenNotSucceedAsync(string email, string userName, int id)
    {
        var user = new User
        {
            Id = id,
            UserName = userName,
            Gender = Gender.Male,
            Email = email,
        };

        var result = await _userManager.CreateAsync(user, "Password");

        if (!result.Succeeded)
        {
            throw new SystemException();
        }

        return user;
    }

    public async Task SeedAsync(EnvironmentType environmentType)
    {
        await SeedAdminAsync();
        const string userName1 = "MockUser1_User";

        if (await _userManager.FindByNameAsync(userName1) is not null)
        {
            return;
        }

        switch (environmentType)
        {
            case EnvironmentType.Development:
                var user = await CreateUserAndThrowWhenNotSucceedAsync("mockEmail1@mock.mock_User", userName1, 380000);
                await CreateUserAndThrowWhenNotSucceedAsync("mockEmail2@mock.mock_User", "MockUser2_User", 380001);
                await CreateUserAndThrowWhenNotSucceedAsync("mockEmail3@mock.mock_User", "MockUser3_User", 380002);
                await CreateUserAndThrowWhenNotSucceedAsync("mockEmail4@mock.mock_User", "MockUser4_User", 380003);
                var user5 = await CreateUserAndThrowWhenNotSucceedAsync("mockEmail5@mock.mock_User", "MockUser5_User", 380004);
                var mockAdmin = await CreateUserAndThrowWhenNotSucceedAsync("mockEmail6@mock.mock_User", "MockUser6_User", 380005);
                await _roleManager.AddToRoleAsync(mockAdmin, DefaultRoles.Admin.Name);
                
                var role = new Role
                {
                    Id = 380000,
                    Name = "MockRole_User",
                    Description = "About"
                };

                _applicationContext.Add(role);
                await _roleManager.AddToRoleAsync(user, role);
                await _roleManager.AddToRoleAsync(user5, role);
                break;
            case EnvironmentType.Production:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
        }
    }

    private async Task SeedAdminAsync()
    {
        var oldAdmin = await _userManager.FindByNameAsync(_adminSettings.UserName);
        if (oldAdmin is not null)
        {
            return;
        }

        var admin = new User
        {
            UserName = _adminSettings.UserName,
            Email = _adminSettings.Email,
            EmailConfirmed = true
        };

        var user = await _userManager.FindByEmailAsync(admin.Email);

        if (user == null)
        {
            var result = await _userManager.CreateAsync(admin, _adminSettings.Password);

            if (!result.Succeeded)
            {
                throw new SystemException();
            }
        }
        else
        {
            admin = user;
        }

        await _userManager.UpdateAsync(admin);
        await _roleManager.AddToRoleAsync(admin, DefaultRoles.Admin.Name);
    }

    public int Priority => 80;
}