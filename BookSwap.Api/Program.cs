using Microsoft.EntityFrameworkCore;
using BookSwap.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookSwapDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BookSwapDb")));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Тестовий ендпоінт
app.MapGet("/api/test-db", async (BookSwapDbContext context) =>
{
    try
    {
        bool canConnect = await context.Database.CanConnectAsync();
        return canConnect 
            ? Results.Ok("Підключення до БД PostgreSQL успішне") 
            : Results.StatusCode(500);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Помилка підключення: {ex.Message}");
    }
})
.WithName("TestDatabaseConnection");

app.Run();