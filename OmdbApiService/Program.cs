using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.AspNetCore;
using SimpleInjector.Integration.AspNetCore.Mvc;
using OmdbApiService.Data;
using OmdbApiService.Services;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add user secrets in development mode
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Create a SimpleInjector container
var container = new Container();
container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

// Add services to the container.
builder.Services.AddControllers();

// Register memory cache
builder.Services.AddMemoryCache();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext as Scoped
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Simple Injector setup
builder.Services.AddSimpleInjector(container, options =>
{
    options.AddAspNetCore().AddControllerActivation();
});

// Register MovieDbContext with SimpleInjector
container.Register<MovieDbContext>(Lifestyle.Scoped);

// Register OMDbService with SimpleInjector as Scoped, including IConfiguration
container.Register<IOMDbService>(() =>
    new OMDbService(new HttpClient(), container.GetInstance<IMemoryCache>(), container.GetInstance<MovieDbContext>(), builder.Configuration),
    Lifestyle.Scoped);

// Add OData to the controllers
builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().OrderBy().Expand());

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
