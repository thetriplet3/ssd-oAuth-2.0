using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using oAuthTwo.Models;

namespace oAuthTwo.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
               
                return RedirectToAction("Profile", "Home", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        public IActionResult Profile()
        {
            GetPostsAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task GetPostsAsync()
        {
            string token = await HttpContext.GetTokenAsync("Google", "access_token");

            Uri apiRequestUri = new Uri("https://content.googleapis.com/plus/v1/people/me/activities/public");
            //request profile image
            using (var webClient = new System.Net.WebClient())
            {

                try
                {
                    webClient.Headers.Add("Authorization", "Bearer " + token);
                    var json = webClient.DownloadString(apiRequestUri);
                    dynamic result = JsonConvert.DeserializeObject(json);
                }
                catch (WebException e)
                {
                    Console.WriteLine(e.Source);
                    using (var reader = new StreamReader(e.Response.GetResponseStream()))
                    {
                        string response = reader.ReadToEnd();
                    }
                    throw;
                }
            }
        }
    }
}
