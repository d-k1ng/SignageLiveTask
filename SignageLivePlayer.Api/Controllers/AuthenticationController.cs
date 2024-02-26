using Microsoft.AspNetCore.Mvc;
using SignageLivePlayer.Api.Authentication;
using SignageLivePlayer.Api.Authentication.Requests;
using SignageLivePlayer.Api.Authentication.Responses;
namespace SignageLivePlayer.Api.Controllers;

/*
 * 
 *  /api/Authentication
 *  
 *  POST /register
 *  POST /login
 * 
 */

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IAuthenticationService _authService) : ControllerBase
{

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        AuthenticationResult result = _authService.Register(request.Email,
                                                            request.Password,
                                                            request.FirstName,
                                                            request.LastName);

        if (result.IsError) return Problem(statusCode: StatusCodes.Status401Unauthorized, title: result.Ex.Message);

        SetJWTCookie(result.Token);

        return Ok(new AuthenticationResponse(result.User.Id, result.User.Email, result.User.FirstName,result.User.LastName, result.Token));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = _authService.Login(request.Email, request.Password);

        if (result.IsError) return Problem(statusCode: StatusCodes.Status401Unauthorized, title: result.Ex.Message);

        SetJWTCookie(result.Token);

        return Ok(new AuthenticationResponse(result.User.Id, result.User.Email, result.User.FirstName, result.User.LastName, result.Token));
    }

    private void SetJWTCookie(string token)
    {
        CookieOptions cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddMinutes(30),
        };
        Response.Cookies.Append("jwtCookie", token, cookieOptions);
    }
}
