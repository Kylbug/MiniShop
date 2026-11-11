
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

// Bestellung aufgeben
app.MapPost("/orders", async (NewOrder input, Shop shop, IHttpClientFactory httpClientFactory) =>
{
    var order = shop.PlaceOrder(input);

    if (order is null)
        return Results.BadRequest("Produkt fehlt oder Menge ist kleiner als 1.");

    var client = httpClientFactory.CreateClient();
    await client.PostAsJsonAsync("http://localhost:5002/deliveries", new { OrderId = order.Id });

    return Results.Created($"/orders/{order.Id}", order);
});

// Alle Bestellungen anzeigen
app.MapGet("/orders", (Shop shop) => shop.Orders);

// Spezifische Bestellung anzeigen
app.MapGet("/orders/{id}", (Guid id, Shop shop) =>
{
    var order = shop.GetOrder(id);
    return order is null ? Results.NotFound() : Results.Ok(order);
});

app.Run();
