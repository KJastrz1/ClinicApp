using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Domain.Models.Accounts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ClinicApp.Infrastructure.Authentication;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(Account account)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, account.Id.Value.ToString()), 
            new(JwtRegisteredClaimNames.Email, account.Email.Value) 
        };
   
        IEnumerable<Claim> roleClaims = account.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name.Value));
        claims.AddRange(roleClaims);
     
        IEnumerable<Claim> permissionClaims = account.Roles
            .SelectMany(role => role.Permissions)
            .Select(permission => new Claim("permission", permission.Name));
        claims.AddRange(permissionClaims);
    
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
   
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials);
      
        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
}
