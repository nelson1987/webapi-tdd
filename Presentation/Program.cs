using Application;
using Domain;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
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
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapPost(Constantes.POST_USUARIO_PATH, async (CreateUser.IHandler handler, CancellationToken cancellationToken) =>
{
    var command = new CreateUser.Command("email@email.com", "123456");
    var response = await handler.HandleAsync(command, cancellationToken);
    return response.IsSuccess
        ? Results.Created(Constantes.GET_USUARIO_PATH, response.Value!.Id)
        : Results.BadRequest(response.Error);
});

app.Run();


namespace Presentation
{
    public partial class Program
    {
    }
}