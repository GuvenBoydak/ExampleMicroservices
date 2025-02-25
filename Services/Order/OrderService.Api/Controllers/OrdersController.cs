﻿using ExampleMicroservice.Shared.ControllerBase;
using ExampleMicroservice.Shared.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Application.Queries.GetOrdersByUserId;

namespace OrderService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : CustomBaseController
{
    private readonly IMediator _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
    {
        _mediator = mediator;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });

        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateOrderCommand createOrderCommand)
    {
        var response = await _mediator.Send(createOrderCommand);

        return CreateActionResultInstance(response);
    }
}