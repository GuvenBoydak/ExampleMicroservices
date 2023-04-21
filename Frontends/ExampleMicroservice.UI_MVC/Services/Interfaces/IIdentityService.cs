using ExampleMicroservice.Shared.Dtos;
using ExampleMicroservice.UI_MVC.Models;
using IdentityModel.Client;

namespace ExampleMicroservice.UI_MVC.Services.Interfaces;


    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }