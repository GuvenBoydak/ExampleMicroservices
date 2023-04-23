using ExampleMicroservice.UI_MVC.Models;
using ExampleMicroservice.UI_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ExampleMicroservice.UI_MVC.Controllers;

public class AuthController : Controller
{
    private readonly IIdentityService _identityService;

    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SigninInput signinInput)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var response = await _identityService.SignIn(signinInput);

        if (!response.IsSuccessful)
        {
            response.Errors.ForEach(x => { ModelState.AddModelError(String.Empty, x); });

            return View();
        }

        return RedirectToAction(nameof(Index), "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await _identityService.RevokeRefreshToken();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}