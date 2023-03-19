using System.Collections.Generic;
using System.Threading.Tasks;
using FoodWebAppMvc.Models;

namespace FoodWebAppMvc.Interfaces{
  public interface IMenuRepository
  {
    Task<Products> GetFoodItemAsync(int productId);
    List<Products> GetAllFoodItems();


  }
}