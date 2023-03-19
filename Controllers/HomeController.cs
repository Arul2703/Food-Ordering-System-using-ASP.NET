using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FoodWebAppMvc.Data;
using FoodWebAppMvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using FoodWebAppMvc.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace FoodWebAppMvc.Controllers;

// [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMenuRepository _db;
    private readonly IMemoryCache _cache;

    public HomeController(ILogger<HomeController> logger, IMenuRepository db, IMemoryCache cache)
    {
        _logger = logger;
        _db = db;
        _cache = cache;
    }

    // public ActionResult Index()
    // {
    //      _logger.LogInformation("Index action of Home controller is called");
    //     List<Products> products = _db.GetAllProducts();
    //     return View(products);
           
    // }
    




    public IActionResult Privacy()
    {
        return View();
    }

    public ActionResult ContactUs()
    {
            return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ContactUs(ContactModel contact)
    {
        return View();            
    }
    public ActionResult Gallery(){ 
        _logger.LogInformation("Gallery Action called");       
        return View();
    }
    public ActionResult AboutUs()
    {
         _logger.LogInformation("About Us Action called");   
        return View();
    }
    public ActionResult Blogs()
    {
        _logger.LogInformation("Blogs Action called"); 
        return View();
    }
    public async Task<ActionResult> Logout()
    {
        _logger.LogInformation("User Logged out");
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index","User");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
