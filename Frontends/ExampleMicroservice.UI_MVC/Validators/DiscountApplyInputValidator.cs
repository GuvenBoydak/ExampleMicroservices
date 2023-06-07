using ExampleMicroservice.UI_MVC.Models.Discounts;
using FluentValidation;

namespace ExampleMicroservice.UI_MVC.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("indirim kupon alanı boş olamaz");
        }
    }
}