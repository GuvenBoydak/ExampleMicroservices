using ExampleMicroservice.UI_MVC.Models;

namespace ExampleMicroservice.UI_MVC.Services.Interfaces;

public interface IUserService
{
    Task<UserViewModel> GetUser();
}