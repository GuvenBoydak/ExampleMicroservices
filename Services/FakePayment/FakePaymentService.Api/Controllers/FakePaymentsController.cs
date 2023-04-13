using ExampleMicroservice.Shared.ControllerBase;
using ExampleMicroservice.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FakePaymentService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FakePaymentsController : CustomBaseController
{
    [HttpPost]
    public IActionResult ReceivePayment()
    {
        return CreateActionResultInstance(Response<NoContent>.Success(200));
    }
}