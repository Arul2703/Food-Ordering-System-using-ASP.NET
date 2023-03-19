using FoodWebAppMvc.Data;
using FoodWebAppMvc.Data.Repositories;
using FoodWebAppMvc.Interfaces;
using FoodWebAppMvc.Models;
using FoodWebAppMvc.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
// using Microsoft.Extensions.Logging.File;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
var configuration = builder.Configuration;

builder.Services.AddDbContext<AppFoodDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/User/Login";
        option.Cookie.Name = "UserCookie";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    })
    .AddCookie("AdminCookie", options =>
    {
        options.LoginPath = "/Admin/Login";
        options.Cookie.Name = "AdminCookie";
    }
);

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
// })
// .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
// {
//     options.LoginPath = "/User/Login";
//     options.Cookie.Name = "UserCookie";
//     options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//     options.Events = new CookieAuthenticationEvents
//     {
//         OnValidatePrincipal = context =>
//         {
//             // Add the cookie name to the user's claims
//             if (context.Principal.Identity is ClaimsIdentity identity)
//             {
//                 identity.AddClaim(new Claim("CookieName", options.Cookie.Name));
//             }
//             return Task.CompletedTask;
//         }
//     };
// })
// .AddCookie("AdminCookie", options =>
// {
//     options.LoginPath = "/Admin/Login";
//     options.Cookie.Name = "AdminCookie";
//     options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//     options.Events = new CookieAuthenticationEvents
//     {
//         OnValidatePrincipal = context =>
//         {
//             // Add the cookie name to the user's claims
//             if (context.Principal.Identity is ClaimsIdentity identity)
//             {
//                 identity.AddClaim(new Claim("CookieName", options.Cookie.Name));
//             }
//             return Task.CompletedTask;
//         }
//     };
// });

builder.Services.AddSession(options =>
{
    // Set a short timeout for testing.
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    // Make the session cookie essential
    options.Cookie.IsEssential = true;
});


// builder.Services.AddLogging(loggingBuilder =>
// {
//     loggingBuilder.ClearProviders();
//     loggingBuilder.AddFile("logs/app-log.txt");
// });
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMenuRepository, MenuItemsRepository>();
builder.Services.AddScoped<CachingFilter>();

var _logger = new LoggerConfiguration()
.WriteTo.File("C:\\Users\\Arularasi\\Desktop\\Aspire Systems Programs (.NET)\\ASP.NET\\ASP.NET MVC\\FoodWebAppMvc\\Logs\\FoodAppLog.log",rollingInterval:RollingInterval.Day)
.CreateLogger();

builder.Logging.AddSerilog(_logger);



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");
// app.MapControllerRoute(
//     name: "admin",
//     pattern: "/{controller=AdminDashboard}/{action=Index}/{id?}",
//     defaults: new { area = "Admin" }
// ).RequireAuthorization("AdminCookie");

app.Run();
