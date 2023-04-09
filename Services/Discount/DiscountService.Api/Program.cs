using System.IdentityModel.Tokens.Jwt;
using DiscountService.Api.Services;
using ExampleMicroservice.Shared.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IDiscountService, DiscountService.Api.Services.DiscountService>();
builder.Services.AddScoped<ISharedIdentityService,SharedIdentityService>();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    opt =>
    {
        opt.Authority = builder.Configuration["IdentityServerURL"];
        opt.Audience = "resource_discount";
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