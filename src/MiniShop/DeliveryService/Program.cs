var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<DeliveryStore>();

var app = builder.Build();

//Vom Monlith gerufen wenn eine Bestellung einkommt
app.MapPost("/deliveries", (NewDelivery newDelivery, DeliveryStore store) =>
{
    var delivery = store.CreateDelivery(newDelivery.OrderId);
    return Results.Created($"/deliveries/{delivery.Id}", delivery);
});

app.MapGet("/deliveries", (DeliveryStore store) => store.GetAll());

app.Run();