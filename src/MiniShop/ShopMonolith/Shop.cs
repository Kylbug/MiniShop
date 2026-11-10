namespace ShopMonolith;

public record NewOrder(string Product, int Quantity);
public record Order(Guid Id, string Product, int Quantity);
public record Delivery(Guid Id, Guid OrderId, string Status);

public class Shop
{
    private readonly List<Order> orders = new();
    private readonly List<Delivery> deliveries = new();

    public IReadOnlyList<Order> Orders => orders;
    public IReadOnlyList<Delivery> Deliveries => deliveries;

    //Bestellung aufgeben
    public Order? PlaceOrder(NewOrder input)
    {
        if (string.IsNullOrWhiteSpace(input.Product) || input.Quantity < 1)
        {
            return null;
        }

        var order = new Order(Guid.NewGuid(), input.Product, input.Quantity);
        orders.Add(order);

        //Lieferung automatisch erstellen.
        deliveries.Add(new Delivery(Guid.NewGuid(), order.Id, "needs packing"));

        return order;
    }

    //Bestellung anzeigen
    public Order? GetOrder(Guid id) => orders.FirstOrDefault(o => o.Id == id);
}