using Blazor.MediatR.Server;
using CqrsTest.Server;
using CqrsTest.Shared;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
// ----- 
// Add services to the container.
builder.Services.AddMediatR(typeof(WeatherForecastQuery), typeof(WeatherForecastQueryHandler));
// ----- 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// ----- 
app.UseBlazorMediatRServer();
// ----- 
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapFallbackToFile("index.html");
app.Run();