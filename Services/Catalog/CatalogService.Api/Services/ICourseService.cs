using CatalogService.Api.Dtos;
using ExampleMicroservice.Shared.Dtos;

namespace CatalogService.Api.Services;

public interface ICourseService
{
    Task<Response<List<CourseDto>>> GetAllAsync();
    Task<Response<CourseDto>> GetByIdAsync(string id);
    Task<Response<List<CourseDto>>> GetAllByUserId(string userId);
    Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
    Task<Response<NoContentDto>> UpdateAsync(CourseUpdateDto courseUpdateDto);
    Task<Response<NoContentDto>> DeleteAsync(string id);
}