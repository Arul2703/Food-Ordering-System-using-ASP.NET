using FoodWebAppMvc.Data;
using FoodWebAppMvc.Models;
using FoodWebAppMvc.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AdminRepository : IAdminRepository
{
    private readonly AppFoodDbContext _db;

      public AdminRepository(AppFoodDbContext db)
      {
          _db = db;
      }

    // Other methods here

    public async Task<AdminLogin> GetAdminByEmailAndPasswordAsync(string email, string password)
    {
            return await _db.adminLogin.FirstOrDefaultAsync(s => s.Email.Equals(email) && s.Password.Equals(password));
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
      return await _db.orders.ToListAsync();
    }

    public async Task<List<InvoiceModel>> GetInvoicesAsync()
    {
      return await _db.invoiceModel.ToListAsync();
    }
}
