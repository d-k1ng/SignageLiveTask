using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignageLivePlayer.Client.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

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
                    return RedirectToAction("Index", "Authorisation", new { message = "Unauthorized. Please Login." });
                }
            }
        }
        return View(playerList);
    }

    public async Task<IActionResult> Add()
    {
        var jwt = Request.Cookies["jwtCookie"];

        using (HttpClient httpClient = new())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            using (HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7012/api/Sites"))
            {

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    TempData["SiteList"] = apiResponse;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Index", "Authorisation", new { message = "Unauthorized. Please Login." });
                }
            }
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(string PlayerName, string SiteId, int CheckInFrequency)
    {
        PlayerCreateDto playerCreateDto = new PlayerCreateDto {
            SiteId = SiteId,
            PlayerName = PlayerName,
            CheckInFrequency = CheckInFrequency
        };

        var jwt = Request.Cookies["jwtCookie"];

        PlayerReadDto receivedPlayer = new PlayerReadDto();
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            StringContent content = new StringContent(JsonConvert.SerializeObject(playerCreateDto), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("https://localhost:7012/api/Players", content))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedPlayer = JsonConvert.DeserializeObject<PlayerReadDto>(apiResponse)!;
                }
                else
                {
                    TempData["Message"] = response.StatusCode;
                    RedirectToAction("Add");
                }
            }
        }
        return View(receivedPlayer);
    }

    public async Task<IActionResult> Update(string id)
    {
        var jwt = Request.Cookies["jwtCookie"];

        PlayerReadDto playerReadDto = new PlayerReadDto();
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            using (var response = await httpClient.GetAsync("https://localhost:7012/api/Players/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                playerReadDto = JsonConvert.DeserializeObject<PlayerReadDto>(apiResponse)!;
            }
        }
        PlayerUpdateModel pum = new PlayerUpdateModel
        {
            PlayerUniqueId = playerReadDto.PlayerUniqueId,
            CheckInFrequency = playerReadDto.CheckInFrequency,
            PlayerName = playerReadDto.PlayerName,
            SiteId = playerReadDto.Site!.Id
        };
        return View(pum);
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, string PlayerUniqueId, string PlayerName, string SiteId, int CheckInFrequency)
    {
        PlayerUpdateDto playerUpdateDto = new PlayerUpdateDto
        {
            SiteId = SiteId,
            PlayerName = PlayerName,
            CheckInFrequency = CheckInFrequency
        };

        var jwt = Request.Cookies["jwtCookie"];

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            StringContent content = new StringContent(JsonConvert.SerializeObject(playerUpdateDto), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PutAsync("https://localhost:7012/api/Players/" + id, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                ViewBag.Result = "Success";
            }
        }

        PlayerUpdateModel pum = new PlayerUpdateModel { 
            PlayerUniqueId = PlayerUniqueId,
            CheckInFrequency = CheckInFrequency,
            PlayerName = PlayerName,
            SiteId = SiteId
        };
        return View(pum);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var jwt = Request.Cookies["jwtCookie"];
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            using (var response = await httpClient.DeleteAsync("https://localhost:7012/api/Players/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
        }

        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
