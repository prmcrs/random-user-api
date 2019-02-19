using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Random.User.Web.Site.Helpers;
using Random.User.Web.Site.Infrastructure;
using Random.User.Web.Site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Random.User.Web.Site.Controllers
{
    public class HomeController : Controller
    {
        public readonly ConfigurationOptions configuration;
        public HomeController(ConfigurationOptions configuration)
        {
            this.configuration = configuration;
        }


        public IActionResult Index()
        {
            List<RootObject> ppl = new List<RootObject>();
            Uri generatorUri = new Uri(configuration.ApiURI + "user?page=1");
            var client = new WebClient();
            string data = client.DownloadString(generatorUri.AbsoluteUri);
            List<RootObject> rs = JsonConvert.DeserializeObject<List<RootObject>>(data);

            if (rs.Count != 0)
            {
                foreach (var i in rs)
                {
                    ppl.Add(i);
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            return View(ppl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
