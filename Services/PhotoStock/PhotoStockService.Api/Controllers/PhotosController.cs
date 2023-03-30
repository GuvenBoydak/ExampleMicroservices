using ExampleMicroservice.Shared.ControllerBase;
using ExampleMicroservice.Shared.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhotoStockService.Api.Dtos;

namespace PhotoStockService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotosController : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
    {
        if (photo != null & photo.Length > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", photo.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);

            var returnPath = "photos/" + photo.FileName;

            PhotoDto photoDto = new() { Url = returnPath };

            return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
        }

        return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty", 404));
    }

    [HttpDelete]
    public IActionResult PhotoDelete(string photoUrl)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", photoUrl);

        if (!System.IO.File.Exists(path))
            return CreateActionResultInstance(Response<NoContent>.Fail("Photo Not Found", 404));

        System.IO.File.Delete(path);

        return CreateActionResultInstance(Response<NoContent>.Success(204));
    }
}