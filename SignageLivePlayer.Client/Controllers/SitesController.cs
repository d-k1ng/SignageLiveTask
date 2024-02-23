using Microsoft.AspNetCore.Mvc;

namespace SignageLivePlayer.Client.Controllers;

public class SitesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
