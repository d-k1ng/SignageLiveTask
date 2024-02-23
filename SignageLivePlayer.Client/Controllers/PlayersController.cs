using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignageLivePlayer.Api.Data.Dtos;
using SignageLivePlayer.Client.Models;
using System.Diagnostics;

namespace SignageLivePlayer.Client.Controllers;

public class PlayersController : Controller
{
    public async Task<IActionResult> Index()
    {
        List<PlayerReadDto> playerList = new List<PlayerReadDto>();
        using (HttpClient httpClient = new())
        {
            using (HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7012/api/Players"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode) return NotFound();
                playerList = JsonConvert.DeserializeObject<List<PlayerReadDto>>(apiResponse)!;
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
