using ExampleMicroservice.Shared.ControllerBase;
using ExampleMicroservice.Shared.Dtos;
using FakePaymentService.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FakePaymentService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FakePaymentsController : CustomBaseController
{
    [HttpPost]
    public IActionResult ReceivePayment(PaymentDto paymentDto)
    {
        return CreateActionResultInstance(Response<NoContent>.Success(200));
    }
}