using Library.Domain.Entities;

namespace Library.Domain.Interfaces.Repositories;

public interface IAccountManager
{
    Task<User?> FindByNameAsync(string name);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<bool> CreateAsync(User user,string password);
    Task<bool> RemoveAsync(User user);
    Task<bool> AddToRoleAsync(User user, string role);
    Task<bool> IsInRoleAsync(User user, string role);
    Task<IList<string>> GetRolesAsync(User user);
    Task<string?> GetAuthenticationTokenAsync(User user, string tokenProvider, string tokenName);
    Task<bool> SetAuthenticationTokenAsync(User user, string tokenProvider, string tokenName, string tokenValue);
    Task<bool> RemoveAuthenticationTokenAsync(User user, string tokenProvider, string tokenName);
}
