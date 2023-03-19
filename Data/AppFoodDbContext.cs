using FoodWebAppMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodWebAppMvc.Data
{
    public class AppFoodDbContext:DbContext
    {
        public AppFoodDbContext(DbContextOptions<AppFoodDbContext> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<SignupLogin> SignupLogin { get; set; }
        public DbSet<AdminLogin> adminLogin { get; set; }
        public DbSet<InvoiceModel> invoiceModel { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<ContactModel> contactModels { get; set; }
        public DbSet<BlogModel> blogModels { get; set; }
        

    }
}