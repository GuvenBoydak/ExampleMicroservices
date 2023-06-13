namespace ExampleMicroservice.UI_MVC.Models.Baskets
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; } = 1;

        public string CourseId { get; set; }
        public string CourseName { get; set; }

        public decimal Price { get; set; }

        private decimal? _discountAppliedPrice;

        public decimal GetCurrentPrice
        {
            get => _discountAppliedPrice != null ? _discountAppliedPrice.Value : Price;
        }

        public void AppliedDiscount(decimal discountPrice)
        {
            _discountAppliedPrice = discountPrice;
        }
    }
}