using ExampleMicroservice.UI_MVC.Models.Discounts;

namespace ExampleMicroservice.UI_MVC.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}