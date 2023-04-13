using ExampleMicroservice.Shared.Dtos;
using MediatR;
using OrderService.Application.Dtos;
using OrderService.Domain.OrderAggregate;
using OrderService.Infrastructure.DBContext;

namespace OrderService.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
{
    private readonly OrderDbContext _dbContext;

    public CreateOrderCommandHandler(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street,
            request.Address.ZipCode, request.Address.Line);

        var newOrder = new Order(request.BuyerId, newAddress);
        request.OrderItems.ForEach(x => { newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl); });

        _dbContext.Orders.Add(newOrder);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Response<CreatedOrderDto>.Success(new CreatedOrderDto { Id = newOrder.Id }, 200);
    }
}