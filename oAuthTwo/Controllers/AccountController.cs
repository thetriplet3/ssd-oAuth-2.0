using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace oAuthTwo.Models
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Login(string returnUrl = "/")
        //{
        //    return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });

        //}
        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Google", new AuthenticationProperties() { RedirectUri = returnUrl });
        }
        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            });
        }

    }
}