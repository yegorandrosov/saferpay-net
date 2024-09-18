using System.Threading.Tasks;
using SaferPay.Models;

namespace SaferPay
{
    public static class SaferPayClientExtensions
    {
        public static Task<PaymentPageInitializeResponse> PaymentPageInitializeAsync(this ISaferPayClient client, PaymentPageInitializeRequest request)
            => client.SendAsync<PaymentPageInitializeResponse, PaymentPageInitializeRequest>("/Payment/v1/PaymentPage/Initialize", request);

        public static Task<PaymentPageAssertResponse> PaymentPageAssertAsync(this ISaferPayClient client, PaymentPageAssertRequest request)
            => client.SendAsync<PaymentPageAssertResponse, PaymentPageAssertRequest>("/Payment/v1/PaymentPage/Assert", request);
    }
}