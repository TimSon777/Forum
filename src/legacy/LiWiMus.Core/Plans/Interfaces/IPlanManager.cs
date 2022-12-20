namespace LiWiMus.Core.Plans.Interfaces;

public interface IPlanManager
{

    Task<bool> HasPermissionAsync(Plan plan, Permission permission);
    Task GrantPermissionAsync(Plan plan, Permission permission);
    Task GrantPermissionAsync(Plan plan, string permissionName);

    Task<bool> DeleteAsync(Plan plan);
    Task<List<Plan>> GetByUserAsync(User user);

    Task<List<Plan>> GetAllAsync();
    Task<Plan?> GetByIdAsync(int planId);
}