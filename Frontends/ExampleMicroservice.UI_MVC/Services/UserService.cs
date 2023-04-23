using ExampleMicroservice.UI_MVC.Models;
using ExampleMicroservice.UI_MVC.Services.Interfaces;

namespace ExampleMicroservice.UI_MVC.Services;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(HttpClient client)
    {
        _client = client;
    }

    public async Task<UserViewModel> GetUser()
    {
        return await _client.GetFromJsonAsync<UserViewModel>("/api/users/getuser");
    }
}