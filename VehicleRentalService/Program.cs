using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using VehicleRentalService.Data;
using VehicleRentalService.Models;
using VehicleRentalService.Models.Core;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Entity Framework
builder.Services.AddDbContext<ServiceDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:VehicleRentalServiceConnection"]);
    opts.EnableSensitiveDataLogging();
    opts.LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddScoped<IServiceRepository, EFServiceRepository>();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityConnection"])
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();





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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var configuration = services.GetRequiredService<IConfiguration>();
        await RoleInitializer.InitializeAsync(userManager, roleManager, configuration);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
