using System.Collections.Generic;
using System.Threading.Tasks;
using FoodWebAppMvc.Models;

namespace FoodWebAppMvc.Interfaces{
  public interface IAdminRepository
  {
    Task<AdminLogin> GetAdminByEmailAndPasswordAsync(string email, string password);
    Task<List<Order>> GetOrdersAsync();
    Task<List<InvoiceModel>> GetInvoicesAsync();
  }
}
