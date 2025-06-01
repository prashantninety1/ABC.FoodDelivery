using ABC.FoodDelivery.RestaurantService.Data;
using ABC.FoodDelivery.RestaurantService.Repositories;
using ABC.FoodDelivery.RestaurantService.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server=tcp:abc-fooddelivery.database.windows.net,1433;Initial Catalog=ABC.FoodDelivery;Persist Security Info=False;User ID=appdbadmin;Password=Admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<Service>();


var host = builder.Build();

// Apply EF Core migrations at startup
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

host.Run();
