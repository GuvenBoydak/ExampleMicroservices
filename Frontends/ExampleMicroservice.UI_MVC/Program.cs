using ExampleMicroservice.Shared.Service;
using ExampleMicroservice.UI_MVC.Extensions;
using ExampleMicroservice.UI_MVC.Handler;
using ExampleMicroservice.UI_MVC.Helpers;
using ExampleMicroservice.UI_MVC.Models;
using ExampleMicroservice.UI_MVC.Services;
using ExampleMicroservice.UI_MVC.Services.Interfaces;
using ExampleMicroservice.UI_MVC.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var clientSettings = builder.Configuration.GetSection("ClientSettings");
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews()
    .AddFluentValidation((fv => fv.RegisterValidatorsFromAssemblyContaining<CourseCreateInputValidator>()));

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IIdentityService, IdentityService>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddAccessTokenManagement();
builder.Services.AddSingleton<PhotoHelper>();

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

builder.Services.AddHttpClientServices(builder.Configuration);

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme, opts =>
    {
        opts.LoginPath = "/Auth/SignIn";
        opts.ExpireTimeSpan = TimeSpan.FromDays(60);
        opts.SlidingExpiration = true;
        opts.Cookie.Name = "udemywebcookie";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();