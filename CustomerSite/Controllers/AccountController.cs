using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Encodings.Web;

namespace CustomerSite.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
        
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "oidc");
        }

        public new IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, "Cookies", "oidc");
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            return View();
        }
    }
}