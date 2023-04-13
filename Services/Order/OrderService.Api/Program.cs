using System.IdentityModel.Tokens.Jwt;
using ExampleMicroservice.Shared.Service;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Infrastructure.DBContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(typeof(CreateOrderCommand).Assembly);
builder.Services.AddTransient<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddDbContext<OrderDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        configure => { configure.MigrationsAssembly("OrderService.Infrastructure"); });
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    opt =>
    {
        opt.Authority = builder.Configuration["IdentityServerURL"];
        opt.Audience = "resource_order";
        opt.RequireHttpsMetadata = false;
    });

var requireAuthorize = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
builder.Services.AddControllers(opt => { opt.Filters.Add(new AuthorizeFilter(requireAuthorize)); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();