using ApiProject3.Data;
using Microsoft.EntityFrameworkCore;
using ApiProject3.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddDbContext<AdventureWorksContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen();
// Configure logging 
builder.Logging.ClearProviders(); 
builder.Logging.AddConsole(); 
builder.Logging.AddFile("d:/VSCode/ApiProject3/Log/myapp-{Date}.txt");
// Register your middleware as a service
 //builder.Services.AddTransient<GlobalExceptionHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
var logger = app.Services.GetRequiredService<ILogger<Program>>(); 
logger.LogInformation("Application has started");
app.UseHttpsRedirection();
 app.UseStaticFiles();
  app.UseRouting(); 
  app.UseAuthorization();
 //  app.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapControllers();
//app.UseMiddleware<GlobalExceptionHandler>();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
