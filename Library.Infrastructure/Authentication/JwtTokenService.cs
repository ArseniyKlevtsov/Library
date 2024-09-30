using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;
using Library.Domain.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Infrastructure.Authentication;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _loginProvider;
    private readonly string _refreshTokenName = "RefreshToken";

    public JwtTokenService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        _loginProvider = _configuration["App:Name"]!;
    }


    public async Task<TokenResponse> GenerateTokensAsync(User user)
    {
        var authClaims = await GetAuthClaimsAsync(user);

        var accessToken = GenerateAccessToken(authClaims);
        var refreshToken = GenerateRefreshToken();
        var isAdmin = await _unitOfWork.AccountManager.IsInRoleAsync(user, "Admin");
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

            var user = await _unitOfWork.AccountManager.FindByNameAsync(username);
            return user;
        }
        catch (Exception)
        {
            return null;
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

        var userRoles = await _unitOfWork.AccountManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        return authClaims;
    }

    public async Task SetRefreshTokenAsync(User user, string refreshToken)
    {
        await _unitOfWork.AccountManager.RemoveAuthenticationTokenAsync(user, _loginProvider, _refreshTokenName);
        await _unitOfWork.AccountManager.SetAuthenticationTokenAsync(user, _loginProvider, _refreshTokenName, refreshToken);
    }
}
