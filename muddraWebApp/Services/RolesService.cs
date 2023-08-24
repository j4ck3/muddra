using Microsoft.AspNetCore.Identity;

namespace muddraWebApp.Services;

public class RolesService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedRoles()
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            var role = new IdentityRole("Admin");
            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            await _roleManager.CreateAsync(role);
        }
        if (!await _roleManager.RoleExistsAsync("User"))
        {
            var role = new IdentityRole("User");
            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            await _roleManager.CreateAsync(role);
        }
    }
}

