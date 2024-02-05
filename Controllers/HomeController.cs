using AGH_movie_rent.Data;
using AGH_movie_rent.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AGH_movie_rent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AGH_movie_rentContext _context;

        public HomeController(ILogger<HomeController> logger, AGH_movie_rentContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

        [Authorize (Roles = "Admin")] //[Authorize(Roles ="Admin, Client")]  
        public IActionResult Secured()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;  
            return View(); 
        }


        [HttpPost("login")]
        public async Task<IActionResult> Validate( string username, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var user = await _context.User.FirstOrDefaultAsync(m => m.Email == username );

            if (user.UserId != 0 && BCrypt.Net.BCrypt.Verify(password, user.HashedPasswd) ) { 
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                claims.Add(new Claim(ClaimTypes.Role, user.Role));
                claims.Add(new Claim("userId", user.UserId.ToString()));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect("/"); 
                   
            }
            TempData["Error"] = "Erorr Username or Password is invalid"; 
            return View("login");
        }


        [Authorize]
         public async Task<IActionResult> Logout()
         {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        } 
     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}