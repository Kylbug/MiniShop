public record NewDelivery(Guid OrderId);
public record Delivery(Guid Id, Guid OrderId, string Status);

public class DeliveryStore
{
    private readonly List<Delivery> deliveries = new();

    public Delivery CreateDelivery(Guid orderId)
    {
        var delivery = new Delivery(Guid.NewGuid(), orderId, "muss eingepackt werden");
        deliveries.Add(delivery);
        return delivery;
    }

    public List<Delivery> GetAll() => deliveries;
}