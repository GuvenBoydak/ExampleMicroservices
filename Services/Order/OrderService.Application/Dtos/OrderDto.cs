namespace OrderService.Application.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public string BuyerId { get; set; }
    public AddressDto Address { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}