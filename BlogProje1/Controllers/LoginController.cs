using CoreDemo.Models;
using DataAccessLayer.Contrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignInViewModel user) 
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.username, user.paswword, false, true);
                if (result.Succeeded)               
                    return RedirectToAction("Index","Dashboard");
                else
                    return RedirectToAction("Index", "Login");
            }
            return View();

        }









        //[HttpPost]
        //public  async Task<IActionResult> Index(Writer  writer)
        //{
        //    Context c = new Context();
        //    var dataavalue = c.Writers.FirstOrDefault(x => x.Email == writer.Email && x.Password == writer.Password);
        //    if (dataavalue!= null)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim (ClaimTypes.Name,writer.Email)
        //        };
        //        var userIdentity = new ClaimsIdentity(claims, "a");
        //        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
        //        await HttpContext.SignInAsync(principal);
        //        return RedirectToAction("Index", "Dashboard");
        //    }
        //    else
        //    {
        //       return View();
        //    }   
        //}
    }
}

