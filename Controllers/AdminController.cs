using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FoodWebAppMvc.Data;
using FoodWebAppMvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FoodWebAppMvc.Interfaces;

namespace FoodWeb.Controllers{
  public class AdminController : Controller
    {
        // AppFoodDbContext db = new AppFoodDbContext();
        private readonly AppFoodDbContext _db;
        // private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAdminRepository _adminRepository;

        public AdminController(AppFoodDbContext db,IAdminRepository adminRepository)
        {
            _db = db;
            _adminRepository = adminRepository;
        }
        // GET: Products
        public ActionResult Index()
        {
                return View();
           
        }  

        [HttpGet]
        public ActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "AdminDashboard");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLogin model)
        {
            var user = await _adminRepository.GetAdminByEmailAndPasswordAsync(model.Email, model.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("AdminEmail", user.Email);
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                      new ClaimsPrincipal(claimsIdentity),
                                      new AuthenticationProperties
                                      {
                                          IsPersistent = true,
                                          ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                                      });
                return RedirectToAction("Index", "AdminDashboard");
            }
            else
            {
                // ViewBag.Message = "Login Failed";
                return View();
            }
        }
    }
}