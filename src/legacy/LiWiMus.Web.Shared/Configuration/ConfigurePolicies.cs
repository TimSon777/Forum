using LiWiMus.Core.Plans;
using LiWiMus.Core.Roles;
using Microsoft.AspNetCore.Authorization;

namespace LiWiMus.Web.Shared.Configuration;

public static class ConfigurePolicies
{
    public static void AddPermissionPolicies(this AuthorizationOptions options)
    {
        var systemPermissions = DefaultSystemPermissions.GetAll();
        foreach (var systemPermission in systemPermissions)
        {
            options.AddPolicy(systemPermission.Name, builder =>
            {
                var claim = systemPermission.GetClaim();
                builder.RequireClaim(claim.Type, claim.Value);
            });
        }

        var plansPermissions = DefaultPermissions.GetAll();
        foreach (var plansPermission in plansPermissions)
        {
            options.AddPolicy(plansPermission.Name, builder =>
            {
                var claim = plansPermission.GetClaim();
                builder.RequireClaim(claim.Type, claim.Value);
            });
        }
    }
}