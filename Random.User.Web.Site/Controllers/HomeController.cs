using System.Diagnostics;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Random.User.Web.Site.Helpers;
using Random.User.Web.Site.Models;

namespace Random.User.Web.Site.Controllers;

public sealed class HomeController(IHttpClientFactory clientFactory, ILogger<HomeController> logger) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var client = clientFactory.CreateClient("RandomUserApi");
            var users = await client.GetFromJsonAsync<List<RootObject>>("user?page=1", cancellationToken);
            return View(users ?? []);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or System.Text.Json.JsonException)
        {
            logger.LogWarning(ex, "The user API is unavailable.");
            ViewData["ApiError"] = "The API is unavailable. Start Random.User.Rest and refresh the page.";
            return View(new List<RootObject>());
        }
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
    });
}
