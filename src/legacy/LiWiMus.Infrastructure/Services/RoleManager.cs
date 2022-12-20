using LiWiMus.Core.Roles;
using LiWiMus.Core.Roles.Exceptions;
using LiWiMus.Core.Roles.Interfaces;
using LiWiMus.Core.Roles.Specifications;
using LiWiMus.Core.Users;
using LiWiMus.Infrastructure.Data;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Infrastructure.Services;

public class RoleManager : IRoleManager
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<SystemPermission> _permissionRepository;
    private readonly ApplicationContext _context;
    
    public RoleManager(IRepository<User> userRepository, IRepository<Role> roleRepository,
                       IRepository<SystemPermission> permissionRepository, ApplicationContext context)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _context = context;
    }

    public async Task<bool> IsInRoleAsync(User user, Role role)
    {
        return await IsInRoleAsync(user, role.Name);
    }

    public async Task<bool> IsInRoleAsync(User user, string roleName)
    {
        var roles = await _roleRepository.GetByUserAsync(user);
        return roles.Any(r => r.Name == roleName);
    }

    public async Task AddToRoleAsync(User user, Role role)
    {
        if (await IsInRoleAsync(user, role))
        {
            throw new UserAlreadyInRoleException(user, role);
        }

        user.Roles.Add(role);
        await _userRepository.UpdateAsync(user);
    }

    public async Task AddToRoleAsync(User user, string roleName)
    {
        var role = await _roleRepository.GetByNameAsync(roleName);
        if (role is null)
        {
            throw new RoleNotFoundException(roleName);
        }

        await AddToRoleAsync(user, role);
    }

    public async Task RemoveFromRoleAsync(User user, Role role)
    {
        if (!await IsInRoleAsync(user, role))
        {
            throw new UserNotInRoleException(user, role);
        }

        if (DefaultRoles.Admin.Name == role.Name)
        {
            throw new RemoveFromAdminRoleException();
        }

        if (DefaultRoles.User.Name == role.Name)
        {
            throw new RemoveFromDefaultRoleException();
        }

        if (user.Roles.Count == 0)
        {
            await _context.Entry(user).Collection(u => u.Roles).LoadAsync();
        }
        
        user.Roles.Remove(role);
        await _userRepository.UpdateAsync(user);
    }

    public async Task RemoveFromRoleAsync(User user, string roleName)
    {
        var role = await _roleRepository.GetByNameAsync(roleName);
        if (role is null)
        {
            throw new RoleNotFoundException(roleName);
        }

        await RemoveFromRoleAsync(user, role);
    }

    public async Task<bool> HasPermissionAsync(Role role, SystemPermission permission)
    {
        var permissions = await _permissionRepository.GetByRoleAsync(role);
        return permissions.Any(p => p.Name == permission.Name);
    }

    public async Task GrantPermissionAsync(Role role, SystemPermission permission)
    {
        if (await HasPermissionAsync(role, permission))
        {
            throw new RoleAlreadyHasPermissionException(permission, role);
        }

        role.Permissions.Add(permission);
        await _roleRepository.UpdateAsync(role);
    }

    public async Task GrantPermissionAsync(Role role, string permissionName)
    {
        var permission = await _permissionRepository.GetByNameAsync(permissionName);
        if (permission is null)
        {
            throw new SystemPermissionNotFoundException(permissionName);
        }

        await GrantPermissionAsync(role, permission);
    }

    public async Task<List<Role>> GetByUserAsync(User user)
    {
        return await _roleRepository.GetByUserAsync(user);
    }
}