using ExampleMicroservice.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ExampleMicroservice.Shared.ControllerBase
{
    public class CustomBaseController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}