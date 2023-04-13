using ExampleMicroservice.Shared.Dtos;
using MediatR;
using OrderService.Application.Dtos;

namespace OrderService.Application.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
{
    public string BuyerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public AddressDto Address { get; set; }
}