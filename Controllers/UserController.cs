using FoodWebAppMvc.Data;
using FoodWebAppMvc.Interfaces;
using FoodWebAppMvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.Cookies;
// using System.Security.Claims;

namespace FoodWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly AppFoodDbContext _db;
         private readonly IUserRepository _userRepository;

        public UserController(AppFoodDbContext db,IUserRepository userRepository)
        {
            _db = db;
             _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signup(SignupLogin signup)
        {
            if (ModelState.IsValid)
            {
                var isEmailAlreadyExists = await _userRepository.IsEmailAlreadyExistsAsync(signup.Email);
                if (isEmailAlreadyExists)
                {
                    ViewBag.Message = "Email Already Registered. Please Try Again With Another Email";
                    return View();
                }
                else
                {
                    await _userRepository.AddUserAsync(signup);
                    return RedirectToAction("Login", "User");
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Menu");
            }
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignupLogin model)
        {
            var user = await _userRepository.GetUsersByEmailAndPasswordAsync(model.Email, model.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.Name);
                List<Claim> claims = new List<Claim>(){
                    new Claim(ClaimTypes.NameIdentifier,model.Email),
                    new Claim("OtherProperties","User Role")

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties(){
                    AllowRefresh = true,
                    IsPersistent = model.RememberMe
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new  ClaimsPrincipal(claimsIdentity),properties);

                return RedirectToAction("Index", "Menu");
            }
            else
            {
                ViewBag.Message = "Login Failed";
                return View();
            }
        }
    }
}
