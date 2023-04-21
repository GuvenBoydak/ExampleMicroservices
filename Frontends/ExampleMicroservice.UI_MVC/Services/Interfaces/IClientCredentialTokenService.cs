namespace ExampleMicroservice.UI_MVC.Services.Interfaces;

public interface IClientCredentialTokenService
{
    Task<String> GetToken();
}