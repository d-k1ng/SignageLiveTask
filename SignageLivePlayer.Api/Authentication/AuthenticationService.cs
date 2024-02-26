using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SignageLivePlayer.Api.Authentication;

public class AuthenticationService(IJwtTokenGenerator _jwtTokenGenerator, IUserRepository _userRepository) : IAuthenticationService
{
    /*
     * Register a new user checking if the supplied email already exists
     * Return a Result to the calling controller that includes a jwt token
     */
    public AuthenticationResult Register(string email, string password, string firstName, string lastName)
    {

        //validate email doesnt exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return new AuthenticationResult(new User(), "", true, new Exception("Duplicate Email"));
        }

        User user = new User { Email = email, Password = password, FirstName = firstName, LastName = lastName };
        _userRepository.Add(user);
        _userRepository.SaveChanges();
        string token = _jwtTokenGenerator.GenerateToken(user, GetClaims(user));

        return new AuthenticationResult(user, token, false, null!);
    }

    /*
     * Handle user login returning a result with a generated jwt token
     * 
     */
    public AuthenticationResult Login(string email, string password)
    {
        User? user = _userRepository.GetUserByEmail(email);

        //validate user exists
        if (user is null) return new AuthenticationResult(new User(), "", true, new Exception("Invalid Credentials"));

        //validate password
        if (!password.Equals(user.Password)) return new AuthenticationResult(new User(), "", true, new Exception("Invalid Credentials"));
  
        //create jwt
        var token = _jwtTokenGenerator.GenerateToken(user, GetClaims(user));

        return new AuthenticationResult(user, token, false, null!);
    }

    /*
     *  retrieve claims for user including roles from UserRoles
     */
    private Claim[] GetClaims(User user)
    {
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        ];

        List<string> roles = _userRepository.GetUserRoles(user.Id);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return [.. claims];
    }
}
