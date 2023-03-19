using FoodWebAppMvc.Data;
using FoodWebAppMvc.Interfaces;
using FoodWebAppMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodWebAppMvc.Repositories
{
    public class MenuItemsRepository : IMenuRepository
    {
        private readonly AppFoodDbContext _db;

        public MenuItemsRepository(AppFoodDbContext db)
        {
            _db = db;
        }

        public List<Products> GetAllFoodItems()
        {
            return _db.Products.ToList();
        }

        public async Task<Products> GetFoodItemAsync(int foodItemId)
        {
            return await _db.Products.FindAsync(foodItemId);
        }
    }
}
