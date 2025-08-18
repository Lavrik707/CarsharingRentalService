using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using VehicleRentalService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ServiceDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:VehicleRentalServiceConnection"]);
});

builder.Services.AddScoped<IServiceRepository, EFServiceRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
