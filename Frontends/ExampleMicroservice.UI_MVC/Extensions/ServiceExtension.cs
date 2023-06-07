using ExampleMicroservice.UI_MVC.Handler;
using ExampleMicroservice.UI_MVC.Models;
using ExampleMicroservice.UI_MVC.Services;
using ExampleMicroservice.UI_MVC.Services.Interfaces;

namespace ExampleMicroservice.UI_MVC.Extensions;

public static class ServiceExtension
{
    public static void AddHttpClientServices(this IServiceCollection service,IConfiguration configuration)
    {
        var serviceApiSettings = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
        
        service.AddHttpClient<IUserService, UserService>(opt =>
        {
            opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        service.AddHttpClient<ICatalogService, CatalogService>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        service.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}{serviceApiSettings.PhotoStock.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        service.AddHttpClient<IBasketService, BasketService>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}{serviceApiSettings.Basket.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        
        service.AddHttpClient<IDiscountService, DiscountService>(opt =>
        {
            opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}{serviceApiSettings.Discount.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

    }
}