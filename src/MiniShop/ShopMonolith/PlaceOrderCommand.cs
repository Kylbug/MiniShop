using ShopMonolith;
public record PlaceOrderDTO(string Product, int Quantity);
public class PlaceOrderCommand
{
    private readonly Shop shop;

    public PlaceOrderCommand(Shop shop)
    {
        this.shop = shop;
    }

    public Order Handle(PlaceOrderDTO dto)
    {
        return shop.PlaceOrder(new NewOrder(dto.Product, dto.Quantity));
    }
}