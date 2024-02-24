using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignageLivePlayer.Api.Data.Dtos;
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
                    return RedirectToAction("Index", "Authorisation", new { message = "Unauthorized. Please Login." });
                }
            }
        }
        return View(siteList);
    }

    public IActionResult Add(string message)
    {
        ViewBag.Message = message;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(   string SiteName,
                                            string SiteAddress1,
                                            string SiteAddress2,
                                            string SiteTown,
                                            string SiteCounty,
                                            string SitePostcode,
                                            string SiteCountry)
    {
        SiteCreateDto siteCreateDto = new SiteCreateDto
        {
                SiteName = SiteName,
                SiteAddress1 = SiteAddress1,
                SiteAddress2 = SiteAddress2,
                SiteTown = SiteTown,
                SiteCounty = SiteCounty,
                SitePostcode = SitePostcode,
                SiteCountry = SiteCountry
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
                    ViewBag.StatusCode = response.StatusCode;
                    RedirectToAction("Add", new { message = "Invalid Credentials" });
                }
            }
        }
        return View(receivedSite);
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
        return View(siteReadDto);
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id,
                                            string SiteName,
                                            string SiteAddress1,
                                            string SiteAddress2,
                                            string SiteTown,
                                            string SiteCounty,
                                            string SitePostcode,
                                            string SiteCountry)
    {
        SiteUpdateDto siteUpdateDto = new SiteUpdateDto
        {
            SiteName = SiteName,
            SiteAddress1 = SiteAddress1,
            SiteAddress2 = SiteAddress2,
            SiteTown = SiteTown,
            SiteCounty = SiteCounty,
            SitePostcode = SitePostcode,
            SiteCountry = SiteCountry
        };

        var jwt = Request.Cookies["jwtCookie"];

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            StringContent content = new StringContent(JsonConvert.SerializeObject(siteUpdateDto), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PutAsync("https://localhost:7012/api/Sites/" + id, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                ViewBag.Result = "Success";
            }
        };
        SiteReadDto siteReadDto = new SiteReadDto
        {
            SiteName = SiteName,
            SiteAddress1 = SiteAddress1,
            SiteAddress2 = SiteAddress2,
            SiteTown = SiteTown,
            SiteCounty = SiteCounty,
            SitePostcode = SitePostcode,
            SiteCountry = SiteCountry
        };
        return View(siteReadDto);
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
