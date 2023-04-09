using System.Data;
using Dapper;
using DiscountService.Api.Models;
using ExampleMicroservice.Shared.Dtos;
using Npgsql;

namespace DiscountService.Api.Services;

public class DiscountService : IDiscountService
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;

    public DiscountService(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
    }


    public async Task<Response<List<Discount>>> GetAll()
    {
        var discount = await _dbConnection.QueryAsync<Discount>("select * from discount");
        return Response<List<Discount>>.Success(discount.ToList(), 200);
    }

    public async Task<Response<Discount>> GetById(int id)
    {
        var discount =
            (await _dbConnection.QueryAsync<Discount>("select * from discount where id = @id", new { id }))
            .SingleOrDefault();
        return discount == null
            ? Response<Discount>.Fail("Discount not found", 404)
            : Response<Discount>.Success(discount, 200);
    }

    public async Task<Response<NoContentDto>> Add(Discount discount)
    {
        var status =
            await _dbConnection.ExecuteAsync("insert into discount(userid,rate,code) values(@UserId,@Rate,@Code)",
                discount);
        return status > 0
            ? Response<NoContentDto>.Success(204)
            : Response<NoContentDto>.Fail("An error accured While adding", 500);
    }

    public async Task<Response<NoContentDto>> Update(Discount discount)
    {
        var status =
            await _dbConnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id",
                new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });
        return status > 0
            ? Response<NoContentDto>.Success(204)
            : Response<NoContentDto>.Fail("Discount not found", 404);
    }

    public async Task<Response<NoContentDto>> Delete(int id)
    {
        var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

        return status > 0
            ? Response<NoContentDto>.Success(204)
            : Response<NoContentDto>.Fail("Discount not found", 404);
    }

    public async Task<Response<Discount>> GetByCodeAndUserId(string code, string userId)
    {
        var discount = (await _dbConnection.QueryAsync<Discount>(
                "select * from discount where code=@Code and userid=@UserId", new { Code = code, Userid = userId }))
            .FirstOrDefault();

        return discount == null
            ? Response<Discount>.Fail("Discount NotFound", 404)
            : Response<Discount>.Success(discount, 200);
    }
}