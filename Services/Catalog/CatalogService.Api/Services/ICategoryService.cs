using CatalogService.Api.Dtos;
using CatalogService.Api.Models;
using ExampleMicroservice.Shared.Dtos;

namespace CatalogService.Api.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
}