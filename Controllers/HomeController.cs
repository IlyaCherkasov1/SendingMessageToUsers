using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendingMessageToUsers.Entities;
using SendingMessageToUsers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendingMessageToUsers.Controllers
{
    public class HomeController : Controller 
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

     
        public async Task<IActionResult> Index()
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new LoginViewModel
            {
                ExternalProviders = externalProviders
            });
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
