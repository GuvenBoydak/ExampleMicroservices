using ExampleMicroservice.UI_MVC.Models.FakePayments;

namespace ExampleMicroservice.UI_MVC.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}