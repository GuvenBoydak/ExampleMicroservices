using System.Linq;
using System.Threading.Tasks;
using ExampleMicroservice.Shared.Dtos;
using IdentityServer.Dtos;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SingUp(SingUpDto singUpDto)
        {
            var user = new ApplicationUser()
                { UserName = singUpDto.UserName, Email = singUpDto.Email, City = singUpDto.City };

            var result = await _userManager.CreateAsync(user, singUpDto.Password);

            if (!result.Succeeded)
                return BadRequest(Response<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));

            return NoContent();
        }
    }
}