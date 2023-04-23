using ExampleMicroservice.UI_MVC.Models.PhotoStocks;

namespace ExampleMicroservice.UI_MVC.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> UploadPhoto(IFormFile photo);

        Task<bool> DeletePhoto(string photoUrl);
    }
}