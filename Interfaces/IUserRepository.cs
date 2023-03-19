using System.Collections.Generic;
using System.Threading.Tasks;
using FoodWebAppMvc.Models;

namespace FoodWebAppMvc.Interfaces{
  public interface IUserRepository
  {
      Task<bool> IsEmailAlreadyExistsAsync(string email);
      Task AddUserAsync(SignupLogin user);
      Task<SignupLogin> GetUsersByEmailAndPasswordAsync(string email, string password);
  }
}
