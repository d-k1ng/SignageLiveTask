using SignageLivePlayer.Api.Data.Models;
using SignageLivePlayer.Api.Data.Repositories.Interfaces;

namespace SignageLivePlayer.Api.Authentication;

public class AuthenticationService(IJwtTokenGenerator _jwtTokenGenerator, IUserRepository _userRepository) : IAuthenticationService
{


    public AuthenticationResult Register(string email, string password, string firstName, string lastName)
    {

        //validate email doesnt exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return new AuthenticationResult(new User(), "", true, new Exception("Duplicate Email")); //Errors.User.DuplicateEmail;
        }

        User user = new User { Email = email, Password = password, FirstName = firstName, LastName = lastName };
        _userRepository.Add(user);
        _userRepository.SaveChanges();
        string token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token, false, null!);
    }

    public AuthenticationResult Login(string email, string password)
    {
        User? user = _userRepository.GetUserByEmail(email);

        //validate user exists
        if (user is null) return new AuthenticationResult(new User(), "", true, new Exception("Invalid Credentials"));//Errors.Authentication.InvalidCredentials;

        //validate password
        if (!password.Equals(user.Password)) return new AuthenticationResult(new User(), "",true, new Exception("Invalid Credentials")); //Errors.Authentication.InvalidCredentials;
  
        //create jwt
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token, false, null!);
    }
}
