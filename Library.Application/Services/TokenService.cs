using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Application.DTOs.AuthDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Library.Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<TokenResponse> GenerateTokensAsync(User user)
    {
        var authClaims = await GetAuthClaimsAsync(user);

        var accessToken = GenerateAccessToken(authClaims);
        var refreshToken = GenerateRefreshToken();
        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        Console.WriteLine(isAdmin);
        Console.WriteLine(user.UserName);
        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            HasAdminRole = isAdmin,
        };
    }

    public async Task<User?> GetUserFromTokenAsync(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
                return null;

            var user = await _userManager.FindByNameAsync(username);
            return user;
        }
        catch (Exception)
        {
            throw new ReadTokenException("Error retrieving user from token");
        }
    }

    private string GenerateAccessToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddSeconds(int.Parse(_configuration["JWT:ExpireInSeconds"]!)),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    private async Task<List<Claim>> GetAuthClaimsAsync(User user)
    {
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        return authClaims;
    }
}