using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure.Repositories;

public class AccountManager : IAccountManager
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    public AccountManager(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> AddToRoleAsync(User user, string role)
    {
        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        var result = await _userManager.CheckPasswordAsync(user, password);
        return result;
    }

    public async Task<bool> CreateAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded;
    }

    public async Task<User?> FindByNameAsync(string name)
    {
        var user = await _userManager.FindByNameAsync(name);
        return user;
    }

    public async Task<string?> GetAuthenticationTokenAsync(User user, string tokenProvider, string tokenName)
    {
        var token =  await _userManager.GetAuthenticationTokenAsync(user, tokenProvider, tokenName);
        return token;
    }

    public async Task<IList<string>> GetRolesAsync(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles;
    }

    public async Task<bool> IsInRoleAsync(User user, string role)
    {
        var isInRole = await _userManager.IsInRoleAsync(user, role);
        return isInRole;
    }

    public async Task<bool> RemoveAsync(User user)
    {
        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> RemoveAuthenticationTokenAsync(User user, string tokenProvider, string tokenName)
    {
        var result = await _userManager.RemoveAuthenticationTokenAsync(user, tokenProvider, tokenName);
        return result.Succeeded;

    }

    public async Task<bool> SetAuthenticationTokenAsync(User user, string tokenProvider, string tokenName, string tokenValue)
    {
        var result = await _userManager.SetAuthenticationTokenAsync(user, tokenProvider, tokenName, tokenValue);
        return result.Succeeded;
    }
}

