using ShopMonolith;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Shop>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => Results.Redirect("/swagger"));

//Bestellung aufgeben
app.MapPost("/orders", (NewOrder input, Shop shop) =>
{
    var order = shop.PlaceOrder(input);
    return order is null
        ? Results.BadRequest("Produkt fehlt oder Menge ist kleiner als 1.")
        : Results.Created($"/orders/{order.Id}", order);
});

//Alle Bestellungen anzeigen
app.MapGet("/orders", (Shop shop) => shop.Orders);

//Spezifische Bestellung anzeigen
app.MapGet("/orders/{id}", (Guid id, Shop shop) =>
{
    var order = shop.GetOrder(id);
    return order is null ? Results.NotFound() : Results.Ok(order);
});

//Alle Lieferungen anzeigen
app.MapGet("/deliveries", (Shop shop) => shop.Deliveries);

app.Run();