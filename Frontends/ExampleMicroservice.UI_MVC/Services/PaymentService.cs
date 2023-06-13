using ExampleMicroservice.UI_MVC.Models.FakePayments;
using ExampleMicroservice.UI_MVC.Services.Interfaces;

namespace ExampleMicroservice.UI_MVC.Services;

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
    {
        var response = await _httpClient.PostAsJsonAsync("fakepayments", paymentInfoInput);

        return response.IsSuccessStatusCode;
    }
}