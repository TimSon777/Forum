using System.Security.Claims;
using LiWiMus.Core.Plans.Interfaces;
using LiWiMus.Core.Roles.Interfaces;
using LiWiMus.Core.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LiWiMus.Infrastructure.Identity;

public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
{
    private readonly IRoleManager _roleManager;
    private readonly IPlanManager _planManager;

    public ApplicationClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor,
                                             IPlanManager planManager, IRoleManager roleManager) : base(
        userManager, optionsAccessor)
    {
        _planManager = planManager;
        _roleManager = roleManager;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        var roles = await _roleManager.GetByUserAsync(user);

        var systemPermissionsClaims = roles
                                      .SelectMany(role => role.Permissions)
                                      .DistinctBy(permission => permission.Name)
                                      .Select(permission => permission.GetClaim());
        identity.AddClaims(systemPermissionsClaims);

        var plans = await _planManager.GetByUserAsync(user);

        var planPermissionsClaims = plans
                                    .SelectMany(p => p.Permissions)
                                    .DistinctBy(permission => permission.Name)
                                    .Select(permission => permission.GetClaim());

        identity.AddClaims(planPermissionsClaims);
        
        return identity;
    }
}