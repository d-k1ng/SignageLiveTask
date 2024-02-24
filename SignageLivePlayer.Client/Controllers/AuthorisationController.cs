using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignageLivePlayer.Api.Authentication.Requests;
using SignageLivePlayer.Api.Authentication.Responses;

namespace SignageLivePlayer.Client.Controllers;

public class AuthorisationController : Controller
{
    public IActionResult Index(string message)
    {
        ViewBag.Message = message;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string email, string password)
    {
        AuthenticationResponse res;
        using (HttpClient httpClient = new())
        {
            var req = new LoginRequest(Email: email, Password: password);

            using (HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7012/api/Authentication/login", req))
            {

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<AuthenticationResponse>(apiResponse)!;
                    //retrieve jwt cookie and store
                    var accessToken = res.Token;
                    SetJWTCookie(accessToken);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Index", new { message = "Invalid Credentials" });
                }
                
            }
        }
        return RedirectToAction("Index", "Players");
    }

    public IActionResult Register(string message)
    {
        ViewBag.Message = message;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string email, string password, string firstName, string lastName)
    {
        AuthenticationResponse res;
        using (HttpClient httpClient = new())
        {
            var req = new RegisterRequest(FirstName: firstName, LastName: lastName, Email: email, Password: password);

            using (HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7012/api/Authentication/register", req))
            {

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<AuthenticationResponse>(apiResponse)!;
                    //retrieve jwt cookie and store
                    var accessToken = res.Token;
                    SetJWTCookie(accessToken);
                    return RedirectToAction("Index", "Players");
                }
            }
        }
        return RedirectToAction("Index", new { message = "Invalid Credentials" });
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
