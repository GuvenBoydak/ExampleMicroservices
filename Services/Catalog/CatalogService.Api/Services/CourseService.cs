using AutoMapper;
using CatalogService.Api.Dtos;
using CatalogService.Api.Models;
using CatalogService.Api.Settings;
using ExampleMicroservice.Shared.Dtos;
using MongoDB.Driver;

namespace CatalogService.Api.Services;

public class CourseService : ICourseService
{
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<CourseDto>>> GetAllAsync()
    {
        var courses = await _courseCollection.Find(course => true).ToListAsync();

        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
            }
        }
        else
            courses = new List<Course>();

        return Response<List<CourseDto>>.Success(_mapper.Map<List<Course>, List<CourseDto>>(courses), 200);
    }

    public async Task<Response<CourseDto>> GetByIdAsync(string id)
    {
        var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();

        if (course == null)
            return Response<CourseDto>.Fail("Course not found", 404);

        course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();

        return Response<CourseDto>.Success(_mapper.Map<Course, CourseDto>(course), 200);
    }

    public async Task<Response<List<CourseDto>>> GetAllByUserId(string userId)
    {
        var courses = await _courseCollection.Find<Course>(x => x.UserId == userId).ToListAsync();

        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
            }
        }
        else
            courses = new List<Course>();

        return Response<List<CourseDto>>.Success(_mapper.Map<List<Course>, List<CourseDto>>(courses), 200);
    }

    public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
    {
        var course = _mapper.Map<CourseCreateDto, Course>(courseCreateDto);

        course.CreatedTime = DateTime.UtcNow;
        await _courseCollection.InsertOneAsync(course);

        return Response<CourseDto>.Success(_mapper.Map<Course, CourseDto>(course), 200);
    }

    public async Task<Response<NoContentDto>> UpdateAsync(CourseUpdateDto courseUpdateDto)
    {
        var course = _mapper.Map<CourseUpdateDto, Course>(courseUpdateDto);

        var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, course);

        if (result == null)
            return Response<NoContentDto>.Fail("Course not Found", 404);

        return Response<NoContentDto>.Success(204);
    }

    public async Task<Response<NoContentDto>> DeleteAsync(string id)
    {
        var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

        if (result.DeletedCount > 0)
            return Response<NoContentDto>.Success(204);

        return Response<NoContentDto>.Fail("Course not Found", 404);
    }
}