using FoodWebAppMvc.Data;
using FoodWebAppMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using FoodWebAppMvc.Interfaces;
using Microsoft.AspNetCore.Http;
using FoodWebAppMvc.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace FoodWebAppMvc.Controllers{
  public class CartController : Controller
{
    private readonly IMenuRepository _db;

    public CartController(IMenuRepository db)
    {
        _db = db;
    }

    public IActionResult Index()
  {
      var cart = HttpContext.Session.GetObject<List<Cart>>("cart") ?? new List<Cart>();
      return View(cart);
  }

    // [CheckLoginFilter]
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddToCart(int itemId)
    {
        var selectedItem = await _db.GetFoodItemAsync(itemId);
        if (selectedItem == null)
        {
            return NotFound();
        }

        var cart = HttpContext.Session.GetObject<List<Cart>>("cart") ?? new List<Cart>();

        var cartItem = cart.FirstOrDefault(item => item.productId == selectedItem.id);
        if (cartItem != null)
        {
            cartItem.qty++;
            cartItem.bill = cartItem.qty * cartItem.price;
        }
        else
        {
            cart.Add(new Cart
            {
                productId = selectedItem.id,
                productName = selectedItem.ProductName,
                price = selectedItem.ProductPrice,
                qty = 1,
                bill = selectedItem.ProductPrice
            });
        }

        HttpContext.Session.SetObject("cart", cart);

        return RedirectToAction("Index","Menu");
    }

}

}

