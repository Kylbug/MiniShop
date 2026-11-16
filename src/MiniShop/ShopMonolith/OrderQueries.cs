using ShopMonolith;
public class ListOrdersQuery
{
    private readonly Shop shop;
    public ListOrdersQuery(Shop shop) { this.shop = shop; }

    public List<Order> Handle() => shop.GetOrders();
}

public class GetOrderQuery
{
    private readonly Shop shop;
    public GetOrderQuery(Shop shop) { this.shop = shop; }

    public Order? Handle(Guid id) => shop.GetOrder(id);
}