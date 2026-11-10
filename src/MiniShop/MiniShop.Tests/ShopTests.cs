using ShopMonolith;

namespace MiniShop.Tests;

public class ShopTests
{
    [Fact]
    [Trait("Issue", "1")]
    public void AcceptValidOrder()
    {
        var shop = new Shop();

        var order = shop.PlaceOrder(new NewOrder("Pralinen", 2));

        Assert.NotNull(order);
        Assert.Single(shop.Orders);
    }

    [Fact]
    [Trait("Issue", "1")]
    public void DeclineInvalidOrder()
    {
        var shop = new Shop();

        var order = shop.PlaceOrder(new NewOrder("", 2));

        Assert.Null(order);
        Assert.Empty(shop.Orders);
    }

    [Fact]
    [Trait("Issue", "2")]
    public void ReturnExistingOrder()
    {
        var shop = new Shop();
        var order = shop.PlaceOrder(new NewOrder("Pralinen", 1))!;

        var found = shop.GetOrder(order.Id);

        Assert.Equal(order, found);
    }

    [Fact]
    [Trait("Issue", "3")]
    public void CreateDeliveryForValidOrder()
    {
        var shop = new Shop();

        var order = shop.PlaceOrder(new NewOrder("Pralinen", 1))!;

        var delivery = Assert.Single(shop.Deliveries);
        Assert.Equal(order.Id, delivery.OrderId);
        Assert.Equal("needs packing", delivery.Status);
    }
}