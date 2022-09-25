using ExKafka.Producer.API;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ProducerService>();

var app = builder.Build();

app.MapPost("/", async ([FromServices] ProducerService service, [FromBody] Order order) =>
{
    return await service.SendMessage(order);
});

app.Run();