using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnalysisData.JwtService.abstractions;
using AnalysisData.Repository.RoleRepository.Abstraction;
using AnalysisData.Repository.UserRepository.Abstraction;
using Microsoft.IdentityModel.Tokens;

namespace AnalysisData.JwtService;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    
    public JwtService(IConfiguration configuration , IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<string> GenerateJwtToken(string userName)
    {
        var user =await _userRepository.GetUser(userName);
        var claims = new List<Claim>
        {
            new Claim("username", userName),
            new Claim("email", user.Email),
            new Claim("firstname", user.FirstName),
            new Claim("lastname", user.LastName),
            new Claim("phone-number", user.PhoneNumber),
            new Claim("email", user.Email),
            new Claim("role", user.Role.RoleName),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}