using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignageLivePlayer.Client.Dtos;
using SignageLivePlayer.Client.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace SignageLivePlayer.Client.Controllers;

public class SitesController : Controller
{
    public async Task<IActionResult> Index()
    {
        var jwt = Request.Cookies["jwtCookie"];

        List<SiteReadDto> siteList = new List<SiteReadDto>();

        using (HttpClient httpClient = new())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            using (HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7012/api/Sites"))
            {

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    siteList = JsonConvert.DeserializeObject<List<SiteReadDto>>(apiResponse)!;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Index", "Authentication", new { message = "Unauthorized. Please Login." });
                }
            }
        }
        return View(siteList);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(SiteViewModel siteViewModel)
    {
        SiteCreateDto siteCreateDto = new SiteCreateDto
        {
                SiteName = siteViewModel.SiteName,
                SiteAddress1 = siteViewModel.SiteAddress1!,
                SiteAddress2 = siteViewModel.SiteAddress2!,
                SiteTown = siteViewModel.SiteTown!,
                SiteCounty = siteViewModel.SiteCounty!,
                SitePostcode = siteViewModel.SitePostcode!,
                SiteCountry = siteViewModel.SiteCountry!
        };

        var jwt = Request.Cookies["jwtCookie"];

        SiteReadDto receivedSite = new SiteReadDto();
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            StringContent content = new StringContent(JsonConvert.SerializeObject(siteCreateDto), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync("https://localhost:7012/api/Sites", content))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedSite = JsonConvert.DeserializeObject<SiteReadDto>(apiResponse)!;
                }
                else
                {
                    TempData["Message"] = response.StatusCode;
                    RedirectToAction("Add"); ViewBag.StatusCode = response.StatusCode;
                }
            }
        }
        siteViewModel.Id = receivedSite.Id;
        siteViewModel.DateCreated = receivedSite.DateCreated;
        return View(siteViewModel);
    }

    public async Task<IActionResult> Update(string id)
    {
        var jwt = Request.Cookies["jwtCookie"];

        SiteReadDto siteReadDto = new SiteReadDto();
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            using (var response = await httpClient.GetAsync("https://localhost:7012/api/Sites/" + id))
            {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    siteReadDto = JsonConvert.DeserializeObject<SiteReadDto>(apiResponse)!;
            }
        }

        SiteViewModel siteViewModel = new SiteViewModel
        {
            Id = siteReadDto.Id,
            SiteName = siteReadDto.SiteName,
            SiteAddress1 = siteReadDto.SiteAddress1,
            SiteAddress2 = siteReadDto.SiteAddress2,
            SiteTown = siteReadDto.SiteTown,
            SiteCounty = siteReadDto.SiteCounty,
            SitePostcode = siteReadDto.SitePostcode,
            SiteCountry = siteReadDto.SiteCountry
        };

        return View(siteViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(SiteViewModel siteViewModel)
    {
        SiteUpdateDto siteUpdateDto = new SiteUpdateDto
        {
            SiteName = siteViewModel.SiteName,
            SiteAddress1 = siteViewModel.SiteAddress1!,
            SiteAddress2 = siteViewModel.SiteAddress2!,
            SiteTown = siteViewModel.SiteTown!,
            SiteCounty = siteViewModel.SiteCounty!,
            SitePostcode = siteViewModel.SitePostcode!,
            SiteCountry = siteViewModel.SiteCountry!
        };

        var jwt = Request.Cookies["jwtCookie"];

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            StringContent content = new StringContent(JsonConvert.SerializeObject(siteUpdateDto), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PutAsync("https://localhost:7012/api/Sites/" + siteViewModel.Id, content))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                }
                else
                {
                    TempData["Message"] = response.StatusCode;
                }

                
            }
        };
        
        return View(siteViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var jwt = Request.Cookies["jwtCookie"];
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            using (var response = await httpClient.DeleteAsync("https://localhost:7012/api/Sites/" + id))
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
