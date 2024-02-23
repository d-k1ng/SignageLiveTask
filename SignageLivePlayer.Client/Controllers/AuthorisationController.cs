using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignageLivePlayer.Api.Authentication.Requests;
using SignageLivePlayer.Api.Authentication.Responses;
using SignageLivePlayer.Api.Data.Dtos;
using System.ComponentModel.DataAnnotations;

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
            var req = new LoginRequest(Email: "admin@admin.admin", Password: "admin");

            using (HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://localhost:7012/api/Authentication/login", req))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode) return View((object)"Login Failed");
                res = JsonConvert.DeserializeObject<AuthenticationResponse>(apiResponse)!;
                //retrieve jwt cookie and store
                var accessToken = res.Token;
                SetJWTCookie(accessToken);
            }
        }
        return RedirectToAction("Index", "Players");

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
