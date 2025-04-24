using Microsoft.AspNetCore.RateLimiting;
using dotenv.net;
using MangaAPI.Repositories.interfaces;
using MangaAPI.Repositories;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MangaAPI.Core;
using MangaAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

DotEnv.Load();

// Manga Collections Repository
builder.Services.AddSingleton<IMangaRepository, MangaRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

// Services
builder.Services.AddSingleton<JwtService>();

// MongoDB Context
builder.Services.AddSingleton<MongoDBContext>();

// JWT Auth
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!);
// var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Rate Limit
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueLimit = 0;
    });

    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AlllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseRateLimiter();

app.MapControllers();

// ใช้งาน CORS policy ที่ประกาศไว้
app.UseCors("AlllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
