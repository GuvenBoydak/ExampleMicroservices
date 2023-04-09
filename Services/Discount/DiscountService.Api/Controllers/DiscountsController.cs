using DiscountService.Api.Models;
using DiscountService.Api.Services;
using ExampleMicroservice.Shared.ControllerBase;
using ExampleMicroservice.Shared.Service;
using Microsoft.AspNetCore.Mvc;

namespace DiscountService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountsController : CustomBaseController
{
    private readonly IDiscountService _discountService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
    {
        _discountService = discountService;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResultInstance(await _discountService.GetAll());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        return CreateActionResultInstance(await _discountService.GetById(id));
    }

    [HttpGet("GetByCode/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var userId = _sharedIdentityService.GetUserId;

        return CreateActionResultInstance(await _discountService.GetByCodeAndUserId(code, userId));
    }


    [HttpPost]
    public async Task<IActionResult> Add(Discount discount)
    {
        return CreateActionResultInstance(await _discountService.Add(discount));
    }

    [HttpPut]
    public async Task<IActionResult> Update(Discount discount)
    {
        return CreateActionResultInstance(await _discountService.Update(discount));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return CreateActionResultInstance(await _discountService.Delete(id));
    }
}