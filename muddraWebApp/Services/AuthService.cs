using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using muddraWebApp.Contexts;
using muddraWebApp.Models.ViewModels;
using System.Security.Claims;

namespace muddraWebApp.Services;

public class AuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RolesService _rolesService;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RolesService rolesService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _rolesService = rolesService;
    }

    public async Task<bool> SignUpAsync(SignUpViewModel model)
    {
        try
        {
            // ------- Init roles, Create user and give user a role
            await _rolesService.SeedRoles();
            var roleName = "user";

            if (!await _userManager.Users.AnyAsync())
                roleName = "admin";

            IdentityUser identityUser = model;
            var result = await _userManager.CreateAsync(identityUser, model.Password);
            
            if (!result.Succeeded)
                return false;
            
            await _userManager.AddToRoleAsync(identityUser, roleName);

            return true;
        }
        catch { return false; }
    }

    public async Task<bool> FindAsync(SignUpViewModel model)
    {
        var _user = await _userManager.FindByEmailAsync(model.Email);

        if (_user != null)
            return true;
        return false;
    }

    public async Task<bool> SignInAsync(SignInViewModel model)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result.Succeeded;
        }
        catch { return false; }
    }

    public async Task<bool> SignOutAsync(ClaimsPrincipal user)
    {
        await _signInManager.SignOutAsync();
        return _signInManager.IsSignedIn(user);
    }
}
