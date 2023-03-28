using CqrsTest.Shared;
using MediatR;

namespace CqrsTest.Server;

public class WeatherForecastQueryHandler : IRequestHandler<WeatherForecastQuery, WeatherForecast[]>
{
    private static readonly string[] Summaries = 
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


    public Task<WeatherForecast[]> Handle(WeatherForecastQuery request, CancellationToken cancellationToken) 
        => Task.FromResult(Randomize());

    private WeatherForecast[] Randomize()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

}