using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SignageLivePlayer.Client.Dtos;
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
                    return RedirectToAction("Index", "Authentication", new { message = "Unauthorized. Please Login." });
                }
            }
        }

        return View(playerList);
    }

    public async Task<IActionResult> Add()
    {
        IEnumerable<SelectListItem>? siteList = await GetSiteList()!;

        return View(new PlayerViewModel { SiteList = siteList });
    }

    [HttpPost]
    public async Task<IActionResult> Add(PlayerViewModel playerViewModel)
    {
        PlayerCreateDto playerCreateDto = new PlayerCreateDto {
            SiteId = playerViewModel.SiteId,
            PlayerName = playerViewModel.PlayerName,
            CheckInFrequency = playerViewModel.CheckInFrequency
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

        IEnumerable<SelectListItem>? siteList = await GetSiteList()!;

        if (siteList is null) return NoContent();

        PlayerViewModel viewModel = new PlayerViewModel
        {
            PlayerUniqueId = receivedPlayer.PlayerUniqueId,
            CheckInFrequency = receivedPlayer.CheckInFrequency,
            PlayerName = receivedPlayer.PlayerName,
            //SiteId = receivedPlayer.Site!.Id,
            //Site = receivedPlayer.Site,
            SiteList = siteList
        };

        return View(viewModel);
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

        IEnumerable<SelectListItem>? siteList = await GetSiteList()!;

        PlayerViewModel pvm = new PlayerViewModel
        {
            PlayerUniqueId = playerReadDto.PlayerUniqueId,
            CheckInFrequency = playerReadDto.CheckInFrequency,
            PlayerName = playerReadDto.PlayerName,
            SiteId = playerReadDto.Site!.Id,
            Site = playerReadDto.Site,
            SiteList = siteList
        };
        return View(pvm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(PlayerViewModel playerViewModel)
    {
        PlayerUpdateDto playerUpdateDto = new PlayerUpdateDto
        {
            SiteId = playerViewModel.SiteId,
            PlayerName = playerViewModel.PlayerName,
            CheckInFrequency = playerViewModel.CheckInFrequency
        };

        var jwt = Request.Cookies["jwtCookie"];

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            StringContent content = new StringContent(JsonConvert.SerializeObject(playerUpdateDto), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PutAsync("https://localhost:7012/api/Players/" + playerViewModel.PlayerUniqueId, content))
            {
                
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                }
                else
                {
                    TempData["Message"] = response.StatusCode;
                    RedirectToAction("Update");
                }
            }
        }
        IEnumerable<SelectListItem>? siteList = await GetSiteList()!;
        playerViewModel.SiteList = siteList;
        return View(playerViewModel);
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

    private async Task<IEnumerable<SelectListItem>>? GetSiteList()
    {
        var jwt = Request.Cookies["jwtCookie"];

        List<SiteReadDto> siteList = new();

        using (HttpClient httpClient = new())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            using (HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7012/api/Sites"))
            {

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    siteList = JsonConvert.DeserializeObject<List<SiteReadDto>>(apiResponse)!;
                    IEnumerable<SelectListItem> siteListItems = siteList.Select(a => new SelectListItem
                    {
                        Text = a.SiteName,
                        Value = a.Id
                    });
                    return siteListItems;
                }
            }
        }
        return null!;
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
