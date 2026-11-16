using Microsoft.AspNetCore.Mvc;
using ShopMonolith;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Shop>();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => Results.Redirect("/swagger"));

var orders = app.MapGroup("/orders");

orders.MapPost("/place-order", async (
    [FromHeader(Name = "X-Api-Key")] string? apiKey,
    NewOrder input,
    Shop shop,
    IConfiguration config,
    IHttpClientFactory httpClientFactory) =>
{
    if (apiKey != config["ApiKey"])
        return Results.Unauthorized();

    var order = shop.PlaceOrder(input);

    if (order is null)
        return Results.BadRequest("Produkt fehlt oder Menge ist kleiner als 1.");

    var client = httpClientFactory.CreateClient();
    await client.PostAsJsonAsync("http://localhost:5002/deliveries", new { OrderId = order.Id });

    return Results.Created($"/orders/{order.Id}", order);
});

orders.MapGet("/", (Shop shop) => shop.Orders);

orders.MapGet("/{id}", (Guid id, Shop shop) =>
{
    var order = shop.GetOrder(id);
    return order is null ? Results.NotFound() : Results.Ok(order);
});

app.Run();