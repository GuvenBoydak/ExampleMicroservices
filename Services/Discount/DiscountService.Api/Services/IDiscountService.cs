using DiscountService.Api.Models;
using ExampleMicroservice.Shared.Dtos;

namespace DiscountService.Api.Services;

public interface IDiscountService
{
    Task<Response<List<Discount>>> GetAll();
    Task<Response<Discount>> GetById(int id);
    Task<Response<NoContentDto>> Add(Discount discount);
    Task<Response<NoContentDto>> Update(Discount discount);
    Task<Response<NoContentDto>> Delete(int id);
    Task<Response<Discount>> GetByCodeAndUserId(string code,string userId);
}