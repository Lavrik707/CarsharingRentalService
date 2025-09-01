using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using VehicleRentalService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Entity Framework
builder.Services.AddDbContext<ServiceDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:VehicleRentalServiceConnection"]);
});

builder.Services.AddScoped<IServiceRepository, EFServiceRepository>();


//Sessions
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(9999);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;


    options.Cookie.Name = "CartData";
});

var app = builder.Build();

app.UseStaticFiles();

app.UseSession();

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
