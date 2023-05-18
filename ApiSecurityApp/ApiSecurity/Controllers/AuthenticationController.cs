using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;
    public AuthenticationController(IConfiguration config) 
    { 
        _config = config;
    }


    // Records = readonly Classes
    public record AuthenticationData(string? UserName, string? Password);
    public record UserData(int UserId, string UserName);

    //api/Authentication/token
    [HttpPost("token")]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        var user = ValidateCredentials(data);

        if(user is null)
            return Unauthorized();
        //TODO: Finalizar Método
        throw new NotImplementedException();
    }

    private string GenerateToken(UserData user)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                _config.GetValue<string>("Authentication:SecretKey")));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //TODO: Finalizar Método
        throw new NotImplementedException();
    }

    private UserData? ValidateCredentials(AuthenticationData data)
    {
        // THIS IS NOT PRODUCTION CODE - THIS IS ONLY A DEMO - DO NOT USE IN REAL LIFE 
        if (CompareValues(data.UserName, "egleticia")
            && CompareValues(data.Password, "Test123"))
        {
            return new UserData(1, data.UserName!);
        }

        if (CompareValues(data.UserName, "tcorey")
           && CompareValues(data.Password, "Test123"))
        {
            return new UserData(2, data.UserName!);
        }
        return null;
    }

    private bool CompareValues(string? actual, string expected)
    {
        if (actual is not null)
        {
            if (actual.Equals(expected))
            {
                return true;
            }
        }
        return false;
    }
}
