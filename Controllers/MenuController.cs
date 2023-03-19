using FoodWebAppMvc.Data;
using FoodWebAppMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using FoodWebAppMvc.Interfaces;

namespace FoodWeb.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuRepository _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MenuController> _logger;
        private readonly IMemoryCache _cache;

        public MenuController(ILogger<MenuController> logger, IMenuRepository db, IWebHostEnvironment webHostEnvironment,IMemoryCache cache)
        {
            _logger = logger;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _cache = cache;
        }

        // public ActionResult Index()
        // {
        //     try
        //     {
        //         List<Products> products = _db.Products.ToList<Products>();
        //         _logger.LogInformation("Retrieved {count} products from database", products.Count);
        //         return View(products);
        //     }
        //     catch (Exception ex)
        //     {
        //         return View("Error", new { message = ex.Message });
        //     }
        // }

        [ServiceFilter(typeof(CachingFilter))]
    public ActionResult Index()
    {

        _logger.LogInformation("Index action of Home controller is called");

        // Try to get the cached result first
        if (_cache.TryGetValue("cached_products", out List<Products> cachedResult))
        {
            // If cached result exists, return it
            _logger.LogInformation("Products details are fetched using Cache Filter");
            return View(cachedResult);
        }

        // Otherwise, fetch data from database and cache it
        List<Products> products = _db.GetAllFoodItems();
        _cache.Set("cached_products", products, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(20)));

        // ViewData["Title"] = "Home Page";
        // ViewData["Message"] = "Welcome to our Food Web App!";
        return View(products);
    }
    }
}
