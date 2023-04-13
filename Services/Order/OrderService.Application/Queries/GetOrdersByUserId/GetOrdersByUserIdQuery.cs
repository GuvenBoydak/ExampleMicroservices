using ExampleMicroservice.Shared.Dtos;
using MediatR;
using OrderService.Application.Dtos;

namespace OrderService.Application.Queries.GetOrdersByUserId;

public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
{
    public string UserId { get; set; }
}
