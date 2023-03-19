using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FoodWebAppMvc.Data;
using FoodWebAppMvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;



namespace FoodWebAppMvc.Controllers{
  // [Authorize(AuthenticationSchemes = "AdminCookie")]
  // [Authorize(Roles = "Admin")]
  public class AdminDashboardController : Controller{
    // [Authorize(Roles = "Admin")]
    

    public IActionResult Index(){
      return View();
    }

    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index","User");
    }
  } 
}