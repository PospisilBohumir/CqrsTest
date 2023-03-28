using MediatR;

namespace CqrsTest.Shared;
public record WeatherForecastQuery() : IRequest<WeatherForecast[]>;