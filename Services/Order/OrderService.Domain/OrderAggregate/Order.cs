using OrderService.Domain.Core;

namespace OrderService.Domain.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    public Order()
    { }

    public Order(string buyerId, Address address)
    {
        BuyerId = buyerId;
        Address = address;
        CreatedDate = DateTime.Now;
        _orderItems = new List<OrderItem>();
    }

    private readonly List<OrderItem> _orderItems;

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    public string BuyerId { get; private set; }
    public Address Address { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
    {
        var existProduct = _orderItems.Any(x => x.ProductId == productId);

        if (!existProduct)
        {
            var newOrderItem = new OrderItem(price, productName, pictureUrl, productId);
            _orderItems.Add(newOrderItem);
        }
    }

    public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
}