using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignageLivePlayer.Api.Data.Dtos;
using SignageLivePlayer.Client.Models;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace SignageLivePlayer.Client.Controllers;

public class PlayersController : Controller
{
    public async Task<IActionResult> Index()
    {

        var jwt = Request.Cookies["jwtCookie"];

        List<PlayerReadDto> playerList = new List<PlayerReadDto>();

        using (HttpClient httpClient = new())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            using (HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7012/api/Players"))
            {

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    playerList = JsonConvert.DeserializeObject<List<PlayerReadDto>>(apiResponse)!;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Index", "Authorisation", new { message = "Please Login again" });
                }
            }
        }
        return View(playerList);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
