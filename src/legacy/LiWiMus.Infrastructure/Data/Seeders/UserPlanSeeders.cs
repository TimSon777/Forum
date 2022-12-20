using LiWiMus.Core.Plans;
using LiWiMus.Core.Plans.Specifications;
using LiWiMus.Core.Users;
using LiWiMus.Core.Users.Enums;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LiWiMus.Infrastructure.Data.Seeders;

// ReSharper disable once UnusedType.Global
public class UserPlanSeeders : ISeeder
{
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Plan> _planRepository;
    private readonly ApplicationContext _applicationContext;

    public UserPlanSeeders(UserManager<User> userManager, IRepository<Plan> planRepository, ApplicationContext applicationContext)
    {
        _userManager = userManager;
        _planRepository = planRepository;
        _applicationContext = applicationContext;
    }

    public async Task SeedAsync(EnvironmentType environmentType)
    {
        const string userName = "MockUser_UserPlan";

        if (await _userManager.FindByNameAsync(userName) is not null)
        {
            return;
        }
        
        switch (environmentType)
        {
            case EnvironmentType.Development:
                var user = new User
                {
                    Email = "mockEmail@mock.mock_UserPlan",
                    UserName = userName,
                    Gender = Gender.Male,
                    Id = 440000
                };

                var result = await _userManager.CreateAsync(user, "Password");

                if (!result.Succeeded)
                {
                    throw new SystemException();
                }

                var plan = await _planRepository.GetByNameAsync(DefaultPlans.Premium.Name);

                if (plan is null)
                {
                    throw new SystemException();
                }
                
                var userPlan = new UserPlan
                {
                    Id = 440000,
                    Plan = plan,
                    Start = DateTime.Now,
                    End = DateTime.Now.PlusYears(),
                    User = user
                };

                _applicationContext.Add(userPlan);
                
                break;
            case EnvironmentType.Production:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
        }
    }

    public int Priority => 35;
}