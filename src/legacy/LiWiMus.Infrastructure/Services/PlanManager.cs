using LiWiMus.Core.Plans;
using LiWiMus.Core.Plans.Exceptions;
using LiWiMus.Core.Plans.Interfaces;
using LiWiMus.Core.Plans.Specifications;
using LiWiMus.Core.Users;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Infrastructure.Services;

public class PlanManager : IPlanManager
{
    private readonly IRepository<Plan> _planRepository;
    private readonly IRepository<UserPlan> _userPlanRepository;
    private readonly IRepository<Permission> _permissionRepository;

    public PlanManager(IRepository<Plan> planRepository,
                       IRepository<Permission> permissionRepository, IRepository<UserPlan> userPlanRepository)
    {
        _planRepository = planRepository;
        _permissionRepository = permissionRepository;
        _userPlanRepository = userPlanRepository;
    }



    public async Task<bool> HasPermissionAsync(Plan plan, Permission permission)
    {
        var permissions = await _permissionRepository.GetByPlanAsync(plan);
        return permissions.Any(p => p.Name == permission.Name);
    }

    public async Task GrantPermissionAsync(Plan plan, Permission permission)
    {
        if (await HasPermissionAsync(plan, permission))
        {
            throw new PlanAlreadyHasPermissionException(plan, permission);
        }

        plan.Permissions.Add(permission);
        await _planRepository.UpdateAsync(plan);
    }

    public async Task GrantPermissionAsync(Plan plan, string permissionName)
    {
        var permission = await _permissionRepository.GetByNameAsync(permissionName);
        if (permission is null)
        {
            throw new PermissionNotFoundException(permissionName);
        }

        await GrantPermissionAsync(plan, permission);
    }

    public async Task<bool> DeleteAsync(Plan plan)
    {
        if (DefaultPlans.GetAll().Select(p => p.Name).Contains(plan.Name))
        {
            throw new DeleteDefaultPlanException();
        }

        await _planRepository.DeleteAsync(plan);
        return true;
    }

    public async Task<List<Plan>> GetByUserAsync(User user)
    {
        var userPlans = await _userPlanRepository.GetActiveAsync(user);
        return userPlans.Select(up => up.Plan).ToList();
    }

    public async Task<List<Plan>> GetAllAsync()
    {
        return await _planRepository.GetAllAsync();
    }

    public async Task<Plan?> GetByIdAsync(int planId)
    {
        return await _planRepository.GetWithPermissionsAsync(planId);
    }
}