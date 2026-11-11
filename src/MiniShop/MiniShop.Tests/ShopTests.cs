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
}
public class DeliveryStoreTests
{
    [Fact]
    [Trait("Issue", "3")]
    public void CheckStatusForNewDelivery()
    {
        var store = new DeliveryStore();

        var delivery = store.CreateDelivery(Guid.NewGuid());

        Assert.Equal("muss eingepackt werden", delivery.Status);
        Assert.Single(store.GetAll());
    }
}