using OrderService.Domain.Core;

namespace OrderService.Domain.OrderAggregate;

public class OrderItem : Entity
{
    public OrderItem(decimal price, string pictureUrl, string productName, string productId)
    {
        Price = price;
        PictureUrl = pictureUrl;
        ProductName = productName;
        ProductId = productId;
    }

    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string PictureUrl { get; private set; }
    public decimal Price { get; private set; }

    public void UpdateOrderItem(string productName, string pictureUrl, decimal price)
    {
        ProductName = productName;
        PictureUrl = pictureUrl;
        Price = price;
    }
}