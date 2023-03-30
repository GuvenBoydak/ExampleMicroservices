using System.Reflection;
using CatalogService.Api.Mapping;
using CatalogService.Api.Services;
using CatalogService.Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var databaseSetting = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
builder.Services.AddSingleton<IDatabaseSettings>(sp => { return databaseSetting; });

#region DI

builder.Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));

builder.Services.AddScoped(typeof(ICourseService), typeof(CourseService));

#endregion

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    opt =>
    {
        //gelen piravete key ile public key karsilamak icin public key alicak adress veriyoruz
        opt.Authority = builder.Configuration["IdentityServerURL"];
        opt.Audience = "resource_catalog"; //gelen token da audience alaninda olamasi gereken key.
        opt.RequireHttpsMetadata = false; //default olarak beklenen https kapatiyoruz.
    });

builder.Services.AddControllers(opt => { opt.Filters.Add(new AuthorizeFilter()); });

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