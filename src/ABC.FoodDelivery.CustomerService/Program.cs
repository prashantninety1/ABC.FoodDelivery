using ABC.FoodDelivery.CustomerService.Data;
using ABC.FoodDelivery.CustomerService.Repositories;
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

builder.Services.AddSingleton<CosmosDbContext>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Build().Run();
