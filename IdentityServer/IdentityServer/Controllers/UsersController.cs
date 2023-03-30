using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ExampleMicroservice.Shared.Dtos;
using IdentityServer.Dtos;
using IdentityServer.Models;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser()
                { UserName = signUpDto.UserName, Email = signUpDto.Email, City = signUpDto.City };

            var result = await _userManager.CreateAsync(user, signUpDto.Password);

            if (!result.Succeeded)
                return BadRequest(Response<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null)
                return BadRequest();

            return Ok(new
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                City = user.City
            });
        }
    }
}