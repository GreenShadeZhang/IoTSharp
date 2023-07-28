using Microsoft.AspNetCore.Mvc;

namespace Verdure.Qinglan.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IQinglanApi _qinglanApi;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,IQinglanApi qinglanApi)
    {
        _logger = logger;
        _qinglanApi = qinglanApi;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        //var result = await _qinglanApi.LoginAsync(new LoginInput { Username = "**", Password = "**" });

        var device = await _qinglanApi.GetPopulationAsync(new List<string>() { "CB262728F796DDB" });

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

