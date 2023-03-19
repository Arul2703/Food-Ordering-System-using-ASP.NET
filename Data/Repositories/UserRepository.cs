using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodWebAppMvc.Interfaces;
using FoodWebAppMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodWebAppMvc.Data.Repositories{
  public class UserRepository : IUserRepository
  {
      private readonly AppFoodDbContext _db;

      public UserRepository(AppFoodDbContext db)
      {
          _db = db;
      }

      public async Task<bool> IsEmailAlreadyExistsAsync(string email)
      {
          return await Task.Run(() => _db.SignupLogin.Any(x => x.Email == email));
      }

      public async Task AddUserAsync(SignupLogin user)
      {
          await _db.SignupLogin.AddAsync(user);
          await _db.SaveChangesAsync();
      }

      public async Task<SignupLogin> GetUsersByEmailAndPasswordAsync(string email, string password)
    {
            return await _db.SignupLogin.FirstOrDefaultAsync(s => s.Email.Equals(email) && s.Password.Equals(password));
    }
  }
}
